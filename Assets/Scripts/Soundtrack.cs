using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Soundtrack : MonoBehaviour
    {
        [SerializeField] private AudioSource _soundtrack;

        private void Start()
        {
            if (_soundtrack != null)
                _soundtrack.Play();
        }
    }
}