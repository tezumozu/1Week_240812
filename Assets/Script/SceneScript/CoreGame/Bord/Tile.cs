using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject.Asteroids;

public class Tile : MonoBehaviour , I_CameraTargettable{

    [SerializeField]
    float CameraRange = 5.0f;

    List<Tile> relatedTileList = new List<Tile>();
    bool isClicked;

    private void Start(){
        isClicked = false;
    }

    //カメラを特定の位置に移動させる
    public IEnumerator TargetThis(GameObject camera){

        var point = (float) Math.Sqrt(2) * CameraRange;
        var NextPos = transform.position;
        NextPos += new Vector3( 0.0f , point , -point );

        while( Vector3.Distance( camera.transform.position , NextPos ) * 0.01f > 0.0001f  ){
            camera.transform.position += (NextPos - camera.transform.position) * 0.01f;
            yield return null;
        }

        camera.transform.position = NextPos;

    }

    public void AddRelatedTile(Tile relatedTile){
        relatedTileList.Add(relatedTile);
    }

}
