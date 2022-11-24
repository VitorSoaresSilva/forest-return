using TMPro;
using UnityEngine;

namespace ForestReturn.Scripts.UI
{
    public class DamageIndicatorManager : MonoBehaviour
    {
        [SerializeField] private GameObject damageIndicatorPrefab;


        public void Spawn(int damage)
        {
            GameObject damageGameObject = Instantiate(damageIndicatorPrefab, transform);
            TextMeshProUGUI text = damageGameObject.GetComponentInChildren<TextMeshProUGUI>();
            text.text = $"{damage}";
        }
    }
}