using System.Collections;
using _Developers.Vitor.Scripts.Character;
using _Developers.Vitor.Scripts.Damage;
using _Developers.Vitor.Scripts.Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace _Developers.Vitor.Scripts.Enemies.StateMachine
{
    public class EnemyStateMachine: BaseCharacter
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
        [SerializeField] private GameObject hitBoxRotateAttack;
        private static readonly int Death = Animator.StringToHash("Death");
        [SerializeField] private Slider life;
        
        private void Start()
        {
            
            updateActive = true;
            updateStateCoroutine = StartCoroutine(nameof(UpdateState));
            ChangeState(new IdleState());
        }

        private void OnEnable()
        {
            OnDead += HandleDead;
            OnHurt += HandleHurt;
        }

        private void HandleHurt(Vector3 knockbackforce)
        {
            if (life != null)
            {
                life.value = CurrentHealth;
            }
        }

        private void OnDisable()
        {
            OnDead -= HandleDead;
            OnHurt -= HandleHurt;
        }

        private void HandleDead()
        {
            StopAllCoroutines();
            navMeshAgent.enabled = false;
            _playerTransform = null;
            canAttack = false;
            isIntangible = true;
            var hitBoxes = GetComponentsInChildren<HitBox>(true);
            foreach (var hitBox in hitBoxes)
            {
                hitBox.gameObject.SetActive(false);
            }
            ChangeState(new IdleState());
            _animator.SetTrigger(Death);
            this.enabled = false;
        }
    
        private void Update()
        {
            if (navMeshAgent.isActiveAndEnabled)
            {
                _animator.SetBool(AnimatorIsMoving,navMeshAgent.velocity.magnitude > 0.01f);
            }
        }

        private IEnumerator UpdateState()
        {
            while (updateActive)
            {
                if (navMeshAgent.isActiveAndEnabled)
                {
                    currentState?.UpdateState();
                }
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
            if (isDead) return;
            var playerCharacter = other.GetComponentInParent<PlayerMain>();
            if (playerCharacter != null)
            {
                playerCharacter.OnDead += HandlePlayerDead;
                _playerTransform = other.transform;
                alertSphereCollider.enabled = false;
                ChangeState(new ChasingState());
            }
        }

        private void HandlePlayerDead() 
        {
            ChangeState(new IdleState());
        }

        public void EndAnimationAttack()
        {
            hitBoxRotateAttack.SetActive(false);
            canCauseDamage = false;
            isAttacking = false;
            ChangeState(new ChasingState());
        }
        public void StartAnimationAttack()
        {
            hitBoxRotateAttack.SetActive(true);
            canCauseDamage = true;
        }
    }
}