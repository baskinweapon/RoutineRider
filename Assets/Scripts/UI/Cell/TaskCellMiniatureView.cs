using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Cell {
    public class TaskCellMiniatureView : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI taskName;
        
        private Task task;
        public void Init(Task task) {
            this.task = task;
        
            this.taskName.text = task.taskName;
        }
    }
}