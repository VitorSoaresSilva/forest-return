using System;
using System.Collections;
using Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Weapons;
using Attribute = Attributes.Attribute;

namespace UI
{
    public class UiManager : Singleton<UiManager>
    {
        [System.Serializable]
        public class AttributesUI : IComparable
        {
            public AttributeType type;
            public TextMeshProUGUI text;
            public string suffix;
            public string prefix;
            public Slider slider;

            public void ChangeText(int currentValue)
            {
                if (text != null)
                {
                    text.text = $"{prefix}{currentValue}{suffix}";
                }
            }

            public void ChangeSlider(int currentValue, int maxValue)
            {
                if (slider != null)
                {
                    slider.value = (float)currentValue / (float)maxValue;
                }
            }

            public int CompareTo(object obj)
            {
                return (int) type - (int) ((AttributesUI) obj)!.type;
            }
        }
        public AttributesUI[] attributesMax;
        public AttributesUI[] attributesCurrent;
        public Image[] slots;
        public ItemPopUp itemPopUp;

        protected override void Awake()
        {
            base.Awake();
            Array.Sort(attributesMax);
        }
        
        public void ActiveSlots(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                slots[i].gameObject.SetActive(true);
            }
        }

        public void UpdateMaxValueAttribute(Attribute attribute)
        {
            if (attributesMax.Length > (int)attribute.Type)
            {
                attributesMax[(int) attribute.Type].ChangeText(attribute.MaxValue);
            }
        }
        public void UpdateCurrentValueAttribute(Attribute attribute)
        {
            if (attributesCurrent.Length > (int)attribute.Type)
            {
                attributesCurrent[(int) attribute.Type].ChangeText(attribute.CurrentValue);
                attributesCurrent[(int) attribute.Type].ChangeSlider(attribute.CurrentValue,attribute.MaxValue);
            }
        }

        public void ShowItem(Weapon weapon)
        {
            itemPopUp.Show(weapon);
        }
    }
}