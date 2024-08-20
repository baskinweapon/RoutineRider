using System;
using DefaultNamespace;
using UnityEngine;
using Zenject;

public class Main : MonoBehaviour {
    
    [SerializeField] private TaskController taskController;
    [Inject]
    private void Construct(TaskController _taskController) {
        taskController = _taskController;
    }
    
    private void Start() {
        
    }
    
    
}

