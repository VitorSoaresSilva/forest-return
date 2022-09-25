using TMPro;
using UnityEngine;

namespace ForestReturn.Scripts.PlayerAction.UI
{
    public class CardLoadGame : MonoBehaviour
    {
        public TextMeshProUGUI lastSaveText;
        public TextMeshProUGUI activeText;
        
        public void Init()
        {
            
        }

        public void Init(string saveText)
        {
            lastSaveText.text = saveText;
        }

        public void SetState(bool state)
        {
            activeText.gameObject.SetActive(state);
        }
    }
}