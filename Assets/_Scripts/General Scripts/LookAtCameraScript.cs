using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera[] cams = FindObjectsOfType<Camera>();

        foreach(Camera cam in cams)
        {
            if (cam.name == "CombatCamera")
            {
                transform.LookAt(cam.transform);
            }
        }
    }
}
