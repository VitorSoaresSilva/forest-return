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

            public void ChangeText(int currentValue, int maxValue)
            {
                if (text != null)
                {
                    // text.text = $"{prefix}{currentValue}{suffix}";
                    text.text = $"{currentValue}/{maxValue}";
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
        
        [Header("Weapon Card")]
        public GameObject root;
        public RawImage image;
        public TextMeshProUGUI title;
        public UIArtifactCard[] uiArtifacts;

        protected override void Awake()
        {
            base.Awake();
            Array.Sort(attributesMax);
        }

        public void UpdateMaxValueAttribute(Attribute attribute)
        {
            if (attributesMax.Length > (int)attribute.Type)
            {
                attributesMax[(int) attribute.Type].ChangeText(attribute.CurrentValue,attribute.MaxValue);
            }
        }
        public void UpdateCurrentValueAttribute(Attribute attribute)
        {
            if (attributesCurrent.Length > (int)attribute.Type)
            {
                attributesCurrent[(int) attribute.Type].ChangeText(attribute.CurrentValue,attribute.MaxValue);
                attributesCurrent[(int) attribute.Type].ChangeSlider(attribute.CurrentValue,attribute.MaxValue);
            }
        }

        public void ShowItem(Weapon weapon)
        {
            if (root.activeSelf) return;
            root.SetActive(true);
            if (weapon.weaponConfig.image != null)
            {
                image.texture = weapon.weaponConfig.image;
            }
            title.text = weapon.weaponConfig.weaponName;
            for (int i = 0; i < weapon.artifacts.Length; i++)
            {
                if (weapon.artifacts[i] != null)
                {
                    uiArtifacts[i].SetValues(weapon.artifacts[i]);
                }
            }
        }
        public void Hide()
        {
            root.SetActive(false);
            image.texture = null;
            title.text = String.Empty;
            foreach (var uiArtifact in uiArtifacts)
            {
                uiArtifact.ResetValues();
            }
        }
    }
}