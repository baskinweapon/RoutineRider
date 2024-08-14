using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TMPro;
using UI.Cell;
using UnityEngine;
using Zenject;

public class FinishTasksView : Window {
    public RectTransform content;
    
    [SerializeField] private TaskCellMiniatureView taskCellPrefab;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private TaskController taskController;
    [Inject]
    private void Construct(TaskController taskController) {
        this.taskController = taskController;
    }

    private void Awake() {
        foreach (var task in taskController.GetCompletedTodayTasks()) {
            OnTaskComplete(task);
        }
        
        SetScoreText(null);
        taskController.OnTaskCompleted += OnTaskComplete;
        
        taskController.OnTaskCreated += SetScoreText;
        taskController.OnTaskDeleted += SetScoreText;
        taskController.OnTaskCompleted += SetScoreText;
    }
    
    private void SetScoreText(Task task) {
        scoreText.text = taskController.GetCompletedTodayTasks().Count + " / " + (taskController.GetTodayTasks().Count + taskController.GetCompletedTodayTasks().Count());
    }
        
    private void OnTaskComplete(Task task) {
        var taskCell = Instantiate(taskCellPrefab, content);
        
        taskCell.Init(task);
    }
}
