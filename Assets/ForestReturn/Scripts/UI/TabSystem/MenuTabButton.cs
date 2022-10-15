using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ForestReturn.Scripts.UI.TabSystem
{
    [RequireComponent(typeof(Image))]
    public class MenuTabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        public TabGroup tabGroup;
        public Image background;
        public Sprite tabIdle;
        public Sprite tabHover;
        public Sprite tabActive;

        public UnityEvent onTabSelected;
        public UnityEvent onTabDeselected;

        private void Start()
        {
            background = GetComponent<Image>();
            tabGroup.Subscribe(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
           tabGroup.OnTabSelected(this);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            tabGroup.OnTabEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tabGroup.OnTabExit(this);
        }

        public void Select()
        {
            onTabSelected?.Invoke();
        }

        public void Deselect()
        {
            onTabDeselected?.Invoke();
        }
    }
}
