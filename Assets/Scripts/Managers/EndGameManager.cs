using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class EndGameManager : MonoBehaviour
    {
        [SerializeField] private Dialogue _greatScoreDialogue;
        [SerializeField] private Dialogue _averageScoreDialogue;
        [SerializeField] private Dialogue _lowScoreDialogue;
        [SerializeField] private Sprite _greatScoreCityImage;
        [SerializeField] private Sprite _averageScoreCityImage;
        [SerializeField] private Sprite _lowScoreCityImage;
        [SerializeField] private Image _background;

        private int _totalScore;

        private void Start()
        {
            _totalScore = GameManager_.Instance.CalculateTotalScore();

            if (_totalScore >= 80)
            {
                Debug.Log("Setting great score dialogue");
                _background.sprite = _greatScoreCityImage;
                DialogueManager.Instance.SetDialogue(_greatScoreDialogue);
            }
            else if (_totalScore >= 60 && _totalScore <= 79)
            {
                Debug.Log("Setting average score dialogue");
                _background.sprite = _averageScoreCityImage;
                DialogueManager.Instance.SetDialogue(_averageScoreDialogue);
            }
            else
            {
                Debug.Log("Setting low score dialogue");
                _background.sprite = _lowScoreCityImage;
                DialogueManager.Instance.SetDialogue(_lowScoreDialogue);
            }

            DialogueManager.Instance.StartDialogue();
        }
    }
}