using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class UIManager : MonoBehaviour
{
    public Button[] gameOverButtons;
    public GameObject gameOverPanel;
    public GameObject startGamePanel;
    public Text scoreText;
    public Text gameOverText;
    public BlockController blockController;
    public RoofDialogueManager dialogueManager;
    public BlockSpawner blockSpawner;


    private const string scoreKey = "RoofScore";
    private bool gameOverSoundTrigger = false;
    // Start is called before the first frame update
    void Start()
    {

        startGamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        foreach(Button button in gameOverButtons)
        {
            button.gameObject.SetActive(false);
        }
        
    }

    public void GameStart()
    {
        startGamePanel.SetActive(false);
        blockSpawner.SpawnBlock();

    }

    // Update is called once per frame
    void Update()
    {
        DisplayScore();
        DisplayGameOverPanel();
        
        
    }

    //function to display score - counts the transform objects stored in grid
    void DisplayScore()
    {
        int score = GetScore();
        //reference canvas object and return text child, update using count
        GameObject scoreObject = GameObject.Find("Canvas");

        if(scoreObject != null)
        {
            scoreText = scoreObject.GetComponentInChildren<Text>();

            if (scoreText != null)
                {
                    scoreText.text = "Score: " + score.ToString();
                }
        }
        
    }
    public void DisplayGameOverPanel()
    {
        bool isGameOver = BlockController.gameOver;
        if(isGameOver == true)
        {
            PlayGameOverSound();
            //Debug.Log("Gameover call from UI");
            gameOverPanel.SetActive(true);
            DisplayEndGameMessage();
            
            
        }

    }
    public void RestartGame()
    {
        Debug.Log("button clicked");
        //reset static variables
        BlockController.ResetGrid();
        BlockController.gameOver = false;
        gameOverSoundTrigger = false;
        
        //restart scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        

    }
    public int GetScore()
    {
        Transform[,] grid = BlockController.GetGrid();
        //loop to count transform objects stored in grid
        int count = 0;
        for(int i = 0; i < grid.GetLength(0); i++)
        {
            for(int j = 0; j < grid.GetLength(1)-2; j++)
            {
                if(grid[i,j] != null)
                {
                    count++;
                }
            }
        }
        return count;
    }
    public void DisplayEndGameMessage()
    {
        int score = GetScore();
        gameOverText.text = dialogueManager.GetGameOverMessage(score);
        DisplayButtons(score);
        SetPlayerPrefScore((float)score);
    }

    public void DisplayButtons(int score)
    {
        //I've probably gone ass ways about this, using essentially the same conditions as in roof dialogue manager
        //might be a better idea just to do it once in DisplayEndGameManager?!
        
        foreach(Button button in gameOverButtons)
        {
            button.gameObject.SetActive(true);
        }
    }     
    public void SetPlayerPrefScore(float score)
    {
        PlayerPrefs.SetFloat(scoreKey, score);
    }
        
    
    public void PlayGameOverSound()
    {
        if(gameOverSoundTrigger == false)
        {
            int score = GetScore();

            if(score > 119)
            {
                GameObject audioObject = GameObject.Find("GameOverSuccessAudioSource");
                AudioSource audioSource = audioObject.GetComponent<AudioSource>();
                if(audioSource != null)
                {
                    AudioManager.Instance.PlaySound(audioSource);
                    Debug.Log("Success sound triggered");
                    gameOverSoundTrigger = true;
                }
                else
                {
                    Debug.Log("Audio Source Not Found");
                }
            }
            else
            {
                GameObject audioObject = GameObject.Find("GameOverFailAudioSource");
                AudioSource audioSource = audioObject.GetComponent<AudioSource>();
                if(audioSource != null)
                {
                    AudioManager.Instance.PlaySound(audioSource);
                    Debug.Log("Fail sound triggered");
                    gameOverSoundTrigger = true;
                }
                else
                {
                    Debug.Log("Audio Source Not Found");
                }
            }
        }





    }

}
