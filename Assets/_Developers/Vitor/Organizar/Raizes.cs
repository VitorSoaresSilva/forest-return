using System.Collections;
using System.Collections.Generic;
using Interactable;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

public class Raizes : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.instance.LoadScene(Enums.Scenes.Level01);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
