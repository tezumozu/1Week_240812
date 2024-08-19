using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileFactory : Factory<Tile,E_DungeonCell>{

    string rootPath;

    public TileFactory(){

        rootPath = "Prefab/InGame/Tiles/";

        //メソッドを登録する
        methodList[E_DungeonCell.Start] = makeStartTile;
        methodList[E_DungeonCell.Goal] = makeGoalTile;
        methodList[E_DungeonCell.Cure] = makeCureTile;
        methodList[E_DungeonCell.Ghost] = makeGhostTile;
        methodList[E_DungeonCell.Item] = makeItemTile;
        methodList[E_DungeonCell.Trap] = makeTrapTile;
        methodList[E_DungeonCell.Wall] = makeWallTile;
        methodList[E_DungeonCell.Way] = makeWayTile;
    }


    private Tile makeStartTile(){
        //プレハブの読み込み
        var path = rootPath + "StartTile";
        var TilePrefab = Resources.Load(path);

        if(TilePrefab == null){
            Debug.Log("GameBoardManager : 読み込み失敗");
        }

        //インスタンス生成
        return GameObject.Instantiate(TilePrefab).GetComponent<Tile>();
    }


    private Tile makeGoalTile(){
        //プレハブの読み込み
        var path = rootPath + "GoalTile";
        var TilePrefab = Resources.Load(path);

        if(TilePrefab == null){
            Debug.Log("GameBoardManager : 読み込み失敗");
        }

        //インスタンス生成
        return GameObject.Instantiate(TilePrefab).GetComponent<Tile>();
    }


    private Tile makeCureTile(){
        //プレハブの読み込み
        var path = rootPath + "CureTile";
        var TilePrefab = Resources.Load(path);

        if(TilePrefab == null){
            Debug.Log("GameBoardManager : 読み込み失敗");
        }

        //インスタンス生成
        return GameObject.Instantiate(TilePrefab).GetComponent<Tile>();
    }


    private Tile makeGhostTile(){
        //プレハブの読み込み
        var path = rootPath + "GhostTile";
        var TilePrefab = Resources.Load(path);

        if(TilePrefab == null){
            Debug.Log("GameBoardManager : 読み込み失敗");
        }

        //インスタンス生成
        return GameObject.Instantiate(TilePrefab).GetComponent<Tile>();
    }


    private Tile makeItemTile(){
        //プレハブの読み込み
        var path = rootPath + "ItemTile";
        var TilePrefab = Resources.Load(path);

        if(TilePrefab == null){
            Debug.Log("GameBoardManager : 読み込み失敗");
        }

        //インスタンス生成
        return GameObject.Instantiate(TilePrefab).GetComponent<Tile>();
    }


    private Tile makeTrapTile(){
        //プレハブの読み込み
        var path = rootPath + "TrapTile";
        var TilePrefab = Resources.Load(path);

        if(TilePrefab == null){
            Debug.Log("GameBoardManager : 読み込み失敗");
        }

        //インスタンス生成
        return GameObject.Instantiate(TilePrefab).GetComponent<Tile>();
    }


    private Tile makeWallTile(){
        //プレハブの読み込み
        var path = rootPath + "WallTile";
        var TilePrefab = Resources.Load(path);

        if(TilePrefab == null){
            Debug.Log("GameBoardManager : 読み込み失敗");
        }

        //インスタンス生成
        return GameObject.Instantiate(TilePrefab).GetComponent<Tile>();
    }


    private Tile makeWayTile(){
        //プレハブの読み込み
        var path = rootPath + "WayTile";
        var TilePrefab = Resources.Load(path);

        if(TilePrefab == null){
            Debug.Log("GameBoardManager : 読み込み失敗");
        }

        //インスタンス生成
        return GameObject.Instantiate(TilePrefab).GetComponent<Tile>();
    }
}
