using UnityEngine;
using UnityEngine.AI;

namespace ForestReturn.Scripts.Enemies
{
    public class Boxer : BaseEnemy
    {
        protected override void Awake()
        {
            base.Awake();
            NavMeshAgent = GetComponent<NavMeshAgent>();
            Animator = GetComponentInChildren<Animator>();
        }
    }
}