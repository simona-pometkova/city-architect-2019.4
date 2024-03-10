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
