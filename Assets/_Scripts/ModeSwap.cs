using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwap : MonoBehaviour
{
    [Header("Exploration Properties")]
    public GameObject exp_cam;
    public GameObject exp_ui;

    [Header("Combat Properties")]
    public GameObject com_cam;
    public GameObject com_ui;

    public void ChangeToCombat()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
        exp_cam.SetActive(true);
        exp_ui.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
