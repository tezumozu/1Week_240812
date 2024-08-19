using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GoOnInputMode : InputMode{
    
    [SerializeField]
    GameBoard gameBoard;

    void Start(){
        gameObject.SetActive(false);
    }

    public override void SetActive(bool flag){
        gameObject.SetActive(flag);
        
        //クリックできるタイルをクリックできる状態にする
        gameBoard.SetIsClickable(flag);
    }

    public void ChangeMode_Item (){
        ChangeMode(E_InGameInputMode.Item);
    }

    public void ChangeMode_Map (){
        ChangeMode(E_InGameInputMode.Map);
    }
}
