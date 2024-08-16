using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxManager : I_TextBoxUpdatable{
    public void UpdateText(Lines textData){

        string text = "";

        foreach (var str in textData.Texts){
            text += str + "\n";
        }

        Debug.Log("TextBoxManager : " + text);

    }

    
    public void SetActive(bool flag){

    }
}
