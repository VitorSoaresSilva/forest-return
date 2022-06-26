using System;
using Artifacts;
using Character;
using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
    [Serializable]
    [CreateAssetMenu(fileName = "newWeapon", menuName = "ScriptableObject/Weapons")]
    public class WeaponsScriptableObject : ScriptableObject
    {
        public string weaponName;
        public Texture image;
        [Range(0,5)] public int slotsAmount;
        public DataDamage DataDamage;
    }
}