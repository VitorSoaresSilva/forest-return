// using Interactable;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool isClosed;
    private static readonly int CloseDoor = Animator.StringToHash("CloseDoor");
    private static readonly int OpenDoor = Animator.StringToHash("OpenDoor");
    // [SerializeField] private KeysScriptableObject[] keysNeededToOpen;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger(isClosed ? CloseDoor : OpenDoor);
    }

    public void Open()
    {
        if (!isClosed) return;
        animator.SetTrigger(OpenDoor);
        isClosed = false;
    }

    public void Close()
    {
        if (isClosed) return;
        animator.SetTrigger(CloseDoor);
        isClosed = true;
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
