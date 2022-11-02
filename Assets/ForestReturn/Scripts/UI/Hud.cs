using System;
using ForestReturn.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace ForestReturn.Scripts.UI
{
    public class Hud : MonoBehaviour
    {
        public Slider lifeSliderFront;
        public Slider lifeSliderBack;
        public Animator hurtAnimator;
        private static readonly int HurtStringHash = Animator.StringToHash("Hurt");
        private void Start()
        {
            UpdateHealthValue();
            UpdateManaValue();
            LevelManager.Instance.PlayerScript.OnHurt += PlayerScriptOnOnHurt;
            LevelManager.Instance.PlayerScript.OnHealthHealed += PlayerScriptOnOnHealthHealed;
            LevelManager.Instance.PlayerScript.OnLifeChanged += UpdateHealthValue;
            LevelManager.Instance.PlayerScript.OnManaChanged += UpdateManaValue;
        }

        private void OnDestroy()
        {
            if (LevelManager.InstanceExists)
            {
                LevelManager.Instance.PlayerScript.OnHurt -= PlayerScriptOnOnHurt;
                LevelManager.Instance.PlayerScript.OnHealthHealed -= PlayerScriptOnOnHealthHealed;
                LevelManager.Instance.PlayerScript.OnLifeChanged -= UpdateHealthValue;
                LevelManager.Instance.PlayerScript.OnManaChanged -= UpdateManaValue;
            }
        }


        private void PlayerScriptOnOnHealthHealed()
        {
            //TODO: add heal effect
        }

        private void PlayerScriptOnOnHurt()
        {
            hurtAnimator.SetTrigger(HurtStringHash);
        }

        private void UpdateHealthValue()
        {
            lifeSliderFront.value = (float)LevelManager.Instance.PlayerScript.CurrentHealth /
                                    (float)LevelManager.Instance.PlayerScript.MaxHealth;
        }

        private void UpdateManaValue()
        {
            
        }
    }
}