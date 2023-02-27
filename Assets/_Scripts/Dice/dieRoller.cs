using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieRoller : MonoBehaviour
{
    public enum dFaces
    {
        /// <summary>
        /// Rolls a d4
        /// </summary>
        d4 = 4,

        /// <summary>
        /// Rolls a d6
        /// </summary>
        d6 = 6,
        
        /// <summary>
        /// Rolls a d8
        /// </summary>
        d8 = 8,

        /// <summary>
        /// Rolls a d10
        /// </summary>
        d10 = 10,

        /// <summary>
        /// Rolls a D12
        /// </summary>
        d12 = 12,

        /// <summary>
        /// Rolls a d20
        /// </summary>
        d20 = 20,
    }
    public GameObject d4, d6, d8, d10, d12, d20;

    public GameObject rolledDie;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //RollDie(dFaces.d4);
            //RollDie(dFaces.d6);
            //RollDie(dFaces.d8);
            //RollDie(dFaces.d10);
            //RollDie(dFaces.d12);
            RollDie(dFaces.d20);
        }
    }


    /// <summary>
    /// Rolls a die identified by the dFaces enum parameter
    /// </summary>
    /// <param name="die"></param>
    public void RollDie(dFaces die)
    {
        switch (die)
        {
            case dFaces.d4:
                rolledDie = Instantiate(d4, transform);
                break;

            case dFaces.d6:
                rolledDie = Instantiate(d6, transform);
                break;

            case dFaces.d8:
                rolledDie = Instantiate(d8, transform);
                break;

            case dFaces.d10:
                rolledDie = Instantiate(d10, transform);
                break;

            case dFaces.d12:
                rolledDie = Instantiate(d12, transform);
                break;

            case dFaces.d20:
                rolledDie = Instantiate(d20, transform);
                break;
        }

        rolledDie.GetComponent<DieScript>().DFace(die);
    }

    public void rerollDie(dFaces dieToReroll)
    {
        RollDie(dieToReroll);
    }
}
