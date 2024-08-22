using System.Collections;
using System.Collections.Generic;
using My1WeekGameSystems_ver3;
using UniRx;
using UnityEngine;
using Zenject;


//インゲーム開始準備状態、インゲーム開始時演習用
public class InitStagingState : State{

    private I_DungeonObjectCreatable dungeonManager;
    private I_BlackInable blackOutAnimManager;
    private StartGameAnim startGameAnim;

    public InitStagingState (I_DungeonObjectCreatable dungeonManager , I_BlackInable animManager){
        this.dungeonManager = dungeonManager;
        blackOutAnimManager = animManager;
    }

    public override IEnumerator UpdateState(){

        var gameBoardAnim = new GameBoardAnim();

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
        coroutine = gameBoardAnim.StartAnim(E_BoardAnim.CreateBoardAnim);
        CoroutineHander.OrderStartCoroutine(coroutine,false);

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }

        //状態が終了
        finishStateSubject.OnNext(Unit.Default);
    }
}
