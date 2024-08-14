using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Zenject;

public class TemplateWindow : Window
{
    [SerializeField] private RectTransform content;
    [SerializeField] TemplateCellView _templateCellView;
    
    private TemplateController _templateController;
    private TaskController _taskController;
    [Inject]
    public void Construct(TemplateController templateController, TaskController taskController) {
        _templateController = templateController;
        _taskController = taskController;
    }
    
    private void Start() {
        foreach (var task in _taskController.GetTasks()) {
            var cell = Instantiate(_templateCellView, content);
            cell.Construct(_templateController, _taskController);
            cell.Set(task.taskName, task.description, task.timeInMinutes);
        }
    }
    
    
}
