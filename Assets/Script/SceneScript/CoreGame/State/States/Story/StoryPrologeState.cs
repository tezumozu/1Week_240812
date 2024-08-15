using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public class StoryPrologeState : State{
    private I_StoryGettable storyManager;
    private I_StoryInputTranslater inputManager;

    public StoryPrologeState(I_StoryGettable storyManager , I_StoryInputTranslater inputManager){
        this.storyManager = storyManager;
        this.inputManager = inputManager;
    }

    public override IEnumerator UpdateState(){
        Debug.Log("State : Prologe");
        yield return null;
        finishStateSubject.OnNext(Unit.Default);
    }
}
