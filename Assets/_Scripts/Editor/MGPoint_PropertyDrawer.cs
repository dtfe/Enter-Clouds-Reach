using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
namespace EnterCloudsReach.Combat
{
    [CustomPropertyDrawer(typeof(MGPoint))]
    public class CustomDrawerEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Get the index of the element being drawn
            int index = int.Parse(property.propertyPath.Split('[', ']')[1]);

            // Set the label for the element
            GUIContent elementLabel;

            if (property.FindPropertyRelative("showParameters").boolValue)
            {
                elementLabel = new GUIContent("Point " + (index + 1));
            }
            else
            {
                elementLabel = new GUIContent("Point " + (index + 1) + " (" + property.FindPropertyRelative("whenToSpawn").floatValue + ")");
            }

            // Draw the foldout toggle
            Rect foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, elementLabel);

            // Draw the parameters if the foldout is expanded and showParameters is true
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;

                Rect goRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(goRect, property.FindPropertyRelative("prefabToSpawn"));

                Rect vecRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(vecRect, property.FindPropertyRelative("position"));

                Rect floatRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 3, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(floatRect, property.FindPropertyRelative("whenToSpawn"));

                EditorGUI.indentLevel--;
            }

            // Update the showParameters bool field
            var showParamsProp = property.FindPropertyRelative("showParameters");
            showParamsProp.boolValue = property.isExpanded;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight;

            if (property.isExpanded)
            {
                height += EditorGUIUtility.singleLineHeight * 3;
            }

            return height;
        }
    }
}

#endif