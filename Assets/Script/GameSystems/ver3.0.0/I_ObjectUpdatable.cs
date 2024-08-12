using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My1WeekGameSystems_ver3{
    interface I_ObjectUpdatable{
        public abstract void InitObject();
        public abstract void StartGame();
        public abstract void UpdateObject();
        public abstract void ReleaseObject();
    }
}

