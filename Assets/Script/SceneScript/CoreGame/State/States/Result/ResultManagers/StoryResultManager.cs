using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryResultManager : ResultManager{

    I_DungeonCheckable dungeonManager;

    public StoryResultManager(I_DungeonCheckable dungeonManager){
        this.dungeonManager = dungeonManager;
    }

    public override void SetActive(bool flag){

    }
    public override IEnumerator ResultAnim(){
        yield return null;
    }
}
