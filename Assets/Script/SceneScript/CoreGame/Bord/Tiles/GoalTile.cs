using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTile : Tile{
    public override IEnumerator TileEffect(){
        Debug.Log("Tile : Goal");
        yield return null;
    }
}
