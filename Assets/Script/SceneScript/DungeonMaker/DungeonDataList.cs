using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonDataList", menuName = "ScriptableObjects/DungeonDataList")]

public class DungeonDataList : ScriptableObject{

    [SerializeField]
    private List<DungeonData> dungeonDataList = new List<DungeonData>();

    public int MaxLevel{
        get{
            int maxNum = 0;
            foreach(var data in dungeonDataList){
                if(data.Level > maxNum){
                    maxNum = data.Level;
                }
            }

            return maxNum;
        }
    }

    public DungeonData GetDungeonData(int level){
        foreach( var data in dungeonDataList){
            if(level == data.Level){
                return data;
            }
        }

        return null;
    }
}


[System.Serializable]
public class DungeonData{

    [SerializeField]
    public int Level = 0;

    [SerializeField]
    public int MaxGohst = 0;
    
    [SerializeField]
    public int MaxTrap = 0;

    [SerializeField]
    public int MaxItem = 0;


    [SerializeField]
    public int MaxCure = 0;
}
