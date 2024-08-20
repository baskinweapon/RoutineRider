using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FunctionPanelView : View {
    public SwitchButton templateButton;
    public SwitchButton finishedButton;
    public Button addButton;
    
    
    [SerializeField] private Window finishedPanel;
    [SerializeField] private Window templatePanel;
    [SerializeField] private Window addPanel;
    
    private TaskController taskController;
    [Inject]
    public void Construct(TaskController taskController) {
        this.taskController = taskController;
    }
    
    
    private List<Window> _panels = new List<Window>();
    private void OnEnable() {
        
        // add all panels to list
        _panels.Add(finishedPanel);
        _panels.Add(templatePanel);
        _panels.Add(addPanel);
        
        addButton.onClick.AddListener(ShowAddTaskPanel);
        finishedButton.OnClick.AddListener(ShowFinished);
        templateButton.OnClick.AddListener(ShowTemplate);
    }
    
    private void OnDisable() {
        addButton.onClick.RemoveListener(ShowAddTaskPanel);
        finishedButton.OnClick.RemoveListener(ShowFinished);
        templateButton.OnClick.RemoveListener(ShowTemplate);
        
        HideAllPanels();
    }
    
    private void ShowAddTaskPanel() {
        HideAllPanels();
        addPanel.SwitchState(true);
    }
    
   private void ShowTemplate(bool state) {
        HideAllPanels();
        templatePanel.SwitchState(state);
    }
    
    private void ShowFinished(bool state) {
        HideAllPanels();
        finishedPanel.SwitchState(state);
    }
    
    private string inputText;
    public void OnTextChanged(string text) {
        if (text.Length > 0) {
            inputText = text;
        } else {
        }
    }

    public void HideAllPanels() {
        foreach (var panel in _panels) {
            panel.SwitchState(false, true);
        }
    }
    
    
    
    
    
}
