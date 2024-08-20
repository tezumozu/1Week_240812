using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public interface I_InGameTurnUpdatable {
    public abstract IEnumerator TakeTurn();
}

