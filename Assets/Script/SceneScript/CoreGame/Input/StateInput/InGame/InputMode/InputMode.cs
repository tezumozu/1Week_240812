using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public abstract class InputMode : MonoBehaviour , I_InputModeUpdatable{

    protected Subject<Unit> takeTurnSubject = new Subject<Unit>();
    protected Subject<E_InGameInputMode> ChangeInputModeSubject = new Subject<E_InGameInputMode>();
    public IObservable<Unit> TakeTurnAsync => takeTurnSubject;
    public IObservable<E_InGameInputMode> ChangeInputModeAsync => ChangeInputModeSubject;

    public abstract void SetActive(bool flag);
    protected void ChangeMode(E_InGameInputMode mode){
        ChangeInputModeSubject.OnNext(mode);
    }
}
