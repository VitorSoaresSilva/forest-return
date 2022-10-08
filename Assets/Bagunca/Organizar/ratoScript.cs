using _Developers.Vitor.Scripts.Interactable;
using _Developers.Vitor.Scripts.Managers;
using UnityEngine;

namespace Bagunca.Organizar
{
    public class ratoScript : MonoBehaviour, IInteractable
    {
        [SerializeField] private Door _door;
        public void Interact()
        {
            _door.Open();
            // GameManager.instance.configLobby.blacksmithSaved = true;
            // TODO: fazer uma animação bonita aqui
            Destroy(gameObject);
        }
    }
}
