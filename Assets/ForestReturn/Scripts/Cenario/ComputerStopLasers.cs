using ForestReturn.Scripts.Interactable;
using ForestReturn.Scripts.Inventory;
using ForestReturn.Scripts.Triggers;
using UnityEngine;
using UnityEngine.Events;

namespace ForestReturn.Scripts.Cenario
{
    public class ComputerStopLasers : MonoBehaviour, IInteractable
    {
        [SerializeField] private TriggerObject alreadyUserTrigger;
        private bool _alreadyUsed;
        public UnityEvent setAsInteractable;
        public UnityEvent setAsNotInteractable;
        // [SerializeField] GameObject
        [SerializeField] private TrapLaser[] trapLasers;
        [SerializeField] private GameObject door;

        private void Start()
        {
            if (InventoryManager.InstanceExists && InventoryManager.Instance.triggerInventory.Contains(alreadyUserTrigger))
            {
                _alreadyUsed = true;
                SetState(false);
            }
        }

        public void Interact()
        {
            if (InventoryManager.InstanceExists && !InventoryManager.Instance.triggerInventory.Contains(alreadyUserTrigger))
            {
                InventoryManager.Instance.triggerInventory.AddTrigger(alreadyUserTrigger);
            }
            _alreadyUsed = true;
            SetState(false);
            setAsNotInteractable?.Invoke();
        }

        private void SetState(bool state)
        {
            foreach (var trapLaser in trapLasers)
            {
                trapLaser.ChangeState(state);
            }
            door.SetActive(state);
        }

        public void SetStatusInteract(bool status)
        {
            if (_alreadyUsed)
            {
                setAsNotInteractable?.Invoke();
                return;
            }
        
            if (status)
            {
                setAsInteractable?.Invoke();
            }
            else
            {
                setAsNotInteractable?.Invoke();
            }
        }
    }
}
