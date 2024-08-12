using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace My1WeekGameSystems_ver3{

    public class SoundPlayer : MonoBehaviour{
        [SerializeField]
        SoundManager SoundManager;

        [SerializeField]
        AudioSource audioSource;

        private S_SoundOptionData option;

        private void Start() {
            option = SoundManager.GetSoundOptionData;

            //オプションが更新されたら数値を更新
            SoundManager.UpdateOptionAsync
            .Subscribe((data) => {
                option = data;
                audioSource.volume = option.Sound * option.BGM;
            })
            .AddTo(this);
        }

        //SE
        public void PlaySE(AudioClip se){
            //音量を
            audioSource.volume = option.Sound * option.SE;
            audioSource.PlayOneShot(se);
        }


        //BGM
        public void PlayBGM(AudioClip bgm){
            StopSound();
            //音量を
            audioSource.volume = option.Sound * option.BGM;
            audioSource.clip = bgm;
            audioSource.loop = true;
            audioSource.Play();
        }

        //ループしない場合
        public void PlayBGM(AudioClip bgm , bool isLoop){
            StopSound();
            //音量を
            audioSource.volume = option.Sound * option.BGM;
            audioSource.clip = bgm;
            audioSource.loop = isLoop;
            audioSource.Play();
        }

        public void StopSound(){
            audioSource.Stop();
        }

        public IEnumerator WaitFinishSEPlay(AudioClip sound){
            PlaySE(sound);

            while(audioSource.isPlaying){
                yield return null;
            }
        }

    }
}
