using _Developers.Vitor.Scripts.Interactable;
using _Developers.Vitor.Scripts.Managers;
using _Developers.Vitor.Scripts.Player;
using _Developers.Vitor.Scripts.Weapons;
using UnityEngine;

namespace Bagunca.Organizar
{
    public class Bau : MonoBehaviour, IInteractable
    {
        [SerializeField] private WeaponsScriptableObject weaponsScriptableObject;

        public void Interact()
        {
            // if (GameManager.instance != null)
            // {
            //     PlayerMain playerMain =  GameManager.instance.PlayerMain;
            //     if (playerMain != null)
            //     {
            //         playerMain._weaponHolder.CollectWeapon(weaponsScriptableObject);
            //         Destroy(gameObject);
            //     }
            // }
        }
    
    }
}
