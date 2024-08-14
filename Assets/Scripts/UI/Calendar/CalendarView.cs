using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UI.Calendar;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

public class CalendarView : MonoBehaviour
{
    public GameObject dayPrefab; // Prefab for each day (Button or Text)
    [SerializeField] private GameObject emptyDayPrefab; // Empty placeholder for alignment
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform contentRect;
    [SerializeField] private MonthCell monthCellPrefab; // Prefab for each month cell
    
    private DateTime currentDate; // Current date to track which month/year is displayed
    private TaskController taskController;
    [Inject]
    private void Construct(TaskController _taskController) {
        taskController = _taskController;
    }
    
    private void Start() {
        currentDate = DateTime.Now;
        var sinceDate = currentDate.AddMonths(-5);
        for (int i = 0; i < 20; i++) {
            MonthCell monthCell = Instantiate(monthCellPrefab, contentRect);
            DisplayCalendar(sinceDate, monthCell);
            sinceDate = sinceDate.AddMonths(1);
        }

        // var layout = contentRect.GetComponent<VerticalLayoutGroup>();
        // layout.spacing = 2;
    }
    
    void Update() {
        UpdateContentVisibility();
    }

    void UpdateContentVisibility() {
        RectTransform viewport = scrollRect.viewport;
        Rect viewportRect = viewport.rect;
        Vector3[] worldCorners = new Vector3[4];
        viewport.GetWorldCorners(worldCorners);

        foreach (RectTransform child in contentRect)
        {
            // Получаем границы элемента в мировых координатах
            Vector3[] childCorners = new Vector3[4];
            child.GetWorldCorners(childCorners);

            // Проверяем, виден ли элемент
            bool isVisible = !(childCorners[1].y < worldCorners[0].y || // Верхняя граница элемента ниже нижней границы видимой области
                               childCorners[0].y > worldCorners[1].y);  // Нижняя граница элемента выше верхней границы видимой области
            
            if (isVisible) child.GetComponent<MonthCell>().Show();
            else child.GetComponent<MonthCell>().Hide();
        }
    }

    // Method to display the calendar
    private void DisplayCalendar(DateTime date, MonthCell monthCell) {
        // Clear previous days
        foreach (Transform child in monthCell.daysContainer) {
            Destroy(child.gameObject);
        }

        // Set month and year text
        monthCell.monthText.text = date.ToString("MMMM yyyy");

        // Get the first day of the month and the number of days in the month
        DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
        int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
        int dayOfWeek = (int)firstDayOfMonth.DayOfWeek;

        if (dayOfWeek == 0) {
            monthCell.MoveContainer();
        } 
        
        // Instantiate day buttons/texts
        for (int i = 0; i < dayOfWeek; i++) {
            Instantiate(emptyDayPrefab, monthCell.daysContainer); // Empty placeholder for alignment
        }

        for (int day = 1; day <= daysInMonth; day++) {
            GameObject dayObject = Instantiate(dayPrefab, monthCell.daysContainer);
            var dayView = dayObject.GetComponent<CalendarCellView>();
            dayView.SetDay(day);
            var task = taskController.GetCompletedTasksByDate(firstDayOfMonth.AddDays(day - 1));
            dayView.SetTaskCount(task.Count);
        }
    }
}
