using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_AnimContlorable<T_AnimName> where T_AnimName : Enum{
    public abstract IEnumerator StartAnim(T_AnimName name);
}
