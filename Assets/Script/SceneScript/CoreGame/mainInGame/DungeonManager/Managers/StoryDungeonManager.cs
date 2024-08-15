using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDungeonManager : DungeonManager , I_StoryGettable {

    private StoryDataList chaperList;
    private Chapter currentChapter ;

    public StoryDungeonManager(GameBordManager gameBordManager):base(gameBordManager){

        //ストーリーデータを取得する
        var path = "Parameta/Story/StoryDataList";
        chaperList = Resources.Load(path) as StoryDataList;

        //チャプター1のデータを取得
        currentChapter = chaperList.GetChapter(1);
    }

    public override IEnumerator CreateDungeon(){

        //ダンジョンの生成
        var dungeonMaker = new DungeonMaker(currentChapter.DungeonLevel);
        var dungeon = dungeonMaker.MakeDungeon();

        //ダンジョンのデータを元にオブジェクトを生成する
        
        

        //プレイヤーを生成する
        //すでにプレイヤーオブジェクトがあれば
        //無ければ生成

        yield return null;

    }
}
