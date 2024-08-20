using UnityEngine;

public class View : MonoBehaviour {
     
    public virtual void ActivateView() {
        gameObject.SetActive(true);
    }
    
    public virtual void DeactivateView(bool isImmediate = false) {
        
    }

    protected void Inactive() {
        gameObject.SetActive(false);
    }
    
    
}
