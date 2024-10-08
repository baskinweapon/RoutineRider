﻿using UnityEngine;
using System;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "SettingsAsset", menuName = "Game/Settings", order = 0)]
public class SettingsAsset : ScriptableObject {
	[SerializeField]
	public GameSettings serializable;
	
	[Header("other info")]
	public string path;
	public string saveName = "save.save";
	
	[Header("debug")]
	public bool debugMode;

	private void OnEnable() {
		path = Application.streamingAssetsPath + $"/settings/{saveName}";
#if UNITY_EDITOR
		path = Application.streamingAssetsPath + $"/settings/{saveName}";
#endif
	}

	private bool FileCheck() {
		if (string.IsNullOrEmpty(path)) OnEnable();

		bool b = File.Exists(path);
		if (!b) {
			if (!Directory.Exists(path)) {
				Directory.CreateDirectory(Path.GetDirectoryName(path) ?? string.Empty);
			}

			File.Create(path).Close();
		}

		return b;
	}

	private void CreateNewSave() {
		SetDefaultSave();

		string repath = path.Replace(saveName, $"broken_save_{DateTime.Now:yy_MM_dd_hhmmss}.th");

		File.Move(path, repath);
	}

	public void SetDefaultSave() {
		serializable = new GameSettings { };
		SaveToFile();
	}
	
	public void LoadFromFile() {
		if (!FileCheck()) {
			SetDefaultSave();
		} else {
			string json;
			using (StreamReader reader = new StreamReader(path)) {
				json = reader.ReadToEnd();
			}

			try {
				serializable = JsonUtility.FromJson<GameSettings>(json);
			}
			catch (Exception e) {
				Debug.LogError("Error: " + e.Message);
				CreateNewSave();
			}
		}
	}
	
	public void ApplyAssetToFile() {
#if UNITY_EDITOR
#endif
		SaveToFile();
	}


	public void SaveToFile() {
		FileCheck();

		using (StreamWriter writer = new StreamWriter(path)) {
			var json = JsonUtility.ToJson(serializable, true);
			writer.Write(json);
			writer.Flush();
		}
		
		Debug.Log("File Saved");
	}
}


#if UNITY_EDITOR

[CustomEditor(typeof(SettingsAsset))]
public class SettingsAssetEditor : Editor {
	public override void OnInspectorGUI() {
		var me = target as SettingsAsset;

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("LoadFromFile")) {
			if (me != null) me.LoadFromFile();
		}

		if (GUILayout.Button("Open settings Folder")) {
			if (me != null) EditorUtility.RevealInFinder(me.path);
		}

		if (GUILayout.Button("SaveToFile")) {
			AssetDatabase.Refresh();
			EditorUtility.SetDirty(target);
			AssetDatabase.SaveAssets();
			if (me != null) me.ApplyAssetToFile();
		}

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space();

		base.OnInspectorGUI();

		EditorGUILayout.Space();


		if (GUILayout.Button("ClearAll")) {
			if (me != null) me.SetDefaultSave();
		}
		
		EditorGUILayout.Space();


		if (GUILayout.Button("SaveAsset")) {
			AssetDatabase.Refresh();
			EditorUtility.SetDirty(target);
			AssetDatabase.SaveAssets();
		}
		
		EditorGUILayout.Space();
	}
}

#endif
