using System;
using System.Collections;
using ForestReturn.Scripts.Managers;
using ForestReturn.Scripts.PlayerScripts;
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
        public float speedSecondLife = 2;
        public float timeSecondLife = 0.6f;
        private float minValue;
        private Coroutine _coroutine;
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

        private void PlayerScriptOnOnHurt(int damageTaken)
        {
            hurtAnimator.SetTrigger(HurtStringHash);
            minValue = LevelManager.Instance.PlayerScript.CurrentHealth;

            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(LifeSlider(LevelManager.Instance.PlayerScript.CurrentHealth + damageTaken));
            }

        }

        private void UpdateHealthValue()
        {
            lifeSliderFront.value = (float)LevelManager.Instance.PlayerScript.CurrentHealth / LevelManager.Instance.PlayerScript.MaxHealth;
            lifeSliderBack.value = lifeSliderFront.value;
        }

        private void UpdateManaValue()
        {
            
        }

        IEnumerator LifeSlider(float maxValue)
        {
            lifeSliderBack.value = maxValue / LevelManager.Instance.PlayerScript.MaxHealth;
            yield return new WaitForSeconds(timeSecondLife);
            float currentValue = maxValue;
            while (currentValue > minValue)
            {
                currentValue -= Time.fixedDeltaTime * speedSecondLife;
                lifeSliderBack.value = currentValue / LevelManager.Instance.PlayerScript.MaxHealth;
                yield return new WaitForFixedUpdate();
            }
            currentValue = minValue / LevelManager.Instance.PlayerScript.MaxHealth;
            lifeSliderBack.value = currentValue;
            _coroutine = null;
            yield return null;
        }
    }
}