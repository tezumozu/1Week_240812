using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public abstract class State : I_StateUpdatable{

    //自身の状態が終了したことを通知する
    protected Subject<Unit> finishStateSubject = new Subject<Unit>();
    public IObservable<Unit> FinishStateAsync{ get{ return finishStateSubject;} }

    public abstract IEnumerator UpdateState();
}
