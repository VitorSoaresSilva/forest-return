using System.Collections;
using System.Collections.Generic;
using Interactable;
using Managers;
using UnityEngine;

public class ratoScript : MonoBehaviour, IInteractable
{
    [SerializeField] private Door _door;
    public void Interact()
    {
        _door.Open();
        GameManager.instance.configLobby.blacksmithSaved = true;
        // TODO: fazer uma animação bonita aqui
        Destroy(gameObject);
    }
}
