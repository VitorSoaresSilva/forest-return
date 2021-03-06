using FMOD.Studio;
using FMODUnity;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool isClosed;
    private static readonly int CloseDoor = Animator.StringToHash("CloseDoor");
    private static readonly int OpenDoor = Animator.StringToHash("OpenDoor");
    public EventReference openDoorEventPath;
    public EventReference closeDoorEventPath;

    private EventInstance openDoor;
    // [SerializeField] private KeysScriptableObject[] keysNeededToOpen;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger(isClosed ? CloseDoor : OpenDoor);
        openDoor = RuntimeManager.CreateInstance(openDoorEventPath);
        RuntimeManager.AttachInstanceToGameObject(openDoor,transform);
        
        
    }

    public void Open()
    {
        if (!isClosed) return;
        animator.SetTrigger(OpenDoor);
        isClosed = false;
        openDoor.setParameterByName("openClose", 0);
        openDoor.start();
    }

    public void Close()
    {
        if (isClosed) return;
        animator.SetTrigger(CloseDoor);
        isClosed = true;
        EventInstance closeDoor = RuntimeManager.CreateInstance(closeDoorEventPath);
        RuntimeManager.AttachInstanceToGameObject(closeDoor,transform); 
        closeDoor.setParameterByName("openClose", 1);
        closeDoor.start();
    }

    // public bool OpenWithKey(KeysScriptableObject[] keysPlayer)
    // {
    //     foreach (var key in keysNeededToOpen)
    //     {
    //         var hasKey = false;
    //         foreach (var keyPlayer in keysPlayer)
    //         {
    //             if (key == null || key.Equals(keyPlayer))
    //             {
    //                 hasKey = true;
    //             }
    //         }
    //         if (!hasKey)
    //         {
    //             return false;
    //         }
    //     }
    //     Open();
    //     return true;
    // }
}
