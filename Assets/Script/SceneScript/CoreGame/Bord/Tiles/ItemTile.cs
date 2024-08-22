using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTile : Tile{

    protected override void InitTile(){
        isWalkable = true;
    }

    protected override IEnumerator TileClickEffect(){
        Debug.Log("Tile : Item");
        yield return null;
    }
}
