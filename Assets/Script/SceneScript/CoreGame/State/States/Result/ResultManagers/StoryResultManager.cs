using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryResultManager : ResultManager{

    I_GameFinishCheckable gameBoardManager;

    public StoryResultManager(I_GameFinishCheckable gameBoardManager){
        this.gameBoardManager = gameBoardManager;
    }

    public override void SetActive(bool flag){

    }
    
    public override IEnumerator ResultAnim(){
        yield return null;
    }
}
