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

    private Dictionary< E_StorySceneState , State > stateDic;
    private List<IDisposable> disposableList;
    private PauseInputTranslater<E_StorySceneState> inputManager;

    private bool isGameFin;


    public StorySceneManager():base(E_StorySceneState.InitStaging){
        stateDic = new Dictionary<E_StorySceneState, State>();
        disposableList = new List<IDisposable>();
        isGameFin = false;
        inputManager = new PauseInputTranslater<E_StorySceneState>(this,E_StorySceneState.Pause);
    }


    public override void InitObject(){

        Debug.Log("GameManager : Init");

        //オブジェクトを生成、初期化、インジェクション
        var timer = new InGameTimer();
        var gameBoardManager = new StoryGameBoardManager();
        var resultManager = new StoryResultManager(gameBoardManager);
        var blackOutStaiging = new BlackOutAnimManager();
        var textBox = new TextBoxManager();

        //入力系
        var StoryInput = new StoryInputTranslater<E_StorySceneState>(this,E_StorySceneState.Story);
        StoryInput.AddActiveState(E_StorySceneState.Prologe);
        var InGameInput = new InGameTurnManager<E_StorySceneState>(this,E_StorySceneState.InGame);
        var ResultInput = new ResultInputTranslater<E_StorySceneState>(this,E_StorySceneState.Result);

        var initStaging = new InitStagingState(gameBoardManager,blackOutStaiging);
        stateDic.Add(E_StorySceneState.InitStaging,initStaging);

        var inGame = new InGameState(gameBoardManager,timer,InGameInput);
        stateDic.Add(E_StorySceneState.InGame,inGame);

        var finishStaging = new FinishStagingState(blackOutStaiging);
        stateDic.Add(E_StorySceneState.FinishStaging,finishStaging);

        var result = new ResultState(resultManager,ResultInput,blackOutStaiging);
        stateDic.Add(E_StorySceneState.Result,result);


        //ストーリー専用
        var prologe = new StoryPrologeState(gameBoardManager,StoryInput,blackOutStaiging,textBox);
        stateDic.Add(E_StorySceneState.Prologe,prologe);

        var story = new StoryState(gameBoardManager,StoryInput,textBox);
        stateDic.Add(E_StorySceneState.Story,story);

        //ゲームの終了を監視する
        var disposable = gameBoardManager.GameFinishAsync.Subscribe((_)=>{
            isGameFin = true;
        });

        disposableList.Add(disposable);


        

        //ポーズの入力を監視
        //InGame状態でも入力を受け取れるようにする
        inputManager.AddActiveState(E_StorySceneState.InGame);

        //ポーズが入力されたらポーズにする <-> ポーズを解除する
        disposable = inputManager.PauseInputAsync.Subscribe( flag => {
            //入力内容を通知
            pauseSubject.OnNext(flag);

            //入力に基づいて現在の状態を通知
            if(flag){
                //ポーズ状態なら
                UpdateStateSubject.OnNext(E_StorySceneState.Pause);
            }else{
                //ポーズが解除されたなら
                UpdateStateSubject.OnNext(currentState);
            }
        });

        disposableList.Add(disposable);

        //入力系をdisposeListに
        disposableList.Add(StoryInput);
        disposableList.Add(ResultInput);
        disposableList.Add(InGameInput);

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

        foreach(var state in stateDic.Keys){
            stateDic[state].Dispose();
        }

        inputManager.Dispose();

        if(disposableList.Count == 0) return;

        //購読の終了
        foreach(var disposable in disposableList){
            disposable.Dispose();
        }

        
    }


}
