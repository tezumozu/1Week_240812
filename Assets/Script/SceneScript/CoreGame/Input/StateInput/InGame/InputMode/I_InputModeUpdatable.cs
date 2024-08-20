using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public interface I_InputModeUpdatable {
    public IObservable<Unit> TakeTurnAsync {get;}
    public IObservable<E_InGameInputMode> ChangeInputModeAsync{get;}

    public abstract void SetActive(bool flag);
}
