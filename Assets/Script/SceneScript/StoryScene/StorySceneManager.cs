using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

using My1WeekGameSystems_ver3;
using Zenject;

public class StorySceneManager : GameManager<E_StorySceneState> , I_Pausable {
    private Subject<bool> pauseSubject = new Subject<bool>();
    public IObservable<bool> PauseAsync{ get{ return pauseSubject;} }

    private Dictionary< E_StorySceneState , I_StateUpdatable > stateDic;
    private List<IDisposable> disposableList;
    private bool isGameFin;
    //private E_StorySceneState currentState;

    public StorySceneManager():base(E_StorySceneState.InitStaging){
        stateDic = new Dictionary<E_StorySceneState, I_StateUpdatable>();
        disposableList = new List<IDisposable>();
        isGameFin = false;
        //currentState = E_StorySceneState.InitStaging;
    }


    public override void InitObject(){
        Debug.Log("GameManager : Init");

        //オブジェクトを生成、初期化、インジェクション
        var timer = new InGameTimer();
        var dungeonManager = new StoryDungeonManager();
        var resultManager = new ResultManager(dungeonManager,timer);

        var initStaging = new InitStagingState(dungeonManager);
        stateDic.Add(E_StorySceneState.InitStaging,initStaging);

        var story = new StoryState(dungeonManager);
        stateDic.Add(E_StorySceneState.Story,story);

        var inGame = new InGameState(dungeonManager,timer);
        stateDic.Add(E_StorySceneState.InGame,inGame);

        var result = new StoryResultState(resultManager);
        stateDic.Add(E_StorySceneState.Result,result);

        //ゲームの終了を監視する
        var disposable = dungeonManager.FinishDungeonAsync.Subscribe((_)=>{
            isGameFin = true;
        });

        //ポーズが入力されたらポーズにする <-> ポーズを解除する

        disposableList.Add(disposable);


    }


    //ゲームを開始するコルーチン
    public IEnumerator GameMain(){

        //ゲームが終了するまで
        while(!isGameFin){

            //InitStaging
            UpdateStateSubject.OnNext(E_StorySceneState.InitStaging);
            var coroutine = stateDic[E_StorySceneState.InitStaging].UpdateState();

            //コルーチンを開始する
            CoroutineHander.OrderStartCoroutine(coroutine);
            //コルーチンの終了を待機する
            while(!CoroutineHander.isFinishCoroutine(coroutine)){
                yield return null;
            }


            //StoryState
            UpdateStateSubject.OnNext(E_StorySceneState.Story);
            coroutine = stateDic[E_StorySceneState.Story].UpdateState();

            //コルーチンを開始する
            CoroutineHander.OrderStartCoroutine(coroutine);
            //コルーチンの終了を待機する
            while(!CoroutineHander.isFinishCoroutine(coroutine)){
                yield return null;
            }


            //InGame
            UpdateStateSubject.OnNext(E_StorySceneState.InGame);
            coroutine = stateDic[E_StorySceneState.InGame].UpdateState();

            //コルーチンを開始する
            CoroutineHander.OrderStartCoroutine(coroutine);
            //コルーチンの終了を待機する
            while(!CoroutineHander.isFinishCoroutine(coroutine)){
                yield return null;
            }


            //Story
            UpdateStateSubject.OnNext(E_StorySceneState.Story);
            coroutine = stateDic[E_StorySceneState.Story].UpdateState();

            //コルーチンを開始する
            CoroutineHander.OrderStartCoroutine(coroutine);
            //コルーチンの終了を待機する
            while(!CoroutineHander.isFinishCoroutine(coroutine)){
                yield return null;
            }
        }


        //Result
        UpdateStateSubject.OnNext(E_StorySceneState.Result);
        var resultCoroutine = stateDic[E_StorySceneState.Result].UpdateState();

        //コルーチンを開始する
        CoroutineHander.OrderStartCoroutine(resultCoroutine);
        //コルーチンの終了を待機する
        while(!CoroutineHander.isFinishCoroutine(resultCoroutine)){
            yield return null;
        }

        yield return null;

        //シーンを終了する

    }





    public override void StartGame(){
        Debug.Log("GameManager : StartGame");

        //ゲームを開始する
        CoroutineHander.OrderStartCoroutine(GameMain());
    }

    public override void ReleaseObject(){
        Debug.Log("GameManager : ReleaseObject");

        if(disposableList.Count == 0) return;

        //購読の終了
        foreach(var disposable in disposableList){
            disposable.Dispose();
        }
    }


}
