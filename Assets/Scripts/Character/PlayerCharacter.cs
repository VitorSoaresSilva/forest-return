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
                UiManager.Instance.UpdateMaxValueAttribute(attribute.Type, attributes[(int)attribute.Type].CurrentValue);
            }
            private void HandleAttributeCurrentValueChanged(Attribute attribute)
            {
                UiManager.Instance.UpdateCurrentValueAttribute(attribute.Type, attributes[(int)attribute.Type].CurrentValue);
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