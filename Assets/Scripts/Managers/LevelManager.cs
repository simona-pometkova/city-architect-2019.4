using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public string MainMenuScene => _mainMenuScene;
        public string CityScene => _cityScene;

        [SerializeField] private string _mainMenuScene;
        [SerializeField] private string _cityScene;
        // Create and reference the other scenes.
        // ! - Download & use NaughtyAttributes to reference Scenes directly, instead of writing their names.

        private Dictionary<string, int> _loadOrder = new Dictionary<string, int>(); // Order of game scenes. Is it mutable?

        public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);

        public void ProgressNext()
        {
            // Load the next scene from the _loadOrder dictionary.
        }
    }
}