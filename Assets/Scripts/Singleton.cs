﻿using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        GameObject go = new GameObject("Manager");
                        _instance = go.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
                _instance = this as T;
            else
                if (_instance != this)
                    Destroy(gameObject);
        }
    }
}