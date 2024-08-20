using System;
using System.Collections;
using System.Collections.Generic;
using My1WeekGameSystems_ver3;
using UniRx;
using UnityEngine;

public class InGameTurnManager<T> : StateObjectManager<T> , I_InGameTurnUpdatable
where T : Enum 
{
    private Subject<I_OrderExecutionable> PlayerOrderSubject = new Subject<I_OrderExecutionable>();
    public IObservable<I_OrderExecutionable> PlayerOrderAsync => PlayerOrderSubject;

    private E_InGameInputMode currentMode;
    private Dictionary< E_InGameInputMode , I_InputModeUpdatable > InputModeDic;

    bool isTakeTurn;


    public InGameTurnManager (I_GameStateUpdatable<T> gameManager , T state) : base(gameManager ,state) {

        currentMode = E_InGameInputMode.GoOn;
        InputModeDic = new Dictionary<E_InGameInputMode, I_InputModeUpdatable>();

        isTakeTurn = false;

        //オブジェクトの取得
        var Canvas = GameObject.Find("Canvas/InGame");
        var goon = Canvas.transform.Find("GoOn").GetComponent< I_InputModeUpdatable >();
        var item = Canvas.transform.Find("Item").GetComponent< I_InputModeUpdatable >();
        var map = Canvas.transform.Find("Map").GetComponent< I_InputModeUpdatable >();

        InputModeDic.Add(E_InGameInputMode.GoOn,goon);
        InputModeDic.Add(E_InGameInputMode.Item,item);
        InputModeDic.Add(E_InGameInputMode.Map,map);


        var disposable = goon.ChangeInputModeAsync.Subscribe((mode) => {
            ToActiveMode(mode);
        });

        disposableList.Add(disposable);


        disposable = item.ChangeInputModeAsync.Subscribe((mode) => {
            ToActiveMode(mode);
        });

        disposableList.Add(disposable);


        disposable = map.ChangeInputModeAsync.Subscribe((mode) => {
            ToActiveMode(mode);
        });

        disposableList.Add(disposable);
    

        //ターンの経過を監視する
        disposable = goon.TakeTurnAsync.Subscribe(_ => {
            isTakeTurn = true;
        });

        disposableList.Add(disposable);


        disposable = item.TakeTurnAsync.Subscribe(_ => {
            isTakeTurn = true;
        });

        disposableList.Add(disposable);


        disposable = map.TakeTurnAsync.Subscribe(_ => {
            isTakeTurn = true;
        });

        disposableList.Add(disposable);
    }




    public IEnumerator TakeTurn(){
        Debug.Log("InGameInputTranslater : ターン開始");
        //PlayerOrderSubject.OnNext(new TestOrder());

        //デフォルトのモード
        ToActiveMode(E_InGameInputMode.GoOn);

        while(!isTakeTurn){
            yield return null;
        }

        isTakeTurn = false;
    }

    protected override void SetActive(bool flag){

    }

    public void ToActiveMode(E_InGameInputMode mode){
        //現在の入力UIを非表示 
        InputModeDic[currentMode].SetActive(false);

        //指定された入力モードを有効にする
        currentMode = mode;
        InputModeDic[currentMode].SetActive(true);
    }
}
