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
    private CalendarController calendarController;
    
    [Inject]
    private void Construct(TaskController _taskController, CalendarController _calendarController) {
        taskController = _taskController;
        calendarController = _calendarController;
    }

    private MonthCell _currentMonthCell;
    private void GenerateCalendar(Task task = null) {
        
        foreach (Transform child in contentRect) {
            Destroy(child.gameObject);
        }
        
        currentDate = DateTime.Now;
        var sinceDate = currentDate.AddMonths(-5);
        for (int i = 0; i < 20; i++) {
            MonthCell monthCell = Instantiate(monthCellPrefab, contentRect);
            DisplayCalendar(sinceDate, monthCell);
            if (currentDate.Date == sinceDate.Date) { _currentMonthCell = monthCell; }
            sinceDate = sinceDate.AddMonths(1);
        }
    }

    private void OnEnable() {
        if (_currentMonthCell != null) {
            ScrollToItem(_currentMonthCell.GetComponent<RectTransform>());
        }
    }

    private void Start() {
        GenerateCalendar();
        
        if (_currentMonthCell != null) {
            ScrollToItem(_currentMonthCell.GetComponent<RectTransform>());
        }
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
    
    private void ScrollToItem(RectTransform targetItem)
    {
        Canvas.ForceUpdateCanvases(); // Обновляем Canvas чтобы получить актуальные размеры

        Vector2 targetLocalPosition = targetItem.localPosition;
        
        // Рассчитываем, на сколько нужно сдвинуть ScrollRect чтобы элемент был в центре
        Vector2 newAnchoredPosition = new Vector2(
            scrollRect.content.anchoredPosition.x,
            -targetLocalPosition.y - (targetItem.rect.height)
        );
        
        // Применяем новую позицию
        scrollRect.content.anchoredPosition = newAnchoredPosition;
    }

    // Method to display the calendar
    private void DisplayCalendar(DateTime date, MonthCell monthCell) {
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
            var dayView = dayObject.GetComponent<DayCellView>();
            dayView.Init(taskController, calendarController, firstDayOfMonth.AddDays(day -1 ));
            dayView.SetDay(day);
            var task = taskController.GetCompletedTasksByDate(firstDayOfMonth.AddDays(day - 1));
            dayView.SetTaskCount(task.Count);
        }
    }
}
