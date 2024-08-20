using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Zenject.Asteroids;

public abstract class Tile :
MonoBehaviour ,
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


    public E_DungeonCell type {
        get;
        private set;
    }

    protected bool isClickable;
    protected bool isTurnable;
    protected bool isWarkable;


    protected Subject<Tile> ClickSubject = new Subject<Tile>();
    public IObservable<Tile> ClickAsync => ClickSubject;

    private void Start(){
        isClickable = false;
        MouseOverEffect.SetActive(false);
        isTurnable = true;
        isWarkable = false;
    }


    //カメラを特定の位置に移動させる
    public virtual IEnumerator TargetThis(GameObject camera){

        var point = (float) Math.Sqrt(2) * CameraRange;
        var NextPos = transform.position;
        NextPos += new Vector3( 0.0f , point , -point );

        while( Vector3.Distance( camera.transform.position , NextPos ) * 0.01f > 0.0001f  ){
            camera.transform.position += (NextPos - camera.transform.position) * 0.01f;
            yield return null;
        }

        camera.transform.position = NextPos;

    }


    //周辺のタイルを覚えておく
    public void AddRelatedTile(Tile relatedTile){
        relatedTileList.Add(relatedTile);
    }



    //クリックできるように変更する
    public virtual void SetIsClickable(bool flag){
        isClickable = flag;
    }



    //クリックされたとき
    public void OnPointerClick(PointerEventData eventData){
        if(!isClickable) return;
        MouseClick();
    }


    //タイルが裏返されたときの処理
    public abstract IEnumerator TileEffect();



    //マウスオーバー
    public void OnPointerEnter(PointerEventData eventData){
        if(!isClickable) return;
        MouseOverEffect.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData){
        if(!isClickable) return;
        MouseOverEffect.SetActive(false);
    }


    //クリックされたときの処理
    protected virtual void MouseClick(){
        print($"オブジェクト {name} がクリックされたよ！");
        isClickable = false;
        MouseOverEffect.SetActive(false);
        ClickSubject.OnNext(this);
    }

}
