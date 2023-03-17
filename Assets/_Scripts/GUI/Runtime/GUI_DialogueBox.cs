using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace EnterCloudsReach.GUI
{
    public class GUI_DialogueBox : MonoBehaviour
    {
        [Header("Referances")]
        [SerializeField] private RectTransform dialogueBox;
        [SerializeField] private TMP_Text dialogueText;

        [Header("Settings")]
        [SerializeField] private AnimationCurve animationInCurve = new AnimationCurve(new Keyframe[2] { new Keyframe(0, -1), new Keyframe(1, 1) });
        [SerializeField, Min(0)] private float animationInLength = 1;
        [SerializeField] private AnimationCurve animationOutCurve = new AnimationCurve(new Keyframe[2] { new Keyframe(0, 1), new Keyframe(1, -1) });
        [SerializeField, Min(0)] private float animationOutLength = 1;
        [SerializeField] private float dialogueTypeSpeed = 0.025f;

        private List<string[]> dialogue = new List<string[]>(); // { "A QUICK BROWN FOX JUMPS OVER THE LAZY DOG\na quick brown fox jumps over the lazy dog", "A QUICK BROWN FOX JUMPS OVER THE LAZY DOG\na quick brown fox jumps over the lazy dog", "A QUICK BROWN FOX JUMPS OVER THE LAZY DOG\na quick brown fox jumps over the lazy dog", };
        private List<CallbackDelegate> callbacks = new List<CallbackDelegate>();

        private float timer = 0;
        private bool open = false;

        public void QueUpText(string[] Text, CallbackDelegate Callback)
        {
            dialogue.Insert(dialogue.Count, Text);
            callbacks.Insert(callbacks.Count, Callback);
        }

        public IEnumerator Start()
        {
            while (true)
            {
                if (dialogue.Count != callbacks.Count)
                {
                    Debug.LogError("Holly fuck some real bad shit is happening!");
                    dialogue = new List<string[]>();
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
                            yield return new WaitForSeconds(dialogueTypeSpeed);

                            if (dialogueText.text.Length > 7 && Input.GetKey(KeyCode.Return) || Input.GetMouseButton(0))
                            {
                                dialogueText.text = text;
                                break;
                            }
                        }

                        yield return new WaitForSeconds(dialogueTypeSpeed);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0));
                    }

                    if (callbacks[0] != null)
                    {
                        callbacks[0].Invoke(0);
                    }

                    dialogue.RemoveAt(0);
                    callbacks.RemoveAt(0);
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
        }
    }
}