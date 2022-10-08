using _Developers.Vitor.Scripts.Managers;
using _Developers.Vitor.Scripts.Player;
using _Developers.Vitor.Scripts.Weapons;
using UnityEngine;

namespace _Developers.Vitor.Scripts.Interactable
{
    public class WeaponCollectable : MonoBehaviour, IInteractable
    {
        [SerializeField] private WeaponsScriptableObject weaponData;
        public void Interact()
        {
            // if (GameManager.instance != null)
            // {
            //     PlayerMain playerMain =  GameManager.instance.PlayerMain;
            //     if (playerMain != null)
            //     {
            //         playerMain._weaponHolder.CollectWeapon(weaponData);
            //         Destroy(gameObject);
            //     }
            // }
        }
    }
}
