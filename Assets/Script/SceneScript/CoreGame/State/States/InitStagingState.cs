using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


//インゲーム開始準備状態、インゲーム開始時演習用
public class InitStagingState : State{

    private I_DungeonObjectCreatable dungeonManager;

    public InitStagingState (I_DungeonObjectCreatable dungeonManager){
        this.dungeonManager = dungeonManager;
    }

    public override IEnumerator UpdateState(){
        Debug.Log("State : Init Staging");
        yield return null;
        finishStateSubject.OnNext(Unit.Default);
    }
}
