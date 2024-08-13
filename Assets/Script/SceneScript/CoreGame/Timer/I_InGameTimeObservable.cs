using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public interface I_InGameTimeObservable{
    public IObservable<float> MeasuredTimeAsnc{get;}
}
