using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class StoryState : State{
    private I_StoryGettable storyManager;

    public StoryState (I_StoryGettable storyManager){
        this.storyManager = storyManager;
    }

    public override IEnumerator UpdateState(){
        Debug.Log("State : Story");
        yield return null;
        finishStateSubject.OnNext(Unit.Default);
    }
}
