using System.Collections;
using System.Collections.Generic;
using My1WeekGameSystems_ver3;
using UnityEngine;

public class MapInputMode : InputMode{
    
    [SerializeField]
    GameObject GameCamera;
    
    void Start(){
        gameObject.SetActive(false);
    }

    public override IEnumerator SetActive(bool flag){
        gameObject.SetActive(flag);

        //カメラを切り替える
        var gameBoard = GameObject.Find("GameBoard").GetComponent<GameBoard>();

        IEnumerator coroutine;
        
        if(flag){
            coroutine = gameBoard.TargetThis(GameCamera);
            CoroutineHander.OrderStartCoroutine(coroutine);

        }else{
            coroutine = gameBoard.currentTile.TargetThis(GameCamera);
            CoroutineHander.OrderStartCoroutine(coroutine);
        }

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }
    }

    public void ChangeMode_Item (){
        ChangeMode(E_InGameInputMode.Item);
    }

    public void ChangeMode_GoOn (){
        ChangeMode(E_InGameInputMode.GoOn);
    }
}
