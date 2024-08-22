using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public interface I_GameBoardChackable {
    public IObservable<int> LifeLostAsync {get;}
    public IObservable<Unit> PlayerGoalAsync {get;}

}
