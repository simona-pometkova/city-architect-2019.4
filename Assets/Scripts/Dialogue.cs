using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : ScriptableObject
{
    [SerializeField] private List<string> _characterOptions;
    [SerializeField] private List<string> _playerOptions;
}
