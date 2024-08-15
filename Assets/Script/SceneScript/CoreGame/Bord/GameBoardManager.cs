using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEditor;
using UnityEngine;

//プレイヤーの駒やタイルを管理する、盤面の状態を報告する
public class GameBoardManager : I_GameBoardChackable , I_BoardUpdatable , I_BoardAnimControlable{

    //ボードを見渡す際のカメラとの距離
    float CameraRange = 10;
    GameObject Board;
    GameObject PlayerObject;

    //プレイヤーのライフがなくなったことを通知する
    private Subject<int> lifeLoseSubject = new Subject<int>();
    public IObservable<int> LifeLoseAsync => lifeLoseSubject; 

    //プレイヤーがゴールしたことを通知する
    private Subject<int> playerGoalSubject = new Subject<int>();
    public IObservable<int> PlayerGoalAsync => playerGoalSubject; 


    public GameBoardManager(){
        //カメラを取得する
        //Plyerやボードを生成する
    }


    public void CleateObject( E_DungeonCell[,] data , int hight ){
        Debug.Log("GameBoardManager : ボードを作成");
    }



    public IEnumerator StartInitStagingAnim(){
        Debug.Log("GameBoardManager : ボードが生成されるアニメーション");
        yield return null;
    }
}
