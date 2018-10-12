using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Gzoe;
using App;

public class PrefabPopUp : BaseBehaviour
{
    //アニメーション
    [SerializeField]
    private Animator _animator;

    private readonly string OpenPopUpTriggerName = "Open";

    /// <summary>
    /// 初期化
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator OnStart() 
    {
        //初期化で非表示に
        gameObject.SetActive(false);

        return base.OnStart();
    }

    public void OpenPopUp() 
    {
        gameObject.SetActive(true);

        _animator.SetTrigger(OpenPopUpTriggerName);
    }
}
