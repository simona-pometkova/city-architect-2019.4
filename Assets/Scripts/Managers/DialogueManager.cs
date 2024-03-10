using Assets;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _dialogueButtonText;
    [SerializeField] private float _dialogueSpeed;
    [SerializeField] private Dialogue _currentDialogue; // This is set in every Scene respectively

    private int _index = 0;
    private bool _sentenceFinished;

    public event Action OnDialogueEnded; 

    private void Start()
    {
        if (_currentDialogue != null)
        {
            _dialogueButtonText.text = _currentDialogue.Sentences[_index].ButtonText;
            NextSentence();
        }
    }

    public void Skip()
    {
        if (_currentDialogue != null)
        {
            StopAllCoroutines();

            if (!_sentenceFinished)
            {
                _dialogueText.text = _currentDialogue.Sentences[_index].SentenceText;
                _dialogueButtonText.text = _currentDialogue.Sentences[_index].ButtonText;
                _sentenceFinished = true;
            }
            else
            {
                if (_index != _currentDialogue.Sentences.Count - 1)
                {
                    _index++;
                    NextSentence();
                }
                else
                {
                    OnDialogueEnded?.Invoke();
                }
            }
        }
    }

    public void NextSentence()
    {
        _sentenceFinished = false;
        _dialogueButtonText.text = _currentDialogue.Sentences[_index].ButtonText;
        _dialogueText.text = "";
        StartCoroutine(WriteSentence());
    }

    IEnumerator WriteSentence()
    {
        foreach (char character in _currentDialogue.Sentences[_index].SentenceText.ToCharArray())
        {
            _dialogueText.text += character;
            yield return new WaitForSeconds(_dialogueSpeed);
        }

        _sentenceFinished = true;
    }
}

//public TMP_Text introDialogueText; // Change this line in your DialogueManager
//public TMP_Text endDialogueText; // Change this line in your DialogueManager

//private int currentIntroIndex = 0; // To keep track of the current index in the texts array
//private int currentEndIndex = 0; // To keep track of the current index in the texts array

//public string[] introDialogueTexts = new string[]
//{
//    "Our city needs you help, Architect. We need to build a new City Hall. We've got three unique challenges for you, and time is of the essence.",
//    "Our city's 100th anniversary is just around the corner, and we need the project done by then. It's a tall order, but I believe you're the perfect person for the job.",
//    "So, what do you say? Will you accept this challenge and help us make history?"
//};
//public string[] endDialogueTexts; // Array of dialogue texts to cycle through

//// City Scene
//// Call this method from a button click event listener
//public void ChangeIntroDialogueText()
//{
//    if (introDialogueTexts.Length == 0) return; // Check if the array is empty to avoid errors

//    introDialogueText.text = introDialogueTexts[currentIntroIndex]; // Set the text to the current index

//    currentIntroIndex++; // Increment the index to point to the next text
//    if (currentIntroIndex >= introDialogueTexts.Length) // If the end of the array is reached,
//        currentIntroIndex = 0; // Loop back to the first element
//}

//// End Scene
//public void ChangeEndDialogueText()
//{
//    if (endDialogueTexts.Length == 0) return; // Check if the array is empty to avoid errors

//    endDialogueText.text = endDialogueTexts[currentEndIndex]; // Set the text to the current index

//    currentEndIndex++; // Increment the index to point to the next text
//    if (currentEndIndex >= endDialogueTexts.Length) // If the end of the array is reached,
//        currentEndIndex = 0; // Loop back to the first element
//}
