using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DefaultNamespace {
    public class TaskController : MonoBehaviour {
        public Action<Task> OnTaskCreated;
        public Action<Task> OnTaskCompleted;
        public Action<Task> OnTaskDeleted;
        
        [Inject] private SaveModel saveModel;
        
        public void CreateTask(Task task) {
            OnTaskCreated?.Invoke(task);
        }
        
        public void CompleteTask(Task task) {
            OnTaskCompleted?.Invoke(task);
        }
        
        public void DeleteTask(Task task) {
            OnTaskDeleted?.Invoke(task);
        }
        
        public List<Task> GetTasks() => saveModel.data.serializable.GetTasks();
        public List<Task> GetTodayTasks() => saveModel.data.serializable.GetTodayTasks();
        public List<Task> GetCompletedTasks() => saveModel.data.serializable.GetCompletedTasks();
        public List<Task> GetCompletedTodayTasks() => saveModel.data.serializable.GetCompletedTodayTasks();

        public List<Task> GetCompletedTasksByDate(DateTime date) {
            return saveModel.data.serializable.GetCompletedTasks().FindAll(task => task.date == date.ToString("MM/dd/yyyy"));   
        }
        
    }
}