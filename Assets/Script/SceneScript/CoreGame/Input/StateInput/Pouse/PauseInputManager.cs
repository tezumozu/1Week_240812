using System;
using System.Collections;
using System.Collections.Generic;
using My1WeekGameSystems_ver3;
using UniRx;
using UnityEngine;

//ポーズに関する入力を取得する(特殊)

public class PauseInputTranslater<T> : StateObjectManager<T> , I_PauseInputTranslatable where T : Enum   {


    private Subject<bool> pauseInputSubject = new Subject<bool>();
    public IObservable<bool> PauseInputAsync => pauseInputSubject;

    public PauseInputTranslater (I_GameStateUpdatable<T> gameManager , T state) : base(gameManager ,state) {

    }

    protected override void SetActive(bool flag){

    }
}
