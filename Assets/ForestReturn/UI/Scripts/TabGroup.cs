    using System;
    using System.Collections.Generic;
using UnityEngine;

namespace ForestReturn.UI.Scripts
{
    public class TabGroup : MonoBehaviour
    {
        public List<TabButton> tabButtons;
        public TabButton selectedTab;
        public List<GameObject> objectsToSwap;

        private void Start()
        {
            ResetTabs();
            if(selectedTab != null)
            {
                OnTabSelected(selectedTab);
            }
        }

        public void Subscribe(TabButton button)
        {
            tabButtons ??= new List<TabButton>();
            tabButtons.Add(button);
        }

        public void OnTabEnter(TabButton button)
        {
            ResetTabs();
            if (selectedTab == null || button != selectedTab)
            { 
                button.background.sprite = button.tabHover;
            }
        }
        public void OnTabExit(TabButton button)
        {
            ResetTabs();
        }
        public void OnTabSelected(TabButton button)
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
            foreach (TabButton tabButton in tabButtons)
            {
                if(selectedTab != null && tabButton == selectedTab) continue;
                tabButton.background.sprite = tabButton.tabIdle;
            }
        }
    
    
    }
}
