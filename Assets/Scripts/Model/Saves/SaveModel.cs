using DefaultNamespace;
using UnityEngine;
using Zenject;

public class SaveModel : MonoBehaviour {
    [SerializeField] public SettingsAsset data;
    
    private TaskController taskController;
    
    [Inject]
    private void Construct(TaskController taskController) {
        this.taskController = taskController;
    }
    
    public void OnEnable() {
        Init();
        
        taskController.OnTaskCreated += SaveNewTask;
        taskController.OnTaskCompleted += SaveCompletedTask;
        taskController.OnTaskDeleted += SaveDeletedTask;
    }
    
    private void Init() {
        data.LoadFromFile();
        data.serializable.Init();
    }
    
    // saves
    void SaveNewTask(Task task) => data.serializable.Add(task);
    void SaveCompletedTask(Task task) => data.serializable.CompleteTask(task);
    void SaveDeletedTask(Task task) => data.serializable.DeleteTask(task);
    
    private void OnApplicationQuit() {
        data.SaveToFile();
    }
    
    private void OnApplicationPause(bool pauseStatus) {
        if (pauseStatus) data.SaveToFile();
    }
        
    public void OnDisable() {
        taskController.OnTaskCreated -= SaveNewTask;
        taskController.OnTaskCompleted -= SaveCompletedTask;
        taskController.OnTaskDeleted -= SaveDeletedTask;
    }
}
