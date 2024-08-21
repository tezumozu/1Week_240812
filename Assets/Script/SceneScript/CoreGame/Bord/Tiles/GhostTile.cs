using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTile : Tile{
    protected override void InitTile(){
        
    }

    protected override IEnumerator TileClickEffect(){
        Debug.Log("Tile : Ghost");
        yield return null;
    }
}
