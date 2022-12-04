using System;
using System.Collections;
using System.Collections.Generic;
using ForestReturn.Scripts;
using UnityEngine;
using UnityEngine.Events;

public class BossScript : MonoBehaviour, IDamageable
{
    [SerializeField] private int life = 9;
    private int _currentDamageTaken;
    public GameObject door;
    private Animator _animator;

    public float timeToWaitAfterHandOnGround = 10;
    public float timeToWaitAfterDamage = 4;
    public float timeToWaitForNewAttack = 8;
    private Coroutine _coroutine;
    private static readonly int AttackAnimationHash = Animator.StringToHash("Attack");
    private static readonly int DeathAnimationHash = Animator.StringToHash("Death");
    private static readonly int HandBackAnimationHash = Animator.StringToHash("HandBack");
    private static readonly int Damage = Animator.StringToHash("Damage");

    public GameObject hurtBox;
    public GameObject hitBox;

    public UnityEvent onBossDead;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        hurtBox.SetActive(false);
        hitBox.SetActive(false);
    }

    public void StartBoss()
    {
        door.SetActive(true);
        Debug.Log("Start");
        Invoke(nameof(DoAttack), 2);
    }

    public void TakeDamage()
    {
        _animator.SetTrigger(Damage);
        StopCoroutine(_coroutine);
        _coroutine = null;
        _currentDamageTaken++;
        life--;
        if (life <= 0)
        {
            HandleDeath();
            return;
        }
        
        if (_currentDamageTaken == 3)
        {
            HandleBossReturnAttack();
            return;
        }

        Debug.Log("Damage comum");
        _coroutine = StartCoroutine(WaitForDamage(timeToWaitAfterDamage));
    }
    private void DoAttack()
    {
        _currentDamageTaken = 0;
        hurtBox.SetActive(false);
        hitBox.SetActive(true);
        _animator.SetTrigger(AttackAnimationHash);
    }

    public void HandleDeath()
    {
        Invoke(nameof(PlayAnimationDeath),1);
    }

    private void PlayAnimationDeath()
    {
        _animator.SetTrigger(DeathAnimationHash);
    }
    public void HandleBossReturnAttack()
    {
        Debug.Log("HandleBossReturnAttack");
        hitBox.SetActive(false);
        _coroutine = null;
        _animator.SetTrigger(HandBackAnimationHash);
        Invoke(nameof(DoAttack),timeToWaitForNewAttack);
        // _coroutine = StartCoroutine(WaitForDamage(timeToWaitForNewAttack));
    }
    public void HandleBossWaitingAttack()
    {
        Debug.Log("HandleBossWaitingAttack");
        hurtBox.SetActive(true);
        hitBox.SetActive(false);
        _coroutine = StartCoroutine(WaitForDamage(timeToWaitAfterHandOnGround));
    }

    public void HandleAnimationDeadEnded()
    {
        onBossDead?.Invoke();
    }

    IEnumerator WaitForDamage(float time)
    {
        yield return new WaitForSeconds(time);
        HandleBossReturnAttack();
        yield return null;

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
