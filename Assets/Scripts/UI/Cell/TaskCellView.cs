using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public class TaskCellView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private Image icon;
    
    private TaskController taskController;
    
    public void Construct(TaskController taskController) {
        this.taskController = taskController;
    }
    
    private Task task;
    public void Init(Task task) {
        this.task = task;
        
        this.title.text = task.taskName;
        this.description.text = task.description;
        this.time.text = task.timeInMinutes.ToString();
    }

    private bool _isDone;
    private Coroutine _coroutine;
    public void DoneClick(bool isDone) {
        _isDone = isDone;
        if (_coroutine != null) return;
        _coroutine = StartCoroutine(WaitAndDestroy());
    }
    
    IEnumerator WaitAndDestroy() {
        yield return new WaitForSeconds(1f);
        
        if (_isDone) {
            taskController.CompleteTask(task);
            Destroy(gameObject);
        }
        
    }
    
    public void OnViewClick() {
        Debug.Log("Clicked");
    }

    public void DeleteClick() {
        taskController.DeleteTask(task);
        
        Destroy(gameObject);
    }
}
