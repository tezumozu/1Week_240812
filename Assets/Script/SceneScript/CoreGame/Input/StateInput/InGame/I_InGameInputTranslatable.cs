using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_InGameInputTranslatable {
    public IObservable<I_OrderExecutionable> PlayerOrderAsync {get;}
    public abstract IEnumerator GetPlayerOrder();
}

