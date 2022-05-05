using System;
using System.Collections;
using Character;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemies
{
    public enum StateMachineEnum
    {
        Chasing,
        Idle,
        Attacking
    }
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyConfig enemyConfig;
        private NavMeshAgent _navMeshAgent;
        private Coroutine stateMachineCoroutine;
        private bool isActive;
        private StateMachineEnum _stateMachineEnum;
        [SerializeField]private Transform _playerTransform;

        private Vector3 _currentTarget;
        // [SerializeField] private float cooldownAttack;
        private bool canAttack;
        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            isActive = true;
            _stateMachineEnum = StateMachineEnum.Idle; 
            stateMachineCoroutine = StartCoroutine(nameof(UpdateStateMachine));
            _navMeshAgent.stoppingDistance = enemyConfig.stopDistance;
            // Attack();
            _currentTarget = GetRandomPointNear(_playerTransform.position - transform.position);
            StartCoroutine(nameof(CoolDownAttack));
        }

        private void Update()
        {
            // GetRandomPointNear(transform.position - _target.position);
        }

        private void OnDisable()
        {
            StopCoroutine(stateMachineCoroutine);
        }
 
        private IEnumerator UpdateStateMachine()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(enemyConfig.updateDelay);
            while (isActive)
            {
                switch (_stateMachineEnum)
                {
                    case StateMachineEnum.Attacking:
                        UpdateAttacking();
                        break;
                    case StateMachineEnum.Chasing:
                        UpdateChasing();
                        break;
                    case StateMachineEnum.Idle:
                        Idle();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                // cooldownAttack = Mathf.Max(enemyConfig.updateDelay - cooldownAttack, 0);
                yield return waitForSeconds;
            }
        }

        private IEnumerator CoolDownAttack()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(enemyConfig.cooldownAttack);
            yield return waitForSeconds;
            canAttack = true;
        }

        private void UpdateChasing()
        {
            // _navMeshAgent.destination = _target.position;
            // _navMeshAgent.destination = GetRandomPointNear(transform.position - _playerTransform.position);
            // Debug.Log(_navMeshAgent.destination);
            if (canAttack && (_navMeshAgent.remainingDistance - _navMeshAgent.stoppingDistance) < 1)
            {
                EnterAttackingMode();       
            }
        }

        private Vector3 GetRandomPointNear(Vector3 directionalVector)
        {
            /*
             * Pegar um vetor direcional
             */
            var rand = Random.Range(0.2f, 0.8f);
            var point = directionalVector * rand;
            var randomAngle = Random.Range(-45, 45);
            var rot = Quaternion.Euler(0, randomAngle, 0) * point;
            return rot;
        }

        private void EnterChasingMode()
        {
            _navMeshAgent.stoppingDistance = enemyConfig.rangeChasing;
            _stateMachineEnum = StateMachineEnum.Chasing;
            _currentTarget = GetRandomPointNear(_playerTransform.position - transform.position);
            _navMeshAgent.destination = _currentTarget;
        }

        private void EnterAttackingMode()
        {
            Debug.Log("Attack mode");
            _navMeshAgent.stoppingDistance = enemyConfig.rangeAttack;
            _stateMachineEnum = StateMachineEnum.Attacking;
            _navMeshAgent.destination = _playerTransform.position;
        }

        private void UpdateAttacking()
        {
            _navMeshAgent.destination = _playerTransform.position;
            var distance = Vector3.Distance(transform.position, _playerTransform.position);
            if (distance < enemyConfig.rangeAttack)
            {
                Attack();
            }
        }

        private void Attack()
        {
            // if attack
            Debug.Log("Attack");
            canAttack = false;
            StartCoroutine(nameof(CoolDownAttack));
            EnterChasingMode();
        }

        private void Idle()
        {
            // Debug.Log("Idle");
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Triggered: " + other.name);
            if (other.transform.parent.TryGetComponent<PlayerCharacter>(out var playerCharacter))
            {
                Debug.Log("Player");
                _playerTransform = other.transform;
                EnterChasingMode();
                //Avisar outros inimgios da mesma sala
            }
        }
        void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_currentTarget, 1);
        }
    }
    
}
