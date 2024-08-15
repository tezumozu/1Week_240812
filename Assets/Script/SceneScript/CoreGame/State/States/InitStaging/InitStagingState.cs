using System.Collections;
using System.Collections.Generic;
using My1WeekGameSystems_ver3;
using UniRx;
using UnityEngine;


//インゲーム開始準備状態、インゲーム開始時演習用
public class InitStagingState : State{

    private I_DungeonObjectCreatable dungeonManager;
    private I_BlackInable blackOutAnimManager;
    private I_BoardAnimControlable gameBoard;
    private StartGameAnim startGameAnim;

    public InitStagingState (I_DungeonObjectCreatable dungeonManager , I_BlackInable animManager , I_BoardAnimControlable gameBoard){
        this.dungeonManager = dungeonManager;
        blackOutAnimManager = animManager;
        this.gameBoard = gameBoard;
        startGameAnim = new StartGameAnim();
    }

    public override IEnumerator UpdateState(){

        //ダンジョン生成(ポーズで止まらない) 
        var coroutine = dungeonManager.CreateDungeon();
        CoroutineHander.OrderStartCoroutine(coroutine,false);

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }


        //Blackoutから開ける
        coroutine = blackOutAnimManager.StartBlackIn();
        CoroutineHander.OrderStartCoroutine(coroutine,false);

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }


        //ダンジョンが生成されるアニメーションを再生
        coroutine = gameBoard.StartInitStagingAnim();
        CoroutineHander.OrderStartCoroutine(coroutine,false);

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }

        //ゲーム開始用のアニメーションを流す
        coroutine = startGameAnim.StartAnim(E_StartGameAnim.StartGame);
        CoroutineHander.OrderStartCoroutine(coroutine,false);

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }


        //状態が終了
        finishStateSubject.OnNext(Unit.Default);
    }
}
