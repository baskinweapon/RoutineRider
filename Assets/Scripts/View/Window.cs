using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Window : View {
    [Header("Window")]
    [SerializeField] private SwitchButton _button;
    [SerializeField] private Image _blockRaycaster;
    
    private RectTransform _content;

    private void OnEnable() {
        _content = GetComponent<RectTransform>();
    }

    public void SwitchState(bool state, bool isImmediate = false) {
        if (_button) _button.SwitchState(state);
        if (state == false) DeactivateView(isImmediate);
        else ActivateView();
    }

    public override void ActivateView() {
        base.ActivateView();
        _blockRaycaster.DOFade(0.61f, 0.61f);
        _content.DOAnchorPos(new Vector2(0, 0), 0.61f);
    }
    
    public override void DeactivateView(bool isImmediate = false) {
        base.DeactivateView(isImmediate);
        if (isImmediate) {
            Inactive();
            return;
        }
        Invoke(nameof(Inactive), 0.32f);
        _blockRaycaster.DOFade(0, 0.32f);
        _content.DOAnchorPos(new Vector2(0, -600), 0.32f);
    }
}
