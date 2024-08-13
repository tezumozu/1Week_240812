using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class InGameTimer : I_InGameTimeObservable , I_TimeMeasurable{
    private Subject<float> timeMeasuredSubject = new Subject<float>();
    public IObservable<float> MeasuredTimeAsnc {
        get{
            return timeMeasuredSubject;
        }
    }


    public IEnumerator StartMeasureTime(){
        yield return null;
    }
    
    public void StopMeasureTime(){

    }
}
