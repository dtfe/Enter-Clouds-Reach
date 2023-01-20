using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieRoller : MonoBehaviour
{
    public GameObject d4, d6, d8, d10, d20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RollDie();
        }
    }

    public void RollDie()
    {
        GameObject rolledDie = Instantiate(d6);
    }
}
