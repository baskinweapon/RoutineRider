using System;
using UnityEngine;
using UnityEngine.UI;

public class Window : View {
    [SerializeField] private SwitchButton _button;
    
    public void SwitchState(bool state) {
        if (_button) _button.SwitchState(state);
        if (state == false) DeactivateView();
        else ActivateView();
    }
}
