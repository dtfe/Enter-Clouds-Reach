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
        exp_cam.SetActive(false);
        exp_ui.SetActive(false);
        com_cam.SetActive(true);
        com_ui.SetActive(true);
        Time.timeScale = 1.5f;
        Time.fixedDeltaTime = 0.2f * Time.timeScale;
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
        
    }
}
