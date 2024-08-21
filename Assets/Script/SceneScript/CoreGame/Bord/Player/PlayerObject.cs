using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : AnimMono<E_PlayerAnim>{

    public void InitObject(){
        Debug.Log("test");
        AnimList[E_PlayerAnim.MoveObject] = moveTileAnim;
    }

    public IEnumerable ToMoveTile(Tile tile){
        yield return null;
    }

    private IEnumerator moveTileAnim(){
        //すでに再生中ならリターン
        if(!isAnimFin) yield break;

        isAnimFin = false;
        animator.SetTrigger("MoveTile");

        while(!isAnimFin){
            yield return null;
        }
    }


}
