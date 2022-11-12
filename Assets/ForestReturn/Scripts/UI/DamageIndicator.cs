using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ForestReturn.Scripts.UI
{
    public class DamageIndicator : MonoBehaviour
    {
        public Animator Animator;
        public AnimationClip[] AnimationClips;
        void Start()
        {
            int random = Random.Range(0, AnimationClips.Length);
           Animator.CrossFade(AnimationClips[random].name,0);
        }
    }
}