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
            lifeSliderFront.value = (float)LevelManager.instance.PlayerScript.CurrentHealth /
                                    (float)LevelManager.instance.PlayerScript.MaxHealth;
            LevelManager.instance.PlayerScript.OnHurt += PlayerScriptOnOnHurt;
            LevelManager.instance.PlayerScript.OnHealthHealed += PlayerScriptOnOnHealthHealed;
        }

        private void PlayerScriptOnOnHealthHealed()
        {
            lifeSliderFront.value = (float)LevelManager.instance.PlayerScript.CurrentHealth /
                                    (float)LevelManager.instance.PlayerScript.MaxHealth;
        }

        private void PlayerScriptOnOnHurt()
        {
            lifeSliderFront.value = (float)LevelManager.instance.PlayerScript.CurrentHealth /
                                    (float)LevelManager.instance.PlayerScript.MaxHealth;
        }
    }
}