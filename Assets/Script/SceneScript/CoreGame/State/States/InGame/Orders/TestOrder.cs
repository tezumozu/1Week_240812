using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOrder : OrderProcess{
    public override IEnumerator OrderExecution(){
        Debug.Log("TestOrder : いろいろ処理");
        yield return null;
    }
}
