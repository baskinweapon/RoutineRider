using UnityEngine;

namespace UI.Calendar {
    public class CalendarController : MonoBehaviour {
        [SerializeField] public readonly string[] Achievements = new string[] {
            "Worst",
            "Bad",
            "Good",
            "Awesome",
            "Perfect"
        };
        
        [SerializeField] public Gradient gradient;
    }
}