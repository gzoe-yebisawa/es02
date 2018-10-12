/// <summary>
/// ベースゲームマネージャー
/// ゲーム全体の管理をする（シングルトンで常駐）
/// 継承して、アプリに応じた処理を追加してください
/// </summary>
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Gzoe
{
    /// <summary>
    /// BaseGameManager
    /// </summary>
    public class BaseGameManager : MonoBehaviour
    {
        public static readonly string PersistentSceneName = "Scn_Persistent";
        /// <summary>
        /// Persistentシーンを読み込む
        /// アトリビュートでシーン開始時に必ず読み込まれるようにする
        /// PersistentシーンにGameManagerを置くことにより、どのシーンを読み込んでも
        /// BaseGameManagerが常駐した状態になる
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void LoadPersistentScene()
        {
            SceneManager.LoadSceneAsync(PersistentSceneName, LoadSceneMode.Additive);
        }

#if UNITY_EDITOR
        [MenuItem("Tools/Gzoe/Load Persistent Scene Additive")]
        private static void LoadPersistentSceneInEditor()
        {
            EditorSceneManager.OpenScene("Assets/Application/Scenes/Persistent.unity", OpenSceneMode.AdditiveWithoutLoading);
        }
#endif

        /// <summary>
        /// インスタンス
        /// </summary>
        private static BaseGameManager instance = null;

        /// <summary>
        /// 初期化かどうか
        /// </summary>
        private static bool initialized = false;

        /// <summary>
        /// インスタンスのアクセサ
        /// </summary>
        /// <value>instance</value>
        public static BaseGameManager Instance
        {
            get
            {
                Assert.IsNotNull(instance, "BaseGameManager is not created!");
                return instance;
            }
        }

        /// <summary>
        /// インスタンスのアクセサ
        /// 継承先用に型変換を行って返す
        /// </summary>
        /// <typeparam name="T">継承先の型</typeparam>
        /// <returns>instance</returns>
        public static T GetInstance<T>() where T : BaseGameManager
        {
            return instance as T;
        }

        /// <summary>
        /// インスタンスがNULLかチェック
        /// </summary>
        /// <returns>nullならtrue</returns>
        public static bool IsInstanceNull()
        {
            return instance == null;
        }

        public static bool IsInitialized()
        {
            if (IsInstanceNull()) return false;

            return initialized;
        }

        /// <summary>
        /// 初期化処理
        /// Start()内で実行されます
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator Initialize()
        {
            yield break;
        }

        /// <summary>
        /// 終了処理
        /// OnDestory()で実行されます
        /// </summary>
        protected virtual void Terminate()
        {

        }

        /// <summary>
        /// Awakeメッセージ
        /// </summary>
        private void Awake()
        {
            // 複数存在していてはいけない
            Assert.IsNull(instance, "BaseGameManager is already created! Type: " + GetType().Name);

            instance = this;

        }

        /// <summary>
        /// Startイベント
        /// 初期化が完了するまで無効化しておきます
        /// </summary>
        /// <returns></returns>
        private IEnumerator Start()
        {
            DebugUtility.LogEditor("BaseGameManager is initializing ...  Type: " + GetType().Name);
            enabled = false;

            yield return Initialize();

            enabled = true;
            initialized = true;
            DebugUtility.LogEditor("BaseGameManager has initialized.  Type: " + GetType().Name);
            yield break;
        }

        /// <summary>
        /// OnDestroyメッセージ
        /// </summary>
        private void OnDestroy()
        {
            Terminate();
            instance = null;
        }
    }
}