using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class GameSettings {
    [SerializeField] private Data data;
    
    public void Init() {
        data ??= new Data {
            tasks = new List<Task>(),
            todayTasks = new List<Task>(),
            completedTasks = new List<Task>(),
            completedTodayTasks = new List<Task>()
        };
        
        // clear today tasks if it's a new day
        if (IsNewDay()) {
            SetNewDay();
        }

        data.lastDay = DateTime.Today.ToString("MM/dd/yyyy");
    }
    
    public void CompleteTask(Task task) {
        data.completedTodayTasks.Add(task);
        if (data.todayTasks.Contains(task))
            data.todayTasks.Remove(data.todayTasks.First(task1 => task1.taskName == task.taskName));
        data.completedTasks.Add(task);
    }
    
    // getters
    public List<Task> GetTasks() => data.tasks;
    public List<Task> GetTodayTasks() => data.todayTasks;
    public List<Task> GetCompletedTasks() => data.completedTasks;
    public List<Task> GetCompletedTodayTasks() => data.completedTodayTasks;
    
    private bool IsNewDay() {
        if (DateTime.TryParse(data.lastDay, out var lastDay)) return DateTime.Today != lastDay;
        return true;
    }
    
    public void SetNewDay() {
        data.completedTodayTasks.Clear();
        data.todayTasks.Clear();
        data.lastDay = DateTime.Today.ToString("MM/dd/yyyy");
    }
    
    public void Add(Task task) {
        task.date = DateTime.Now.ToString("MM/dd/yyyy");
        data.tasks.Add(task);
        data.todayTasks.Add(task);
    }
    
    public void AddTask(Task task) {
        if (!data.tasks.Contains(task)) {
            data.tasks.Add(task);
        }
        data.todayTasks.Add(task);
    }
    
    public void AddCompletedTask(Task task) {
        data.completedTasks.Add(task);
    }
    
    public void DeleteTask(Task task) {
        data.tasks.Remove(task);
        data.todayTasks.Remove(task);
        data.completedTasks.Remove(task);
        data.completedTodayTasks.Remove(task);
    }
}

[Serializable]
public class Data {
    public string lastDay;
    
    public List<Task> tasks = new List<Task>();
    public List<Task> todayTasks = new List<Task>();
    public List<Task> completedTasks = new List<Task>();
    public List<Task> completedTodayTasks = new List<Task>();
    
    public int totalTask => tasks.Count;
    public int totalCompletedTask => completedTasks.Count;
    public int totalCompletedTodayTask => completedTodayTasks.Count;
}

[Serializable]
public class Task {
    public string taskName;
    public string description;
    public int timeInMinutes;
    public int iconID;
    public bool isCompleted;
    public string date;
    
    public Task(string taskName, string description, int timeInMinutes) {
        isCompleted = false;
        this.taskName = taskName;
        this.timeInMinutes = timeInMinutes;
        this.description = description;
    }
}



