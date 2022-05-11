using UnityEngine;
using UnityEngine.UI;

namespace Interactable
{
    [CreateAssetMenu(fileName = "Keys", menuName = "ScriptableObject/Key", order = 0)]
    public class KeysScriptableObject : ScriptableObject
    {
        public string description;
        public Image image;
        public GameObject prefab;
    }
}