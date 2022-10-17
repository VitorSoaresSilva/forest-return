using System;
using UnityEngine;
using UnityEngine.AI;

namespace ForestReturn.Scripts.NPCs
{
    public class Blacksmith : MonoBehaviour,IBaseNpc
    {
        public GameObject[] objectsToEnable;
        private Animator _animator;
        private static readonly int WorkingHashAnimation = Animator.StringToHash("Working");
        [SerializeField] private NavMeshAgent _navMeshAgent;
        private static readonly int IsWalking = Animator.StringToHash("isWalking");

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void InitOnLobby()
        {
            if (_animator == null)
            {
                _animator = GetComponentInChildren<Animator>();
            }
            foreach (var objectToEnable in objectsToEnable)
            {
                objectToEnable.SetActive(true);
            }
            _animator.SetTrigger(WorkingHashAnimation);
        }

        public void Interact()
        {
            Debug.Log("Interact Blacksmith");
        }

        public void SetStatusInteract(bool status)
        {
            Debug.Log("Set as Interact Blacksmith");
        }
        private void Update()
        {
            if (!_navMeshAgent.isActiveAndEnabled) return;
            _animator.SetBool(IsWalking,_navMeshAgent.velocity.magnitude > 0.1f);
        }
    }
}