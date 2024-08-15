using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My1WeekGameSystems_ver3;

//入力用のオブジェクトを生成し、共有する

public class StoryInputTranslater<T> : InputTranslater<T> , I_StoryInputTranslater
where T : Enum
{
    public StoryInputTranslater (I_GameStateUpdatable<T> gameManager , T state ) : base( gameManager , state ) {

    }

    protected override void SetActive(bool flag){

    }
}