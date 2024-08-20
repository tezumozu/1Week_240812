using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureTile : Tile{
    public override IEnumerator TileEffect(){
        Debug.Log("Tile : Cure");
        yield return null;
    }
}
