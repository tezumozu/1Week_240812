using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class StoryGameBoardManager : GameBoardManager , I_StoryGettable {

    private StoryDataList chaperList;
    private Chapter currentChapter ;
    private int currentChapterNum;
    private GameBoardManager gameBoardManager;
    private bool isClear;

    public StoryGameBoardManager(){

        //ストーリーデータを取得する
        var path = "Parameta/Story/StoryDataList";
        chaperList = Resources.Load(path) as StoryDataList;

        //チャプター1のデータを取得
        currentChapterNum = 1;
        isClear = false;

        currentChapter = chaperList.GetChapter(1);

        //プレイヤーがゴールした
        var disposable = gameBoard.PlayerGoalAsync.Subscribe(x => {

            //チャプターをクリアした
            isClear = true;

            //新しいチャプターデータを取得する
            var NextChapter = chaperList.GetChapter(currentChapterNum+1);

            //次のチャプターがなければ
            if(NextChapter == null){
                gameFinishSubject.OnNext(true);
            }
        });

        disposableList.Add(disposable);

        //プレイヤーがクリアした
        disposable = gameBoard.LifeLostAsync.Subscribe(life => {
            if(life <= 0){
                gameFinishSubject.OnNext(false);
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
        gameBoard.CreateObject(dungeon,dungeonMaker.DungeonHight);

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
