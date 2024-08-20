using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTile : Tile{
    public override IEnumerator TileEffect(){
        Debug.Log("Tile : Item");
        yield return null;
    }
}
