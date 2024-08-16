using System;
using System.Collections;
using System.Collections.Generic;
using My1WeekGameSystems_ver3;
using UnityEngine;

//リザルトに必要なデータを収集し、リザルトUIに反映する
public abstract class ResultManager : I_ResultUpdatable{

    bool isSkip;

    public abstract void SetActive(bool flag);

    public abstract IEnumerator ResultAnim();

    public virtual void isStaigingSkip(){
        isSkip = true;
    }

}
