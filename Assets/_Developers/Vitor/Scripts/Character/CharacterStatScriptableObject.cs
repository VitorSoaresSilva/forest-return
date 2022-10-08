using UnityEngine;

namespace _Developers.Vitor.Scripts.Character
{
    [CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObject/Character", order = 0)]
    public class CharacterStatScriptableObject : ScriptableObject
    {
        public int attack;
        public int trueDamageAttack;
        public int armor;
        public int stamina;
        public int health;
        public int mana;
    }
}