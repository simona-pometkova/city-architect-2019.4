using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofDialogueManager : MonoBehaviour
{

    //access in unity editor
    public string[] gameOverMessages;
    



    public string GetGameOverMessage(int score)
    {
        if(gameOverMessages.Length < 3)
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
            else if(score < 150 && score > 119)
            {
                return (gameOverMessages[1] + score.ToString());
            }
            else
            {
                return (gameOverMessages[2]);
            }
        }

        
    }
}
