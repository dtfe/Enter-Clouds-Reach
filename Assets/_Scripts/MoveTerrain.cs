using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MoveTerrain : MonoBehaviour
{
    Vector3 v;
    void Awake()
    {
        v = new Vector3(0,10,0);
        gameObject.transform.position += v;
    }
}
