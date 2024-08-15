using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//実行する処理の流れを表す仮装クラス
public abstract class OrderProcess : I_OrderExecutionable{
    public abstract IEnumerator OrderExecution();
}
