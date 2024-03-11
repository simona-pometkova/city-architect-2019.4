using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager_ : Singleton<GameManager_>
    {
        public const int MAX_POINTS_FOUNDATION_GAME = 30;
        public const int MAX_POINTS_WALLS_GAME = 30;
        public const int MAX_POINTS_ROOF_GAME = 150;

        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private DialogueManager _dialogueManager;

        public float FoundationScore { get; set; }
        public float WallsScore { get; set; }
        public float RoofScore { get; set; }

        private void Start()
        {
            DialogueManager.Instance.OnDialogueEnded += ProgressGame;

            if (SceneManager.GetActiveScene().name == _levelManager.SeeScoreScene)
            {
                Debug.Log("Calculating total score...");
                CalculateTotalScore();
            }

            Debug.Log($"Foundation score: {FoundationScore.ToString()}");
            Debug.Log($"Walls score: {WallsScore.ToString()}");
            Debug.Log($"Roof score: {RoofScore.ToString()}");
        }

        private void OnDestroy()
        {
            if (_dialogueManager != null)
                _dialogueManager.OnDialogueEnded -= ProgressGame;
        }

        public void StartGame()
        {
            _levelManager.LoadScene(_levelManager.CityScene);
        }

        public void ProgressGame()
        {
            if (_levelManager.NextScene == _levelManager.EndGameScene)
                EndGame();
            else
                _levelManager.LoadNext();
        }

        public int CalculateTotalScore()
        {
            float normalizedScoreFoundation = Mathf.Clamp01((float)FoundationScore / MAX_POINTS_FOUNDATION_GAME) * 33f;
            float normalizedScoreWalls = Mathf.Clamp01((float)WallsScore / MAX_POINTS_WALLS_GAME) * 33f;
            float normalizedScoreRoof = Mathf.Clamp01((float)RoofScore / MAX_POINTS_ROOF_GAME) * 33f;

            return Mathf.RoundToInt(normalizedScoreFoundation + normalizedScoreWalls + normalizedScoreRoof);
        }

        public void EndGame()
        {
            _levelManager.LoadScene(_levelManager.EndGameScene);
        }
    }
}