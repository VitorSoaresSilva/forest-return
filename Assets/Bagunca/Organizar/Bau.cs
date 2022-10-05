using Interactable;
using Managers;
using Player;
using UnityEngine;
using Weapons;

public class Bau : MonoBehaviour, IInteractable
{
    [SerializeField] private WeaponsScriptableObject weaponsScriptableObject;

    public void Interact()
    {
        if (GameManager.instance != null)
        {
            PlayerMain playerMain =  GameManager.instance.PlayerMain;
            if (playerMain != null)
            {
                playerMain._weaponHolder.CollectWeapon(weaponsScriptableObject);
                Destroy(gameObject);
            }
        }
    }
    
}
