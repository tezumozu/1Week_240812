using System;
using System.Collections;
using System.Collections.Generic;
using My1WeekGameSystems_ver3;
using UniRx;
using UnityEngine;

public class InGameInputTranslater<T> : InputTranslater<T> , I_InGameInputTranslatable
where T : Enum 
{
    private Subject<I_OrderExecutionable> PlayerOrderSubject = new Subject<I_OrderExecutionable>();
    public IObservable<I_OrderExecutionable> PlayerOrderAsync => PlayerOrderSubject;



    public InGameInputTranslater (I_GameStateUpdatable<T> gameManager , T state) : base(gameManager ,state) {

    }

    public IEnumerator GetPlayerOrder(){
        Debug.Log("InGameInputTranslater : プレイヤーの入力待ち");
        PlayerOrderSubject.OnNext(new TestOrder());
        yield return null;
    }

    protected override void SetActive(bool flag){

    }
}
