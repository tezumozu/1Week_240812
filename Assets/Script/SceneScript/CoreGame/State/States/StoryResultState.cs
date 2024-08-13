using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

//ストーリー用のリザルト状態、

public class StoryResultState : State{

    I_ResultGettable resultManager;

    public StoryResultState (I_ResultGettable resultManager){
        this.resultManager = resultManager;
    }

    public override IEnumerator UpdateState(){
        Debug.Log("State : Story");
        yield return null;
        finishStateSubject.OnNext(Unit.Default);
    }
}
