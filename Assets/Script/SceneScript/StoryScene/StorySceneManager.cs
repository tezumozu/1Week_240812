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
        var gameBord = new GameBordManager();
        var dungeonManager = new StoryDungeonManager(gameBord);
        var resultManager = new ResultManager(gameBord,timer);

        var initStaging = new InitStagingState(dungeonManager);
        stateDic.Add(E_StorySceneState.InitStaging,initStaging);

        var inGame = new InGameState(gameBord,timer);
        stateDic.Add(E_StorySceneState.InGame,inGame);

        var finishStaging = new FinishStagingState();
        stateDic.Add(E_StorySceneState.FinishStaging,finishStaging);

        var result = new ResultState(resultManager);
        stateDic.Add(E_StorySceneState.Result,result);


        //ストーリー専用
        var prologe = new StoryPrologeState(dungeonManager);
        stateDic.Add(E_StorySceneState.Prologe,prologe);

        var story = new StoryState(dungeonManager);
        stateDic.Add(E_StorySceneState.Story,story);

        //ゲームの終了を監視する
        var disposable = dungeonManager.FinishDungeonAsync.Subscribe((_)=>{
            isGameFin = true;
        });

        //ポーズが入力されたらポーズにする <-> ポーズを解除する


        disposableList.Add(disposable);


    }


    //ゲームを開始するコルーチン
    public IEnumerator GameMain(){

        //prologe
        currentState = E_StorySceneState.Prologe;
        UpdateStateSubject.OnNext(currentState);
        var coroutine = stateDic[currentState].UpdateState();

        //コルーチンを開始する
        CoroutineHander.OrderStartCoroutine(coroutine);
        //コルーチンの終了を待機する
        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }


        //ゲームが終了するまで
        while(!isGameFin){

            //InitStaging
            currentState = E_StorySceneState.InitStaging;
            UpdateStateSubject.OnNext(currentState);
            coroutine = stateDic[currentState].UpdateState();

            //コルーチンを開始する
            CoroutineHander.OrderStartCoroutine(coroutine);
            //コルーチンの終了を待機する
            while(!CoroutineHander.isFinishCoroutine(coroutine)){
                yield return null;
            }


            //InGame
            currentState = E_StorySceneState.InGame;
            UpdateStateSubject.OnNext(currentState);
            coroutine = stateDic[currentState].UpdateState();

            //コルーチンを開始する
            CoroutineHander.OrderStartCoroutine(coroutine);
            //コルーチンの終了を待機する
            while(!CoroutineHander.isFinishCoroutine(coroutine)){
                yield return null;
            }


            //Story
            currentState = E_StorySceneState.Story;
            UpdateStateSubject.OnNext(currentState);
            coroutine = stateDic[currentState].UpdateState();

            //コルーチンを開始する
            CoroutineHander.OrderStartCoroutine(coroutine);
            //コルーチンの終了を待機する
            while(!CoroutineHander.isFinishCoroutine(coroutine)){
                yield return null;
            }


            //FinishStaging
            currentState = E_StorySceneState.FinishStaging;
            UpdateStateSubject.OnNext(currentState);
            coroutine = stateDic[currentState].UpdateState();

            //コルーチンを開始する
            CoroutineHander.OrderStartCoroutine(coroutine);
            //コルーチンの終了を待機する
            while(!CoroutineHander.isFinishCoroutine(coroutine)){
                yield return null;
            }
        }


        //Result
        currentState = E_StorySceneState.Result;
        UpdateStateSubject.OnNext(currentState);
        coroutine = stateDic[currentState].UpdateState();

        //コルーチンを開始する
        CoroutineHander.OrderStartCoroutine(coroutine);
        //コルーチンの終了を待機する
        while(!CoroutineHander.isFinishCoroutine(coroutine)){
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
