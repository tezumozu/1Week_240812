using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "StoryDataList", menuName = "ScriptableObjects/StoryDataList")]
public class StoryDataList : ScriptableObject{
    [SerializeField]
    private List<Chapter> chapterList = new List<Chapter>();

    [SerializeField]
    private List<Lines> prologeTexts = new List<Lines>();

    public IReadOnlyCollection<Lines> PrologeTexts => prologeTexts;

    public Chapter GetChapter(int chapterNum){

        foreach( var data in chapterList){
            if(chapterNum == data.ChapterNum){
                return data;
            }
        }

        return null;

    }
}



[System.Serializable]
public class Chapter{

    [SerializeField]
    private List<Lines> chapterTexts = new List<Lines>();

    public IReadOnlyCollection<Lines> ChapterTexts => chapterTexts;

    [SerializeField]
    public int DungeonLevel;

    [SerializeField]
    public int ChapterNum;
}



[System.Serializable]
public class Lines{
    [SerializeField]
    public E_Facial Face;

    [SerializeField]
    private List<string> texts = new List<string>();
    public IReadOnlyCollection<string> Texts => texts;
}