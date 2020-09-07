
using UnityEngine;

using System.IO;
using System.Text;
using UnityEditor;

// コードの自動生成
public class MVPRU : EditorWindow
{

        private string _baseClassName = string.Empty;
        private string _sceneName = string.Empty;

        
        [MenuItem("Window/MVPRU")]
        private static void Open()
        {
            GetWindow<MVPRU>("MVPRU");
        }


        private void OnGUI()
        {
            EditorGUILayout.LabelField("SceneName");
            _sceneName = GUILayout.TextField(_sceneName);
            EditorGUILayout.LabelField("Create Base Class Name");
            _baseClassName = EditorGUILayout.TextField(_baseClassName);

            if (GUILayout.Button("CreateScript"))
            {
                string path = Application.dataPath;
                string namePath = "Scripts/" + _sceneName + "/";
                path += "/"+namePath;

                
                CreateScriptAsset(_sceneName+".Models", _baseClassName, "Model", path + "/Models",_sceneName);
                //CreateScriptAsset("Script."+_sceneName+".Presenters", _baseClassName, "Presenter", path+ "/Presenters",_sceneName);
                CreateScriptAsset(_sceneName+".Views", _baseClassName, "View", path + "/Views",_sceneName);
                
                Debug.Log($"Create Script Path : {path}");
            }


            if (GUILayout.Button("ClearScript"))
            {
                string path = Application.dataPath;
                string namePath = "script/" + _sceneName + "/";
                path += "/"+namePath;
                
                RemoveScriptAsset(_baseClassName, "Model", path + "/Models");
                RemoveScriptAsset(_baseClassName, "Presenter", path+ "/Presenters");
                RemoveScriptAsset(_baseClassName, "View", path + "/Views");
                SafeCreateDirectory(path + "/ZenjectInstaller/");
                Debug.Log($"Remove Script Path : {path}");
            }

        }

        private const string TemplateScriptFilePath = "ScriptTemplate/";

        private static void CreateScriptAsset(string nameSpace, string baseClassName, string domainName, string filePath,string sceneName)
        {
            string templateRawText = Resources.Load($"{TemplateScriptFilePath}{domainName}.cs").ToString();
            string replacedText = templateRawText.Replace("#SCRIPTNAME#", baseClassName).Replace("#NAMESPACE", nameSpace).Replace("#SCRIPTSCENENAME", sceneName);
            var encoding = new UTF8Encoding(true, false);

            if (Path.GetExtension(filePath) != "")
            {
                // If you select Non directory, then get parent directory.
                filePath = Directory.GetParent(filePath).FullName + "/";
            }

            SafeCreateDirectory(filePath);
            filePath += "/";

            string fileName = $"{baseClassName}{domainName}.cs";
            File.WriteAllText(filePath + fileName, replacedText, encoding);

            var createdScript = AssetDatabase.LoadAssetAtPath<MonoScript>(filePath + fileName);
            ProjectWindowUtil.ShowCreatedAsset(createdScript);
            AssetDatabase.Refresh();
        }

        private static void RemoveScriptAsset( string baseClassName, string domainName, string filePath)
        {
            if (Path.GetExtension(filePath) != "")
            {
                // If you select Non directory, then get parent directory.
                filePath = Directory.GetParent(filePath).FullName + "/";
            }
            filePath += "/";

            string fileName = $"{baseClassName}{domainName}.cs";

            File.Delete(filePath + fileName);

            AssetDatabase.Refresh();
            
        }
        
        
        private static DirectoryInfo SafeCreateDirectory( string path )
        {
            return Directory.Exists( path ) ? null : Directory.CreateDirectory( path );
        }
}