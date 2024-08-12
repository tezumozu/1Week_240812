using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class DungeonMaker{
    DungeonDigger dungeonDigger;
    E_DungeonCell[,] dungeon;

    public DungeonMaker(int level){
        //levelが2未満ならlevelを2にする
        if(level < 1){
            level = 1;
        }
        dungeonDigger = new DungeonDigger( level + 1 );
        dungeon = new E_DungeonCell[ dungeonDigger.hight - 2 , dungeonDigger.hight - 2 ];
    }


    public E_DungeonCell[,] MakeDungeon(){

        //最低値は2
        
        var baseDungion = dungeonDigger.DigDungeon();

        var wallList = new List<Cell>();
        var wayList = new List<Cell>();

        for(int x = 1; x < dungeonDigger.hight - 1; x++){
            for(int y = 1; y < dungeonDigger.hight - 1; y++){
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

        //オバケの位置をランダムに決める
        //オバケの数を決める
        int ghostNum = wallList.Count / 10 + 1;
        Debug.Log( "霊 : " + ghostNum );
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
        int trapNum = wallList.Count / 9 + 1;
        Debug.Log( "罠 : " + trapNum );
        //罠の数までランダムな壁を罠に置き換える
        for(int i = 0 ; i < trapNum; i++){
            //ランダムな座標を取得
            var target = wallList[Random.Range(0,wallList.Count)];

            //罠に置き換える
            dungeon[ target.x , target.y ] = E_DungeonCell.Trap; 

            //置き換えた場所を削除
            wallList.Remove(target);

        }

        //アイテムや回復の位置をランダムに決める
        //アイテムや回復の数を決める
        int racNum = wayList.Count / 10 + 1;
        //アイテムや回復の数までランダムな壁をアイテムや回復に置き換える
        for(int i = 0 ; i < racNum; i++){
            //ランダムな座標を取得
            var target = wayList[Random.Range(0,wayList.Count)];

            //回復かアイテムに置き換える
            if(Random.Range(0.0f,1.0f) < 0.5f){
                dungeon[ target.x , target.y ] = E_DungeonCell.Item;
            }else{
                dungeon[ target.x , target.y ] = E_DungeonCell.Cure;
            }

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
        for( int x = 0; x < dungeonDigger.hight-2; x++ ){
            for( int y = 0; y < dungeonDigger.hight-2; y++ ){
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
