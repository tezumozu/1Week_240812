using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace My1WeekGameSystems_ver3{

    public interface I_GameStateUpdatable<T> where T : Enum {
        public IObservable<T> UpdateStateAsync { get; }
    }

}
