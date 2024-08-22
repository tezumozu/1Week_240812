using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInputMode : InputMode{

    [SerializeField]
    GameBoard gameBoard;

    void Start(){
        gameObject.SetActive(false);
    }

    public override IEnumerator SetActive(bool flag){
        gameObject.SetActive(flag);
        yield return null;
    }

    public void ChangeMode_Map (){
        ChangeMode(E_InGameInputMode.Map);
    }

    public void ChangeMode_GoOn (){
        ChangeMode(E_InGameInputMode.GoOn);
    }
}
