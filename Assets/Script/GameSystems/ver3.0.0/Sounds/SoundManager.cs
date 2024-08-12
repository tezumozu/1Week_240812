using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;

using Zenject;

namespace My1WeekGameSystems_ver3{

    public class SoundManager : MonoBehaviour{

        [SerializeField]
        Slider SoundSlider;

        [SerializeField]
        Slider SESlider;

        [SerializeField]
        Slider BGMSlider;

        [SerializeField]
        SoundPlayer soundPlayer;

        [SerializeField]
        AudioClip desitionSE;

        [SerializeField]
        AudioClip TestBGM;

        //サウンド設定の初期値
        private static S_SoundOptionData SoundOptionData = new S_SoundOptionData(1.0f , 0.5f , 0.5f);

        private Subject<S_SoundOptionData> UpdateOptionSubject = new Subject<S_SoundOptionData>();
        public IObservable<S_SoundOptionData> UpdateOptionAsync => UpdateOptionSubject;

        private bool isPlayTestBGM = false;

        public static S_SoundOptionData GetSoundOptionData{
            get{return SoundOptionData;}
        }

        private void Awake() {
            Debug.Log("test");
            //現在の数値に直す
            SoundSlider.value = SoundOptionData.Sound;
            SESlider.value = SoundOptionData.SE;
            BGMSlider.value = SoundOptionData.BGM;
        }

        public void SetActive(bool flag){
            gameObject.SetActive(flag);

            //現在の数値に直す
            SoundSlider.value = SoundOptionData.Sound;
            SESlider.value = SoundOptionData.SE;
            BGMSlider.value = SoundOptionData.BGM;
        }

        public void OnUpdateOption(){
            SoundOptionData = new S_SoundOptionData(SoundSlider.value , SESlider.value , BGMSlider.value);
            UpdateOptionSubject.OnNext(SoundOptionData);
            soundPlayer.PlaySE(desitionSE);
        }

        public void PlayTestBGM(){
            isPlayTestBGM = !isPlayTestBGM;
            
            if(isPlayTestBGM){
                soundPlayer.PlayBGM(TestBGM);
            }else{
                soundPlayer.StopSound();
            }
        } 
    }
}
