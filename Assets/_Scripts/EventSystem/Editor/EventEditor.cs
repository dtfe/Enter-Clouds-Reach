using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EnterCloudsReach.EventSystem
{
    [CustomEditor(typeof(EventClass), true)]
    public class EventEditor : Editor
    {
        private EventClass script;
        private bool eventFold = true;
        private bool otherFold = true;

        public override void OnInspectorGUI()
        {
            script = (EventClass)target;

            EditorGUI.BeginChangeCheck();
            serializedObject.UpdateIfRequiredOrScript();

            SerializedProperty property = serializedObject.GetIterator();
            bool expanded = true;

            while (property.NextVisible(expanded))
            {
                if ("m_Script" == property.propertyPath)
                {
                    GUI.enabled = false;
                    EditorGUILayout.PropertyField(property, true);
                    EditorGUILayout.Space();
                    GUI.enabled = true;

                    eventFold = EditorGUILayout.Foldout(eventFold, "Event Settings", EditorStyles.foldoutHeader);
                    if (eventFold)
                    {
                        EditorGUI.indentLevel += 1;
                        DrawCustomGUI();
                        EditorGUI.indentLevel -= 1;
                    }

                    otherFold = EditorGUILayout.Foldout(otherFold, "Other Settings", EditorStyles.foldoutHeader);
                }
                else
                {
                    if (otherFold)
                    {
                        EditorGUI.indentLevel += 1;
                        EditorGUILayout.PropertyField(property, true);
                        EditorGUI.indentLevel -= 1;
                    }
                }

                expanded = false;
            }

            serializedObject.ApplyModifiedProperties();

            if (EditorGUI.EndChangeCheck())
            {
                //Undo.RecordObject(target, "Modified EventClass in " + script.gameObject.name);
            }
        }

        private void DrawCustomGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Event Name", GUILayout.Width(100));
            if (script.eventName == "{ This Is A Specific Name To Be Replaced By Editor (DoNotRemove!) }")
            {
                script.eventName = script.gameObject.name;
            }
            script.eventName = EditorGUILayout.TextField(script.eventName);
            EditorGUILayout.LabelField("Text Boxes", GUILayout.Width(100));
            int boxes = Mathf.Max(EditorGUILayout.IntField(script.eventText.Length), 1);
            EditorGUILayout.EndHorizontal();

            if (boxes != script.eventText.Length)
            {
                script.eventText = ResizeArray(script.eventText, boxes);
            }
            for (int i = 0; i < script.eventText.Length; i++)
            {
                script.eventText[i] = EditorGUILayout.TextField(script.eventText[i]);
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(11));

            if (GUILayout.Button("Add Event Choice") && script.eventChoices.Length < 5)
            {
                Undo.RecordObject(target, "Added Event Choice in " + script.gameObject.name);
                script.eventChoices = ResizeArray(script.eventChoices, script.eventChoices.Length + 1);
            }

            if (GUILayout.Button("Remove Event Choice") && script.eventChoices.Length > 1)
            {
                Undo.RecordObject(target, "Removed Event Choice in " + script.gameObject.name);
                script.eventChoices = ResizeArray(script.eventChoices, script.eventChoices.Length - 1);
            }

            EditorGUILayout.EndHorizontal();

            // Make sure that there is at least one Event and draww all of them
            if (script.eventChoices.Length <= 0)
            {
                script.eventChoices = new EventClass[1];
            }

            EditorGUILayout.BeginHorizontal();
            int indent = EditorGUI.indentLevel;

            for (int i = 0; i < script.eventChoices.Length; i++)
            {
                if (i == 1)
                {
                    EditorGUI.indentLevel = 0;
                }

                script.eventChoices[i] = (EventClass)EditorGUILayout.ObjectField(script.eventChoices[i], typeof(EventClass), true);
            }

            EditorGUI.indentLevel = indent;
            EditorGUILayout.EndHorizontal();

            // Add space at the end
            EditorGUILayout.Space();
        }

        private T[] ResizeArray<T>(T[] Array, int Length)
        {
            T[] strings = new T[Length];

            if (Array.Length > 0)
            {
                for (int i = 0; i < Length; i++)
                {
                    strings[i] = Array[Mathf.Clamp(i, 0, Array.Length - 1)];
                }
            }

            return strings;
        }
    }
}
