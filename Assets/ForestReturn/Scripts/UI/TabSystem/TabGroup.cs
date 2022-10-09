    using System;
    using System.Collections.Generic;
using UnityEngine;

namespace ForestReturn.UI.Scripts
{
    public class TabGroup : MonoBehaviour
    {
        public List<ForestReturn.Scripts.UI.TabSystem.TabButton> tabButtons;
        public ForestReturn.Scripts.UI.TabSystem.TabButton selectedTab;
        public List<GameObject> objectsToSwap;

        private void Start()
        {
            ResetTabs();
            if(selectedTab != null)
            {
                OnTabSelected(selectedTab);
            }
        }

        public void Subscribe(ForestReturn.Scripts.UI.TabSystem.TabButton button)
        {
            tabButtons ??= new List<ForestReturn.Scripts.UI.TabSystem.TabButton>();
            tabButtons.Add(button);
        }

        public void OnTabEnter(ForestReturn.Scripts.UI.TabSystem.TabButton button)
        {
            ResetTabs();
            if (selectedTab == null || button != selectedTab)
            { 
                button.background.sprite = button.tabHover;
            }
        }
        public void OnTabExit(ForestReturn.Scripts.UI.TabSystem.TabButton button)
        {
            ResetTabs();
        }
        public void OnTabSelected(ForestReturn.Scripts.UI.TabSystem.TabButton button)
        {
            if (selectedTab != null)
            {
                selectedTab.Deselect();
            }
            
            selectedTab = button;
            selectedTab.Select();
            ResetTabs();
            button.background.sprite = button.tabActive;
            int index = button.transform.GetSiblingIndex();
            for (int i = 0; i < objectsToSwap.Count; i++)
            {
                objectsToSwap[i].SetActive(i == index);
            }
        }

        public void ResetTabs()
        {
            foreach (ForestReturn.Scripts.UI.TabSystem.TabButton tabButton in tabButtons)
            {
                if(selectedTab != null && tabButton == selectedTab) continue;
                tabButton.background.sprite = tabButton.tabIdle;
            }
        }
    
    
    }
}
