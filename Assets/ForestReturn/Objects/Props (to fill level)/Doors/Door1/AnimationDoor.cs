using UnityEngine;
[RequireComponent(typeof(Animator))]
public class AnimationDoor : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private const string OpenDoorTrigger = "OpenDoor";
    private const string CloseDoorTrigger = "CloseDoor";
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Open()
    {
        _animator.SetTrigger(OpenDoorTrigger);
    }

    public void Close()
    {
        _animator.SetTrigger(CloseDoorTrigger);
    }
}
