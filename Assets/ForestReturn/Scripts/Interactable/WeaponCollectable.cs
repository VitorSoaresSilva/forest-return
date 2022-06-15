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
            Debug.Log("opa");
            if (GameManager.instance != null)
            {
                PlayerMain playerMain =  GameManager.instance.GetPlayerScript();
                if (playerMain != null)
                {
                    playerMain._weaponHolder.EquipWeapon(new Weapon(weaponData));
                    Destroy(gameObject);
                }
            }
        }
    }
}
