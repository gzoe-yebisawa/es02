/// <summary>
///	タイトルシーンマネージャー
///	</summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Gzoe;
using App;

public class HistorySceneController : BaseBehaviour
{
    [SerializeField] private Animator _anim = new Animator();
    [SerializeField] private GameObject _graphObject = new GameObject();

    protected override IEnumerator OnStart() 
    {
        _graphObject.SetActive(false);

        return base.OnStart();
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
    /// 履歴ボタン選択時の処理
    /// </summary>
    public void ClickRecordButton() 
    {
        _anim.SetTrigger("OutGraph");
    }

    /// <summary>
    /// グラフボタン選択時の処理
    /// </summary>
    public void ClickGraphButton() 
    {
        _anim.SetTrigger("InGraph");
    }

    /// <summary>
    /// 右ボタン(メモ画面)選択時の処理
    /// </summary>
    public void ClickRightArrowButton() 
    {
        _anim.SetTrigger("InMemo");
    }
    /// <summary>
    /// 左ボタン(測定履歴画面)選択時の処理
    /// </summary>
    public void ClickLeftArrowButton() 
    {
        _anim.SetTrigger("InRecord");
    }

    /// <summary>
    /// 1週間ボタン選択時の処理
    /// </summary>
    public void ClickWeekButton() 
    {

    }

    /// <summary>
    /// 1か月ボタン選択時の処理
    /// </summary>
    public void ClickMonthButton() 
    {

    }

    /// <summary>
    /// 3か月ボタン選択時の処理
    /// </summary>
    public void ClickThreeMonthButton() 
    {

    }

    /// <summary>
    /// すべてボタン選択時の処理
    /// </summary>
    public void ClickAllButton() 
    {

    }
}
