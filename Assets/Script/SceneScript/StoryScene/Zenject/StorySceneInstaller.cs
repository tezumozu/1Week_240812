using UnityEngine;
using Zenject;

using My1WeekGameSystems_ver3;

public class StorySceneInstaller : MonoInstaller {
    public override void InstallBindings() {

        var gameManager = new StorySceneManager();

        //GameManager
        Container
            .Bind<I_SceneLoadNoticable>()
            .To<StorySceneManager>()
            .FromInstance(gameManager);	

        Container
            .Bind<I_GameStateUpdatable<E_StorySceneState>>()
            .To<StorySceneManager>()
            .FromInstance(gameManager);

        Container
            .Bind<GameManager<E_StorySceneState>>()
            .To<StorySceneManager>()
            .FromInstance(gameManager);

        Container
            .Bind<I_Pausable>()
            .To<StorySceneManager>()
            .FromInstance(gameManager);


        //ObjectUpdater
        Container
            .Bind<I_ObjectUpdatable>()
            .To<StoryObjectUpdater>()
            .AsSingle();


    }
}