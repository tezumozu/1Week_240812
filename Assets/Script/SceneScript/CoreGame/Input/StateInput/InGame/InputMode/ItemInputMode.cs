using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInputMode : InputMode{

    [SerializeField]
    GameBoard gameBoard;

    void Start(){
        SetActive(false);
    }

    public override void SetActive(bool flag){
        gameObject.SetActive(flag);
    }

    public void ChangeMode_Map (){
        ChangeMode(E_InGameInputMode.Map);
    }

    public void ChangeMode_GoOn (){
        ChangeMode(E_InGameInputMode.GoOn);
    }
}
