using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayTile : Tile{

    protected override void InitTile(){
        
    }

    protected override IEnumerator TileClickEffect(){
        Debug.Log("Tile : Way");
        yield return null;
    }
}
