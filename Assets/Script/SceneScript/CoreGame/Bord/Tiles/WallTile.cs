using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTile : Tile{
    public override IEnumerator TileEffect(){
        Debug.Log("Tile : Wall");
        yield return null;
    }
}
