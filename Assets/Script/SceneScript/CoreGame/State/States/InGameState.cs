using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public class InGameState : State{

    I_TimeMeasurable Timer;
    I_GameObjectGettable dungeonManager;

    public InGameState (I_GameObjectGettable dungeonManager, I_TimeMeasurable Timer){
        this.Timer = Timer;
        this.dungeonManager = dungeonManager;
    }

    public override IEnumerator UpdateState(){
        Debug.Log("State : InGame");
        yield return null;
        finishStateSubject.OnNext(Unit.Default);
    }
    
}
