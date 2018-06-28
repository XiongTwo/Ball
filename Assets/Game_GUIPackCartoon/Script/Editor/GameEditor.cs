using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class GameEditor : EditorWindow {

    [MenuItem("GameEditor/EditorWindow")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        GameEditor window = (GameEditor)EditorWindow.GetWindow(typeof(GameEditor));
        window.Show();
    }
    void OnGUI()
    {
        if (GUILayout.Button("Create Wall"))
        {
            Manage.Instance.Create_Wall();
        }
        if (GUILayout.Button("Create Stick"))
        {
            Manage.Instance.Create_Stick();
        }
        if (GUILayout.Button("Create Ball"))
        {
            Manage.Instance.Create_Ball();
        }
        if (GUILayout.Button("Create Star"))
        {
            Manage.Instance.Create_Star();
        }
        if (GUILayout.Button("Save Config"))
        {
            string _config = Manage.Instance.Sava_Config();
            string _path=EditorUtility.SaveFilePanel("Save Config", "Assets/Resources/Config","","txt");
            Debug.LogError(_path);
            /*if (File.Exists(_path))
            {
                File.Delete(_path);
                Debug.LogError("Delete:" + _path);
            }*/
            StreamWriter _StreamWriter = File.CreateText(_path);
            _StreamWriter.Write(_config);
            _StreamWriter.Close();
            UnityEditor.AssetDatabase.Refresh();
        }
        if (GUILayout.Button("Load Config"))
        {
            string _path = EditorUtility.OpenFilePanel("Load Config", "Assets/Resources/Config", "txt");
            StreamReader _StreamReader = File.OpenText(_path);
            string _config = _StreamReader.ReadToEnd();
            Manage.Instance.Create_Game(_config);
        }
        if (GUILayout.Button("isKinematic"))
        {
            Rigidbody2D[] _Rigidbody2D = GameObject.FindObjectsOfType<Rigidbody2D>();
            for (int i = 0; i < _Rigidbody2D.Length; i++)
            {
                _Rigidbody2D[i].isKinematic = !_Rigidbody2D[i].isKinematic;
            }
        }
        if (GUILayout.Button("Delete Game"))
        {
            Manage.Instance.Delete_Game();
        }
        if (GUILayout.Button("Delete PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
        }
        if (GUILayout.Button("Test"))
        {
            Wall[] Wall = GameObject.FindObjectsOfType<Wall>();
            Debug.LogError(Wall.Length);
        }
    }
}
