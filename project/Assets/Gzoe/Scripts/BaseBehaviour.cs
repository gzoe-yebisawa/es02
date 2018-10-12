/// <summary>
/// 基底Behaviour
/// MonoBehaviourにGameManagerの初期化を待つ機能をつけたもの
/// </summary>
using System.Collections;
using UnityEngine;

namespace Gzoe
{
    /// <summary>
    /// 基底Behaviour
    /// </summary>
    public class BaseBehaviour : MonoBehaviour
    {
        /// <summary>
        /// 派生クラス用Start処理
        /// 非同期で処理できるようにIEnumeratorを返す
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator OnStart()
        {
            yield break;
        }

        /// <summary>
        /// Startイベント
        /// GameManagerの初期化が終わるまで待機
        /// </summary>
        /// <returns></returns>
        private IEnumerator Start()
        {
            DebugUtility.LogEditor(GetType().Name + " is initializing ...");

            // 初期化が完了するまで無効化
            enabled = false;

            // GameManagerの初期化待ち
            while (!BaseGameManager.IsInitialized())
            {
                yield return null;
            }

            // オブジェクト固有の初期化
            yield return OnStart();

            // 初期化が完了したので有効化
            enabled = true;

            DebugUtility.LogEditor(GetType().Name + " is initialized.");
            yield break;
        }
    }
}