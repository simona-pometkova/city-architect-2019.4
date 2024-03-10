using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager_ : Singleton<GameManager_>
    {
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private DialogueManager _dialogueManager;

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