using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_PauseInputTranslatable {
    public IObservable<bool> PauseInputAsync {get;}
}
