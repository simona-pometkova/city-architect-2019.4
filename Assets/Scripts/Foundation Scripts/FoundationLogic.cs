using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class FoundationLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text heightText;
    public TMP_Text widthText;
    public InputField answerInput;
    public TMP_Text scoreText;
    public GameObject validationPanel;

    private int[] questionArray;

    private int score;

    private int questionNum = 1;


    void Start()
    {
        validationPanel.SetActive(false);
        GenerateQuestion();

        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        answerInput.onValueChanged.AddListener(ValidateInput);
        
    }

    void GenerateQuestion()
    {

        int height, width;

        if(questionNum <= 9)
        {
            height = Random.Range(1,10);
            width = Random.Range(1,10);
            heightText.text = height.ToString();
            widthText.text = width.ToString();
        }
        else if(questionNum <= 19)
        {
            height = Random.Range(1,100);
            width = Random.Range(1,10);
            heightText.text = height.ToString();
            widthText.text = width.ToString();
        }
        else if(questionNum <= 30)
        {
            height = Random.Range(1,100);
            width = Random.Range(1,100);
            heightText.text = height.ToString();
            widthText.text = width.ToString();
        }


    }

    public void CheckAnswer()
    {
        int answer, height, width;
        height = int.Parse(heightText.text);
        width = int.Parse(widthText.text);
        answer = int.Parse(answerInput.text);
        if(questionNum == 30)
            {
                //end game logic
            }
        else
            {
                if(height * width == answer)
                    {
                        Debug.Log("Correct");
                        answerInput.text = "";
                        GenerateQuestion();
                        questionNum++;
                        score++;
                    }
                else
                    {
                        Debug.Log("Wrong");
                        answerInput.text = "";
                        GenerateQuestion();
                        questionNum++;

                    }
            }

    }
    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void DisplayValidationError(float duration)
    {
        validationPanel.SetActive(true);
        StartCoroutine(CloseDelay(duration));
    }

    void ValidateInput(string fieldInput)
    {
        int result;
        bool isInt = int.TryParse(answerInput.text, out result);

        if(!isInt && answerInput.text != "")
        {
            DisplayValidationError(2f);
        }
    }
    IEnumerator CloseDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        validationPanel.SetActive(false);
    }
}
