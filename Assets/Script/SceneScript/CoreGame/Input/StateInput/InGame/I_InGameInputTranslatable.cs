using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public interface I_InGameInputTranslatable {

    public abstract void ToActiveMode(E_InGameInputMode mode);

    public abstract IEnumerator TakeTurn();
}

