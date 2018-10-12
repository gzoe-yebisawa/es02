/// <summary>
///	撮影シーンマネージャー
///	</summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Gzoe;
using App;

public class PhotoGraphSceneManager : BaseBehaviour
{
    [SerializeField]
    private PrefabPopUp _popup;

    [SerializeField]
    private GameObject _headerObject;

    /// <summary>
    /// 測定完了時の処理
    /// </summary>
    public void ResultMeasurement() 
    {
        _headerObject.SetActive(false);

        _popup.OpenPopUp();
    }

    /// <summary>
    /// 戻るボタン選択時の処理
    /// </summary>
    public void ClickBackButton() 
    {
        var appManager = AppManager.Instance;

        appManager.SceneLoader.LoadSceneWithFade(SceneName.Application_Scenes_Scn_TopMenu);
    }

    /// <summary>
    /// 再測定選択時の処理
    /// </summary>
    public void ClickRetryButton() 
    {
        var appManager = AppManager.Instance;

        appManager.SceneLoader.LoadSceneWithFade(SceneName.Application_Scenes_Scn_PhotoGraph);
    }

    /// <summary>
    /// 決定ボタン選択時の処理
    /// </summary>
    public void ClickDecideButton() 
    {

        var appManager = AppManager.Instance;

        appManager.SceneLoader.LoadSceneWithFade(SceneName.Application_Scenes_Scn_History);
    }
}
