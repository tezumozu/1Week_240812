using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public abstract class State : I_StateUpdatable , IDisposable{

    //自身の状態が終了したことを通知する
    protected Subject<Unit> finishStateSubject = new Subject<Unit>();
    public IObservable<Unit> FinishStateAsync{ get{ return finishStateSubject;} }

    protected List<IDisposable> disposableList = new List<IDisposable>();


    public abstract IEnumerator UpdateState();

    public virtual void Dispose(){
        if(disposableList.Count == 0) return;

        //購読の終了
        foreach(var disposable in disposableList){
            disposable.Dispose();
        }
    }
}
