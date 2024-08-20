using System;
using TMPro;
using UI.Calendar;
using UnityEngine;
using UnityEngine.UI;

public class DayCellView : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI taskCountText;
    [SerializeField] private TextMeshProUGUI infoText;

    private TaskController taskController;
    private CalendarController calendarController;
    public void Init(TaskController _taskController, CalendarController _calendarController, DateTime date) {
        taskController = _taskController;
        calendarController = _calendarController;
        
        if (date == DateTime.Today) {
            taskController.OnTaskCompleted += AddTaskCount;
        }
    }
    
    public void SetDay(int day) {
        dayText.text = day.ToString();
    }
    
    private int _taskCount;
    public void SetTaskCount(int count) {
        _taskCount = count;
        taskCountText.text = count.ToString();
        CalculateColorTaskCount();
    }

    public void AddTaskCount(Task task) {
        taskCountText.text = (++_taskCount).ToString();
        CalculateColorTaskCount();
    }
    
    private void CalculateColorTaskCount() {
        background.color = calendarController.gradient.Evaluate((float)_taskCount / 12);
        infoText.text = calendarController.Achievements[Mathf.Clamp((int)(_taskCount / 3), 0, 4)];
    }
}
