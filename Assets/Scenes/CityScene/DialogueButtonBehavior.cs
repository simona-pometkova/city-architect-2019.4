using UnityEngine;
using UnityEngine.UI; // For UI components
using UnityEngine.SceneManagement; // For scene management

public class ButtonClickTrigger : MonoBehaviour
{
    public Text buttonText; // Assign in the Inspector
    private int currentIndex = 0; // To track the current text index
    private string[] texts = { "Continue", "Continue", "Accept" }; // Button texts

    public void OnButtonClick()
    {
        if (currentIndex < texts.Length)
        {
            buttonText.text = texts[currentIndex]; // Update the button text
            currentIndex++; // Move to the next text
        }
        if (currentIndex == texts.Length) // Check if it's time to load a new scene
        {
            SceneManager.LoadScene("RoofScene"); // Load the next scene
        }
    }
}
