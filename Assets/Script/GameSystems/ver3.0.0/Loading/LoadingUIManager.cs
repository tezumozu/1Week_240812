using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace My1WeekGameSystems_ver3{
    public class LoadingUIManager : MonoBehaviour{
        [SerializeField]
        Slider loadingSlider;

        public void UpdateSlider(float value){
            loadingSlider.value = value;
        }
    }
}
