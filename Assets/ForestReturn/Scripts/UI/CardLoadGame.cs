using TMPro;
using UnityEngine;

namespace ForestReturn.Scripts.UI
{
    public class CardLoadGame : MonoBehaviour
    {
        public TextMeshProUGUI lastSaveText;
        public TextMeshProUGUI activeText;

        public void Init(string saveText)
        {
            Debug.Log(saveText);
            lastSaveText.text = saveText;
        }

        public void SetState(bool state)
        {
            activeText.gameObject.SetActive(state);
        }
    }
}