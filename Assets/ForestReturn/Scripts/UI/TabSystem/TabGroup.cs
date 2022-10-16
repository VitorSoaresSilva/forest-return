using System.Collections.Generic;
using _Developers.Vitor.Scripts.Utilities;
using UnityEngine;

namespace ForestReturn.Scripts.UI.TabSystem
{
    public class TabGroup : Singleton<TabGroup>
    {
        public List<MenuTabButton> tabButtons;
        public MenuTabButton selectedMenuTab;
        public List<GameObject> objectsToSwap;
        private int _currentSelectedIndex = -1;

        private void Start()
        {
            ResetTabs();
            if(selectedMenuTab != null)
            {
                OnTabSelected(selectedMenuTab);
            }
            
        }

        public void Subscribe(MenuTabButton button)
        {
            tabButtons ??= new List<MenuTabButton>();
            tabButtons.Add(button);
            if (_currentSelectedIndex == -1)
            {
                OnTabSelected(button);
            }
        }

        public void OnTabEnter(MenuTabButton button)
        {
            ResetTabs();
            if (selectedMenuTab == null || button != selectedMenuTab)
            { 
                button.background.sprite = button.tabHover;
            }
        }
        public void OnTabExit(MenuTabButton button)
        {
            ResetTabs();
        }
        public void OnTabSelected(MenuTabButton button)
        {
            if (selectedMenuTab != null)
            {
                selectedMenuTab.Deselect();
            }
            _currentSelectedIndex = tabButtons.FindIndex(tabButton => button == tabButton);
            selectedMenuTab = button;
            selectedMenuTab.Select();
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
            if (tabButtons == null || tabButtons.Count == 0) return;
            
            foreach (MenuTabButton tabButton in tabButtons)
            {
                if(selectedMenuTab != null && tabButton == selectedMenuTab) continue;
                tabButton.background.sprite = tabButton.tabIdle;
            }
        }

        public void ChangeTab(int direction)
        {
            var desiredIndex = (_currentSelectedIndex + direction + tabButtons.Count) % tabButtons.Count;
            
            // if (direction == 1)
            // {
            //     desiredIndex = (_currentSelectedIndex + direction) % tabButtons.Count;
            // }
            // else if(direction == -1)
            // {
            //     if (_currentSelectedIndex == 0)
            //     {
            //         desiredIndex = tabButtons.Count - 1;
            //     }
            //     else
            //     {
            //         desiredIndex = _currentSelectedIndex + direction;
            //     }
            // }
            OnTabSelected(tabButtons[desiredIndex]);
        }
    
    
    }
}
