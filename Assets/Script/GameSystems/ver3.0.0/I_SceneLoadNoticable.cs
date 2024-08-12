using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace My1WeekGameSystems_ver3{

    public interface I_SceneLoadNoticable {
        public IObservable<E_SceneName> SceneLoadAsync { get; }
    }

}
