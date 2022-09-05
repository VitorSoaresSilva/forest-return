using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction
{
    [CreateAssetMenu(fileName = "new Parameter", menuName = "Parameter", order = 0)]
    public class ParameterObject : ScriptableObject
    {
        public string parameterName;
        public string description;
    }
}