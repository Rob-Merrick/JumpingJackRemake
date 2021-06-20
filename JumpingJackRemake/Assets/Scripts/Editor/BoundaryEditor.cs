using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Boundary))]
public class BoundaryEditor : Editor
{
	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		SerializedProperty horizontalBoundaryModeProperty = serializedObject.FindProperty("_horizontalBoundaryMode");
		SerializedProperty verticalBoundaryModeProperty = serializedObject.FindProperty("_verticalBoundaryMode");
		SerializedProperty horizontalWrapModeProperty = serializedObject.FindProperty("_horizontalArea");
		SerializedProperty verticalWrapModeProperty = serializedObject.FindProperty("_verticalArea");
		SerializedProperty leftEdgeOverrideProperty = serializedObject.FindProperty("_leftEdgeOverride");
		SerializedProperty rightEdgeOverrideProperty = serializedObject.FindProperty("_rightEdgeOverride");
		SerializedProperty bottomEdgeOverrideProperty = serializedObject.FindProperty("_bottomEdgeOverride");
		SerializedProperty topEdgeOverrideProperty = serializedObject.FindProperty("_topEdgeOverride");
		SerializedProperty leftEdgeCallbackProperty = serializedObject.FindProperty("_leftEdgeCallback");
		SerializedProperty rightEdgeCallbackProperty = serializedObject.FindProperty("_rightEdgeCallback");
		SerializedProperty bottomEdgeCallbackProperty = serializedObject.FindProperty("_bottomEdgeCallback");
		SerializedProperty topEdgeCallbackProperty = serializedObject.FindProperty("_topEdgeCallback");
		EditorGUILayout.PropertyField(horizontalBoundaryModeProperty);
		EditorGUILayout.PropertyField(verticalBoundaryModeProperty);
		EditorGUILayout.PropertyField(horizontalWrapModeProperty);

		if(horizontalWrapModeProperty.intValue == (int) ScreenRegion.Custom)
		{
			EditorGUILayout.PropertyField(leftEdgeOverrideProperty);
			EditorGUILayout.PropertyField(rightEdgeOverrideProperty);
			WarnOnBadInput(Boundary.IsOverrideValid(ScreenRegion.Custom, leftEdgeOverrideProperty.floatValue, rightEdgeOverrideProperty.floatValue), "The left edge must be less than the right");
		}

		EditorGUILayout.PropertyField(verticalWrapModeProperty);

		if(verticalWrapModeProperty.intValue == (int) ScreenRegion.Custom)
		{
			EditorGUILayout.PropertyField(bottomEdgeOverrideProperty);
			EditorGUILayout.PropertyField(topEdgeOverrideProperty);
			WarnOnBadInput(Boundary.IsOverrideValid(ScreenRegion.Custom, bottomEdgeOverrideProperty.floatValue, topEdgeOverrideProperty.floatValue), "The bottom edge must be less than the top edge");
		}

		EditorGUILayout.PropertyField(leftEdgeCallbackProperty);
		EditorGUILayout.PropertyField(rightEdgeCallbackProperty);
		EditorGUILayout.PropertyField(bottomEdgeCallbackProperty);
		EditorGUILayout.PropertyField(topEdgeCallbackProperty);
		serializedObject.ApplyModifiedProperties();
	}

	private void WarnOnBadInput(bool isInputValid, string warning)
	{
		if(!isInputValid)
		{
			Color previousColor = GUI.contentColor;
			GUI.contentColor = Color.red;
			EditorGUILayout.LabelField(warning);
			GUI.contentColor = previousColor;
		}
	}
}
