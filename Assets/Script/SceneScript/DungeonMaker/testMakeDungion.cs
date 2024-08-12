using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMakeDungion : MonoBehaviour{
    // Start is called before the first frame update
    void Start(){
        //ダンジョンを生成
        var dungeon = new DungeonMaker(5);
        dungeon.MakeDungeon();

        //Debug.Logでコンソールに出力
        Debug.Log(dungeon);
    }



}
