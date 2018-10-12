/// <summary>
/// デバッグ用ユーティリティ
/// </summary>
using System.Diagnostics;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gzoe
{
#if UNITY_EDITOR
    /// <summary>
    /// ユーティリティー用データ
    /// コンパイル前後でもデータを保持させるためにScriptableSingletonとしています
    /// </summary>
    /// <typeparam name="DebugUtilityData"></typeparam>
    public class DebugUtilityData : ScriptableSingleton<DebugUtilityData>
    {
        /// <summary>
        /// LogEditorがアクティブか？
        /// instanceを明示的に書かなくても済むように用意
        /// </summary>
        /// <value>instance.isLogEditorActive</value>
        static public bool IsLogEditorActive
        {
            get
            {
                return instance.isLogEditorActive;
            }

            set
            {
                instance.isLogEditorActive = value;
            }
        }

        /// <summary>
        /// LogEditorが有効か？
        /// </summary>
        private bool isLogEditorActive = false;
    }
#endif

    static public class DebugUtility
    {
#if UNITY_EDITOR
        /// <summary>
        /// LogEditorのメニューパス
        /// </summary>
        static private readonly string LogEditorMenuPath = "Tools/Gzoe/LogEditor";
        /// <summary>
        /// LogEditorのタグ名
        /// </summary>
        static private readonly string LogEditorTagName = "[LogEditor]";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static DebugUtility()
        {
            Menu.SetChecked(LogEditorMenuPath, DebugUtilityData.IsLogEditorActive);
        }

        /// <summary>
        /// LogEditorの有効無効切り替え
        /// </summary>
        [MenuItem("Tools/Gzoe/LogEditor")]
        static void ToggleLogEditor()
        {
            DebugUtilityData.IsLogEditorActive = !Menu.GetChecked(LogEditorMenuPath);
            Menu.SetChecked(LogEditorMenuPath, DebugUtilityData.IsLogEditorActive);
        }
#endif

        /// <summary>
        /// エディタ専用ログ
        /// 実装テスト時用に使われることを想定
        /// メニューで有効無効を切り替えられる
        /// </summary>
        /// <param name="msg">ログメッセージ</param>
        [Conditional("DEBUG")]
        static public void LogEditor(string msg)
        {
#if UNITY_EDITOR
            if (!DebugUtilityData.IsLogEditorActive) return;
            UnityEngine.Debug.unityLogger.Log(LogEditorTagName + "[" + Time.frameCount + "]", msg);
#endif
        }
    }
}