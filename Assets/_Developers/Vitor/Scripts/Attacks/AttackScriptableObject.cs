using UnityEngine;

namespace ForestReturn.Scripts.Attacks
{
    [CreateAssetMenu(fileName = "New Attack", menuName = "ScriptableObject/Attack", order = 0)]
    public class AttackScriptableObject : ScriptableObject
    {
        public Vector2 knockBackForce;
    }
}