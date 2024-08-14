using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public class TemplateCellView : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI emoji;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private Button addTaskButton;

    public Action OnAddTask;
    
    private TemplateController _templateController;
    private TaskController _taskController;
    public void Construct(TemplateController templateController, TaskController taskController) {
        _templateController = templateController;
        this._taskController = taskController;
    }

    private void OnEnable() {
        addTaskButton.onClick.AddListener(AddClick);
    }
    
    private void OnDisable() {
        addTaskButton.onClick.RemoveListener(AddClick);
    }

    public void Set(string title, string description, int time) {
        this.title.text = title;
        // this.description.text = description;
        this.time.text = time.ToString();
    }

    private void AddClick() {
        var newTask = new Task(title.text, "", Convert.ToInt32(time.text));
        _taskController.CreateTask(newTask);
        OnAddTask?.Invoke();
    }
}
