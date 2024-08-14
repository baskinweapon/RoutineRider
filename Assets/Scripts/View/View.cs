using System;
using UnityEngine;


public class View : MonoBehaviour {
    
    public void ActivateView() {
        gameObject.SetActive(true);
    }
    
    public void DeactivateView() {
        gameObject.SetActive(false);
    }
    
    
}
