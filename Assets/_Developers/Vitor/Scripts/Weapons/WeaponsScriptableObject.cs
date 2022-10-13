using System;
using _Developers.Vitor.Scripts.Character;
using UnityEngine;

namespace _Developers.Vitor.Scripts.Weapons
{
    // [Serializable]
    // [CreateAssetMenu(fileName = "newWeapon", menuName = "ScriptableObject/Weapons")]
    public class WeaponsScriptableObject : ScriptableObject
    {
        public string weaponName;
        public Texture image;
        [Range(0,5)] public int slotsAmount;
        public DataDamage DataDamage;
    }
}