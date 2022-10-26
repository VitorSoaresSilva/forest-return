using ForestReturn.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace ForestReturn.Scripts.UI
{
    public class Hud : MonoBehaviour
    {
        public Slider lifeSliderFront;
        public Slider lifeSliderBack;
        

        private void Start()
        {
            lifeSliderFront.value = (float)LevelManager.Instance.PlayerScript.CurrentHealth /
                                    (float)LevelManager.Instance.PlayerScript.MaxHealth;
            LevelManager.Instance.PlayerScript.OnHurt += PlayerScriptOnOnHurt;
            LevelManager.Instance.PlayerScript.OnHealthHealed += PlayerScriptOnOnHealthHealed;
        }

        private void OnDisable()
        {

        }

        private void PlayerScriptOnOnHealthHealed()
        {
            lifeSliderFront.value = (float)LevelManager.Instance.PlayerScript.CurrentHealth /
                                    (float)LevelManager.Instance.PlayerScript.MaxHealth;
        }

        private void PlayerScriptOnOnHurt()
        {
            lifeSliderFront.value = (float)LevelManager.Instance.PlayerScript.CurrentHealth /
                                    (float)LevelManager.Instance.PlayerScript.MaxHealth;
        }
    }
}