#if (UNITY_EDITOR)

using UnityEngine;
using UnityEditor;

public class EditorWindow_AssetBundleBuilder_Asset
{
    #region Variables
    #region Editor Window
    private EditorWindow_AssetBundleBuilder _editorWindow;
    #endregion
    #region Dropdown
    private bool _dropdown = true;
    #endregion
    #region Asset Name
    private string _assetName = "Asset Name";
    public string assetName { get { return _assetName; } set { _assetName = value; } }
    #endregion
    #region Asset Path
    private string _assetPath = "Asset Path";
    public string assetPath { get { return _assetPath; } set { _assetPath = value; } }
    #endregion
    #endregion

    #region Constructors
    public EditorWindow_AssetBundleBuilder_Asset(EditorWindow_AssetBundleBuilder editorWindow)
    {
        _editorWindow = editorWindow;
    }
    #endregion

    #region Methods
    #region OnGUI
    public void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        _dropdown = EditorGUILayout.Foldout(_dropdown, _assetName);
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button(EditorGUIUtility.IconContent("d_clear"), GUILayout.Width(20), GUILayout.Height(16)))
        {
            _editorWindow.RemoveAsset(this);
        }
        GUI.backgroundColor = Color.white;
        EditorGUILayout.EndHorizontal();
        if (_dropdown)
        {
            _assetName = EditorGUILayout.TextField("Asset Name", _assetName);
            _assetPath = EditorGUILayout.TextField("Asset Path", _assetPath);

            EditorGUILayout.Space();
        }
    }
    #endregion
    #endregion
}
#endif