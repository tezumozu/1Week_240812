using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayTile : Tile{
    public override IEnumerator TileEffect(){
        Debug.Log("Tile : Way");
        yield return null;
    }
}
