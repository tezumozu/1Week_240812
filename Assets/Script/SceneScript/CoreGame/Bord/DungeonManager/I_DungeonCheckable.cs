using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_DungeonCheckable{
    public IObservable<bool> FinishDungeonAsync{get;}
}
