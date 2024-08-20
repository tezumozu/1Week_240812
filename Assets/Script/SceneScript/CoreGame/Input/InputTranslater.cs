using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using My1WeekGameSystems_ver3;
using UnityEngine;

using UniRx;

//自信が有効となる状態を把握し、その状態で扱うGameObjectを管理する

public abstract class StateObjectManager<T> : IDisposable 
where T: Enum
{

    //入力が有効になる状態のリスト
    HashSet<T> isActiveStateList;
    protected List<IDisposable> disposableList;

    protected StateObjectManager (I_GameStateUpdatable<T> gameManager , T activeState){

        disposableList = new List<IDisposable>();

        isActiveStateList = new HashSet<T>();
        //デフォルトのアクティブ状態を設定
        isActiveStateList.Add(activeState);

        //状態遷移が起きた時、自身がアクティブになるか確認
        var disposable = gameManager.UpdateStateAsync.Subscribe( state => {

            foreach(var activeState in isActiveStateList){
                
                //文字列に変換してエラー回避
                if(state.ToString() == activeState.ToString()){
                    SetActive(true);
                    return;
                }
            }

            SetActive(false);
        });

        disposableList.Add(disposable);
    }

    public void AddActiveState(T state){
        isActiveStateList.Add(state);
    }

    protected abstract void SetActive(bool flag);

    public virtual void Dispose(){
        if(disposableList.Count == 0) return;

        //購読の終了
        foreach(var disposable in disposableList){
            disposable.Dispose();
        }
    }
}
