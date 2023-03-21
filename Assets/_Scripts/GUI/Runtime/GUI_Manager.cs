using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.GUI
{
    public delegate void CallbackDelegate(int ID);

    public class GUI_Manager : MonoBehaviour
    {
        public const string GUI_PREFAB = "GUI/GameGUI";
        public static GUI_Manager Manager { get; private set; } = null;
        public static GUI_DialogueBox DialogueBox { get; private set; } = null;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void InitializeGUI()
        {
            Debug.Log("Loading GUI with path \"" + GUI_PREFAB + "\"!");
            GameObject obj = Instantiate(Resources.Load<GameObject>(GUI_PREFAB));
            obj.name = "GameGUI";

            // This is here to avoid it popping up in the build
            Debug.LogWarning("Remove the line below to reveal the GUI");
            obj.GetComponentInChildren<Canvas>().gameObject.SetActive(false);

            Manager = obj.GetComponentInChildren<GUI_Manager>();
            DialogueBox = obj.GetComponentInChildren<GUI_DialogueBox>();
        }

        public static void QueUpDialogue(string[] Text, CallbackDelegate Callback = null)
        {
            DialogueBox.QueUpText(Text, Callback);
        }

        public static void QueUpDialogue(List<string> Text, CallbackDelegate Callback = null)
        {
            QueUpDialogue(Text.ToArray(), Callback);
        }

        public static void QueUpDialogue(string Text, CallbackDelegate Callback = null)
        {
            string[] text = new string[1];
            text[0] = Text;

            QueUpDialogue(text, Callback);
        }
    }
}
