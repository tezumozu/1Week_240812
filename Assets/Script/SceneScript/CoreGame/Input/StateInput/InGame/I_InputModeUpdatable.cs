using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_InputModeUpdatable {
    public IObservable<I_OrderExecutionable> PlayerOrderAsync{get;}
    public IObservable<E_InGameInputMode> ChangeInputModeAsync{get;}

    public abstract IEnumerator GetPlayerOrder();
}
