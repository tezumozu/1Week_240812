using System.Collections;
using System.Collections.Generic;
using My1WeekGameSystems_ver3;
using UnityEngine;

public class GameBoardAnim : AnimManager<E_BoardAnim>{

    GameBoard gameBoard;

    public GameBoardAnim(){

        gameBoard = GameObject.Find("GameBoard").GetComponent<GameBoard>();

        AnimList[E_BoardAnim.CreateBoardAnim] = CreateBoardAnim;
    }

    private IEnumerator CreateBoardAnim(){

        //アニメーション終了待機
        var coroutine = gameBoard.StartAnim(E_BoardAnim.CreateBoardAnim);
        CoroutineHander.OrderStartCoroutine(coroutine);

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }

    }

}
