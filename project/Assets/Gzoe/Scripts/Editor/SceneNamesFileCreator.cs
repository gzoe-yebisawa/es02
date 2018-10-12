/// <summary>
/// 
/// </summary>
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Gzoe
{
    public static class SceneNamesFileCreator
    {
        /// <summary>
        /// 出力フィアル名
        /// </summary>
        private static readonly string SceneNamesFileName = "SceneNames.cs";

        /// <summary>
        /// ファイル出力メソッド
        /// エディタのメニューから実行
        /// </summary>
        [MenuItem("Tools/Gzoe/Create Scene Names File")]
        private static void create()
        {
            // 出力先はApplicationフォルダ直下
            string outputFile = Application.dataPath + "/" + "Application/" + SceneNamesFileName;

            //出力ファイルを作成し、書き込み
            using (var sw = new StreamWriter(outputFile))
            {
                // namespace App の enum とする
                sw.WriteLine("/// <summary>");
                sw.WriteLine("/// シーン名用Enum");
                sw.WriteLine("/// SceneNamesFileCreatorが作成しました");
                sw.WriteLine("/// </summary>");
                sw.WriteLine("");
                sw.WriteLine("namespace App");
                sw.WriteLine("{");
                sw.WriteLine("    public enum SceneName");
                sw.WriteLine("    {");
                // BuildSettingsに設定されているシーンのパスを読み取り、C#で有効な形式のEnum名に変換し、書き込み
                for (var i = 0; i < EditorBuildSettings.scenes.Length; ++i)
                {
                    var path = EditorBuildSettings.scenes[i].path;
                    path = Path.GetDirectoryName(path) + "/" + Path.GetFileNameWithoutExtension(path);

                    path = path.Replace('/', '_');

                    // 全パス共通のAssets_は削除
                    path = path.Substring("Assets_".Length);

                    // IDとの対応を明示的にするためIDも付け加えています
                    sw.WriteLine("        " + path + " = " + i + ",");
                }
                sw.WriteLine("    }");
                sw.WriteLine("}");
            }

            // ファイル変更を反映
            AssetDatabase.Refresh();
        }
    }
}