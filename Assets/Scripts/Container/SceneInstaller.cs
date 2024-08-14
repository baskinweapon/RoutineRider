using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private TaskController taskController;
    [SerializeField] private SaveModel saveModel;
    [SerializeField] private TabController tabController;
    [SerializeField] private WindowController windowController;
    [SerializeField] private TemplateController templateController;
    
    public override void InstallBindings() {
        Container.Bind<TaskController>().FromInstance(taskController);
        Container.Bind<SaveModel>().FromInstance(saveModel);
        Container.Bind<TabController>().FromInstance(tabController);
        Container.Bind<WindowController>().FromInstance(windowController);
        Container.Bind<TemplateController>().FromInstance(templateController);

    }
}

