using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using My1WeekGameSystems_ver3;

using Zenject;

public class StoryObjectUpdater : I_ObjectUpdatable{
    [Inject]
    GameManager<E_StorySceneState> gameManager;

    public void InitObject(){
        //Debug.Log("ObjectUpdater : InitObject");
        
        gameManager.InitObject();
    }

    public void UpdateObject(){
        //Debug.Log("ObjectUpdater : UpdateObject");
    }

    public void StartGame(){
        //Debug.Log("ObjectUpdater : StartGame");
        gameManager.StartGame();
    }

    public void ReleaseObject(){
        //Debug.Log("ObjectUpdater : ReleaseObject");
        gameManager.ReleaseObject();
    }
}
