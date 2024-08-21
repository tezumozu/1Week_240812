using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTile : Tile{

    protected override void InitTile(){
        
    }
    
    protected override IEnumerator TileClickEffect(){
        Debug.Log("Tile : Wall");
        yield return null;
    }
}
