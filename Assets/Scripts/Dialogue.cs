using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public List<string> Sentences => _sentences;

    [SerializeField] private List<string> _sentences;
}
