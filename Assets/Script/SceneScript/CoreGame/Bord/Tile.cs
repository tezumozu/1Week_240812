using System;
using System.Collections;
using System.Collections.Generic;
using My1WeekGameSystems_ver3;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Zenject.Asteroids;

public abstract class Tile :
AnimMono<E_TileAnim> ,
I_CameraTargettable ,
I_FactoryMakable , 
I_TileEffectable , 
IPointerClickHandler , 
IPointerEnterHandler , 
IPointerExitHandler {

    [SerializeField]
    protected float CameraRange = 3.0f;

    [SerializeField]
    protected GameObject MouseOverEffect;

    protected List<Tile> relatedTileList = new List<Tile>();
    public IReadOnlyCollection<Tile> RelatedTileList => relatedTileList;


    public E_DungeonCell TileType {
        get;
        protected set;
    }

    protected bool isClickable;
    protected bool isTurned;
    protected bool isWalkable;


    protected Subject<Tile> ClickSubject = new Subject<Tile>();
    public IObservable<Tile> ClickAsync => ClickSubject;

    //プレイヤーがタイルを踏んだ
    protected Subject<Unit> stepedOnSubject = new Subject<Unit>();
    public IObservable<Unit> StepedOnAsync => stepedOnSubject;

    private void Start(){
        isClickable = false;
        isTurned = false;
        isWalkable = false;
        TileType = E_DungeonCell.Way;

        MouseOverEffect.SetActive(false);
        
        AnimList.Add(E_TileAnim.CreateTile,creatTile);
        AnimList.Add(E_TileAnim.ClearTile,clearTile);
        AnimList.Add(E_TileAnim.TurnOver,turnOverTile);

        InitTile();
    }


    //Tile固有

    //タイルの初期化
    protected abstract void InitTile();

    //タイルが裏返されたときの効果
    public virtual IEnumerator TileEffect(){
        IEnumerator coroutine = null;
        
        //タイルがひっくりかえってなかったらひっくり返す
        if(!isTurned){
            isTurned = true;

            coroutine = StartAnim(E_TileAnim.TurnOver);
            CoroutineHander.OrderStartCoroutine(coroutine);

            while(!CoroutineHander.isFinishCoroutine(coroutine)){
                yield return null;
            }


        }


        //タイルごとの処理
        coroutine = TileClickEffect();
        CoroutineHander.OrderStartCoroutine(coroutine);

        while(!CoroutineHander.isFinishCoroutine(coroutine)){
            yield return null;
        }


        if(isWalkable){
            var gameBoard = GameObject.Find("GameBoard").GetComponent<GameBoard>();

            coroutine = gameBoard.MovePlayer(this);
            CoroutineHander.OrderStartCoroutine(coroutine);

            while(!CoroutineHander.isFinishCoroutine(coroutine)){
                yield return null;
            }
        }

    }

    protected abstract IEnumerator TileClickEffect();
    

    //周辺のタイルを覚えておく
    public void AddRelatedTile(Tile relatedTile){
        relatedTileList.Add(relatedTile);
    }



    //I_CameraTargetable
    //カメラを特定の位置に移動させる
    public virtual IEnumerator TargetThis(GameObject camera){

        var point = (float) Math.Sqrt(2) * CameraRange;
        var NextPos = transform.position;
        NextPos += new Vector3( 0.0f , point , -point );

        while( Vector3.Distance( camera.transform.position , NextPos ) * 0.3f > 0.001f  ){
            camera.transform.position += (NextPos - camera.transform.position) * 0.3f;
            yield return null;
        }

        camera.transform.position = NextPos;
    }




    


    //Event系
    //クリック可能か設定する
    public virtual void SetIsClickable(bool flag){
        isClickable = flag;
    }

    //クリックされる
    public void OnPointerClick(PointerEventData eventData){
        if(!isClickable) return;

        isClickable = false;
        MouseOverEffect.SetActive(false);
        ClickSubject.OnNext(this);
        
    }

    //マウスオーバー
    public void OnPointerEnter(PointerEventData eventData){
        if(!isClickable) return;
        MouseOverEffect.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData){
        if(!isClickable) return;
        MouseOverEffect.SetActive(false);
    }





    //Anim
    private IEnumerator turnOverTile(){
        //すでに再生中ならリターン
        if(!isAnimFin) yield break;

        //タイルがひっくりかえった
        isTurned = true;

        isAnimFin = false;
        animator.SetTrigger("TurnOver");

        while(!isAnimFin){
            yield return null;
        }

    }


    private IEnumerator creatTile(){
        //すでに再生中ならリターン
        if(!isAnimFin) yield break;

        isAnimFin = false;
        animator.SetTrigger("CreateTile");

        while(!isAnimFin){
            yield return null;
        }
        
    }


    private IEnumerator clearTile(){
        //すでに再生中ならリターン
        if(!isAnimFin) yield break;

        isAnimFin = false;
        animator.SetTrigger("ClearTile");

        while(!isAnimFin){
            yield return null;
        }
        
    }


    

}
