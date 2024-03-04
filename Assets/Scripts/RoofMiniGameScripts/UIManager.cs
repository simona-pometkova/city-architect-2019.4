using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    public GameObject gameOverPanel;
    public Text textObject;
    public BlockController blockController;
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        
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
        //reference canvas object and return text child, update using count
        GameObject scoreObject = GameObject.Find("Canvas");

        if(scoreObject != null)
        {
            textObject = scoreObject.GetComponentInChildren<Text>();

            if (textObject != null)
                {
                    textObject.text = "Score: " + count.ToString();
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

}
