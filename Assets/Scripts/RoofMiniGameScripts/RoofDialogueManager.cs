using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofDialogueManager : MonoBehaviour
{

    //access in unity editor
    public string[] gameOverMessages;
    



    public string GetGameOverMessage(int score)
    {
        if(gameOverMessages.Length < 2)
        {
            Debug.LogError("Array size not large enough");
            return "";
        }
        else
        {
            if(score == 150)
            {
                return (gameOverMessages[0] + score.ToString());
            }
            else
            {
                return (gameOverMessages[1] + score.ToString());
            }
        }

        
    }
}
