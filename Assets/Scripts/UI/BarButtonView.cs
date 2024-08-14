using System;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class BarButtonView : View {
    public Sprite iconSprite;
    public string title;
    
    public Tabs tabType;
    [SerializeField] private Image[] images;
    [SerializeField] private TextMeshProUGUI[] titles;
    
    public Action<Tabs> OnClick;
    
    [SerializeField] private GameObject[] states;

    private void OnEnable() {
        foreach (var img in images) {
            img.sprite = iconSprite;
        }

        foreach (var t in titles) {
            t.text = title;
        }
    }

    public void Click() {
        SwitchState(true);
        OnClick?.Invoke(tabType);
    }

    public void SwitchState(bool state) {
        if (state) {
            states[0].SetActive(true);
            states[1].SetActive(false);
        } else {
            states[0].SetActive(false);
            states[1].SetActive(true);
        }
    }
}
