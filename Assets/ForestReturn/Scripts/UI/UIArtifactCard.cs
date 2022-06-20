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

        public void ReceiveData(ArtifactsScriptableObject artifact,int indexId)
        {
            gameObject.SetActive(true);
            image.texture = artifact.model2d;
            titleName.text = artifact.artifactName;
            for (int i = 0; i < artifact.modifiers.Length; i++)
            {
                var text = $"{artifact.modifiers[i].value} {artifact.modifiers[i].type}";
                modifiersText[i].text = text;
                modifiersText[i].gameObject.SetActive(true);
            }
        }
        public void ResetValues()
        {
            gameObject.SetActive(false);
            image.texture = null;
            titleName.text = string.Empty;
            foreach (var textMeshProUGUI in modifiersText)
            {
                textMeshProUGUI.text = string.Empty;
                textMeshProUGUI.gameObject.SetActive(false);
            }
        }
    
    }
}
