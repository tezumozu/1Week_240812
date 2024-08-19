using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Factory<T,U>  
where T : I_FactoryMakable 
where U : Enum
{
    protected delegate T createObject();
    protected Dictionary<U,createObject> methodList;

    public Factory (){
        methodList = new Dictionary<U, createObject>();
    }

    public T CreateObject(U type){
        return methodList[type]();
    }
}
