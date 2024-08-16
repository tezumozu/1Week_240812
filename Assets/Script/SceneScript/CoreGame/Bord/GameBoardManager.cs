using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UniRx;
using UnityEditor;
using UnityEngine;

//プレイヤーの駒やタイルを管理する、盤面の状態を報告する
public class GameBoardManager : I_GameBoardChackable , I_BoardUpdatable , I_BoardAnimControlable{

    //ボードを見渡す際のカメラとの距離
    float CameraRange = 10;
    GameObject Board;
    GameObject PlayerObject;

    float tileDistance = 0.5f;
    float tileSize = 2.0f;

    int dungeonHight;

    //プレイヤーのライフがなくなったことを通知する
    private Subject<int> lifeLoseSubject = new Subject<int>();
    public IObservable<int> LifeLoseAsync => lifeLoseSubject; 

    //プレイヤーがゴールしたことを通知する
    private Subject<int> playerGoalSubject = new Subject<int>();
    public IObservable<int> PlayerGoalAsync => playerGoalSubject; 


    public GameBoardManager(){
        //カメラを取得する

        //プレイヤーを取得する

        //ボードを取得する
        Board = GameObject.Find("GameBoard");

    }


    public void CleateObject( E_DungeonCell[,] data , int hight ){

        dungeonHight = hight;

        //ボードを空にする
        foreach(Transform tile in Board.transform){
            //tileを削除
            GameObject.Destroy(tile.gameObject);
        }

        Debug.Log(Board.transform.childCount);

        //プレハブの読み込み
        var path = "Prefab/InGame/SampleTile";
        var TilePrefab = Resources.Load(path);

        if(TilePrefab == null){
            Debug.Log("GameBoardManager : 読み込み失敗");
        }

        Debug.Log(hight);

        //追加する
        for(int x = 0; x < hight; x++){
            for(int y = 0; y < hight; y++){
                var tile_x = ( x % hight - hight / 2 ) * (tileSize + tileDistance);
                var tile_y = ( y % hight - hight / 2 ) * (tileSize + tileDistance);

                //インスタンス生成
                var tile = ( GameObject ) GameObject.Instantiate(TilePrefab);
                //子オブジェクトに追加
                tile.transform.parent = Board.transform;

                //ローカル座標を変更
                tile.transform.Translate(tile_x , 0.0f , tile_y , Space.Self);

            }    
        }

    }



    public IEnumerator StartInitStagingAnim(){
        Debug.Log("GameBoardManager : ボードが生成されるアニメーション");
        yield return null;
    }
}
