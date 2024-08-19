using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInputMode : InputMode{
    
    void Start(){
        SetActive(false);
    }

    public override void SetActive(bool flag){
        gameObject.SetActive(flag);
    }

    public void ChangeMode_Item (){
        ChangeMode(E_InGameInputMode.Item);
    }

    public void ChangeMode_GoOn (){
        ChangeMode(E_InGameInputMode.GoOn);
    }
}
