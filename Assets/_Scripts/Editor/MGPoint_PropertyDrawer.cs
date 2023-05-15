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
        private List<Rect> rects = new List<Rect>();
        private int numbOfRects;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Get the index of the element being drawn
            int index = int.Parse(property.propertyPath.Split('[', ']')[1]);
            rects.Clear();

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

                Rect typeRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * rects.Count + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("type"));
                rects.Add(typeRect);

                Rect goRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * rects.Count + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(goRect, property.FindPropertyRelative("prefabToSpawn"));
                rects.Add(goRect);

                Rect vecRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * rects.Count + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(vecRect, property.FindPropertyRelative("position"));
                rects.Add(vecRect);
                /*
                SerializedProperty posProp = property.FindPropertyRelative("position");
                EditorGUI.PropertyField(vecRect, posProp);
                Rect buttonRect = new Rect(position.x + position.width - 55f, vecRect.y, 50f, vecRect.height + EditorGUIUtility.singleLineHeight);
                if (UnityEngine.GUI.Button(buttonRect, new GUIContent("Set", "Set position to current transform"), EditorStyles.miniButton))
                {
                    Transform transform = ((MonoBehaviour)property.serializedObject.targetObject).transform;

                    // Call the SetPositionToCurrentTransform method with the GameObject reference
                    MGPoint point = (MGPoint)fieldInfo.GetValue(property.serializedObject.targetObject);
                    point.SetPositionToCurrentTransform(transform);
                    posProp.vector3Value = point.position;
                }*/

                if (property.FindPropertyRelative("type").enumValueIndex == (int)MGPoint.TypeOfPoint.Slide)
                {
                    Rect endvecRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * rects.Count + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
                    EditorGUI.PropertyField(endvecRect, property.FindPropertyRelative("endPos"));
                    rects.Add(endvecRect);
                }

                Rect floatRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * rects.Count + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(floatRect, property.FindPropertyRelative("whenToSpawn"));
                rects.Add(floatRect);

                EditorGUI.indentLevel--;
                numbOfRects = rects.Count;
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
                height += EditorGUIUtility.singleLineHeight * numbOfRects;
            }

            return height;
        }
    }
}

#endif