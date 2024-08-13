using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_StateUpdatable {

    //各状態における処理を実装する
    public abstract IEnumerator UpdateState();
}
