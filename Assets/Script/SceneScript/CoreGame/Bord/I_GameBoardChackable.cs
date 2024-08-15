using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_GameBoardChackable {
    public IObservable<int> LifeLoseAsync {get;}
    public IObservable<int> PlayerGoalAsync {get;}

}
