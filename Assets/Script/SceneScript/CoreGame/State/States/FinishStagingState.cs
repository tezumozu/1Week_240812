using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class FinishStagingState : State{

    public FinishStagingState (){
    }

    public override IEnumerator UpdateState(){
        Debug.Log("State : FinsihStaging");
        yield return null;
        finishStateSubject.OnNext(Unit.Default);
    }
}
