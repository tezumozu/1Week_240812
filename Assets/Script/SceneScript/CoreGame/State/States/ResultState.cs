using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public class ResultState : State{

    private I_ResultGettable resultManager;

    public ResultState (I_ResultGettable resultManager){
        this.resultManager = resultManager;
    }

    public override IEnumerator UpdateState(){
        Debug.Log("State : Result");
        yield return null;
        finishStateSubject.OnNext(Unit.Default);
    }
}
