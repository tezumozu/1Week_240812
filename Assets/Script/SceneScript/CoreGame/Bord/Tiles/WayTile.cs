using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using My1WeekGameSystems_ver3;

public class WayTile : Tile{

    protected override void InitTile(){
        isWalkable = true;
        isTurned = false;
        isClickable = false;
        TileType = E_DungeonCell.Way; 
    }

    protected override IEnumerator TileClickEffect(){

        yield return null;
        
    }
}
