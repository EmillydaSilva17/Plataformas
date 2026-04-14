using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioPlayer), true)]
public class AudioPlayerEditor : Editor
{
	SerializedProperty clipsProp;
	SerializedProperty audioSourcesProp;
	SerializedProperty selectedIndexProp;

	void OnEnable()
	{
		clipsProp = serializedObject.FindProperty("clips");
		audioSourcesProp = serializedObject.FindProperty("audioSources");
		selectedIndexProp = serializedObject.FindProperty("selectedIndex");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(audioSourcesProp, true);
		EditorGUILayout.PropertyField(clipsProp, true);

		EditorGUILayout.PropertyField(selectedIndexProp);

		EditorGUILayout.Space();

		EditorGUILayout.LabelField("Playback Controls (Play Mode only)", EditorStyles.boldLabel);

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Play")) PlayButtonPressed();
		if (GUILayout.Button("Stop")) StopButtonPressed();
		if (GUILayout.Button("Pause")) PauseButtonPressed();
		if (GUILayout.Button("Resume")) ResumeButtonPressed();
		EditorGUILayout.EndHorizontal();

		if (!EditorApplication.isPlaying)
		{
			EditorGUILayout.HelpBox("Playback controls work only in Play Mode. Enter Play Mode to use them.", MessageType.Info);
		}

		serializedObject.ApplyModifiedProperties();
	}

	private AudioPlayer GetTarget()
	{
		return target as AudioPlayer;
	}

	private void PlayButtonPressed()
	{
		if (!EditorApplication.isPlaying)
		{
			Debug.LogWarning("Enter Play Mode to use playback controls.");
			return;
		}
		var player = GetTarget();
		if (player == null) return;
		player.PlaySelected();
	}

	private void StopButtonPressed()
	{
		if (!EditorApplication.isPlaying)
		{
			Debug.LogWarning("Enter Play Mode to use playback controls.");
			return;
		}
		var player = GetTarget();
		if (player == null) return;
		player.StopSelected();
	}

	private void PauseButtonPressed()
	{
		if (!EditorApplication.isPlaying)
		{
			Debug.LogWarning("Enter Play Mode to use playback controls.");
			return;
		}
		var player = GetTarget();
		if (player == null) return;
		player.PauseSelected();
	}

	private void ResumeButtonPressed()
	{
		if (!EditorApplication.isPlaying)
		{
			Debug.LogWarning("Enter Play Mode to use playback controls.");
			return;
		}
		var player = GetTarget();
		if (player == null) return;
		player.ResumeSelected();
	}
}