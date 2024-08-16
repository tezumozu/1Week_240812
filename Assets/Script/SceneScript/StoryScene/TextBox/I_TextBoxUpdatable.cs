using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_TextBoxUpdatable{
    public abstract void UpdateText(Lines text);
    public abstract void SetActive(bool flag);
}
