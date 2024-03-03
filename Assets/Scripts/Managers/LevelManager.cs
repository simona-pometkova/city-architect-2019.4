using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class LevelManager : Singleton<LevelManager>
    {
        public string MainMenuScene => _mainMenuScene;
        public string CityScene => _cityScene;

        [SerializeField, Scene] private string _mainMenuScene;
        [SerializeField, Scene] private string _cityScene;
        // Reference the other scenes.

        private Dictionary<string, int> _loadOrder = new Dictionary<string, int>(); // Order of game scenes. Is it mutable?

        public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);

        public void ProgressNext()
        {
            // Load the next scene from the _loadOrder dictionary.
        }
    }
}