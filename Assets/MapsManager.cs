using System;
using System.Collections;
using System.Collections.Generic;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.Triggers;
using ForestReturn.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class MapsManager : MonoBehaviour
{
    public Sprite lobby;
    public Sprite level1;
    public Sprite level2;
    public Sprite level3;
    public Image image;
    
    
    
    private void OnEnable()
    {
        UpdateMap();
    }

    private void UpdateMap()
    {
        if (LevelManager.InstanceExists)
        {
            switch (LevelManager.Instance.sceneIndex)
            {
                case Enums.Scenes.Lobby:
                    image.sprite = lobby;
                    break;
                case Enums.Scenes.Level01:
                    image.sprite = level1;
                    break;
                case Enums.Scenes.Level02:
                    image.sprite = level2;
                    break;
                case Enums.Scenes.Level03:
                    image.sprite = level3;
                    break;
            }
        }    
    }
}
