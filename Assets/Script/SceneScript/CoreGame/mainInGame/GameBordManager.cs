using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//プレイヤーの駒やタイルを管理する、盤面の状態を報告する
public class GameBordManager : I_GameBordChackable , I_BordAnimControlable{

    //ボードを見渡す際のカメラとの距離
    float CameraRange = 10;
    GameObject Bord;
    GameObject PlayerObject;

    //プレイヤーのライフがなくなったことを通知する
    //プレイヤーがゴールしたことを通知する

    public GameBordManager(){
        //Plyerやボードを生成する

    }

    public void CleateObject( E_DungeonCell[,] data , int hight ){

    }
}
