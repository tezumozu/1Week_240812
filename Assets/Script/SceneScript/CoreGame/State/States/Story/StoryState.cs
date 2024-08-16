using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using My1WeekGameSystems_ver3;

public class StoryState : State{
    private I_StoryGettable storyManager;
    private I_StoryInputTranslater inputManager;
    private I_TextBoxUpdatable textBox;

    public StoryState (I_StoryGettable storyManager , I_StoryInputTranslater inputManager , I_TextBoxUpdatable textBox){
        this.storyManager = storyManager;
        this.inputManager = inputManager;
        this.textBox = textBox;
    }

    public override IEnumerator UpdateState(){

        //ストーリーデータを取得する
        var StoryLines = storyManager.GetStoryList();
        IEnumerator coroutine;

        //テキストを表示
        foreach(var text in StoryLines){

            //テキストを更新する
            textBox.UpdateText(text);


            //入力待ちする(クリック待ち)
            coroutine = inputManager.WaitPlayerInput();
            CoroutineHander.OrderStartCoroutine(coroutine);

            while(!CoroutineHander.isFinishCoroutine(coroutine)){
                yield return null;
            }


        }

        //テキストBoxを消す
        textBox.SetActive(false);

        finishStateSubject.OnNext(Unit.Default);
    }
}
