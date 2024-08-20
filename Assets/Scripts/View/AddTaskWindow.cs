using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AddTaskWindow : Window {
    [SerializeField] private TMP_InputField _nameinputField;
    [SerializeField] private TMP_InputField _descriptionInputField;
    [SerializeField] private TMP_InputField _timeInputField;
    
    private TaskController taskController;

    [Inject]
    private void Construct(TaskController _taskController) {
        taskController = _taskController;
    }
    
    public void AddTaskClick() {
        string taskName = _nameinputField.text;
        string taskDescription = _descriptionInputField.text;
        string taskTime = _timeInputField.text;
        if (string.IsNullOrEmpty(taskName)) return;
        
        var task = new Task(taskName, taskDescription, Convert.ToInt32(taskTime));
        taskController.CreateTask(task);
        
        DeactivateView();
    }
}
