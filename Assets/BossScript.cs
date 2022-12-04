using System;
using System.Collections;
using System.Collections.Generic;
using ForestReturn.Scripts;
using UnityEngine;
using UnityEngine.Events;

enum BossState
{
    Idle,
    CoolDownAttack,
    Waiting,
    Dead
}

public class BossScript : MonoBehaviour, IDamageable
{
    [SerializeField] private int life = 9;
    private int _currentDamageTaken;
    public GameObject door;
    private Animator _animator;
    private BossState _state;
    public float timeToWaitAfterHandOnGround = 10;
    public float timeToWaitAfterDamage = 4;
    public float timeToWaitForNewAttack = 8;
    // private Coroutine _coroutine;
    private static readonly int AttackAnimationHash = Animator.StringToHash("Attack");
    private static readonly int DeathAnimationHash = Animator.StringToHash("Death");
    private static readonly int HandBackAnimationHash = Animator.StringToHash("HandBack");
    private static readonly int Damage = Animator.StringToHash("Damage");

    public GameObject hurtBox;
    public GameObject hitBox;

    public UnityEvent onBossDead;

    private float _timeToAttack;
    private float _timeToWaitBeforeReturnHand;
    private float _timeToWaitBeforeDie;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        hurtBox.SetActive(false);
        hitBox.SetActive(false);
        _state = BossState.Idle;
    }

    private void Update()
    {
        switch (_state)
        {
            case BossState.Idle:
                //
                break;
            case BossState.CoolDownAttack:
                if (_timeToAttack < Time.time)
                {
                    // DOAttack
                    DoAttack();
                }
                break;
            case BossState.Waiting:
                if (_timeToWaitBeforeReturnHand < Time.time)
                {
                    HandleBossReturnAttack();
                }
                break;
            case BossState.Dead:
                if (_timeToWaitBeforeDie < Time.time)
                {
                    PlayAnimationDeath();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    [ContextMenu("Start")]
    public void StartBoss()
    {
        door.SetActive(true);
        Debug.Log("Start");
        _timeToAttack = Time.time + 2;
        _state = BossState.CoolDownAttack;
    }
    private void DoAttack()
    {
        
        Debug.Log("Do Attack");
        _state = BossState.Idle;
        _currentDamageTaken = 0;
        hurtBox.SetActive(false);
        hitBox.SetActive(true);
        _animator.SetTrigger(AttackAnimationHash);
    }
    public void HandleBossWaitingAttack()
    {
        Debug.Log("HandleBossWaitingAttack");
        hurtBox.SetActive(true);
        hitBox.SetActive(false);
        _state = BossState.Waiting;
        _timeToWaitBeforeReturnHand = Time.time + timeToWaitAfterHandOnGround;
    }

    private void HandleBossReturnAttack()
    {
        Debug.Log("HandleBossReturnAttack");
        hitBox.SetActive(false);
        _state = BossState.CoolDownAttack;
        _timeToAttack = Time.time + timeToWaitForNewAttack;
        _animator.SetTrigger(HandBackAnimationHash);
    }

    private void TakeDamage()
    {
        if (_state != BossState.Waiting || _currentDamageTaken > 3) return;
        _animator.SetTrigger(Damage);
        _currentDamageTaken++;
        life--;
        if (life <= 0)
        {
            Debug.Log("Dead");
            HandleDeath();
            return;
        }
        
        if (_currentDamageTaken == 3)
        {
            Debug.Log("3 damage taken");
            HandleBossReturnAttack();
            return;
        }
        Debug.Log("Damage comum");
        _timeToWaitBeforeReturnHand = Time.time + timeToWaitAfterDamage;
    }


    private void HandleDeath()
    {
        _state = BossState.Dead;
        _timeToWaitBeforeDie = Time.time + 1;
    }

    private void PlayAnimationDeath()
    {
        _state = BossState.Idle;
        _animator.SetTrigger(DeathAnimationHash);
    }
    public void HandleAnimationDeadEnded()
    {
        onBossDead?.Invoke();
    }
    public void TakeDamage(int damage)
    {
        TakeDamage();
    }
    public void TakeDamage(int damage, bool ignoreIntangibility, float timeToReduceMoveSpeed)
    {
        TakeDamage();
    }
}
