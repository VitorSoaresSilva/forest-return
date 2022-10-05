using System;
using UnityEngine;

namespace UI
{
    public class SimplePlayAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void Awake()
        {
            animator.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            animator.enabled = true;
        }
    }
}
