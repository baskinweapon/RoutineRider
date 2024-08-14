using System;
using UnityEngine;

public enum Tabs {
    Tasks,
    Calendar,
    Dairy,
}

public class TabBarView : View {
    private Tabs currentTab;
    public Action<Tabs> OnTabChanged;
    
    [SerializeField] private BarButtonView[] buttons;
    
    private void OnEnable() {
        foreach (var button in buttons) {
            button.OnClick += SwitchTab;
        }
        
        SwitchTab(Tabs.Tasks);
    }

    public void SwitchTab(Tabs tab) {
        foreach (var but in buttons) {
            but.SwitchState(but.tabType == tab);
        }
        
        currentTab = tab;
        OnTabChanged?.Invoke(tab);
    }
}
