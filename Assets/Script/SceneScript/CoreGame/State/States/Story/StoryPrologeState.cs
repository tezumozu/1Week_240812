using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;
using My1WeekGameSystems_ver3;

public class StoryPrologeState : State{
    private I_StoryGettable storyManager;
    private I_StoryInputTranslater inputManager;
    private I_TextBoxUpdatable textBox;
    private BlackOutAnimManager blackOutAnimManager;

    public StoryPrologeState(I_StoryGettable storyManager , I_StoryInputTranslater inputManager ,BlackOutAnimManager blackOutAnimManager , I_TextBoxUpdatable textBox){
        this.storyManager = storyManager;
        this.inputManager = inputManager;
        this.blackOutAnimManager = blackOutAnimManager;
        this.textBox = textBox;
    }

    public override IEnumerator UpdateState(){
        //ストーリーデータを取得する
        var prologeLines = storyManager.GetProloge();


        //テキストBoxを有効にする
        textBox.SetActive(true);


        //ブラックイン
        var coroutine = blackOutAnimManager.StartBlackIn();
        CoroutineHander.OrderStartCoroutine(coroutine);

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }



        //テキストを表示
        foreach(var text in prologeLines){

            //テキストを更新する
            textBox.UpdateText(text);


            //入力待ちする(クリック待ち)
            coroutine = inputManager.WaitPlayerInput();
            CoroutineHander.OrderStartCoroutine(coroutine);

            while(!CoroutineHander.isFinishCoroutine(coroutine)){
                yield return null;
            }


        }



        //ブラックアウト
        coroutine = blackOutAnimManager.StartBlackOut();
        CoroutineHander.OrderStartCoroutine(coroutine);

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }

        //テキストBoxを消す
        textBox.SetActive(false);

        finishStateSubject.OnNext(Unit.Default);
    }
}
