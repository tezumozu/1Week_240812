using System;
using System.Collections;
using System.Collections.Generic;
using My1WeekGameSystems_ver3;
using UnityEngine;

public class ResultInputTranslater<T> : InputTranslater<T> , I_ResultInputTranslater
where T : Enum
{
    public ResultInputTranslater (I_GameStateUpdatable<T> gameManager , T state) : base(gameManager ,state) {

    }

    protected override void SetActive(bool flag){

    }
}
