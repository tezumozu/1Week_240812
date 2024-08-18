using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public interface I_GameFinishCheckable{
    public IObservable<bool> GameFinishAsync{get;}
    public IObservable<Unit> PlayerGoalAsync{get;}
}
