using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager_ : Singleton<GameManager_>
    {
        public int TotalScore => _totalScore;

        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private DialogueManager _dialogueManager;

        public int _totalScore = 0;

        private void Start()
        {
            DialogueManager.Instance.OnDialogueEnded += ProgressGame;
        }

        public void StartGame()
        {
            _levelManager.LoadScene(_levelManager.CityScene);
        }

        public void ProgressGame()
        {
            _levelManager.LoadNext();
        }

        public void EndGame()
        {
            _levelManager.LoadScene(_levelManager.EndGameScene);
        }
    }
}