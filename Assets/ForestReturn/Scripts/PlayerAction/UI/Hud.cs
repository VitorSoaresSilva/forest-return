using System;
using ForestReturn.Scripts.PlayerAction.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace ForestReturn.Scripts.PlayerAction.UI
{
    public class Hud : MonoBehaviour
    {
        public Slider lifeSliderFront;
        public Slider lifeSliderBack;

        private void Start()
        {
            lifeSliderFront.value = (float)LevelManager.instance.playerScript.CurrentHealth /
                                    (float)LevelManager.instance.playerScript.MaxHealth;
            LevelManager.instance.playerScript.OnHurt += PlayerScriptOnOnHurt;
            LevelManager.instance.playerScript.OnHealthHealed += PlayerScriptOnOnHealthHealed;
        }

        private void PlayerScriptOnOnHealthHealed()
        {
            lifeSliderFront.value = (float)LevelManager.instance.playerScript.CurrentHealth /
                                    (float)LevelManager.instance.playerScript.MaxHealth;
        }

        private void PlayerScriptOnOnHurt()
        {
            lifeSliderFront.value = (float)LevelManager.instance.playerScript.CurrentHealth /
                                    (float)LevelManager.instance.playerScript.MaxHealth;
        }
    }
}