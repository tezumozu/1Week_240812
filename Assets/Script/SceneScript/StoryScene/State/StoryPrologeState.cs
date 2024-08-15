using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public class StoryPrologeState : State{
    private I_StoryGettable storyManager;

    public StoryPrologeState (I_StoryGettable storyManager){
        this.storyManager = storyManager;
    }

    public override IEnumerator UpdateState(){
        Debug.Log("State : Prologe");
        yield return null;
        finishStateSubject.OnNext(Unit.Default);
    }
}
