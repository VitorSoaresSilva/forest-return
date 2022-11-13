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
        public Slider lifeSliderCurrentHealth;
        public Slider lifeSliderDamage;
        public Slider lifeSliderHeal;
        public Animator hurtAnimator;
        private static readonly int HurtStringHash = Animator.StringToHash("Hurt");
        public float speedSecondLife = 2;
        public float timeSecondLife = 0.6f;
        private float minValue;
        private float maxValueOnHeal;
        private float timeToSecondLifeDelay;
        private float timeToHealDelay;
        private Coroutine _coroutineDamage;
        private Coroutine _coroutineHeal;
        private void Start()
        {
            lifeSliderHeal.value = 0;
            lifeSliderDamage.value = 0;
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


        private void PlayerScriptOnOnHealthHealed(int oldValue, int newValue)
        {
            if (_coroutineDamage != null)
            {
                StopCoroutine(_coroutineDamage);
            }

            maxValueOnHeal = newValue;
            lifeSliderHeal.value =  maxValueOnHeal / LevelManager.Instance.PlayerScript.MaxHealth;
            lifeSliderDamage.value = 0;
            timeToHealDelay = Time.time + timeSecondLife;
            if (_coroutineHeal == null)
            {
                _coroutineHeal = StartCoroutine(LifeSliderOnHeal(oldValue));
            }

            // var oldValue = lifeSliderCurrentHealth.value;
            // maxValueOnHeal = LevelManager.Instance.PlayerScript.CurrentHealth;
            // StopCoroutine(_coroutine);
            // // lifeSliderDamage.value = maxValueOnHeal / LevelManager.Instance.PlayerScript.MaxHealth;
            // lifeSliderHeal.value = LevelManager.Instance.PlayerScript.CurrentHealth;

        }

        private void PlayerScriptOnOnHurt(int damageTaken)
        {
            hurtAnimator.SetTrigger(HurtStringHash);
            minValue = LevelManager.Instance.PlayerScript.CurrentHealth;
            timeToSecondLifeDelay = Time.time + timeSecondLife;

            if (_coroutineHeal != null)
            {
                StopCoroutine(_coroutineHeal);
                lifeSliderCurrentHealth.value = minValue / LevelManager.Instance.PlayerScript.MaxHealth;
                lifeSliderHeal.value = 0;
            }
            
            if (_coroutineDamage == null)
            {
                _coroutineDamage = StartCoroutine(LifeSlider(LevelManager.Instance.PlayerScript.CurrentHealth + damageTaken));
            }

        }

        private void UpdateHealthValue()
        {
            Debug.Log("Update");
            lifeSliderCurrentHealth.value = (float)LevelManager.Instance.PlayerScript.CurrentHealth / LevelManager.Instance.PlayerScript.MaxHealth;
            if (_coroutineDamage == null)
            {
                lifeSliderDamage.value = lifeSliderCurrentHealth.value;
            }

            if (_coroutineHeal == null)
            {
                lifeSliderHeal.value = lifeSliderCurrentHealth.value;
            }
        }

        private void UpdateManaValue()
        {
            
        }

        IEnumerator LifeSlider(float maxValue)
        {
            lifeSliderDamage.value = maxValue / LevelManager.Instance.PlayerScript.MaxHealth;
            float currentValue = maxValue;
            while (currentValue > minValue)
            {
                if (timeToSecondLifeDelay<Time.time)
                {
                    currentValue -= Time.fixedDeltaTime * speedSecondLife;
                }
                lifeSliderDamage.value = currentValue / LevelManager.Instance.PlayerScript.MaxHealth;
                yield return new WaitForFixedUpdate();
            }
            currentValue = minValue / LevelManager.Instance.PlayerScript.MaxHealth;
            lifeSliderDamage.value = currentValue;
            _coroutineDamage = null;
            yield return null;
        }
        IEnumerator LifeSliderOnHeal(float value)
        {
            float currentValue = value;
            Debug.Log(currentValue + " " + maxValueOnHeal);
            while (currentValue < maxValueOnHeal)
            {
                if (timeToHealDelay<Time.time)
                {
                    currentValue += Time.fixedDeltaTime * speedSecondLife;
                }

                Debug.Log("CurrValue " + currentValue);
                lifeSliderCurrentHealth.value = currentValue / LevelManager.Instance.PlayerScript.MaxHealth;
                // lifeSliderCurrentHealth.value = currentValue;
                yield return new WaitForFixedUpdate();
            }
            currentValue = maxValueOnHeal / LevelManager.Instance.PlayerScript.MaxHealth;
            lifeSliderCurrentHealth.value = currentValue;
            _coroutineHeal = null;
            yield return null;
        }
    }
}