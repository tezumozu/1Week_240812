using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_TimeMeasurable {
    public abstract IEnumerator StartMeasureTime();
    public abstract void StopMeasureTime();
}
