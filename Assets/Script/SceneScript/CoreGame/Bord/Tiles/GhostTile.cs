using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTile : Tile{
    public override IEnumerator TileEffect(){
        Debug.Log("Tile : Ghost");
        yield return null;
    }
}
