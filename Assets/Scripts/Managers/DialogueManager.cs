using Assets;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

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
            StartDialogue();
        }
    }

    public void StartDialogue()
    {
        _dialogueButtonText.text = _currentDialogue.Sentences[_index].ButtonText;
        NextSentence();
    }

    public void Skip()
    {
        if (_currentDialogue != null)
        {
            StopAllCoroutines();

            if (!_sentenceFinished)
            {
                if (SceneManager.GetActiveScene().name == "SeeScoreScene" && _index == 1)
                {
                    _dialogueText.text = $"You've got {GameManager_.Instance.TotalScore.ToString()} points! Let’s look at what you’ve built for our beautiful city.";
                }
                else
                {
                    _dialogueText.text = _currentDialogue.Sentences[_index].SentenceText;
                }

                _dialogueButtonText.text = _currentDialogue.Sentences[_index].ButtonText;
                _sentenceFinished = true;
            }
            else
            {
                if (_index != _currentDialogue.Sentences.Count - 1)
                {
                    _index++;

                    if (SceneManager.GetActiveScene().name == "SeeScoreScene")
                    {
                        if (_index == 1)
                        {
                            Debug.Log("Setting text...");
                            NextSentence($"You've got {GameManager_.Instance.TotalScore.ToString()} points! Let’s look at what you’ve built for our beautiful city.");
                        }
                    } else
                    {
                        NextSentence();
                    }
                }
                else
                {
                    OnDialogueEnded?.Invoke();
                }
            }
        }
    }

    public void NextSentence(string sentence = null)
    {
        _sentenceFinished = false;
        _dialogueButtonText.text = _currentDialogue.Sentences[_index].ButtonText;
        _dialogueText.text = "";

        if (sentence != null)
        {
            StartCoroutine(WriteSentence(sentence));
        }
        else
        {
            StartCoroutine(WriteSentence());
        }
    }

    IEnumerator WriteSentence(string sentence = null)
    {
        var currentSentence = sentence == null ? _currentDialogue.Sentences[_index].SentenceText : sentence;

        foreach (char character in currentSentence.ToCharArray())
        {
            _dialogueText.text += character;
            yield return new WaitForSeconds(_dialogueSpeed);
        }

        _sentenceFinished = true;
    }

    public void SetDialogue(Dialogue dialogue)
    {
        _currentDialogue = dialogue;
    }
}
