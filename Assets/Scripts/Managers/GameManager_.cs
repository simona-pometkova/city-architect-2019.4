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

        public float FoundationScore;
        public float WallsScore;
        public float RoofScore;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            DialogueManager.Instance.OnDialogueEnded += ProgressGame;
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
            RoofScore = PlayerPrefs.GetFloat("RoofScore");
            FoundationScore = PlayerPrefs.GetFloat("FoundationScore");
            WallsScore = PlayerPrefs.GetFloat("WallScore");
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