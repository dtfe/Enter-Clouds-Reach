using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Button self;
    public Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        buttons = FindObjectOfType<UiAnimator>().GetComponentsInChildren<Button>();
        self = GetComponent<Button>();
        foreach(Button i in buttons)
        {
            self.onClick.AddListener(() => { DeactivateButton(i); Debug.Log("deactivated"); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void movePlayerToPoint(string ID)
    {
        FindObjectOfType<NavmeshPointFinderScript>().findPoint(ID);
    }


    public void ActivateButton()
    {
        self.interactable = true;
    }
    public void DeactivateButton(Button btn)
    {
        btn.interactable = false;
    }
}
