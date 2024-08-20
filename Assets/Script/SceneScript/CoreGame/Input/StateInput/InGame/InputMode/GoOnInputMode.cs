using System.Collections;
using System.Collections.Generic;
using My1WeekGameSystems_ver3;
using UniRx;
using UnityEngine;

public class GoOnInputMode : InputMode{
    
    [SerializeField]
    GameBoard gameBoard;

    bool isInputActive;

    void Start(){
        gameObject.SetActive(false);

        //タイルがクリックされたら
        gameBoard.ClickedTileAsync.Subscribe(Tile => {
            
            //現在アクティブでないなら
            if(isInputActive) return;

            //連続で入力されないようにUIをオフにする
            SetActive(false);

            //Tileごとに処理をする
            var coroutine = TileEffectExecution(Tile);
            CoroutineHander.OrderStartCoroutine(coroutine);
            

        }).AddTo(this);

    }

    public override void SetActive(bool flag){
        gameObject.SetActive(flag);
        
        //クリックできるタイルをクリックできる状態にする
        gameBoard.SetClickable(flag);
    }

    public void ChangeMode_Item (){
        ChangeMode(E_InGameInputMode.Item);
    }

    public void ChangeMode_Map (){
        ChangeMode(E_InGameInputMode.Map);
    }


    private IEnumerator TileEffectExecution(I_TileEffectable tile){

        var coroutine = tile.TileEffect();
        CoroutineHander.OrderStartCoroutine(coroutine);

        while(CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }

        Debug.Log("Test");

        takeTurnSubject.OnNext(Unit.Default);
    }
}
