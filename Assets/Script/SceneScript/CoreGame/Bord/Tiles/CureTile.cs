using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureTile : Tile{

    protected override void InitTile(){
        isWalkable = true;
    }

    protected override IEnumerator TileClickEffect(){
        Debug.Log("Tile : Cure");
        yield return null;
    }
}
