using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace My1WeekGameSystems_ver3{

    public abstract class GameManager<T> : I_SceneLoadNoticable , I_GameStateUpdatable<T>
    where T : Enum
    {

        protected T currentState;

        protected Subject<T> UpdateStateSubject;
        protected Subject<E_SceneName> SceneLoadSubject;

        public IObservable<T> UpdateStateAsync { get {return UpdateStateSubject;} }
        public IObservable<E_SceneName> SceneLoadAsync { get {return SceneLoadSubject;} } 



        public GameManager(T initState){

            currentState = initState;

            UpdateStateSubject = new Subject<T>();
            SceneLoadSubject = new Subject<E_SceneName>();

        }



        public abstract void InitObject();
        public abstract void StartGame();
        public abstract void ReleaseObject();

    }

}
