using System;
using System.Collections;
using System.Collections.Generic;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace ForestReturn.Scripts.Enemies
{
    public class BaseEnemy : BaseCharacter
    {
        public EnemyState state { get; private set; }
        public EnemyAttack[] Attacks;
        
        private Player _playerRef;
        private Coroutine _updateCoroutine;
        protected NavMeshAgent NavMeshAgent;
        protected Animator Animator;
        
        [Header("Nav Mesh")] 
        [SerializeField] private float chasingStoppingDistance;
        
        
        [Header("Attack")]
        private bool _isAttacking;
        private float _nextTimeAttack;
        private int _nextAttackIndex = -1;
        private int[] _attackRandomizer;

        private CapsuleCollider _myCollider;
        
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        private static readonly int DeadHashAnimation = Animator.StringToHash("IsDead");

        private void Start()
        {
            _updateCoroutine = StartCoroutine(UpdateState());
            _myCollider = GetComponentInChildren<CapsuleCollider>();
            InitAttackRandomizer();
            foreach (var enemyAttack in Attacks)
            {
                enemyAttack.hitBox.TryGetComponent(out HitBox hitBox);
                if (hitBox == null)
                {
                    hitBox = enemyAttack.hitBox.AddComponent<HitBox>();
                }
                hitBox.damage = enemyAttack.damage;
            }
            
            OnDead += HandleOnDead;
        }

        private void HandleOnDead()
        {
            StopCoroutine(_updateCoroutine);
            Animator.SetTrigger(DeadHashAnimation);
            foreach (var enemyAttack in Attacks)
            {
                if (enemyAttack.hitBox != null)
                {
                    Destroy(enemyAttack.hitBox);
                }
            }
            _myCollider.enabled = false;
        }

        private void InitAttackRandomizer()
        {
            int[] attacksWeight = new int[Attacks.Length];
            int weightAmount = 0;
            for (int i = 0; i < Attacks.Length; i++)
            {
                weightAmount += Attacks[i].weightPriority;
                attacksWeight[i] = Attacks[i].weightPriority;
            }
            _attackRandomizer = new int[weightAmount];
            var s = 0;
            for (int i = 0; i < attacksWeight.Length; i++)
            {
                for (int j = 0; j < attacksWeight[i]; j++)
                {
                    _attackRandomizer[s] = i;
                    s++;
                }
            }
        }

        private void OnDisable()
        {
            if (_playerRef != null)
            {
                _playerRef.OnDead -= PlayerRefOnOnDead;
            }
        }

        public void PlayerDetected()
        {
            if (state == EnemyState.Idle)
            {
                _playerRef = LevelManager.Instance.PlayerScript;
                state = EnemyState.Chasing;
                SetNextAttack();
                _playerRef.OnDead += PlayerRefOnOnDead;
            }
        }

        private void PlayerRefOnOnDead()
        {
            StopCoroutine(_updateCoroutine);
        }


        #region StateMachine
        public void Idle()
        {
            
        }
        private void Chasing()
        {
            if (_nextTimeAttack < Time.time)
            {
                SetAttackingState();
                return;
            }
            if (NeedToMove())
            {
                Move();
            }
        }
        private void Attacking()
        {
            NavMeshAgent.destination = _playerRef.gameObject.transform.position;
            if (!_isAttacking && Vector3.Distance(_playerRef.transform.position, transform.position) <
                Attacks[_nextAttackIndex].stopDistance)
            {
                _isAttacking = true;
                NavMeshAgent.isStopped = true;
                
                Animator.SetTrigger(Attacks[_nextAttackIndex].animationTrigger);
            }
        }
        private void SetAttackingState()
        {
            state = EnemyState.Attacking;
            NavMeshAgent.stoppingDistance = Attacks[_nextAttackIndex].stopDistance;
        }

        private void SetChasingState()
        {
            NavMeshAgent.isStopped = false;
            state = EnemyState.Chasing;
            NavMeshAgent.stoppingDistance = chasingStoppingDistance;
            SetNextAttack();
        }

        #endregion

        #region Handles
        public void HandleAnimationAttackEnded()
        {
            _isAttacking = false;
            SetChasingState();
        }

        public void HandleStartHitBox()
        {
            if (Attacks[_nextAttackIndex].hitBox != null)
            {
                Attacks[_nextAttackIndex].hitBox.SetActive(true);
            }
        }

        public void HandleEndHitBox()
        {
            foreach (var enemyAttack in Attacks)
            {
                if (enemyAttack.hitBox != null)
                {
                    enemyAttack.hitBox.SetActive(false);
                }
            }
        }
        #endregion

        private void SetNextAttack()
        {
            _nextAttackIndex = _attackRandomizer[Random.Range(0, _attackRandomizer.Length)];
            _nextTimeAttack = Time.time + Attacks[_nextAttackIndex].cooldown;
            
        }



        private IEnumerator UpdateState()
        {
            while (!IsDead)
            {
                Animator.SetBool(IsMoving,NavMeshAgent.velocity.magnitude > 0.01f);
                switch (state)
                {
                    case EnemyState.Idle:
                        break;
                    case EnemyState.Chasing:
                        Chasing();
                        break;
                    case EnemyState.Attacking:
                        Attacking();
                        break;
                }
                yield return new WaitForSeconds(0.06f);
            }
        }
        private bool NeedToMove()
        {
            // var distance = Vector3.Distance(transform.position, _playerRef.transform.position);
            // var x = distance < minDistance || distance > maxDistance;
            // return (x);
            return true;
        }

        private void Move()
        {
            NavMeshAgent.isStopped = false;
            // NavMeshAgent.updateRotation = true;
            var playerDistance = Vector3.Distance(transform.position, _playerRef.transform.position);
            var destination = _playerRef.transform.position;
            NavMeshAgent.destination = destination;
            // if (playerDistance < minDistance)
            // {
            //     
            //     destination = (transform.position - _playerRef.transform.position).normalized *
            //                 Random.Range(minDistance, maxDistance);
            // }
            // else
            // {
            //     destination = _playerRef.transform.position;
            // }
            //Código para fazer o inimigo recuar será feito depois
            
            
            // var dot = Vector3.Dot(transform.forward, (destination - transform.forward).normalized);
            // if (dot < 0)
            // {
            //     var destinationDistance = Vector3.Distance(transform.position, destination);
            //     if (destinationDistance > distanceToTurnBackwards)
            //     {
            //         NavMeshAgent.updateRotation = true; 
            //     }
            //     else
            //     {
            //         NavMeshAgent.updateRotation = false;
            //     }
            // }
            // else
            // {
            //     NavMeshAgent.updateRotation = true;
            // }
        }
    }


    public enum EnemyState
    {
        Idle,
        Chasing,
        Attacking
    }

    [Serializable]
    public struct EnemyAttack
    {
        public int damage;
        public float cooldown;
        [Range(1,5)]
        public int weightPriority;
        [Range(1,5)]
        public int stopDistance;
        public string animationTrigger;
        public GameObject hitBox;
    }
}