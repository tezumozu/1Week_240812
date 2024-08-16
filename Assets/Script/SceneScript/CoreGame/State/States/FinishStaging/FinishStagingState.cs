using System.Collections;
using System.Collections.Generic;
using My1WeekGameSystems_ver3;
using UniRx;
using UnityEngine;


//インゲーム開始準備状態、インゲーム開始時演習用
public class FinishStagingState : State{
    private I_BlackOutable blackOutAnimManager;

    public FinishStagingState ( I_BlackOutable animManager){
        blackOutAnimManager = animManager;
    }

    public override IEnumerator UpdateState(){

        //Blackoutから開ける
        var coroutine = blackOutAnimManager.StartBlackOut();
        CoroutineHander.OrderStartCoroutine(coroutine,false);

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }


        //状態が終了
        finishStateSubject.OnNext(Unit.Default);
    }
}
