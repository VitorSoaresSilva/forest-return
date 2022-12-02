using UnityEngine;
using UnityEngine.EventSystems;

namespace ForestReturn.Scripts.UI
{
    public enum MaskType
    {
        Life,
        Mana
    }
    public class MaskUI : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        public CraftsmanStore craftsmanStore;
        public GameObject selectedImage;
        public MaskType maskType;
        
        public void OnSelect(BaseEventData eventData)
        {
            switch (maskType)
            {
                case MaskType.Mana:
                    craftsmanStore.EquipManaMask();
                    break;
                case MaskType.Life:
                    craftsmanStore.EquipLifeMask();
                    break;
            }

            selectedImage.SetActive(true);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            selectedImage.SetActive(false);
        }
    }
}