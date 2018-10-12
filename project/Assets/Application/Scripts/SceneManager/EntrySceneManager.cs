using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Gzoe;
using App;

public class EntrySceneManager : BaseBehaviour
{

    protected override IEnumerator OnStart() 
    {

        var appManager = AppManager.Instance;

        appManager.SceneLoader.LoadSceneWithFade(SceneName.Application_Scenes_Scn_TopMenu);

        return base.OnStart();
    }
}
