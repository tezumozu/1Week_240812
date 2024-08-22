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
            if(!isInputActive) return;

            //連続で入力されないようにオフにする
            var coroutine = SetActive(false);
            CoroutineHander.OrderStartCoroutine(coroutine);

            //Tileごとに処理をする
            coroutine = TileEffectExecution(Tile);
            CoroutineHander.OrderStartCoroutine(coroutine);
            
        }).AddTo(this);

    }

    public override IEnumerator SetActive(bool flag){
        yield return null;

        //自身を有効・無効にする
        isInputActive = flag;
        gameObject.SetActive(flag);
        
        //クリックできるタイルをクリックできる状態を変更する
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

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }

        takeTurnSubject.OnNext(Unit.Default);
    }
}
