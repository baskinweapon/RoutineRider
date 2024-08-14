using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowController : MonoBehaviour {
    public Window[] windows;
    
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
           foreach (var win in windows) {
               var rectTransform = win.GetComponent<RectTransform>();
               
               Vector2 localMousePosition = rectTransform.InverseTransformPoint(Input.mousePosition);
               if (win.gameObject.activeSelf && rectTransform.rect.Contains(localMousePosition))
               { } else if (win.gameObject.activeSelf) {
                   win.SwitchState(false);
               }
           }
        }
    }
    
    public static bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
