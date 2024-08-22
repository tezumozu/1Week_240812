using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTile : Tile{

    protected override void InitTile(){
        //タイルの設定
        isWalkable = true;
        isTurned = false;
        isClickable = false;
        TileType = E_DungeonCell.Start;  
    }

    protected override IEnumerator TileClickEffect(){
        Debug.Log("Tile : Start");
        yield return null;
    }
}
