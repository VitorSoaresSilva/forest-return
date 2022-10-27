using ForestReturn.Scripts.Enemies;
using UnityEngine;
using UnityEngine.Events;

namespace ForestReturn.Scripts.Cenario
{
    public class Door : MonoBehaviour
    {
        public UnityEvent openDoorEvent;
        public UnityEvent closeDoorEvent;
        
        public void CloseDoor()
        {
            closeDoorEvent.Invoke();
        }
        
        public void OpenDoor()
        {
            openDoorEvent.Invoke();
        }
    }
}