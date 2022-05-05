using System;
using System.Collections;
using Character;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy1 : MonoBehaviour
    {
        private Transform _playerTransform;
        [SerializeField] private EnemyConfig enemyConfig;
        private bool isActive;
        private float _coolDownAttack;
        private NavMeshAgent _navMeshAgent;
        private bool attackState = false;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            isActive = true;
            StartCoroutine(nameof(UpdateStateMachine));
        }

        public IEnumerator UpdateStateMachine()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(enemyConfig.updateDelay);
            while (isActive)
            {
                _coolDownAttack = Mathf.Max(0, _coolDownAttack - enemyConfig.updateDelay);
                if (attackState)
                {
                    UpdateAttackState();
                }
                else if (_coolDownAttack <= 0)
                {
                    EnterAttackState();
                }
                else
                {
                    var distance = Vector3.Distance(transform.position, _playerTransform.position);
                    if (distance < enemyConfig.minDistance || distance > enemyConfig.maxDistance)
                    { 
                        _navMeshAgent.destination =  GetRandomPointNear(-_playerTransform.position - transform.position);
                    }
                }
                yield return waitForSeconds;
            }
        }

        private void UpdateAttackState()
        {
            _navMeshAgent.destination = _playerTransform.position;
            var distance = Vector3.Distance(transform.position, _playerTransform.position);
            if (distance < enemyConfig.rangeAttack)
            {
                Attack();
            }
        }

        private void EnterAttackState()
        {
            attackState = true;
            _navMeshAgent.stoppingDistance = enemyConfig.rangeAttack;
        }
        private void Attack()
        {
            Debug.Log("attack");
            attackState = false;
            EnterChasingMode();
        }

        private void EnterChasingMode()
        {
            _navMeshAgent.stoppingDistance = enemyConfig.rangeChasing;
        }

        private Vector3 GetRandomPointNear(Vector3 directionalVector)
        {
            var rand = Random.Range(0.2f, 0.8f);
            var point = directionalVector * rand;
            var randomAngle = Random.Range(-45, 45);
            var rot = Quaternion.Euler(0, randomAngle, 0) * point;
            return rot;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent.TryGetComponent<PlayerCharacter>(out var playerCharacter))
            {
                _playerTransform = other.transform;
            }
        }
    }
    
}
