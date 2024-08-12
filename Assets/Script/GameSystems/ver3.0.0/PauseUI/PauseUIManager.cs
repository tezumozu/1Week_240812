using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;
using Zenject;

namespace My1WeekGameSystems_ver3{

    public abstract class PauseUIManager : MonoBehaviour {
        private Subject<Unit> BackToTitleSubject = new Subject<Unit>();
        public IObservable<Unit> BackToTitleAsync => BackToTitleSubject;

        [SerializeField]
        GameObject CheckDisitionUI;

        [SerializeField]
        SoundPlayer soundPlayer;

        [SerializeField]
        AudioClip desitionSE;

        [SerializeField]
        AudioClip cancellSE;

        [Inject]
        protected void InitManager(I_Pausable gameManager) {
            gameManager.PauseAsync
            .Subscribe((flag)=>{
                if(flag){
                    SetActive(true);
                    soundPlayer.PlaySE(desitionSE);
                }else{
                    SetActive(false);
                    soundPlayer.PlaySE(cancellSE);
                }
            })
            .AddTo(this);

            //確認UIをfalseに
            CheckDisitionUI.SetActive(false);

            //念のため自身もfalseに
            SetActive(false);
        }

        public void OnPushBackToTitleButton(){
            soundPlayer.PlaySE(desitionSE);
            CheckDisitionUI.SetActive(true);
        }

        public void CanselBackToTitle(){
            soundPlayer.PlaySE(cancellSE);
            CheckDisitionUI.SetActive(false);
        }

        public void DisitionBackToTitle(){
            soundPlayer.PlaySE(desitionSE);
            BackToTitleSubject.OnNext(Unit.Default);
        }

        private void SetActive(bool flag){
            if(!flag) CheckDisitionUI.SetActive(false);
            gameObject.SetActive(flag);
        }
    }

}
