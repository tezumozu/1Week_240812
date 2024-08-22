using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

//モードに合わせたダンジョンの情報を取得し、ダンジョンを生成し、ダンジョンの状態を監視し、オブジェクトなど必要な情報を提供する
public abstract class GameBoardManager : I_DungeonObjectCreatable , I_GameFinishCheckable , IDisposable {

    //ゲームが終了したことを通知する
    protected Subject<bool> gameFinishSubject = new Subject<bool>();
    public IObservable<bool> GameFinishAsync => gameFinishSubject;

    //プレイヤーがゴールしたことを通知する
    protected Subject<Unit> playerGoalSubject = new Subject<Unit>();
    public IObservable<Unit> PlayerGoalAsync => playerGoalSubject;


    protected List<IDisposable> disposableList = new List<IDisposable>();
    protected GameBoard gameBoard;

    public GameBoardManager (){
        //GameBoardを取得
        this.gameBoard = GameObject.Find("GameBoard").GetComponent<GameBoard>();
        var disposable = gameBoard.PlayerGoalAsync.Subscribe( _ => {
            playerGoalSubject.OnNext(Unit.Default);
        });

        disposableList.Add(disposable);
    }

    //ダンジョンを生成する
    public abstract IEnumerator CreateDungeon();

    public void Dispose(){

    }

}
