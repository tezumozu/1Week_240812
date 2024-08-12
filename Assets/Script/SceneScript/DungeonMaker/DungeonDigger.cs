using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

//穴掘り法でダンジョンを生成する
public unsafe class DungeonDigger{

    public readonly int level;
    public readonly int hight;
    private E_DungeonCell[,] dungeon;

    private Cell goalCell; 

    private List<Cell> startCellList;

    //進める方向リストを作成
    List<Direction> directionList;

   
    public DungeonDigger (int level){
            //高さ ( level + 1 ) * 2 + 1 の正方形の迷路を作成する

            //levelが1未満ならlevelを1にする
            if(level < 1){
                level = 1;
            }

            //5以上の奇数
            hight = ( level + 1 ) * 2 + 1;

            dungeon = new E_DungeonCell[hight,hight]; 

            startCellList = new List<Cell>();
            directionList = new List<Direction>();
    }


    //ダンジョンを生成する
    public E_DungeonCell[,] DigDungeon(){

        //ダンジョンを初期化する
        for( int x = 0; x < hight; x++ ){
            for( int y = 0; y < hight; y++ ){
                if( x == 0 || y == 0 || x == hight - 1 || y == hight - 1){

                    //外周を一度通路にする
                    dungeon[x,y] = E_DungeonCell.Way;

                }else {

                    //それ以外を壁にする
                    dungeon[x,y] = E_DungeonCell.Wall;
                }
            }
        }

        //ゴール地点を初期化する(スタート地点を設定する)
        goalCell = new Cell () {x = 1 , y = 1 , distance = 0};

        //ダンジョンを作る
        Dig(goalCell);


        //外壁を戻す
        for( int x = 0; x < hight; x++ ){
            for( int y = 0; y < hight; y++ ){
                if( x == 0 || y == 0 || x == hight - 1 || y == hight - 1){

                    //外周を一度通路にする
                    dungeon[x,y] = E_DungeonCell.Wall;

                }
            }
        }

        //スタート地点を追加する
        dungeon[1,1] = E_DungeonCell.Start;

        //ゴール地点を追加する
        dungeon[ goalCell.x , goalCell.y ] = E_DungeonCell.Goal;

        return dungeon;
    }


    //指定されたマスからランダムな方向に2マスずつ進み、掘れなくなるまで行う
    private void Dig(Cell target){

        while (true){

            //進める方向を確認する

            //上方向に進めるか ( 進みたい方向の2マスが壁か確認 )
            if (this.dungeon[target.x , target.y - 1] == E_DungeonCell.Wall && dungeon[target.x, target.y - 2] == E_DungeonCell.Wall)
                directionList.Add(Direction.Up);

            //下方向に進めるか ( 進みたい方向の2マスが壁か確認 )
            if (this.dungeon[target.x , target.y + 1] == E_DungeonCell.Wall && dungeon[target.x, target.y + 2] == E_DungeonCell.Wall)
                directionList.Add(Direction.Down);

            //右方向に進めるか ( 進みたい方向の2マスが壁か確認 )
            if (this.dungeon[target.x + 1 , target.y] == E_DungeonCell.Wall && dungeon[target.x + 2, target.y] == E_DungeonCell.Wall)
                directionList.Add(Direction.Right);

            //上方向に進めるか ( 進みたい方向の2マスが壁か確認 )
            if (this.dungeon[target.x - 1 , target.y] == E_DungeonCell.Wall && dungeon[target.x -2 , target.y] == E_DungeonCell.Wall)
                directionList.Add(Direction.Left);


            //もしどの方向も進めないなら穴掘り終了
            if(directionList.Count == 0) break;

            //もし進めるならランダムに進む方向を決定
            Direction dir = directionList[Random.Range(0,directionList.Count)];

            //指定されたマスを掘る( 再開箇所候補に追加する )
            makeWay(target);

            //使いまわすために空にする
            directionList.Clear();

            //進行方向を掘る
            switch (dir) {
                case Direction.Up:

                    target = new Cell() { x = target.x , y = target.y - 1 , distance = target.distance + 1 };
                    makeWay(target);

                    target = new Cell() { x = target.x , y = target.y - 1 , distance = target.distance + 1 };
                    makeWay(target);

                    break;


                case Direction.Down:

                    target = new Cell() { x = target.x , y = target.y + 1 , distance = target.distance + 1 };
                    makeWay(target);

                    target = new Cell() { x = target.x , y = target.y + 1 , distance = target.distance + 1 };
                    makeWay(target);

                    break;


                case Direction.Right:

                    target = new Cell() { x = target.x + 1 , y = target.y , distance = target.distance + 1 };
                    makeWay(target);

                    target = new Cell() { x = target.x + 1 , y = target.y , distance = target.distance + 1 };
                    makeWay(target);
                    
                    break;


                case Direction.Left:

                     target = new Cell() { x = target.x - 1 , y = target.y , distance = target.distance + 1 };
                    makeWay(target);

                    target = new Cell() { x = target.x - 1 , y = target.y , distance = target.distance + 1 };
                    makeWay(target);

                    break;
            }
        }

        //掘り進めなくなったらすでに掘った別のマスから穴掘り再開
        Cell startCell = getStartCell();

        if(startCell != null) Dig(startCell);


    }


    private void makeWay(Cell target){
        //ターゲットの位置をWayに変更
        dungeon[ target.x , target.y ] = E_DungeonCell.Way;

        //ゴール地点の更新
        if(goalCell.distance < target.distance){
            goalCell = target;
        }

        //x,yどちらも奇数なら
        if(target.x % 2 == 1 && target.y % 2 ==1){
            startCellList.Add(target);
        }
    }


    private Cell getStartCell(){
        if(startCellList.Count == 0){
            return null;
        }

        int randIndex = Random.Range(0,startCellList.Count);
        var result = startCellList[randIndex];
        startCellList.Remove(result);
        return result;
    }


    //デバッグ用
    public override string ToString(){
        string dungeonText = "";

        //外壁を戻す
        for( int x = 0; x < hight; x++ ){
            for( int y = 0; y < hight; y++ ){
                if(dungeon[x,y] == E_DungeonCell.Wall){

                    dungeonText += "■ ";

                }else if (dungeon[x,y] == E_DungeonCell.Start){

                    dungeonText += "入 ";

                }else if (dungeon[x,y] == E_DungeonCell.Goal){

                    dungeonText += "出 ";

                }else{
                    dungeonText += "□ ";
                }
            }

            dungeonText += "\n";
        }

        return dungeonText;
    } 




    private class Cell{
        public int x { get; set;}
        public int y { get; set;}
        public int distance { get; set;}
    }

    private enum Direction{
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }
}
