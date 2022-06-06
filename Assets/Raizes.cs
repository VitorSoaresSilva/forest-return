using System.Collections;
using System.Collections.Generic;
using Interactable;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Raizes : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
