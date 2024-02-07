using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameFlowController : MonoBehaviour
    {
        [SerializeField] private string _cityViewSceneName;
        [SerializeField] private string _mainMenuSceneName;

        // Might optimise into one method. Setting up initial code
        public void StartGame() => SceneManager.LoadScene(_cityViewSceneName);

        public void ExitGame() => SceneManager.LoadScene(_mainMenuSceneName);
    }
}