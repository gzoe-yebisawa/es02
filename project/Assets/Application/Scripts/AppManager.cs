using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Gzoe;

namespace App
{
    /// <summary>
    /// アプリ管理
    /// </summary>
    public class AppManager : BaseGameManager
    {
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <typeparam name="AppManager"></typeparam>
        /// <returns></returns>
        public static new AppManager Instance { get { return GetInstance<AppManager>(); } }
        
        /// <summary>
        /// シーンローダー
        /// </summary>
        [SerializeField]
        private SceneLoader sceneLoader;

        /// <summary>
        /// 管理者画面オープンボタン
        /// </summary>
        [SerializeField]
        private Button openAdminDialogButton;

        /// <summary>
        /// 管理者画面オープンボタン必要タップ回数
        /// </summary>
        private int adminDialotTapCount = 10;

        /// <summary>
        /// 管理者画面オープンボタン最大タップ間隔
        /// </summary>
        [SerializeField]
        private float adminDialogTime = 1.0f;

        /// <summary>
        /// シーンローダー参照プロパティ
        /// </summary>
        /// <value>sceneLoader</value>
        public SceneLoader SceneLoader { get { return sceneLoader; } }
        
        /// <summary>
        /// セーブ先ディレクトリ
        /// </summary>
        /// <value></value>
        public string SaveDirectory { get { return Application.persistentDataPath + "/Data/"; } }
        

        #region BaseGameManagerのオーバーロード
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <returns></returns>
        protected override IEnumerator Initialize()
        {
            yield break;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        protected override void Terminate()
        {

        }
        #endregion

        #region MonoBehaviourのオーバーロード
        private void Update()
        {
#if UNITY_EDITOR
#endif
        }
        #endregion
    }
}