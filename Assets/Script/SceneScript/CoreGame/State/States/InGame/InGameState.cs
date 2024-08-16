using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;
using My1WeekGameSystems_ver3;
using System;
using Zenject;
using System.Runtime.CompilerServices;

public class InGameState : State{

    I_TimeMeasurable Timer;
    I_GameBoardChackable boardManager;
    I_InGameInputTranslatable inputManager;
    private bool isGameFin;
    private I_OrderExecutionable order;

    

    //ゲームにおける一連の流れを整理
    public InGameState (I_GameBoardChackable boardManager, I_TimeMeasurable Timer , I_InGameInputTranslatable inputManager){
        this.Timer = Timer;
        this.boardManager = boardManager;
        this.inputManager = inputManager;
        isGameFin = false;
        disposableList = new List<IDisposable>();


        //もしライフをすべて失ったらゲームを終了する
        var disposable = boardManager.LifeLoseAsync.Subscribe(life => {
            if(life <= 0){
                isGameFin = true;
            }
        });

        disposableList.Add(disposable);


        //プレイヤーがゴールしたらゲームを終了する
        disposable = boardManager.PlayerGoalAsync.Subscribe(_ => {
            isGameFin = true;
        });

        disposableList.Add(disposable);

        

        //入力を受けた時の処理
        disposable = inputManager.PlayerOrderAsync.Subscribe(order => {
            this.order = order;
        });

        disposableList.Add(disposable);
    }

    public override IEnumerator UpdateState(){

        //タイマーを起動 (ポーズで終了しない)
        CoroutineHander.OrderStartCoroutine(Timer.StartMeasureTime(),false);


        //ゲームが終了するまで( ライフ0 か ゴール か)
        //while(!isGameFin){

            //入力待ち
            var coroutine = inputManager.GetPlayerOrder();
            CoroutineHander.OrderStartCoroutine(coroutine);

            while(!CoroutineHander.isFinishCoroutine(coroutine)){
                yield return null;
            }

            //アクションの処理
            coroutine = order.OrderExecution();
            CoroutineHander.OrderStartCoroutine(coroutine);

            while(!CoroutineHander.isFinishCoroutine(coroutine)){
                yield return null;
            }

        //}


        //タイマーを停止
        Timer.StopMeasureTime();
        finishStateSubject.OnNext(Unit.Default);
    }
    
}
