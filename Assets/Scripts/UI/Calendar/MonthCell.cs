using System;
using TMPro;
using UnityEngine;

namespace UI.Calendar {
    public class MonthCell : MonoBehaviour {
        public TextMeshProUGUI monthText;
        public TextMeshProUGUI yearText;
        public Transform daysContainer;

        private void Start() {
            var tr = GetComponent<RectTransform>();
            tr.sizeDelta = daysContainer.GetComponent<RectTransform>().sizeDelta;
            if (_needMove) {
                tr.sizeDelta += new Vector2(0, 52);
            }
        }

        public void Hide() {
            daysContainer.gameObject.SetActive(false);
            monthText.gameObject.SetActive(false);
        }
        
        public void Show() {
            daysContainer.gameObject.SetActive(true);
            monthText.gameObject.SetActive(true);
        }

        private bool _needMove;
        public void MoveContainer() {
            var rect = daysContainer.GetComponent<RectTransform>();
            Vector2 currentPosition = rect.anchoredPosition;
            currentPosition.y -= 26;
            rect.anchoredPosition = currentPosition;

            var text = monthText.GetComponent<RectTransform>();
            Vector2 textPosition = text.anchoredPosition;
            textPosition.y -= 52;
            text.anchoredPosition = textPosition;
            
            _needMove = true;
        }
    }
}