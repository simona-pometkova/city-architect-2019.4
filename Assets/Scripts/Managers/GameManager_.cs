using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager_ : Singleton<GameManager_>
    {
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private PuzzleManager _puzzleManager;
        [SerializeField] private DialogueManager _dialogueManager;

        /* Everything is subject to change in the process of development. 
         * Code may and probably will be optimised, refactored, some scripts 
         * deleted and etc as I notice some things may be unnecessary or made better.
         */

        public void StartGame()
        {
            // Call the LevelManager and load the CityScene
            _levelManager.LoadScene(_levelManager.CityScene);
        }

        public void ProgressGame()
        {
            // Call the LevelManager to load the corresponding Scene, 
            // also call the PuzzleManager to start the puzzle.
            _levelManager.ProgressNext();
        }

        public void EndGame()
        {
            // Call the PuzzleManager to get the final score and implement logic
            // depending on what it is. 
        }

        // What else?
    }
}