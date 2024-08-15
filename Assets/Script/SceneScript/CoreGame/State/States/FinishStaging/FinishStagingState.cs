using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class FinishStagingState : State{

    I_BlackOutable blackOutAnimManager;

    public FinishStagingState (I_BlackOutable animManager){
        blackOutAnimManager = animManager;
    }

    public override IEnumerator UpdateState(){
        Debug.Log("State : FinsihStaging");
        yield return null;
        finishStateSubject.OnNext(Unit.Default);
    }
}
