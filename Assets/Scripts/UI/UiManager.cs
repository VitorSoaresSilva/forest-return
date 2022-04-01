using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class UiManager : MonoBehaviour
    {
        public TextMeshProUGUI health;
        public TextMeshProUGUI damage;
        public TextMeshProUGUI trueDamage;
        public TextMeshProUGUI stamina;
        public TextMeshProUGUI mana;

        public Image[] slots;

        public void ActiveSlots(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                slots[i].gameObject.SetActive(true);
            }
        }
    }
}