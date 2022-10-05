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
            if (GameManager.instance != null)
            {
                PlayerMain playerMain =  GameManager.instance.PlayerMain;
                if (playerMain != null)
                {
                    playerMain._weaponHolder.CollectWeapon(weaponData);
                    Destroy(gameObject);
                }
            }
        }
    }
}
