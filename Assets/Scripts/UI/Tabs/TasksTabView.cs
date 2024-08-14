using DefaultNamespace;
using UnityEngine;
using Zenject;

public class TasksTabView : View
{
    private TaskController taskController;
    
    [Inject]
    private void Construct(TaskController taskController) {
        this.taskController = taskController;
    }
    
    
}
