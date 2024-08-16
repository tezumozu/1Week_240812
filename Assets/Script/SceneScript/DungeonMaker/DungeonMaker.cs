using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class DungeonMaker{
    E_DungeonCell[,] dungeon;
    public int DungeonHight 
    { 
        get;
        private set;
        
    }
    DungeonData param;

    public DungeonMaker(int level){
        DungeonHight = 0;

        //levelが1未満ならlevelを1にする
        if(level < 1){
            level = 1;
        }

        //ダンジョンデータを取得
        var path = "Parameta/Dungeon/DungeonDataList";
        var dataList = Resources.Load(path) as DungeonDataList;

        if(dataList == null){
            Debug.Log("DungeonDataList : データを読み込めませんでした。");
        }

        if(level > dataList.MaxLevel){
            level = dataList.MaxLevel;
        }

        param = dataList.GetDungeonData(level);

        if(param == null){
            Debug.Log("DungeonLevel : " + level + " データなし！");
        }

    }


    public E_DungeonCell[,] MakeDungeon(){

        if(param == null){
            return null;
        }

        //ダンジョンを生成
        var dungeonDigger = new DungeonDigger( param.Level + 1 );
        DungeonHight = dungeonDigger.hight;

        //最低値は2
        var baseDungion = dungeonDigger.DigDungeon();

        dungeon = new E_DungeonCell[ DungeonHight - 2 , DungeonHight - 2 ];

        var wallList = new List<Cell>();
        var wayList = new List<Cell>();

        for(int x = 1; x < DungeonHight - 1; x++){
            for(int y = 1; y < DungeonHight - 1; y++){
                //外壁の内側をコピーする
                dungeon[ x - 1 , y - 1 ] = baseDungion[x,y];

                //壁と道の座標リストを作る
                if(baseDungion[x,y] == E_DungeonCell.Wall){
                   wallList.Add(new Cell(){x = x - 1 , y = y - 1});

                }else if(baseDungion[x,y] == E_DungeonCell.Way){
                    wayList.Add(new Cell(){x = x - 1 , y = y - 1});
                }
                
            }
        }

        //壁の内側分の高さへ変更
        DungeonHight = DungeonHight - 2;


        //普通の道や壁を特殊なマスに置き換える
        //オバケの数を決める
        int ghostNum = param.MaxGohst;
        //オバケの数までランダムな壁をゴーストに置き換える
        for(int i = 0 ; i < ghostNum; i++){
            //ランダムな座標を取得
            var target = wallList[Random.Range(0,wallList.Count)];

            //オバケに置き換える
            dungeon[ target.x , target.y ] = E_DungeonCell.Ghost; 

            //置き換えた場所を削除
            wallList.Remove(target);

        }


        //罠の位置をランダムに決める
        //罠の数を決める
        int trapNum = (int) ( param.MaxTrap * Random.Range( 0.8f , 1.2f ) );
        //罠の数までランダムな壁を罠に置き換える
        for(int i = 0 ; i < trapNum; i++){
            //ランダムな座標を取得
            var target = wallList[Random.Range(0,wallList.Count)];

            //罠に置き換える
            dungeon[ target.x , target.y ] = E_DungeonCell.Trap; 

            //置き換えた場所を削除
            wallList.Remove(target);

        }


        //アイテムをランダムに決める
        //アイテムの数を決める
        int itemNum = (int) ( param.MaxItem * Random.Range( 0.8f , 1.2f ) );
        //アイテムの数までランダムな道をアイテムに置き換える
        for(int i = 0 ; i < itemNum; i++){
            //ランダムな座標を取得
            var target = wayList[Random.Range(0,wayList.Count)];

            dungeon[ target.x , target.y ] = E_DungeonCell.Item;

            //置き換えた場所を削除
            wayList.Remove(target);
        }


        //回復をランダムに決める
        //回復の数を決める
        int cureNum = (int) ( param.MaxCure * Random.Range( 0.8f , 1.2f ) );
        //回復の数までランダムな道を回復に置き換える
        for(int i = 0 ; i < cureNum; i++){
            //ランダムな座標を取得
            var target = wayList[Random.Range(0,wayList.Count)];

            dungeon[ target.x , target.y ] = E_DungeonCell.Cure;

            //置き換えた場所を削除
            wayList.Remove(target);
        }

        return dungeon;
    }


    private class Cell{
        public int x {get; set;}
        public int y {get; set;}
    }


    //デバッグ用
    public override string ToString(){
        string dungeonText = "";

        //外壁を戻す
        for( int x = 0; x < DungeonHight ; x++ ){
            for( int y = 0; y < DungeonHight ; y++ ){
                if(dungeon[x,y] == E_DungeonCell.Wall){

                    dungeonText += "■ ";

                }else if (dungeon[x,y] == E_DungeonCell.Start){

                    dungeonText += "入 ";

                }else if (dungeon[x,y] == E_DungeonCell.Goal){

                    dungeonText += "出 ";
                
                }else if (dungeon[x,y] == E_DungeonCell.Ghost){

                    dungeonText += "霊 ";

                }else if (dungeon[x,y] == E_DungeonCell.Trap){

                    dungeonText += "罠 ";

                }else if (dungeon[x,y] == E_DungeonCell.Item){

                    dungeonText += "具 ";
                
                }else if (dungeon[x,y] == E_DungeonCell.Cure){

                    dungeonText += "癒 ";

                }else{
                    dungeonText += "□ ";
                }
            }

            dungeonText += "\n";
        }

        return dungeonText;
    } 
}
