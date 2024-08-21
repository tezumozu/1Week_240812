using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTile : Tile{

    protected override void InitTile(){
        
    }
    
    protected override IEnumerator TileClickEffect(){
        Debug.Log("Tile : Trap");
        yield return null;
    }
}
