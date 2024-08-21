using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;
using UniRx;

namespace My1WeekGameSystems_ver3{
    
    public abstract class GameLoopManager : MonoBehaviour { 
        
        bool isHaveToLoading;

        private E_SceneName nextScene;

        private E_LoopState currentState;

        private SceneLoader sceneLoader;

        private IDisposable sceneLoadDispose;
        
        [SerializeField] 
        private LoadingUIManager loadingUIManager;

        [SerializeField]
        public int GameFrameLate = 60;

        [Inject]
        private I_ObjectUpdatable objectUpdataer;


        //ゲームマネージャーの監視
        [Inject]
        private void InjectInit(I_SceneLoadNoticable gameManager){
            //フレームレートを設定
            Application.targetFrameRate = GameFrameLate;

            sceneLoadDispose = gameManager.SceneLoadAsync
            .Subscribe( (sceneName) => {
                activeIsHaveToLoading(sceneName);
            });
            
            sceneLoader = new SceneLoader(loadingUIManager);
        }


        //自身の初期化処理
        void Start(){
            //パラメータ初期化
            isHaveToLoading = false;
            currentState = E_LoopState.Init;
        }


        //各フレームごとの処理
        void Update(){
            switch(currentState){

                //ゲームを初期化する
                case E_LoopState.Init :
                    //ゲームの初期化
                    objectUpdataer.InitObject();

                    //ゲームの開始
                    currentState = E_LoopState.Update;
                    objectUpdataer.StartGame();
                break;



                //Update処理
                case E_LoopState.Update :
                    //オブジェクトをUpdate
                    objectUpdataer.UpdateObject();

                    //シーンロードが必要ならば
                    if(isHaveToLoading){
                        
                        currentState = E_LoopState.Loading;

                        //不要なオブジェクトを開放する
                        objectUpdataer.ReleaseObject();

                        //読み込みを開始する(コルーチン)
                        StartCoroutine(sceneLoader.LoadScene(nextScene));

                        //読み込みを開始したのでフラグをfalseに
                        isHaveToLoading = false;

                        sceneLoadDispose.Dispose();
                    }

                    break;



                //Loading時（シーン切り替え時）の処理
                case E_LoopState.Loading :
                    //基本待機　何かあれば
                    break;
            }
        }


        //シーン切り替え時の処理
        private void activeIsHaveToLoading(E_SceneName sceneName){
            isHaveToLoading = true;
            nextScene = sceneName;
        }

        //ループの状態管理
        private enum E_LoopState {
            Init,
            Update,
            Loading
        }
    }
}
