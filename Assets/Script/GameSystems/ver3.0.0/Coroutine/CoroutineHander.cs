using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

using Zenject;

namespace My1WeekGameSystems_ver3{

    public class CoroutineHander : MonoSingleton <CoroutineHander>{
        
        //登録されたコルーチンのリスト
        static Dictionary<IEnumerator,bool> activeCoroutinDic;

        //監視用コルーチンのリスト
        static Dictionary<IEnumerator,Coroutine> checkerCoroutineDic;

        //シーンが更新された際の処理
        [Inject]
        private void UpdateSceneInjection(I_Pausable pauseAsync , I_SceneLoadNoticable sceneloadAsync){

            //ポーズになったらコルーチンを止める
            pauseAsync.PauseAsync.Subscribe((flag)=>{
                var keys = activeCoroutinDic.Keys;

                foreach ( var coroutine in keys ) { 

                    //ポーズで止まるコルーチンであれば
                    if (activeCoroutinDic[coroutine]) { 

                        //ポーズなら
                        if(flag){
                            //コルーチンを止める
                            instance.StopCoroutine(coroutine);
                        }else{

                            //ポーズが解除されたら再開
                            instance.StartCoroutine(coroutine);
                        }
                        
                    }
                    
                }

            }).AddTo(instance);


            //シーンの終了を監視
            sceneloadAsync.SceneLoadAsync.Subscribe((_)=>{
                
                var keys = checkerCoroutineDic.Keys;

                //登録された全てのコルーチンに対して
                foreach ( var coroutine in keys ) {

                    //登録されている全てのコルーチンを停止
                    instance.StopCoroutine(coroutine);

                    //監視用のコルーチンを停止
                    instance.StopCoroutine(checkerCoroutineDic[coroutine]);

                }
                
                //辞書を空にする（これでガベージコレクション行になる？）
                activeCoroutinDic.Clear();
                checkerCoroutineDic.Clear();

            }).AddTo(instance);


        }

        //初期化処理
        protected override void OnInitialize() {

            activeCoroutinDic = new Dictionary<IEnumerator,bool>();
            checkerCoroutineDic = new Dictionary<IEnumerator,Coroutine>();

        }




        //ポーズによって停止できるコルーチンを開始する：false の場合はポーズによって停止しない
        public static void OrderStartCoroutine(IEnumerator coroutine,bool isPausable){

            //コルーチンを再開する場合
            if(activeCoroutinDic.ContainsKey(coroutine)) {

                instance.StartCoroutine(coroutine);
                return ;
            }


            Coroutine checkerCoroutine = instance.StartCoroutine ( CheckFinishCoroutine( coroutine , isPausable ) );
            checkerCoroutineDic.Add( coroutine , checkerCoroutine );

        }

        //ポーズによって停止しないコルーチンを開始する：省略用
        public static void OrderStartCoroutine(IEnumerator coroutine){

            //コルーチンを再開する場合
            if(activeCoroutinDic.ContainsKey(coroutine)) {

                instance.StartCoroutine(coroutine);
                return ;
            }

            Coroutine checkerCoroutine = instance.StartCoroutine ( CheckFinishCoroutine( coroutine , false ) );
            checkerCoroutineDic.Add( coroutine , checkerCoroutine );
        }





        //登録されたコルーチンの終了を監視するコルーチン
        private static IEnumerator CheckFinishCoroutine(IEnumerator coroutine,bool isPausable){
            
            //呼び出し元から受け取ったコルーチンを開始、
            var activeCoroutine = instance.StartCoroutine(coroutine);

            //受け取ったコルーチンのポインタをキーに開始したコルーチンのポインタを辞書に保存
            activeCoroutinDic.Add(coroutine,isPausable);

            //同フレーム内で辞書の追加と削除を絶対に行わない
            yield return null ;
            
            //呼び出し元のコルーチンの終了を待つ
            yield return activeCoroutine;

            //終了したので辞書から削除
            activeCoroutinDic.Remove(coroutine);
            checkerCoroutineDic.Remove(coroutine);
        }




        //特定のコルーチンを停止する
        public static void OrderStopCoroutine(IEnumerator target){
            //コルーチンが登録されていない場合リターン
            if(!activeCoroutinDic.ContainsKey(target)) return;
            
            instance.StopCoroutine(target);
        }

        //コルーチンを再開する
        public static void ReStartCoroutine(IEnumerator target){
            //コルーチンが登録されていない場合リターン
            if(!activeCoroutinDic.ContainsKey(target)) return;

            instance.StartCoroutine(target);
        }

        //登録されているコルーチンが終了しているか確認する
        public static bool isFinishCoroutine(IEnumerator target){
            return !activeCoroutinDic.ContainsKey(target);
        }

    }

    

}
