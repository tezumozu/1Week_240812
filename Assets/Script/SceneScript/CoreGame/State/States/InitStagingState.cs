using System.Collections;
using System.Collections.Generic;
using My1WeekGameSystems_ver3;
using UniRx;
using UnityEngine;


//インゲーム開始準備状態、インゲーム開始時演習用
public class InitStagingState : State{

    private I_DungeonObjectCreatable dungeonManager;

    public InitStagingState (I_DungeonObjectCreatable dungeonManager){
        this.dungeonManager = dungeonManager;
    }

    public override IEnumerator UpdateState(){

        //ダンジョン生成(ポーズで止まらない)
        var coroutine = dungeonManager.CreateDungeon();
        CoroutineHander.OrderStartCoroutine(coroutine,false);

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }

        //Blackoutから開ける
        //ダンジョンが生成されるアニメーションを再生
        //ゲーム開始の演出
        
        yield return null;
        finishStateSubject.OnNext(Unit.Default);
    }
}
