using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTile : Tile{
    public override IEnumerator TileEffect(){
        Debug.Log("Tile : Start");
        yield return null;
    }
}
