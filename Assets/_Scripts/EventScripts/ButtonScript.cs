using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Button self;
    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateButton()
    {
        self.enabled = true;
    }
}
