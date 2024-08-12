using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UniRx;

public class LoginUIManager : MonoBehaviour {


    //UI入力
    [SerializeField]
    GameObject Buttons;

    [SerializeField]
    TMP_InputField ID_Input;

    [SerializeField]
    TMP_InputField PASS_Input;



    //テキスト
    [SerializeField]
    TextMeshProUGUI ID_Text_UI;

    [SerializeField]
    TextMeshProUGUI PASS_Text_UI;

    [SerializeField]
    string defoultInputText;

    [SerializeField]
    string defoultErrorText;



    //ログイン中を通知するSubject
    Subject<bool> isLoginSubject = new Subject<bool>();
    public IObservable<bool> isLoadingAsync => isLoginSubject;



    // Start is called before the first frame update
    void Start(){
        //エラー部分のテキストを初期化
        ID_Text_UI.text = defoultInputText;
        PASS_Text_UI.text = defoultInputText;

        //ログイン中だったら
        if(false){

        }
    }



    public void LoginAcount(){

        //ID、PASSの取得
        var id = ID_Input.text;
        var pass = PASS_Input.text;

        Buttons.SetActive(false);
        isLoginSubject.OnNext(true);

        this.StartCoroutine(LoginAcountCoroutine(id,pass));

    }



    public void CreateAcount(){
        
    }



    IEnumerator LoginAcountCoroutine(string id , string pass){


        yield return null;
    }



    IEnumerator CreateAcountCoroutine(){
        yield return null;
    }
}
