using Artifacts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIArtifactCard : MonoBehaviour
    {
        public RawImage image;
        public TextMeshProUGUI titleName;
        public TextMeshProUGUI[] modifiersText;
        public Button equipButton;
        public Button closeButton;
        private int _index;
        public void ReceiveData(ArtifactsScriptableObject artifact,int indexId, bool buttonEquip = true)
        {
            gameObject.SetActive(true);
            _index = indexId;
            image.texture = artifact.model2d;
            titleName.text = artifact.artifactName;
            for (int i = 0; i < artifact.modifiers.Length; i++)
            {
                var text = $"{artifact.modifiers[i].value} {artifact.modifiers[i].type}";
                modifiersText[i].text = text;
                modifiersText[i].gameObject.SetActive(true);
            }

            if (buttonEquip)
            {
                equipButton.onClick.AddListener(CallEquipArtifact);
                equipButton.gameObject.SetActive(true);
                closeButton.gameObject.SetActive(false);
            }
            else
            {
                equipButton.gameObject.SetActive(false);
                closeButton.gameObject.SetActive(true);
                closeButton.onClick.AddListener(Close);
            }
        }

        private void CallEquipArtifact()
        {
            UiManager.instance.EquipArtifact(_index);
            UiManager.instance.ShowArtifactInventoryBlacksmith();
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }
    
    }
}
