using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTile : Tile{

    protected override void InitTile(){
        
    }

    protected override IEnumerator TileClickEffect(){
        Debug.Log("Tile : Goal");
        yield return null;
    }
}
