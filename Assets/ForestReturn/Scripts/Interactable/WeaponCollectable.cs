using Artifacts;
using Managers;
using Player;
using UI;
using UnityEngine;
using Weapons;

namespace Interactable
{
    public class WeaponCollectable : MonoBehaviour, IInteractable
    {
        [SerializeField] private WeaponsScriptableObject weaponData;
        public void Interact()
        {
            PlayerMain playerMain =  GameManager.instance.GetPlayerScript();
            playerMain._weaponHolder.EquipWeapon(new Weapon(weaponData));
            Destroy(gameObject);
        }
    }
}