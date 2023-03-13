using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GUI_DialogueBox : MonoBehaviour
{
    private static List<string> _dialogue = new List<string>(); // { "A QUICK BROWN FOX JUMPS OVER THE LAZY DOG\na quick brown fox jumps over the lazy dog", "A QUICK BROWN FOX JUMPS OVER THE LAZY DOG\na quick brown fox jumps over the lazy dog", "A QUICK BROWN FOX JUMPS OVER THE LAZY DOG\na quick brown fox jumps over the lazy dog", };
    
    public static void QueUpDialogue(string Text)
    {
        _dialogue.Insert(_dialogue.Count, Text);
    }

    [SerializeField] private RectTransform dialogueBox;
    [SerializeField] private AnimationCurve animationInCurve;
    [SerializeField] private AnimationCurve animationOutCurve;
    [SerializeField] private TMP_Text dialogueText;

    private float timer = 0;
    private bool open = false;
    private bool typing = false;

    public IEnumerator Start()
    {
        while (true)
        {
            if (_dialogue.Count > 0 && open && timer > 1)
            {
                typing = true;
                dialogueText.text = "";

                foreach (char letter in _dialogue[0])
                {
                    dialogueText.text += letter;
                    yield return new WaitForSeconds(0.025f);
                }

                yield return new WaitUntil(() => Input.GetKey(KeyCode.Return) || Input.GetMouseButtonDown(0));
                _dialogue.RemoveAt(0);

                if (_dialogue.Count <= 0)
                {
                    typing = false;
                }
            }
            yield return new WaitForSecondsRealtime(0.25f);
        }
    }

    public void Update()
    {
        if (open && timer > 1 && !typing && _dialogue.Count <= 0)
        {
            open = false;
            timer = 0;
        }
        else if (!open && timer > 1 && _dialogue.Count > 0)
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

        timer += Time.deltaTime * 2;
    }
}
