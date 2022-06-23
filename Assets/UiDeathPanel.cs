using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Utilities;

public class UiDeathPanel : MonoBehaviour
{
    public void LoadLobbyLevel()
    {
        GameManager.instance.LoadScene(Enums.Scenes.Lobby);
    }
}
