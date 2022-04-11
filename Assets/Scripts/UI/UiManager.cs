using System;
using System.Collections;
using Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Attribute = Attributes.Attribute;

namespace DefaultNamespace.UI
{
    public class UiManager : Singleton<UiManager>
    {
        [System.Serializable]
        public class AttributesUI : IComparable
        {
            public AttributeType type;
            public TextMeshProUGUI text;
            public string suffix;

            public void ChangeText(int currentValue)
            {
                text.text = $"{suffix}: {currentValue}";
            }

            public int CompareTo(object obj)
            {
                return (int) type - (int) ((AttributesUI) obj)!.type;
            }
        }
        public AttributesUI[] attributesMax;
        public AttributesUI[] attributesCurrent;
        public Image[] slots;

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

        public void UpdateMaxValueAttribute(AttributeType type, int value)
        {
            attributesMax[(int) type].ChangeText(value);
        }
        public void UpdateCurrentValueAttribute(AttributeType type, int value)
        {
            attributesCurrent[(int) type].ChangeText(value);
        }
    }
}