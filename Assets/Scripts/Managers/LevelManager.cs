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
        public string EndGameScene => _endGameScene;
        public string NextScene => _nextScene;

        public string SeeScoreScene => _seeScoreScene;

        [SerializeField, Scene] private string _mainMenuScene;
        [SerializeField, Scene] private string _cityScene;
        [SerializeField, Scene] private string _nextScene; // This is set in every Scene respectively
        [SerializeField, Scene] private string _endGameScene;
        [SerializeField, Scene] private string _seeScoreScene;
        
        public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);

        public void LoadNext() => SceneManager.LoadScene(_nextScene);
    }
}