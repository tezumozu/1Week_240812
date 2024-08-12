using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

namespace My1WeekGameSystems_ver3{
    public class SceneLoader {
        private AsyncOperation asyncLoad;
        private LoadingUIManager uiManager;
        private float currentTime;
        private readonly float loadingDilay = 1.0f;

        public SceneLoader(LoadingUIManager uiManager){
            uiManager.UpdateSlider(0.0f);
            currentTime = 0;
        }
        
        public IEnumerator LoadScene(E_SceneName sceneName){
            //シーン読み込み開始
            asyncLoad = SceneManager.LoadSceneAsync(Enum.GetName(typeof(E_SceneName),sceneName));

            asyncLoad.allowSceneActivation = false;

            //演出としてローディングの時間を一定時間確保する
            while( asyncLoad.progress < 0.9f || currentTime < loadingDilay ){
                currentTime += Time.deltaTime;
                uiManager.UpdateSlider(asyncLoad.progress * (currentTime / loadingDilay) + 0.1f);
                yield return null;
            }

            asyncLoad.allowSceneActivation = true;
        }
    }
}
