using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

//モードに合わせたダンジョンの情報を取得し、ダンジョンを生成し、ダンジョンの状態を監視し、オブジェクトなど必要な情報を提供する
public abstract class DungeonManager : I_DungeonObjectCreatable {

    //ダンジョンが終了しクリアしか通知する
    protected Subject<bool> finishDungeonSubject = new Subject<bool>();
    public IObservable<bool> FinishDungeonAsync{ get{ return finishDungeonSubject;} }
    protected List<IDisposable> disposableList = new List<IDisposable>();
    protected GameBoardManager gameBoard;


    public DungeonManager (GameBoardManager gameBoard){

        this.gameBoard = gameBoard;
    }

    //ダンジョンを生成する
    public abstract IEnumerator CreateDungeon();

}
