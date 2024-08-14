using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalendarCellView : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI taskCountText;
    
    public void SetDay(int day) {
        dayText.text = day.ToString();
    }
    
    public void SetTaskCount(int count) {
        taskCountText.text = count.ToString();
        background.color = count > 0 ? Color.green : Color.gray;
    }
}
