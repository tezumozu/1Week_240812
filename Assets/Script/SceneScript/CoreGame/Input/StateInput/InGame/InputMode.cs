using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public abstract class InputMode : MonoBehaviour , I_InputModeUpdatable{

    protected Subject<I_OrderExecutionable> PlayerOrderSubject = new Subject<I_OrderExecutionable>();
    protected Subject<E_InGameInputMode> ChangeInputModeSubject = new Subject<E_InGameInputMode>();
    public IObservable<I_OrderExecutionable> PlayerOrderAsync => PlayerOrderSubject;
    public IObservable<E_InGameInputMode> ChangeInputModeAsync => ChangeInputModeSubject;

    public abstract void SetActive(bool flag);
    protected void ChangeMode(E_InGameInputMode mode){
        ChangeInputModeSubject.OnNext(mode);
    }
}
