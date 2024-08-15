using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//インプットを受け取るためのオブジェクトを管理する
public abstract class InpuitManager <T> where T : Enum{
    Dictionary<T,InputGetter> inputGetterList;
}
