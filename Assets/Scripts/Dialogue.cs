using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public List<Sentence> Sentences => _sentences;

    [SerializeField] private List<Sentence> _sentences;

    [Serializable]
    public struct Sentence
    {
        public string SentenceText;
        public string ButtonText;
    }
}
