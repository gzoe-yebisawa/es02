using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;
using Gzoe;

namespace App
{
    /// <summary>
    /// WWWでロードするテクスチャの管理クラス
    /// </summary>
    public class WwwTextureManager : BaseBehaviour
    {
        /// <summary>
        /// WWWリクエスト
        /// </summary>
        private class WwwRequest
        {
            /// <summary>
            /// 参照カウント
            /// </summary>
            public int refCount = 0;

            /// <summary>
            /// WWW
            /// </summary>
            public WWW www = null;
        }

        /// <summary>
        /// リクエストキャッシュ
        /// </summary>
        /// <typeparam name="string">WWWパス</typeparam>
        /// <typeparam name="WwwRequest">リクエスト</typeparam>
        /// <returns></returns>
        private Dictionary<string, WwwRequest> requestCache = new Dictionary<string, WwwRequest>();

        /// <summary>
        /// テクスチャをロード
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="onLoaded">ロード完了後のコールバック</param>
        /// <returns></returns>
        public IEnumerator LoadTexture(string path, UnityAction<Texture> onLoaded)
        {
            if (requestCache.ContainsKey(path))
            {
                // キャッシュに既にリクエストがあれば、キャッシュから取得する
                var req = requestCache[path];
                // 参照カウントは1以上のはず
                Assert.IsTrue(req.refCount >= 1);

                // 参照カウント増加
                req.refCount++;

                var www = req.www;

                // wwwが完了済みか？
                if (requestCache[path].www.isDone)
                {
                    // 完了済みなのでコールバックに渡す
                    onLoaded(www.texture);
                    yield break;
                }
                else
                {
                    // 未完了なので待つ
                    while (!www.isDone)
                    {
                        yield return null;
                    }
                    onLoaded(www.texture);
                }
            }
            else
            {
                // キャッシュに無いのでリクエストを作成
                var req = new WwwRequest();
                req.refCount = 1;
                req.www = new WWW(path);

                // キャッシュに追加
                requestCache[path] = req;

                // ロード完了まで待つ
                yield return req.www;

                onLoaded(req.www.texture);
            }
        }

        /// <summary>
        /// テクスチャアンロード
        /// </summary>
        /// <param name="path">パス</param>
        public void UnloadTexture(string path)
        {
            // キャッシュに無いパスなら何もしない
            if (!requestCache.ContainsKey(path))
            {
                return;
            }

            var req = requestCache[path];
            Assert.IsTrue(req.refCount > 0);
            // 参照カウントを減らす
            req.refCount--;

            // 参照カウントが0なら完全にアンロード
            if (req.refCount == 0)
            {
                requestCache.Remove(path);
                StartCoroutine(WaitForWwwDoneAndDispose(req.www));
            }
        }

        /// <summary>
        /// ロード中のWWWの完了を待ってからDispose
        /// </summary>
        /// <param name="www"></param>
        /// <returns></returns>
        private IEnumerator WaitForWwwDoneAndDispose(WWW www)
        {
            while (!www.isDone)
            {
                yield return null;
            }

            www.Dispose();
        }
    }
}