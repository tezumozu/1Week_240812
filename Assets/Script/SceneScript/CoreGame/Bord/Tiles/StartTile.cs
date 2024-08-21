using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTile : Tile{

    protected override void InitTile(){
        //タイルの設定
        isWalkable = true;
        isTurned = true; // すでにめくれている
        isClickable = false;        
    }

    protected override IEnumerator TileClickEffect(){
        Debug.Log("Tile : Start");
        yield return null;
    }
}
