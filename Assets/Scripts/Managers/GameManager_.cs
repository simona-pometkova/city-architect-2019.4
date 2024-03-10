using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager_ : Singleton<GameManager_>
    {
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private PuzzleManager _puzzleManager;
        [SerializeField] private DialogueManager _dialogueManager;

        private void Start()
        {
            DialogueManager.Instance.OnDialogueEnded += ProgressGame;
        }

        public void StartGame()
        {
            // Call the LevelManager and load the CityScene
            _levelManager.LoadScene(_levelManager.CityScene);
        }

        public void ProgressGame()
        {
            _levelManager.LoadScene(_levelManager.FoundationScene);
        }

        public void EndGame()
        {
            // Call the PuzzleManager to get the final score and implement logic
            // depending on what it is. 
        }
    }
}