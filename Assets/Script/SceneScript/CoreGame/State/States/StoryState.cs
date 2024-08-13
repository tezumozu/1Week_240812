using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

//ストーリー進行時の処理、テキスト表示など
public class StoryState : State{

    I_StoryGettable dungeonManager;

    public StoryState (I_StoryGettable dungeonManager){
        this.dungeonManager = dungeonManager;
    }

    public override IEnumerator UpdateState(){
        Debug.Log("State : Story");
        yield return null;
        finishStateSubject.OnNext(Unit.Default);
    }
}
