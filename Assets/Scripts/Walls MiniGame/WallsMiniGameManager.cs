using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Walls_MiniGame
{
    public class WallsMiniGameManager : MonoBehaviour
    {
        [SerializeField] private float _initialBrickShowDuration;
        [SerializeField] private float _brickShowDurationDecrease;
        [SerializeField] private int _initialBricksInSequence;
        [SerializeField] private int _bricksInSequenceIncrease;
        [SerializeField] private float _timeBetweenBricks;
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _button;

        private List<Brick> _bricks;
        private List<Brick> _filledBricks;

        private bool _waitingForPlayerInput;

        private float _brickShowDuration;
        private int _bricksInSequence;
        private int _level = 1;
        private int _attempt = 1;
        private int _score = 0;

        private List<Brick> _correctSequence;
        private List<Brick> _playerSequence;

        private Dictionary<int, int> _pointsByLevel;

        private void Start()
        {
            _button.gameObject.SetActive(false);
            _bricks = GetComponentsInChildren<Brick>().ToList();
            _filledBricks = new List<Brick>();

            _pointsByLevel = new Dictionary<int, int>()
            {
                { 1, 5 },
                { 2, 10 },
                { 3, 15 }
            };

            _brickShowDuration = _initialBrickShowDuration;
            _bricksInSequence = _initialBricksInSequence;
            StartCoroutine(ShowSequence());
        }

        private IEnumerator ShowSequence()
        {
            _correctSequence = new List<Brick>();
            yield return new WaitForSeconds(2f);

            for (int i = 1; i <= _bricksInSequence; i++)
            {
                Brick brick = ChooseRandomBrick();
                _correctSequence.Add(brick);
                brick.ShowBrick();
                yield return new WaitForSeconds(_brickShowDuration);
                brick.HideBrick();
                yield return new WaitForSeconds(_timeBetweenBricks);
            }

            _waitingForPlayerInput = true;
            _playerSequence = new List<Brick>();
        }

        public void AddToSequence(Brick brick)
        {
            if (_waitingForPlayerInput)
            {
                if (_playerSequence.Count != _correctSequence.Count)
                {
                    if (!_playerSequence.Contains(brick))
                    {
                        _playerSequence.Add(brick);
                        brick.ShowBrick();
                    }

                    if (_playerSequence.Count == _correctSequence.Count)
                    {
                        _waitingForPlayerInput = false;
                        StartCoroutine(Progress());
                    }
                }
            }
        }

        private IEnumerator Progress()
        {
            bool isCorrect = CompareSequences();

            if (isCorrect)
            {
                if (_level == 3)
                {
                    yield return StartCoroutine(DisplayText("Good job! Brick by brick, you have built the wall.", 4f));
                    End();
                } 
                else
                {
                    yield return StartCoroutine(DisplayText("Good job, keep going!", 2f));
                    _score += _pointsByLevel[_level];
                    _scoreText.text = $"Score: {_score.ToString()}";
                    NextLevel();
                }
            } 
            else
            {
                if (_attempt == 1)
                {
                    yield return StartCoroutine(DisplayText($"Oops, you entered a wrong sequence! {(3 - _attempt).ToString()} attempt(s) left.", 3f));
                    Retry();
                }
                else if (_attempt == 2)
                {
                    yield return StartCoroutine(DisplayText($"Oops, you entered a wrong sequence! Last attempt!", 2.5f));
                    Retry();
                }
                else
                {
                    yield return StartCoroutine(DisplayText($"No more attempts on this level.", 2f));
                    if (_level == 3)
                    {
                        ClearBricks();
                        yield return StartCoroutine(DisplayText("I guess this could be called a wall...", 3.5f));
                        End();
                    }
                    else
                    {
                        NextLevel();
                    }
                }
            }
        }

        private IEnumerator DisplayText(string text, float seconds)
        {
            _progressText.text = text;
            yield return new WaitForSeconds(1f);
            _progressText.gameObject.SetActive(true);
            yield return new WaitForSeconds(seconds);
            _progressText.gameObject.SetActive(false);
        }

        private void NextLevel()
        {
            _level++;
            _attempt = 1;
            _filledBricks.AddRange(_playerSequence);
            _brickShowDuration -= _brickShowDurationDecrease;
            _bricksInSequence += _bricksInSequenceIncrease;

            StartCoroutine(ShowSequence());
        }

        private void Retry()
        {
            _attempt++;
            ClearBricks();
            StartCoroutine(ShowSequence());
        }

        private void ClearBricks()
        {
            foreach (Brick brick in _playerSequence)
            {
                brick.HideBrick();
            }
        }

        private void End()
        {
            _button.gameObject.SetActive(true);
        }

        private bool CompareSequences()
        {
            for (int i = 0; i < _playerSequence.Count; i++)
            {
                if (_playerSequence[i] != _correctSequence[i])
                    return false;
            }

            return true;
        }

        private Brick ChooseRandomBrick()
        {
            List<Brick> _availableBricks = _bricks.Except(_correctSequence).ToList().Except(_filledBricks).ToList();
            return _availableBricks[UnityEngine.Random.Range(0, _availableBricks.Count)];
        }
    }
}