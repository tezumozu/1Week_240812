using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using My1WeekGameSystems_ver3;
using UniRx;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Zenject;

//プレイヤーの駒やタイルを管理する、盤面の状態を報告する
public class GameBoard: MonoBehaviour , I_GameBoardChackable , I_CameraTargettable , I_BoardClickable{

    //ボードを見渡す際のカメラとの距離
    float CameraRange = 10;
    GameObject PlayerObject;

    float tileDistance = 0.5f;
    float tileSize = 2.0f;

    int dungeonHight;

    Tile[,] tileList;
    Tile currentTile;



    //ライフがなくなったときに通知する
    private Subject<int> lifeLostSubject = new Subject<int>();
    public IObservable<int> LifeLostAsync => lifeLostSubject; 

    //プレイヤーがゴールしたことを通知する
    private Subject<int> playerGoalSubject = new Subject<int>();
    public IObservable<int> PlayerGoalAsync => playerGoalSubject; 

    //クリックされたTileを返す
    private Subject<Tile> clickedTileSubject = new Subject<Tile>();
    public IObservable<Tile> ClickedTileAsync => clickedTileSubject; 



    void Start(){

        //プレイヤーを取得する

    }




    public void CreateObject( E_DungeonCell[,] data , int hight ){

        dungeonHight = hight;
        tileList = new Tile[hight,hight];

        //ボードを空にする
        foreach(Transform tile in gameObject.transform){
            //tileを削除
            Destroy(tile.gameObject);
        }
        

        var TileFactory = new TileFactory();


        //追加する
        for(int x = 0; x < hight; x++){

            for(int y = 0; y < hight; y++){

                var tile_x = ( x % hight - hight / 2 ) * (tileSize + tileDistance);
                var tile_y = ( y % hight - hight / 2 ) * (tileSize + tileDistance);

                var tileObject = TileFactory.CreateObject(data[x,y]);

                //子オブジェクトに追加
                tileObject.transform.parent = gameObject.transform;

                //ローカル座標を変更
                tileObject.transform.Translate(tile_x , 0.0f , -tile_y , Space.Self);

                tileList[x,y] = tileObject.GetComponent<Tile>();

                if(data[x,y] == E_DungeonCell.Start){
                    currentTile = tileList[x,y];
                }

                tileList[x,y].ClickAsync.Subscribe( tile => {

                    clickedTileSubject.OnNext(tile);

                }).AddTo(this);

            }    
        }

        

        //隣接するマスを登録する
        for(int x = 0; x < hight; x++){

            for(int y = 0; y < hight; y++){

                //隣接するマスを登録する
                if( ! (x - 1 < 0) ) tileList[x,y].AddRelatedTile(tileList[ x - 1 , y ]);

                if( ! (y - 1 < 0) ) tileList[x,y].AddRelatedTile(tileList[ x , y - 1 ]);

                if( ! (x + 1 >= dungeonHight) ) tileList[x,y].AddRelatedTile(tileList[ x + 1 , y ]);

                if( ! (y + 1 >= dungeonHight) ) tileList[x,y].AddRelatedTile(tileList[ x , y + 1 ]);

            }    
        }

    }


    public void SetClickable(bool flag){
        foreach(var tile in currentTile.RelatedTileList){
            tile.SetIsClickable(flag);
        }

        //カメラをスタート地点へ
        var Camera = GameObject.Find("MainCamera");
        var coroutine = tileList[0,0].TargetThis(Camera);
        CoroutineHander.OrderStartCoroutine(coroutine);
    }




    //アニメ
    public IEnumerator StartInitStagingAnim(){
        //
        Debug.Log("GameBoardManager : ボードが生成されるアニメーション");
        yield return null;
        
    }



    //カメラを特定の位置に移動させる
    public IEnumerator TargetThis(GameObject camera){
        yield return null;
    }
}
