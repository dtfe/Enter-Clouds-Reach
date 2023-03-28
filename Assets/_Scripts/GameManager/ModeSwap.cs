
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnterCloudsReach.Combat;

public class ModeSwap : MonoBehaviour
{
    [Header("Exploration Properties")]
    public GameObject exp_cam;
    public GameObject exp_ui;

    [Header("Combat Properties")]
    public GameObject com_cam;
    public GameObject com_ui;
    GameObject[] exp;
    GameObject com;

    public void ChangeToCombat()
    {
        exp_cam.SetActive(false);
        exp_ui.SetActive(false);
        com_cam.SetActive(true);
        com_ui.SetActive(true);
    }

    public void ChangeToExploration()
    {
        exp_cam.SetActive(true);
        exp_ui.SetActive(true);
        com_cam.SetActive(false);
        com_ui.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {   
       
        ChangeToExploration();
    }

    // Update is called once per frame
    void Update()
    {
        if(exp_cam == null || com_cam == null)
        {
        SetGo();
        }
    }
    void SetGo()
    {
        exp = GameObject.FindGameObjectsWithTag("Exploration");
        com = GameObject.Find("Combat");
        
        foreach(GameObject go in exp)
        {
            if(go.GetComponent<Canvas>())
            {
                exp_ui = go;
            }
            else exp_cam = go;
        }
        if(com != null)
        {
        for(int i = 0; i < com.transform.childCount; i++)
        {
            if (com.transform.GetChild(i).tag != "Combat")
            {
                continue;
            }
            if (com.transform.GetChild(i).GetComponent<Canvas>())
            {
                com_ui = com.transform.GetChild(i).gameObject;
            }
            else com_cam = com.transform.GetChild(i).gameObject;

        }
        }
    }
}
