using System;
using DefaultNamespace;
using UnityEngine;
using Zenject;

public class TodoView : View {
    public RectTransform content;
    
    [SerializeField] private TaskCellView taskCellPrefab;
    private TaskController taskController;
    
    [Inject]
    private void Construct(TaskController taskController) {
        this.taskController = taskController;
    }

    private void Awake() {
        foreach (var task in taskController.GetTodayTasks()) {
            OnTaskCreated(task);
        }
    }

    private void OnEnable() {
        taskController.OnTaskCreated += OnTaskCreated;
    }
    
    private void OnDisable() {
        taskController.OnTaskCreated -= OnTaskCreated;
    }
    
    private void OnTaskCreated(Task task) {
        var taskCell = Instantiate(taskCellPrefab, content);
        taskCell.Construct(taskController);
        taskCell.Init(task);
    }
    
}
