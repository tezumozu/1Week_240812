using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameAnim : AnimManager<E_StartGameAnim>{
    
    public StartGameAnim(){
        AnimList = new Dictionary<E_StartGameAnim, coroutineMethod >();

        //アニメーションの登録
        AnimList[E_StartGameAnim.StartGame] = PlayStartAnimation;
    }

    private IEnumerator PlayStartAnimation(){
        yield return null;
    }

}
