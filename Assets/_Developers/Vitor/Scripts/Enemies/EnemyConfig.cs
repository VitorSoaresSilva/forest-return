using UnityEngine;

namespace _Developers.Vitor.Scripts.Enemies
{
    // [CreateAssetMenu(fileName = "Enemy Config", menuName = "ScriptableObject/Enemy", order = 0)]
    public class EnemyConfig : ScriptableObject
    {
        public float minDistance;
        public float maxDistance;
        public float updateDelay;
        public float stopDistance;
        public float rangeAttack;
        public float rangeChasing;
        public float cooldownAttack;
    }
}