using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericSingletonObject<T> 
where T : GenericSingletonObject<T>, new(){
    private static T m_Instance = null;
    public static T instance
    {
        get {
            if( m_Instance != null ){
                return m_Instance;
            }

            //インスタンスがない場合は生成
            m_Instance = new T();
            
            //インスタンスを初期化する
            m_Instance.OnInitialize();

            return m_Instance;

        }
    }

    abstract public void OnInitialize();

}
