using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_BoardClickable{
    IObservable<Tile> ClickedTileAsync {get;}
    public abstract void SetClickable(bool flag);
}
