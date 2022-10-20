using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Developers.Vitor.Scripts.Attributes
{
        [Serializable]
        public class Attribute
        {
            public AttributeType Type { get; private set; }
            public delegate void ChangedValue(Attribute attribute);
            public ChangedValue ChangedMaxValue;
            public ChangedValue ChangedCurrentValue;
            private List<int> _modifiers;
            public int BaseValue { get; private set; }
            private int _maxValue;
            public int MaxValue
            {
                get => _maxValue;
                private set
                {
                    _maxValue = Mathf.Max(value,0);
                    ChangedMaxValue?.Invoke(this);                
                }
            }

            private int _currentValue;
            public int CurrentValue
            {
                get => _currentValue;
                set
                {
                    _currentValue = value;
                    // _currentValue = Mathf.Max(value,0);
                    // _currentValue = Mathf.Min(_currentValue, MaxValue);
                    ChangedCurrentValue?.Invoke(this);
                }
            }
            public Attribute(int baseValue, AttributeType type)
            {
                BaseValue = baseValue;
                MaxValue = baseValue;
                CurrentValue = baseValue;
                Type = type;
                _modifiers = new List<int>();
                UpdateMaxValue();
            }
        
            private void UpdateMaxValue()
            {
                int oldValue = MaxValue;
                MaxValue = BaseValue + _modifiers.Sum();
                CurrentValue += MaxValue - oldValue;
            }
            public void AddModifier(int newModifier)
            {
                if (newModifier == 0) return;
                _modifiers.Add(newModifier);
                UpdateMaxValue();
            }
            public void RemoveModifier(int newModifier)
            {
                if (newModifier == 0) return;
                _modifiers.Remove(newModifier);
                UpdateMaxValue();
            }
        }
    
}