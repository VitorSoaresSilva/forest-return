using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ForestReturn.Scripts.UI
{
    public class CardLoadGame : MonoBehaviour
    {
        public TextMeshProUGUI lastSaveText;
        public TextMeshProUGUI activeText;

        public Sprite Empty, EmptyHover, Standard, StandardHover;
        public Image cardImage;
        public Button button;
        
        public void Init(string saveText, bool state)
        {
            SpriteState spriteState = new SpriteState();
            lastSaveText.text = saveText;
            if (state)
            {
                spriteState.highlightedSprite = StandardHover;
                cardImage.sprite = Standard;
            }
            else
            {
                spriteState.highlightedSprite = EmptyHover;
                cardImage.sprite = Empty;
            }
            button.spriteState = spriteState;
        }

        public void SetState(bool state)
        {
            activeText.gameObject.SetActive(state);
        }
        
    }
}