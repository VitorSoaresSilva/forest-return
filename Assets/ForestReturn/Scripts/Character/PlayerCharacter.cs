using System;
using DefaultNamespace.UI;
using UnityEngine;
using Attribute = Attributes.Attribute;

namespace Character
{
    public class PlayerCharacter : BaseCharacter
    {
       protected override void Awake()
       {
           base.Awake();
           foreach (var attribute in attributes)
           {
               attribute.ChangedMaxValue += HandleAttributeMaxValueChanged;
               attribute.ChangedCurrentValue += HandleAttributeCurrentValueChanged;
               HandleAttributeMaxValueChanged(attribute);
               HandleAttributeCurrentValueChanged(attribute);
           }
       }

       #region  HandleChangeValues
            private void HandleAttributeMaxValueChanged(Attribute attribute)
            {
                Debug.Log(UiManager.instance.slots.Length);
                UiManager.instance.UpdateMaxValueAttribute(attribute.Type, attributes[(int)attribute.Type].MaxValue);
                // if (UiManager.Instance != null)
                // {
                // Debug.Log("HandleAttributeMaxValueChanged");
                // }
            }
            private void HandleAttributeCurrentValueChanged(Attribute attribute)
            {
                UiManager.instance.UpdateCurrentValueAttribute(attribute.Type, attributes[(int)attribute.Type].CurrentValue);
            }
        #endregion

        private void OnDestroy()
        {
            foreach (var attribute in attributes)
            {
                attribute.ChangedMaxValue -= HandleAttributeMaxValueChanged;
                attribute.ChangedCurrentValue -= HandleAttributeCurrentValueChanged;
            }
        }
    }
}