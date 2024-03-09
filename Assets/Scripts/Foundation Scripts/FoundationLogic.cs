using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class FoundationLogic : MonoBehaviour
{
    //Public variables - assign in unity insepctor
    public TMP_Text heightText;
    public TMP_Text widthText;
    public InputField answerInput;
    public TMP_Text scoreText;
    public TMP_Text questionNumberText;
    public TMP_Text timerText;
    public TMP_Text endGameText;
    //validation panel popup if invalid input - ie not an integer
    public GameObject validationPanel;
    public GameObject endGamePanel;



    //Keep score
    private int score;
    //question number - used to determine difficulty and end game check when it gets to 30
    private int questionNum = 1;
    private float timeRemaining = 60f;
    private bool timerActive = false;


    void Start()
    {
        //set validation panel false
        validationPanel.SetActive(false);
        endGamePanel.SetActive(false);
        timerActive = true;
        //generate first question
        GenerateQuestion();
        //event listener for answer validation popup
        answerInput.onValueChanged.AddListener(ValidateInput);

        
    }

    // Update is called once per frame
    void Update()
    {
        //update score and question title
        UpdateScore();
        UpdateQuestionTitle();
        UpdateTimer();
        EndTimeCheck();
        
        
    }

    void GenerateQuestion()
    {

        //This method generates random numbers, stores them in the text variables
        //with increasing difficulty depending on the number of questions answered
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
        //assign in inspector on button click
        //checks the input field if it's an integer and compares it to heightvalue * widthvalue
        int answer, height, width;
        height = int.Parse(heightText.text);
        width = int.Parse(widthText.text);
        answer = int.Parse(answerInput.text);
        if(questionNum == 30)
            {
                EndGame();
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
    void UpdateQuestionTitle()
    {
        questionNumberText.text = "Question #" + questionNum.ToString();
    }

    void DisplayValidationError(float duration)
    {
        //display panel
        validationPanel.SetActive(true);
        //start coroutine to display panel for set duration
        StartCoroutine(CloseDelay(duration));
    }
    
    void ValidateInput(string fieldInput)
    {
        //validates input. trys to part an int and returns a bool.
        int result;
        bool isInt = int.TryParse(answerInput.text, out result);

        if(!isInt && answerInput.text != "")
        {
            DisplayValidationError(2f);
        }
    }

    void UpdateTimer()
    {
        timerText.text = "Time: " + Mathf.Max(0,Mathf.RoundToInt(timeRemaining)).ToString();
        if(timerActive)
        {
            timeRemaining -= Time.deltaTime;
        }
    }

    void EndTimeCheck()
    {
        if (timerActive && timeRemaining <= 0)
            {
                // Timer has run out, invoke the onTimerEnd event
                timerActive = false;
                EndGame();
                Debug.Log("Time run out");
            }
    }

    void EndGame()
    {
        endGamePanel.SetActive(true);
        timerActive = false;

        if(score <= 10)
        {
            endGameText.text = "You scored: " + score.ToString();
        }
        else if(score <=20)
        {
            endGameText.text = "Well Done! You scored: " + score.ToString();
        }
        else if(score <= 30)
        {
            endGameText.text = "Amazing! You scored: " + score.ToString();
        }
        else if(score == 30)
        {
            endGameText.text = "You're a Genius! You scored: " + score.ToString();
        }
    }
    //used for the coroutine delay and sets panel back to false;
    IEnumerator CloseDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        validationPanel.SetActive(false);
    }
}
