using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public class ResultState : State{

    private I_ResultGettable resultManager;
    private I_ResultInputTranslater inputManager;
    private I_BlackInable blackOutStaiging;

    public ResultState (I_ResultGettable resultManager , I_ResultInputTranslater inputManager , I_BlackInable blackOutStaiging){
        this.resultManager = resultManager;
        this.inputManager = inputManager;
        this.blackOutStaiging = blackOutStaiging;
    }

    public override IEnumerator UpdateState(){
        Debug.Log("State : Result");
        yield return null;
        finishStateSubject.OnNext(Unit.Default);
    }
}
