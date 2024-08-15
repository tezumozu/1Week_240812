using System;
using System.Collections;
using System.Collections.Generic;
using My1WeekGameSystems_ver3;
using UnityEngine;

public class ResultManager : I_ResultGettable{
    public ResultManager (I_GameBordChackable bordManager , I_InGameTimeObservable Timer){

    }

    public ResultData GetResultData(){
        return new ResultData();
    }
}
