using Managers;
using UnityEngine;

namespace Interactable
{
    public class Portal : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform pointToTeleport;
        
        public void Interact()
        {
            GameManager.instance.PlayerMain.HandleTeleportActivated(pointToTeleport.position);
        }
        /*
         * Um broto no chão, que ao me aproximar dele e interagir, vou ter teletransportado para o outro ponto
         *
         * ativar animação do player
         *
         *
         * interagir
         *  desligar o input
         *  ativar animação
         */
    }
}