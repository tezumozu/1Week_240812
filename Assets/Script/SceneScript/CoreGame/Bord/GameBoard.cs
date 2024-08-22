using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using My1WeekGameSystems_ver3;
using UniRx;
using UnityEditor;
using UnityEngine;
using Zenject;

//プレイヤーの駒やタイルを管理する、盤面の状態を報告する
public class GameBoard: 
AnimMono<E_BoardAnim> ,
I_GameBoardChackable ,
I_CameraTargettable ,
I_BoardClickable
{

    PlayerObject player;

    [SerializeField]
    GameObject GameCamera;

    float tileDistance = 0.5f;
    float tileSize = 2.0f;

    int dungeonHight;

    Tile[,] tileList;
    public Tile currentTile {
        get;
        private set;
    }



    //ライフがなくなったときに通知する
    private Subject<int> lifeLostSubject = new Subject<int>();
    public IObservable<int> LifeLostAsync => lifeLostSubject; 

    //プレイヤーがゴールしたことを通知する
    private Subject<Unit> playerGoalSubject = new Subject<Unit>();
    public IObservable<Unit> PlayerGoalAsync => playerGoalSubject; 

    //クリックされたTileを返す
    private Subject<Tile> clickedTileSubject = new Subject<Tile>();
    public IObservable<Tile> ClickedTileAsync => clickedTileSubject; 



    void Start(){

        //アニメーションの登録
        AnimList.Add(E_BoardAnim.CreateBoardAnim , createBoardAnim);


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
                tileObject.transform.Translate(tile_x , 10.0f , -tile_y , Space.Self);

                tileList[x,y] = tileObject.GetComponent<Tile>();

                if(data[x,y] == E_DungeonCell.Start){
                    currentTile = tileList[x,y];
                }

                if(data[x,y] == E_DungeonCell.Goal){
                    tileList[x,y].StepedOnAsync.Subscribe(_ => {
                        Debug.Log("GameBoard : GOAL");
                        //ゴールしたことを通知
                        playerGoalSubject.OnNext(Unit.Default);
                    }).AddTo(this);
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
    }




    //アニメ
    private IEnumerator createBoardAnim(){

        //カメラを直す
        var coroutine = TargetThis(GameCamera);
        CoroutineHander.OrderStartCoroutine(coroutine);

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }


        //ボードを生成するアニメ
        coroutine = callCreateTile();
        CoroutineHander.OrderStartCoroutine(coroutine);

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }


        //ちらっと見せる
        foreach(var tile in tileList){
            coroutine = tile.StartAnim(E_TileAnim.ClearTile);
            CoroutineHander.OrderStartCoroutine(coroutine);
        }

        //全てのコルーチンが終わったら終了
        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }


        //スタート地点とゴール地点をめくる
        coroutine = currentTile.StartAnim(E_TileAnim.TurnOver);
        CoroutineHander.OrderStartCoroutine(coroutine);

        for(int x = 0; x < dungeonHight; x++){

            for(int y = 0; y < dungeonHight; y++){

                if(tileList[y,x].TileType == E_DungeonCell.Goal){
                    coroutine = tileList[y,x].StartAnim(E_TileAnim.TurnOver);
                    CoroutineHander.OrderStartCoroutine(coroutine);
                }
            }    
        }

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }


        //カメラをスタート地点へ
        coroutine = currentTile.TargetThis(GameCamera);
        CoroutineHander.OrderStartCoroutine(coroutine);

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }


        //プレイヤーを表す駒を置く
        //プレイヤーがなければ
        GameObject playerObject;
        if(player == null){
            string path = "Prefab/InGame/Player";
            var PlayerPrefab = Resources.Load(path);

            if(PlayerPrefab == null){
                Debug.Log("GameBoardManager : 読み込み失敗");
            }

            //インスタンス生成
            playerObject = ( GameObject ) GameObject.Instantiate(PlayerPrefab);
            player = playerObject.GetComponent<PlayerObject>();
            player.InitObject();

            player.transform.parent = gameObject.transform;
            
        }else{
            playerObject = player.gameObject;
            coroutine = player.StartAnim(E_PlayerAnim.Init);
            CoroutineHander.OrderStartCoroutine(coroutine);
        }

        //座標を変更
        playerObject.transform.position = new Vector3 (currentTile.transform.position.x , 5.0f + 0.2f , currentTile.transform.position.z); 

        coroutine = player.StartAnim(E_PlayerAnim.SetPlayer);
        CoroutineHander.OrderStartCoroutine(coroutine);

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }
        
    }



    private IEnumerator callCreateTile(){

        IEnumerator coroutine = null;

        for(int x = 0; x < dungeonHight; x++){
            for(int y = 0; y < dungeonHight; y++){
                
                coroutine = tileList[y,x].StartAnim(E_TileAnim.CreateTile);
                CoroutineHander.OrderStartCoroutine(coroutine);

                //時間経過でアニメーションを呼び出すために待つ
                float currentTime = 0.0f;
                float callTime = 0.01f; // 秒

                while(callTime > currentTime){
                    currentTime += Time.deltaTime;
                    yield return null;
                }

            }
        }
        
        //最後のコルーチンが終了するのを待つ
        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }

    }



    //カメラを特定の位置に移動させる
    public IEnumerator TargetThis(GameObject camera){
        float range = dungeonHight * (tileSize + tileDistance) - tileDistance;

        var point = (float) Math.Sqrt(2) * range / 2;
        var NextPos = transform.position;
        NextPos += new Vector3( 0.0f , point , -point );

        while( Vector3.Distance( camera.transform.position , NextPos ) * 0.5f > 0.001f  ){
            camera.transform.position += (NextPos - camera.transform.position) * 0.5f;
            yield return null;
        }

        camera.transform.position = NextPos;

    }


    //プレイヤーを移動させる
    public IEnumerator MovePlayer(Tile tile){
        //プレイヤーに移動させる
        var coroutine = player.ToMoveTile(tile);

        CoroutineHander.OrderStartCoroutine(coroutine);

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }

        //プレイヤーがいるタイルを更新
        currentTile = tile;

        //カメラを移動
        coroutine = currentTile.TargetThis(GameCamera);

        CoroutineHander.OrderStartCoroutine(coroutine);

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }
    }


    
}
