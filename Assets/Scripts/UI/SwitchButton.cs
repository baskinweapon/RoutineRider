using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SwitchButton : MonoBehaviour
{
    [SerializeField] private GameObject[] states; 
    public UnityEvent<bool> OnClick;
    
    private Button _button;
    private void OnEnable() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Click);
        
        SwitchState(false);
    }

    private void OnDisable() {
        _button.onClick.RemoveListener(Click);
    }
    
    private bool _state;
    private void Click() {
        _state = !_state;
        SwitchState(_state);
        OnClick?.Invoke(_state);
    }

    public void SwitchState(bool state) {
        _state = state;
        if (state) {
            states[0].SetActive(true);
            states[1].SetActive(false);
        } else {
            states[0].SetActive(false);
            states[1].SetActive(true);
        }
    }
}
