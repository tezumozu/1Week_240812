using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_ResultUpdatable {
    public abstract void SetActive(bool flag);
    public abstract IEnumerator ResultAnim();
}
