using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ブラックアウト、インなど基本的なアニメーションを制御する

public class BlackOutAnimManager : I_BlackInable , I_BlackOutable{
    public IEnumerator StartBlackOut(){
        yield return null;
    }

    public IEnumerator StartBlackIn(){
        yield return null;
    }
}
