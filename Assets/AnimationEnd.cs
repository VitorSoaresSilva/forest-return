using System.Collections;
using System.Collections.Generic;
using Enemies.StateMachine;
using UnityEngine;

public class AnimationEnd : MonoBehaviour
{
    public EnemyStateMachine enemyStateMachine;
    public void EndAnimationAttack()
    {
        enemyStateMachine.EndAnimationAttack();
        Debug.Log("Animation end");
    }
    public void StartAnimationAttack()
    {
        enemyStateMachine.StartAnimationAttack();
        Debug.Log("Animation Start");
    }
}
