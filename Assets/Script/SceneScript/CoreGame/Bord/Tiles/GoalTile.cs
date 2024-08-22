using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GoalTile : Tile{

    protected override void InitTile(){
        //タイルの設定
        isWalkable = true;
        isTurned = false;
        isClickable = false;
        TileType = E_DungeonCell.Goal;  
    }

    protected override IEnumerator TileClickEffect(){
        Debug.Log("Tile : Goal");
        stepedOnSubject.OnNext(Unit.Default);
        yield return null;
    }
}
