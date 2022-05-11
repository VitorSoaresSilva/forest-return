using System;
using Interactable;
using UnityEngine;

namespace _Developers.Vitor
{
    public class testeKey : MonoBehaviour
    {
        public Door door;
        public KeysScriptableObject[] keysIHave;

        private void Start()
        {
            Debug.Log("Door: " + door.OpenWithKey(keysIHave));
        }
    }
}