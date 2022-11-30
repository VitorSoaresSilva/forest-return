using System;
using System.Collections;
using System.Linq;
using ForestReturn.Scripts.Inventory;
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
        public Slider cooldownVineSlider;
        public Animator hurtAnimator;
        private static readonly int HurtStringHash = Animator.StringToHash("Hurt");
        public float speedSecondLife = 2;
        public float timeSecondLife = 0.6f;
        public GameObject prefabItemCollected;
        public GameObject itemCollectedParent;
        private float _minValue;
        private float _maxValueOnHeal;
        private float _timeToSecondLifeDelay;
        private float _timeToHealDelay;
        private Coroutine _coroutineDamage;
        private Coroutine _coroutineHeal;
        public GameObject[] manaObjectsActive;
        public GameObject[] manaObjectsUsed;
        
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
            LevelManager.Instance.PlayerScript.OnNotEnoughMana += PlayerScriptOnOnNotEnoughMana;
            
            LevelManager.Instance.PlayerScript.OnVineSkillCoolDownChanged += PlayerScriptOnOnVineSkillCoolDownChanged;
            if(InventoryManager.InstanceExists)
            {
                InventoryManager.Instance.inventory.OnItemCollected += InventoryOnItemCollected;
            }
        }

        private void PlayerScriptOnOnVineSkillCoolDownChanged(float value)
        {
            cooldownVineSlider.value = value;
        }

        private void PlayerScriptOnOnNotEnoughMana()
        {
            Debug.Log("Not enough Mana");
            //TODO: Adicionar aviso para o player que nao tem mana
        }

        private void OnDestroy()
        {
            if (LevelManager.InstanceExists)
            {
                LevelManager.Instance.PlayerScript.OnHurt -= PlayerScriptOnOnHurt;
                LevelManager.Instance.PlayerScript.OnHealthHealed -= PlayerScriptOnOnHealthHealed;
                LevelManager.Instance.PlayerScript.OnLifeChanged -= UpdateHealthValue;
                LevelManager.Instance.PlayerScript.OnManaChanged -= UpdateManaValue;
                LevelManager.Instance.PlayerScript.OnNotEnoughMana -= PlayerScriptOnOnNotEnoughMana;
                LevelManager.Instance.PlayerScript.OnVineSkillCoolDownChanged -= PlayerScriptOnOnVineSkillCoolDownChanged;
            }
            if ( InventoryManager.InstanceExists)
            {
                InventoryManager.Instance.inventory.OnItemCollected -= InventoryOnItemCollected;
            }
        }


        private void PlayerScriptOnOnHealthHealed(int oldValue, int newValue)
        {
            if (_coroutineDamage != null)
            {
                StopCoroutine(_coroutineDamage);
            }
            _maxValueOnHeal = newValue;
            lifeSliderHeal.value =  _maxValueOnHeal / LevelManager.Instance.PlayerScript.MaxHealth;
            lifeSliderDamage.value = 0;
            _timeToHealDelay = Time.time + timeSecondLife;
            if (_coroutineHeal == null)
            {
                _coroutineHeal = StartCoroutine(LifeSliderOnHeal(oldValue));
            }

        }
        private void InventoryOnItemCollected(ItemCollectedData itemCollectedData)
        {
            if (prefabItemCollected == null || itemCollectedParent == null) return; // bug da unity
            var item = Instantiate(prefabItemCollected,itemCollectedParent.transform);
            if (itemCollectedParent != null)
            {
                item.transform.SetParent(itemCollectedParent.transform);
            }
            var itemCollectedAlert = item.GetComponent<ItemCollectedAlert>();
            itemCollectedAlert.SetText(itemCollectedData);
        }

        private void PlayerScriptOnOnHurt(int damageTaken)
        {
            hurtAnimator.SetTrigger(HurtStringHash);
            _minValue = LevelManager.Instance.PlayerScript.CurrentHealth;
            _timeToSecondLifeDelay = Time.time + timeSecondLife;

            if (_coroutineHeal != null)
            {
                StopCoroutine(_coroutineHeal);
                lifeSliderCurrentHealth.value = _minValue / LevelManager.Instance.PlayerScript.MaxHealth;
                lifeSliderHeal.value = 0;
            }
            
            if (_coroutineDamage == null)
            {
                _coroutineDamage = StartCoroutine(LifeSlider(LevelManager.Instance.PlayerScript.CurrentHealth + damageTaken));
            }

        }

        private void UpdateHealthValue()
        {
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
            int maxMana = LevelManager.Instance.PlayerScript.MaxMana;
            int currentMana = LevelManager.Instance.PlayerScript.CurrentMana;
/*
 * total 6
 * max 3
 * cur 3
 */
            
            for (int i = 0; i < manaObjectsActive.Length; i++)
            {
                manaObjectsActive[i].SetActive(false);
                manaObjectsUsed[i].SetActive(false);
                
                
                
                if (i < currentMana)
                {
                    manaObjectsActive[i].SetActive(true);
                }else if (i < maxMana)
                {
                    manaObjectsUsed[i].SetActive(true);
                }
            }
            
        }

        IEnumerator LifeSlider(float maxValue)
        {
            lifeSliderDamage.value = maxValue / LevelManager.Instance.PlayerScript.MaxHealth;
            float currentValue = maxValue;
            while (currentValue > _minValue)
            {
                if (_timeToSecondLifeDelay<Time.time)
                {
                    currentValue -= Time.fixedDeltaTime * speedSecondLife;
                }
                lifeSliderDamage.value = currentValue / LevelManager.Instance.PlayerScript.MaxHealth;
                yield return new WaitForFixedUpdate();
            }
            currentValue = _minValue / LevelManager.Instance.PlayerScript.MaxHealth;
            lifeSliderDamage.value = currentValue;
            _coroutineDamage = null;
            yield return null;
        }
        IEnumerator LifeSliderOnHeal(float value)
        {
            float currentValue = value;
            while (currentValue < _maxValueOnHeal)
            {
                if (_timeToHealDelay<Time.time)
                {
                    currentValue += Time.fixedDeltaTime * speedSecondLife;
                }
                lifeSliderCurrentHealth.value = currentValue / LevelManager.Instance.PlayerScript.MaxHealth;
                // lifeSliderCurrentHealth.value = currentValue;
                yield return new WaitForFixedUpdate();
            }
            currentValue = _maxValueOnHeal / LevelManager.Instance.PlayerScript.MaxHealth;
            lifeSliderCurrentHealth.value = currentValue;
            _coroutineHeal = null;
            yield return null;
        }


        public void UpdateCooldownVinesSlider(float value01)
        {
            cooldownVineSlider.value = value01;
        }
    }
}