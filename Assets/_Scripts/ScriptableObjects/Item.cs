using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace EnterCloudsReach.Inventory
{
    public enum itemType
    {
        Weapon,
        Note,
        Consumable,
        Armor
    }

    [CreateAssetMenu(fileName = "New Item", menuName = "Items/Create New Item")]
    public class Item : ScriptableObject
    {
        public int id;
        public string itemName;
        public int weight;
        public int value;
        public Sprite icon;

        // Notes Params
        [HideInInspector] [SerializeField] public string notes;

        // Consumables Params
        [HideInInspector] [SerializeField] public int consumableUses;

        // Stat Modifiers

        [HideInInspector] [SerializeField] public bool affectStats;
        [HideInInspector] [SerializeField] public int Brawn;
        [HideInInspector] [SerializeField] public int Agility;
        [HideInInspector] [SerializeField] public int Endurance;
        [HideInInspector] [SerializeField] public int Knowledge;
        [HideInInspector] [SerializeField] public int Wisdom;
        [HideInInspector] [SerializeField] public int Charm;

        // Armor params
        [HideInInspector, SerializeField] public slotType equipmentSlot;
        // Weapon params
        [HideInInspector, SerializeField] public GameObject[] attacks;

        [Header("Presets")]
        public itemType itemClass;


#if UNITY_EDITOR

        [CustomEditor(typeof(Item))]
        public class ItemEditor : Editor
        {
            private Item targetItem;

            private string notesText;

            private int consumeableUses;

            private bool affectStats;
            private int Brawn;
            private int Agility;
            private int Endurance;
            private int Knowledge;
            private int Wisdom;
            private int Charm;

            private slotType equipmentSlot;

            public override void OnInspectorGUI()
            {
                targetItem = (Item)target;
                EditorGUI.BeginChangeCheck();

                base.OnInspectorGUI();
                switch (targetItem.itemClass)
                {
                    case itemType.Weapon:
                        Weapon();
                        break;

                    case itemType.Note:
                        Notes();
                        break;

                    case itemType.Consumable:
                        Consumables();
                        break;

                    case itemType.Armor:
                        Armor();
                        break;
                }


                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(targetItem, "Undo Item");
                    targetItem.notes = notesText;
                    targetItem.consumableUses = consumeableUses;
                    targetItem.affectStats = affectStats;
                    targetItem.Brawn = Brawn;
                    targetItem.Agility = Agility;
                    targetItem.Endurance = Endurance;
                    targetItem.Knowledge = Knowledge;
                    targetItem.Wisdom = Wisdom;
                    targetItem.Charm = Charm;
                    targetItem.equipmentSlot = equipmentSlot;
                }
            }
            private void Weapon()
            {
                EditorGUILayout.LabelField("Weapon data");
                equipmentSlot = (slotType)EditorGUILayout.EnumPopup(targetItem.equipmentSlot);
                serializedObject.Update();
                var sO = serializedObject.FindProperty("attacks");
                sO.arraySize = 2;
		        EditorGUILayout.PropertyField(sO);
		        serializedObject.ApplyModifiedProperties();
            }
            private void Notes()
            {
                GUIStyle myStyle = new GUIStyle(EditorStyles.textField);
                myStyle.wordWrap = true;
                
                EditorGUILayout.LabelField("Note Text");
                notesText = EditorGUILayout.TextArea(targetItem.notes, myStyle, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(false), GUILayout.MaxWidth(300), GUILayout.MaxHeight(200));
            }
            private void Consumables()
            {
                EditorGUILayout.LabelField("Consumable Parameters");
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Uses");
                consumeableUses = EditorGUILayout.IntField(targetItem.consumableUses);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Stat Modifications?");
                affectStats = EditorGUILayout.Toggle(targetItem.affectStats);
                EditorGUILayout.EndHorizontal();
                if (affectStats)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Brawn", GUILayout.Width(50));
                    Brawn = EditorGUILayout.IntField(targetItem.Brawn);
                    EditorGUILayout.LabelField("Agility", GUILayout.Width(50));
                    Agility = EditorGUILayout.IntField(targetItem.Agility);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Endurance", GUILayout.Width(70));
                    Endurance = EditorGUILayout.IntField(targetItem.Endurance);
                    EditorGUILayout.LabelField("Knowledge", GUILayout.Width(70));
                    Knowledge = EditorGUILayout.IntField(targetItem.Knowledge);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Wisdom", GUILayout.Width(50));
                    Wisdom = EditorGUILayout.IntField(targetItem.Wisdom);
                    EditorGUILayout.LabelField("Charm", GUILayout.Width(50));
                    Charm = EditorGUILayout.IntField(targetItem.Charm);
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.Space();
            }
            private void Armor()
            {
                EditorGUILayout.LabelField("Armor Parameters");
                equipmentSlot = (slotType)EditorGUILayout.EnumPopup(targetItem.equipmentSlot);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Stat Modifications?");
                affectStats = EditorGUILayout.Toggle(targetItem.affectStats);
                EditorGUILayout.EndHorizontal();
                if (affectStats)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Brawn", GUILayout.Width(50));
                    Brawn = EditorGUILayout.IntField(targetItem.Brawn);
                    EditorGUILayout.LabelField("Agility", GUILayout.Width(50));
                    Agility = EditorGUILayout.IntField(targetItem.Agility);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Endurance", GUILayout.Width(70));
                    Endurance = EditorGUILayout.IntField(targetItem.Endurance);
                    EditorGUILayout.LabelField("Knowledge", GUILayout.Width(70));
                    Knowledge = EditorGUILayout.IntField(targetItem.Knowledge);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Wisdom", GUILayout.Width(50));
                    Wisdom = EditorGUILayout.IntField(targetItem.Wisdom);
                    EditorGUILayout.LabelField("Charm", GUILayout.Width(50));
                    Charm = EditorGUILayout.IntField(targetItem.Charm);
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
#endif
    }
}

