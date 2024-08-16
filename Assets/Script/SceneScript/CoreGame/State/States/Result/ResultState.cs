using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;
using My1WeekGameSystems_ver3;

public class ResultState : State{

    private I_ResultUpdatable resultManager;
    private I_ResultInputTranslater inputManager;
    private I_BlackInable blackOutStaiging;

    public ResultState (I_ResultUpdatable resultManager , I_ResultInputTranslater inputManager , I_BlackInable blackOutStaiging){
        this.resultManager = resultManager;
        this.inputManager = inputManager;
        this.blackOutStaiging = blackOutStaiging;
    }

    public override IEnumerator UpdateState(){
        
        //ブラックイン
        var coroutine = blackOutStaiging.StartBlackIn();
        CoroutineHander.OrderStartCoroutine(coroutine);

        while(CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }


        //UIを表示するアニメーション
        coroutine = resultManager.ResultAnim();
        CoroutineHander.OrderStartCoroutine(coroutine);

        while(CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }


        //プレイヤーの入力待ち(終了待ち)
        coroutine = inputManager.WaitPlayerInput();
        CoroutineHander.OrderStartCoroutine(coroutine);

        while(CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }

        finishStateSubject.OnNext(Unit.Default);
    }
}
