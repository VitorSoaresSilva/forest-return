using System;
using ForestReturn.Scripts.Inventory;
using TMPro;
using UnityEngine;

namespace ForestReturn.Scripts.UI
{
    public class ItemCollectedAlert : MonoBehaviour
    {
        public float timeOfLive;
        private float _timeToDie;
        public TextMeshProUGUI text;
        private void Start()
        {
            _timeToDie = Time.time + timeOfLive;
        }

        private void Update()
        {
            if (_timeToDie < Time.time)
            {
                Destroy(gameObject);
            }
        }

        public void SetText(ItemCollectedData itemCollectedData)
        {

            if (itemCollectedData.CurrentAmount > 0)
            {
                text.text = $"+{itemCollectedData.CollectedAmount} {itemCollectedData.Item.itemName} (x{itemCollectedData.CurrentAmount})";
            }
            else
            {
                text.text = $"+{itemCollectedData.CollectedAmount} {itemCollectedData.Item.itemName}";
            }
        }
    }
}