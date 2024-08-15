using System;
using System.Collections;
using System.Collections.Generic;
using My1WeekGameSystems_ver3;
using UnityEngine;

public abstract class AnimManager<T_AnimName> : I_AnimContlorable<T_AnimName>
where T_AnimName : Enum
{

    protected delegate IEnumerator coroutineMethod();

    protected Dictionary< T_AnimName , coroutineMethod > AnimList;

    public IEnumerator StartAnim(T_AnimName name){
        //アニメーションのコルーチンを起動
        IEnumerator coroutine = AnimList[name]();
        CoroutineHander.OrderStartCoroutine( coroutine );

        //アニメーションが終わるまで待機
        while(CoroutineHander.isFinishCoroutine( coroutine )){
            yield return null;
        }
    }
}
