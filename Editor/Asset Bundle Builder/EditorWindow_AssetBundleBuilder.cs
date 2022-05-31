#if (UNITY_EDITOR)

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class EditorWindow_AssetBundleBuilder : EditorWindow
{
    #region Variables
    private Vector2 _scrollPosition;
    #region Asset Bundle Assets
    private List<EditorWindow_AssetBundleBuilder_Asset> _assets = new List<EditorWindow_AssetBundleBuilder_Asset>();
    public void RemoveAsset(EditorWindow_AssetBundleBuilder_Asset asset)
    {
        _assets.Remove(asset);
    }
    #endregion
    #region Asset Bundle Name
    private string _assetBundleName = "New Asset Bundle";
    #endregion
    #region Asset Bundle Variant
    private string _assetBundleVariant = "";
    #endregion
    #region Asset Bundle Output Path
    private string _outputPath = "Assets/AssetBundles";
    #endregion
    #region Asset Bundle Build Options
    private BuildAssetBundleOptions _buildAssetBundleOptions = BuildAssetBundleOptions.None;
    #endregion
    #region Asset Bundle Build Target
    private BuildTarget _buildTarget = BuildTarget.StandaloneWindows;
    #endregion
    #endregion

    #region Methods
    #region Menu Item
    [MenuItem("Tools/AssetBundles/Builder")]
    static void Init()
    {
        EditorWindow_AssetBundleBuilder _editorWindow = (EditorWindow_AssetBundleBuilder)EditorWindow.GetWindow(typeof(EditorWindow_AssetBundleBuilder));
        
        _editorWindow.titleContent = new GUIContent("AssetBundle Builder");

        _editorWindow.Show();
    }
    #endregion
    #region OnGUI
    private void OnGUI()
    {
        #region Asset Bundle Assets
        if (_assets == null)
        {
            _assets = new List<EditorWindow_AssetBundleBuilder_Asset>();
        }
        EditorGUILayout.LabelField("Asset References");
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
        for (int _i = 0; _i < _assets.Count; _i++)
        {
            _assets[_i].OnGUI();
        }
        EditorGUILayout.EndScrollView();
        if (GUILayout.Button("Add"))
        {
            _assets.Add(new EditorWindow_AssetBundleBuilder_Asset(this));
        }
        #endregion
        #region Asset Bundle Name
        _assetBundleName = EditorGUILayout.TextField("Asset Bundle Name: ", _assetBundleName);
        #endregion
        #region Asset Bundle Variant
        _assetBundleVariant = EditorGUILayout.TextField("Asset Bundle Variant: ", _assetBundleVariant);
        #endregion
        #region Asset Bundle Output Path
        _outputPath = EditorGUILayout.TextField("Output Path:", _outputPath);
        #endregion
        #region Asset Bundle Build Options
        _buildAssetBundleOptions = (BuildAssetBundleOptions)EditorGUILayout.EnumFlagsField("Build Options", _buildAssetBundleOptions);
        #endregion
        #region Asset Bundle Build Target
        _buildTarget = (BuildTarget)EditorGUILayout.EnumFlagsField("Build Target", _buildTarget);
        #endregion
        if (GUILayout.Button("Build"))
        {
            if (!Directory.Exists(_outputPath))
            {
                Directory.CreateDirectory(_outputPath);
            }

            string[] _addressableNames = new string[_assets.Count];
            string[] _assetNames = new string[_assets.Count];
            for (int _i = 0; _i < _assets.Count; _i++)
            {
                _addressableNames[_i] = _assets[_i].assetName;
                _assetNames[_i] = _assets[_i].assetPath;
            }
            
            AssetBundleBuild[] _assetBundleBuild = new AssetBundleBuild[1];
            _assetBundleBuild[0].assetBundleName = _assetBundleName;
            if (_assetBundleVariant != "")
            {
                _assetBundleBuild[0].assetBundleVariant = _assetBundleVariant;
            }
            _assetBundleBuild[0].addressableNames = _addressableNames;
            _assetBundleBuild[0].assetNames = _assetNames;

            BuildPipeline.BuildAssetBundles(
                _outputPath,
                _assetBundleBuild,
                _buildAssetBundleOptions,
                _buildTarget
            );
        }
    }
    #endregion
    #endregion
}
#endif