using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using My1WeekGameSystems_ver3;

public class AnimMono<T_AnimName> : MonoBehaviour , I_AnimContlorable<T_AnimName>
where T_AnimName : Enum
{
    protected delegate IEnumerator coroutineMethod();
    protected bool isAnimFin = true;

    [SerializeField]
    protected Animator animator;


    protected Dictionary< T_AnimName , coroutineMethod > AnimList = new Dictionary<T_AnimName, coroutineMethod>();

    public IEnumerator StartAnim(T_AnimName name){

        if(isSafeUpdate){
            animator.enabled = false;
            InitializeThresholdTime();
        } 


        //アニメーションのコルーチンを起動
        IEnumerator coroutine = AnimList[name]();
        CoroutineHander.OrderStartCoroutine( coroutine );

        //アニメーションが終わるまで待機
        while(!CoroutineHander.isFinishCoroutine( coroutine )){
            yield return null;
        }
    }


    //アニメーター用
    public void FinishAnim(){
        isAnimFin = true;
    }



    //負荷軽減用の処理


    //更新頻度を下げて荷軽減するか
    [SerializeField]
    bool isSafeUpdate;

    [SerializeField,Range(1,60)]
    float SafeFPS = 30;

    float m_thresholdTime;
    float m_skippedTime;


    private void Update() {
        
        if(isAnimFin) return;
        if(!isSafeUpdate) return;

        m_skippedTime += Time.deltaTime;

        //更新タイミングでないならリターン
        if (m_thresholdTime > m_skippedTime) return;

        //更新タイミングで更新をかける
        animator.Update(m_skippedTime);
        m_skippedTime = 0f;

    }

    // 閾値時間の初期化
    void InitializeThresholdTime(){
        m_thresholdTime = 1f / SafeFPS;
        // 更新タイミングがバラつくように初期値に乱数を与える
        m_skippedTime = UnityEngine.Random.Range(0f, m_thresholdTime);
    }
}