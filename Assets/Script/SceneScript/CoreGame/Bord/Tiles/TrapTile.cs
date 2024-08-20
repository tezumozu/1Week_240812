using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTile : Tile{
    public override IEnumerator TileEffect(){
        Debug.Log("Tile : Trap");
        yield return null;
    }
}
