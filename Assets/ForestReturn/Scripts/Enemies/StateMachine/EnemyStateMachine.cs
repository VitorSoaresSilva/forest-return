using System;
using System.Collections;
using Character;
using Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;
using Weapons;

namespace Enemies.StateMachine
{
    public class EnemyStateMachine: MonoBehaviour
    {
        private BaseState currentState;
        public Transform _playerTransform;
        public EnemyConfig enemyConfig;
        public bool canAttack = false;
        public Coroutine cooldownAttackCoroutine;
        public Coroutine updateStateCoroutine;
        private bool updateActive;
        public NavMeshAgent navMeshAgent;
        [SerializeField] private SphereCollider alertSphereCollider;
        public Animator _animator;
        private const string AnimatorIsMoving = "isMoving";
        public string AnimatorAttackTrigger = "Attack";
        public bool isAttacking;
        public bool canCauseDamage;
        private WeaponHolder _weaponHolder;

        private void Awake()
        {
            // _animator = GetComponent<Animator>();
            _weaponHolder = GetComponent<WeaponHolder>();
        }

        private void Start()
        {
            updateActive = true;
            updateStateCoroutine = StartCoroutine(nameof(UpdateState));
            ChangeState(new IdleState());
        }

        private void Update()
        {
            // Debug.Log(navMeshAgent.velocity.magnitude);
            _animator.SetBool(AnimatorIsMoving,navMeshAgent.velocity.magnitude > 0.01f);
        }

        private IEnumerator UpdateState()
        {
            while (updateActive)
            {
                currentState?.UpdateState();
                yield return new WaitForSeconds(enemyConfig.updateDelay);
            }
            yield return null;
        }

        public void ChangeState(BaseState newState)
        {
            currentState?.DestroyState();
            currentState = newState;
            
            if (currentState == null) return;
            currentState.owner = this;
            currentState.PrepareState();
        }

        public IEnumerator CooldownAttack()
        {
            WaitForSeconds wait = new WaitForSeconds(enemyConfig.cooldownAttack);
            yield return wait;
            canAttack = true;
        }
        private void OnTriggerEnter(Collider other)
        {
            var playerCharacter = other.GetComponentInParent<PlayerMain>();
            if (playerCharacter != null)
            {
                if (canCauseDamage)
                {
                    playerCharacter.TakeDamage(_weaponHolder.DataDamage);
                }
                else
                {
                    _playerTransform = other.transform;
                    alertSphereCollider.enabled = false;
                    ChangeState(new ChasingState());
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            var playerCharacter = other.GetComponentInParent<PlayerMain>();
            if (playerCharacter != null)
            {
                if (canCauseDamage)
                {
                    playerCharacter.TakeDamage(_weaponHolder.DataDamage);
                }
            }
        }

        public void EndAnimationAttack()
        {
            canCauseDamage = false;
            isAttacking = false;
            ChangeState(new ChasingState());
        }
        public void StartAnimationAttack()
        {
            canCauseDamage = true;
        }
    }
}