using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Gzoe;

namespace App
{
    /// <summary>
    /// シーンローダー
    /// </summary>
    public class SceneLoader : BaseBehaviour
    {
        private readonly string FadeInTriggerName = "FadeIn";
        private readonly string FadeOutTriggerName = "FadeOut";

        /// <summary>
        /// フェード用オーバーレイ画像
        /// </summary>
        [SerializeField]
        private Animator overlayImageAnimator = null;

        /// <summary>
        /// シーンロード中フラグ
        /// </summary>
        private bool isSceneLoading = false;

        /// <summary>
        /// フェード付きでシーンをロード
        /// </summary>
        /// <param name="sceneName">ロードするシーン名</param>
        /// <param name="overlayImageFadeOutCondition">ロード完了後シーン表示に必要な条件</param>
        public void LoadSceneWithFade(SceneName sceneName, Func<bool> overlayImageFadeOutCondition = null)
        {
            // 非同期なのでコルーチンで処理
            StartCoroutine(LoadSceneWithFadeCoroutine(sceneName, overlayImageFadeOutCondition));
        }

        /// <summary>
        /// LoadSceneWithFadeのコルーチン
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="overlayImageFadeOutCondition"></param>
        /// <returns></returns>
        private IEnumerator LoadSceneWithFadeCoroutine(SceneName sceneName, Func<bool> overlayImageFadeOutCondition)
        {
            if (isSceneLoading)
            {
                // 既にロード中だったら何もしない
                Debug.unityLogger.LogWarning("LoadSceneWithFading", "Now loading scene. Ignore this call.");
                yield break;
            }

            // ロード中フラグをtrueにしておく
            isSceneLoading = true;

            Assert.IsNotNull(overlayImageAnimator);

            // オーバーレイ画像のゲームオブジェクトを有効化
            overlayImageAnimator.gameObject.SetActive(true);

            // オーバーレイ画像のフェードイン開始
            overlayImageAnimator.SetTrigger(FadeInTriggerName);
            yield return null;

            // フェードイン完了待ち
            while (true)
            {
                var info = overlayImageAnimator.GetCurrentAnimatorStateInfo(0);
                if (info.normalizedTime >= 1.0f) break;
                yield return null;
            }

            // シーンロード
            yield return SceneManager.LoadSceneAsync((int)sceneName);

            // オーバーレイ画像のフェードアウトに条件があれば、条件を満たすまで待つ
            if (overlayImageFadeOutCondition != null)
            {
                while (!overlayImageFadeOutCondition())
                {
                    yield return null;
                }
            }

            // オーバーレイ画像のフェードアウト開始
            overlayImageAnimator.SetTrigger(FadeOutTriggerName);
            yield return null;

            // フェードアウト完了待ち
            while (true)
            {
                var info = overlayImageAnimator.GetCurrentAnimatorStateInfo(0);
                if (info.normalizedTime >= 1.0f) break;
                yield return null;
            }

            // オーバーレイ画像のゲームオブジェクトを無効化
            overlayImageAnimator.gameObject.SetActive(false);

            // ロード中フラグをfalseにする
            isSceneLoading = false;
        }
    }
}