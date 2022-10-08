using UnityEngine;

namespace ForestReturn.Scripts
{
    [CreateAssetMenu(fileName = "newAttribute", menuName = "ScriptableObject/Action/Attributes", order = 1)]
    public class Attributes : ScriptableObject
    {
        public int health;
        public int mana;
        public int defense;
        public int damage;
    }
}