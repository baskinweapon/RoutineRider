using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    private List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    private GameObject prefabs;
    
    void Awake() {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++) {
            tmp = Instantiate(objectToPool, transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    
    public GameObject GetPooledObject() {
        for(int i = 0; i < amountToPool; i++) {
            if(!pooledObjects[i].activeInHierarchy) {
                return pooledObjects[i];
            }
        }
        var tmp = Instantiate(objectToPool, transform);
        pooledObjects.Add(tmp);
        return tmp;
    }
}
