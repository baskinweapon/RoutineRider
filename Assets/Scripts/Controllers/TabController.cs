using System;
using UnityEngine;

[Serializable]
public struct Tab {
    public GameObject tab;
    public Tabs tabType;
}

namespace DefaultNamespace {
    
    public class TabController : MonoBehaviour {
        public Tab[] tabs;
        
        [SerializeField] private TabBarView tabBarView;
        
        
        private void Awake() {
            foreach (var tab in tabs) {
                tab.tab.SetActive(false);
            }
            
            tabBarView.OnTabChanged += SwitchTab;
        }
        
        public void SwitchTab(Tabs tab) {
            foreach (var t in tabs) {
                t.tab.SetActive(t.tabType == tab);
            }
        }
    }
}