using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button[] gameOverButtons;
    public GameObject gameOverPanel;
    public Text scoreText;
    public Text gameOverText;
    public BlockController blockController;
    public RoofDialogueManager dialogueManager;
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        foreach(Button button in gameOverButtons)
        {
            button.gameObject.SetActive(false);
        }
        
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
        
    }

    public void DisplayButtons(int score)
    {
        //I've probably gone ass ways about this, using essentially the same conditions as in roof dialogue manager
        //might be a better idea just to do it once in DisplayEndGameManager?!
        
        if (score > 119)
        {
            foreach(Button button in gameOverButtons)
            {
                button.gameObject.SetActive(true);
            }
        }
        else
        {
            gameOverButtons[0].gameObject.SetActive(true);
            gameOverButtons[1].gameObject.SetActive(true);
        }
        
    }

}
