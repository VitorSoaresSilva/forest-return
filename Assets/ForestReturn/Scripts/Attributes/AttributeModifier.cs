using System;

namespace Attributes
{
    [Serializable]
    public struct AttributeModifier
    {
        public int value;
        public AttributeType type;
        public AttributeModifier(int value,AttributeType type)
        {
            this.value = value; 
            this.type = type;
        }
    }
}