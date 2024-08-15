using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public class InGameState : State{

    I_TimeMeasurable Timer;
    I_GameBordChackable bordManager;

    public InGameState (I_GameBordChackable bordManager, I_TimeMeasurable Timer){
        this.Timer = Timer;
        this.bordManager = bordManager;
    }

    public override IEnumerator UpdateState(){
        Debug.Log("State : InGame");
        yield return null;
        finishStateSubject.OnNext(Unit.Default);
    }
    
}
