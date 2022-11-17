using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwitchScene
{
    void SwitchScene(string nameScene);

    IEnumerator SwitchSceneRutiner(string nameScene);
}
