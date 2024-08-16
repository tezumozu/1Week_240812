using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

public class StoryDungeonManager : DungeonManager , I_StoryGettable {

    private StoryDataList chaperList;
    private Chapter currentChapter ;
    private int currentChapterNum;
    private GameBoardManager gameBoardManager;
    private bool isClear;



    public StoryDungeonManager(GameBoardManager gameBoardManager):base(gameBoardManager){

        this.gameBoardManager = gameBoardManager;

        //ストーリーデータを取得する
        var path = "Parameta/Story/StoryDataList";
        chaperList = Resources.Load(path) as StoryDataList;

        //チャプター1のデータを取得
        currentChapterNum = 1;
        isClear = false;

        currentChapter = chaperList.GetChapter(1);


        //ボードの状態を監視
        var disposable = gameBoardManager.LifeLoseAsync.Subscribe(x => {
            if(x <= 0){
                finishDungeonSubject.OnNext(isClear);
            }
        });

        disposableList.Add(disposable);


        disposable = gameBoardManager.PlayerGoalAsync.Subscribe(_ => {
            //チャプターをクリアした
            isClear = true;

            //新しいチャプターデータを取得する
            var NextChapter = chaperList.GetChapter(currentChapterNum+1);

            //次のチャプターがなければ
            if(NextChapter == null){
                finishDungeonSubject.OnNext(isClear);
            }
        });

        disposableList.Add(disposable);
    }



    public override IEnumerator CreateDungeon(){

        //チャプターをクリアしたら次のチャプターを読み込む
        if(isClear){
            isClear = false;
            currentChapterNum++;
            currentChapter = chaperList.GetChapter(currentChapterNum);
        }


        //ダンジョンの生成
        var dungeonMaker = new DungeonMaker(currentChapter.DungeonLevel);
        var dungeon = dungeonMaker.MakeDungeon();


        //ダンジョンのデータを元にオブジェクトを生成する
        gameBoardManager.CleateObject(dungeon,dungeonMaker.DungeonHight);

        yield return null;

    }


    public IReadOnlyCollection<Lines> GetStoryList(){
        //負けなら敗北時ストーリーを渡す
        if(isClear){
            return currentChapter.ChapterTexts;
        }
        return chaperList.LoseTexts;
    }

    public IReadOnlyCollection<Lines> GetProloge(){
        return chaperList.PrologeTexts;
    }
}
