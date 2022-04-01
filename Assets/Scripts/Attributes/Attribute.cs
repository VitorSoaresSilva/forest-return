using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Attributes
{
    [Serializable]
    public struct Attribute
    {
        public int BaseValue { get; set; }
        public int CurrentValue { get; set; }
        private List<int> _modifiers;
        public UnityEvent changedValue;
        public Attribute(int baseValue)
        {
            BaseValue = baseValue;
            CurrentValue = baseValue;
            _modifiers = new List<int>();
            changedValue = new UnityEvent();
            UpdateValue();
        }
        private void UpdateValue()
        {
            CurrentValue = Mathf.Max(BaseValue + _modifiers.Sum(),0);
            changedValue?.Invoke();
        }

        public void AddModifier(int newModifier)
        {
            if (newModifier == 0) return;
            _modifiers.Add(newModifier);
            UpdateValue();
        }

        public void RemoveModifier(int newModifier)
        {
            if (newModifier == 0) return;
            _modifiers.Remove(newModifier);
            UpdateValue();
        }
    }
}