using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Gzoe;
using App;

public class TopMenuSceneManager : BaseBehaviour
{

    [SerializeField]
    private PrefabPopUp _popup;

    protected override IEnumerator OnStart() {
        yield return null;
    }
    

    public void ClickCameraButton() 
    {
        _popup.OpenPopUp();
    }

    public void ClickHistoryButton() 
    {
        var appManager = AppManager.Instance;

        appManager.SceneLoader.LoadSceneWithFade(SceneName.Application_Scenes_Scn_History);
    }

    public void ClickOptionButton() 
    {
        var appManager = AppManager.Instance;

        appManager.SceneLoader.LoadSceneWithFade(SceneName.Application_Scenes_Scn_Option);
    }

    public void ClickBeforeMotionButton() 
    {
        var appManager = AppManager.Instance;

        appManager.SceneLoader.LoadSceneWithFade(SceneName.Application_Scenes_Scn_PhotoGraph);
    }

    public void ClickAfterMotionButton() {
        var appManager = AppManager.Instance;

        appManager.SceneLoader.LoadSceneWithFade(SceneName.Application_Scenes_Scn_PhotoGraph);
    }
}
