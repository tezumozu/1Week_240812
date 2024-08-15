using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class StoryState : State{
    private I_StoryGettable storyManager;
    private I_StoryInputTranslater inputManager;

    public StoryState (I_StoryGettable storyManager , I_StoryInputTranslater inputManager){
        this.storyManager = storyManager;
        this.inputManager = inputManager;
    }

    public override IEnumerator UpdateState(){
        Debug.Log("State : Story");
        yield return null;
        finishStateSubject.OnNext(Unit.Default);
    }
}
