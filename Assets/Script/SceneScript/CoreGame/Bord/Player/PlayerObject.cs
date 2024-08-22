using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using My1WeekGameSystems_ver3;
using Zenject.Asteroids;

public class PlayerObject : AnimMono<E_PlayerAnim>{

    public void InitObject(){
        AnimList[E_PlayerAnim.MoveObject] = moveTileAnim;
        AnimList[E_PlayerAnim.SetPlayer] = setPlayerAnim;
        AnimList[E_PlayerAnim.Init] = initPlayerAnim;
    }

    public IEnumerator ToMoveTile(Tile tile){

        //向きを変更
        var vec = transform.position - tile.transform.position;
        var dot = Vector3.Dot(new Vector3 (0,0,1.0f) , vec);
        Debug.Log(dot);
        //左右の移動か
        if(dot < 0.5 && dot > - 0.5){

            if( transform.position.x < tile.transform.position.x ){
                transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);

            }else if( transform.position.x > tile.transform.position.x ){
                transform.rotation = Quaternion.Euler(0.0f,180.0f,0.0f);

            }

        }else{

            if( transform.position.z < tile.transform.position.z ){
            transform.rotation = Quaternion.Euler(0.0f,270.0f,0.0f);

            }else if( transform.position.z > tile.transform.position.z ){
                transform.rotation = Quaternion.Euler(0.0f,90.0f,0.0f);
            }

        }
        

        //移動自身を移動させながらアニメーションを再生
        var coroutine = StartAnim(E_PlayerAnim.MoveObject);
        CoroutineHander.OrderStartCoroutine(coroutine);


        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }
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

    private IEnumerator setPlayerAnim(){
        //すでに再生中ならリターン
        if(!isAnimFin) yield break;

        isAnimFin = false;
        animator.SetTrigger("SetPlayer");
        
        while(!isAnimFin){
            yield return null;
        }

    }

    private IEnumerator initPlayerAnim(){
        //すでに再生中ならリターン
        if(!isAnimFin) yield break;

        animator.SetTrigger("Init");
    }

}
