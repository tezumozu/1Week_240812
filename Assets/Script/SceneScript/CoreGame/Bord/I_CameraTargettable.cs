using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_CameraTargettable {
    public abstract IEnumerator TargetThis(GameObject camera);
}
