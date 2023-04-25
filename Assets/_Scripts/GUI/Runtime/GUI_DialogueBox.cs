using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using EnterCloudsReach.EventSystem;

#if UNITY_EDITOR
using UnityEditor.Presets;
#endif

namespace EnterCloudsReach.GUI
{
    public class GUI_DialogueBox : MonoBehaviour
    {
        [Header("Referances")]
        [SerializeField] private GameObject dialogueEventPrefab;
        [SerializeField] private Transform dialogueEventParent;
        [SerializeField] private RectTransform dialogueBox;
        [SerializeField] private TMP_Text dialogueText;

        [Header("Settings")]
        [SerializeField] private AnimationCurve animationInCurve = new AnimationCurve(new Keyframe[2] { new Keyframe(0, -1), new Keyframe(1, 1) });
        [SerializeField, Min(0)] private float animationInLength = 1;
        [SerializeField] private AnimationCurve animationOutCurve = new AnimationCurve(new Keyframe[2] { new Keyframe(0, 1), new Keyframe(1, -1) });
        [SerializeField, Min(0)] private float animationOutLength = 1;
        [SerializeField] private float dialogueTypeSpeed = 0.025f;

        private List<string[]> dialogue = new List<string[]>(); // { "A QUICK BROWN FOX JUMPS OVER THE LAZY DOG\na quick brown fox jumps over the lazy dog", "A QUICK BROWN FOX JUMPS OVER THE LAZY DOG\na quick brown fox jumps over the lazy dog", "A QUICK BROWN FOX JUMPS OVER THE LAZY DOG\na quick brown fox jumps over the lazy dog", };
        private List<string[]> commands = new List<string[]>();
        private List<CallbackDelegate> callbacks = new List<CallbackDelegate>();

        private List<GUI_DialogueEvent> events = new List<GUI_DialogueEvent>();
        public int eventReturnIndex = -1;

        private float timer = 100;
        private bool open = false;
        private bool pressed = false;
        [SerializeField] private TMP_Text rollC;
        [SerializeField] private TMP_Text rollToBeat;
        [HideInInspector] public EventClass rollEvent;
        [HideInInspector] public int indexRoll;
        public GameObject RollPopUP;

        public void QueUpText(string Text)
        {
            string[] text = new string[1];
            text[0] = Text;

            QueUpText(text);
        }

        public void QueUpText(string[] Text)
        {
            for (int i = 0; i < Text.Length; i++)
            {
                Text[i] = Text[i].Replace("\\n", "\n");
            }

            dialogue.Insert(dialogue.Count, Text);
            commands.Insert(commands.Count, new string[0]);
            callbacks.Insert(callbacks.Count, null);
        }

        public void QueUpText(string Text, string[] Commands, CallbackDelegate Callback)
        {
            string[] text = new string[1];
            text[0] = Text;

            QueUpText(text, Commands, Callback);
        }

        public void QueUpText(string[] Text, string[] Commands, CallbackDelegate Callback)
        {
            for (int i = 0; i < Text.Length; i++)
            {
                // All variations of spaces to remove
                Text[i] = Text[i].Replace(" \\n ", "\n");
                Text[i] = Text[i].Replace(" \\n", "\n");
                Text[i] = Text[i].Replace("\\n ", "\n");
                Text[i] = Text[i].Replace("\\n", "\n");
            }

            dialogue.Insert(dialogue.Count, Text);
            commands.Insert(commands.Count, Commands);
            callbacks.Insert(callbacks.Count, Callback);
        }

        public IEnumerator Start()
        {
            while (true)
            {
                if (dialogue.Count != commands.Count || dialogue.Count != callbacks.Count || commands.Count != callbacks.Count)
                {
                    Debug.LogError("Holly fuck some real bad shit is happening!");
                    dialogue = new List<string[]>();
                    commands = new List<string[]>();
                    callbacks = new List<CallbackDelegate>();
                    continue;
                }

                if (dialogue.Count > 0 && open && timer > animationInLength)
                {
                    foreach (string text in dialogue[0])
                    {
                        dialogueText.text = "";

                        foreach (char letter in text)
                        {
                            dialogueText.text += letter;
                            if (letter != ' ')
                            {
                                yield return new WaitForSeconds(dialogueTypeSpeed);
                            }

                            if (!pressed && (Input.GetKey(KeyCode.Return) || Input.GetMouseButton(0)))
                            {
                                pressed = true;
                                dialogueText.text = text;
                                break;
                            }
                        }

                        if (!pressed)
                        {
                            yield return new WaitForSeconds(dialogueTypeSpeed * 3);
                        }

                        yield return new WaitUntil(() => !pressed && (Input.GetKey(KeyCode.Return) || Input.GetMouseButton(0)));
                        pressed = true;
                    }

                    if (callbacks[0] != null)
                    {
                        if (commands[0].Length > 0)
                        {
                            eventReturnIndex = -1;
                            for (int i = 0; i < commands[0].Length; i++)
                            {
                                SetupEvent(i, commands[0][i]);
                            }

                            yield return new WaitUntil(() => eventReturnIndex > -1);

                            try
                            {
                                callbacks[0].Invoke(eventReturnIndex);
                            }
                            catch (System.Exception)
                            {
                                Debug.LogWarning("No next event found!");
                            }
                        }
                        else
                        {
                            try
                            {
                                callbacks[0].Invoke(0);
                            }
                            catch (System.Exception)
                            {
                                Debug.LogWarning("No next event found!");
                            }
                        }
                    }

                    dialogue.RemoveAt(0);
                    commands.RemoveAt(0);
                    callbacks.RemoveAt(0);

                    foreach (GUI_DialogueEvent dialogueevent in events)
                    {
                        dialogueevent.gameObject.SetActive(false);
                    }
                }
                yield return new WaitForSecondsRealtime(0.25f);
            }
        }

        public void Update()
        {
            if (open && timer > animationInLength && dialogue.Count <= 0)
            {
                open = false;
                timer = 0;
            }
            else if (!open && timer > animationOutLength && dialogue.Count > 0)
            {
                dialogueText.text = "";
                open = true;
                timer = 0;

                foreach (GUI_DialogueEvent dialogueevent in events)
                {
                    dialogueevent.gameObject.SetActive(false);
                }
            }

            if (open)
            {
                dialogueBox.anchoredPosition = new Vector2(0, animationInCurve.Evaluate(timer) * 133);
            }
            else
            {
                dialogueBox.anchoredPosition = new Vector2(0, animationOutCurve.Evaluate(timer) * 133);
            }

            timer += Time.deltaTime;

            if (pressed && Input.GetKeyUp(KeyCode.Return) || Input.GetMouseButtonUp(0))
            {
                pressed = false;
            }
        }

        private void SetupEvent(int Index, string EventName)
        {
            if (events.Count == Index)
            {
                GameObject obj = Instantiate(dialogueEventPrefab, dialogueEventParent);
                events.Add(obj.GetComponent<GUI_DialogueEvent>());
            }
            events[Index].Initialize(EventName, Index);
            events[Index].gameObject.SetActive(true);
        }
        public void SetRollCheckInfo(string txt)
        {
            StartCoroutine(rollinfodelay(txt));
        }
        IEnumerator rollinfodelay(string txt)
        {
            yield return new WaitForSeconds(1f);
            rollC.text = txt;
            rollToBeat.gameObject.SetActive(true);
        }
        public void EmptyRollInfo()
        {
        rollC.SetText(""); 
        rollToBeat.gameObject.SetActive(false);
        }
        public void RollEvent(EventClass rollevent, int i)
        {
            rollEvent = rollevent;
            indexRoll = i;
        }
    }
}