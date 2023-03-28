using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieRoller : MonoBehaviour
{
    public static DieRoller Instance { get; private set; }

    public GameObject d4, d6, d8, d10, d12, d20;

    public GameObject rolledDie;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Can only have one \"DieRoller\"!");
            Destroy(gameObject);
        }
    }

#if UNITY_EDITOR
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (Random.Range(0, 6))
            {
                case 0:
                    RollDie(dFaces.d4, null);
                    return;

                case 1:
                    RollDie(dFaces.d6, null);
                    return;

                case 2:
                    RollDie(dFaces.d8, null);
                    return;

                case 3:
                    RollDie(dFaces.d10, null);
                    return;

                case 4:
                    RollDie(dFaces.d12, null);
                    return;

                case 5:
                    RollDie(dFaces.d20, null);
                    return;
            }
        }
    }
#endif

    /// <summary>
    /// Rolls a die identified by the dFaces enum parameter and calls the inputted callback
    /// </summary>
    public static void RollDie(dFaces Die, RollCallback Callback) // Wrapper for a local function
    {
        Instance._RollDie(Die, Callback);
    }

    private void _RollDie(dFaces Die, RollCallback Callback)
    {
        if (rolledDie != null)
        {
            Destroy(rolledDie);
        }

        rolledDie = Instantiate(GetDiePrefab(Die), transform);
        rolledDie.GetComponent<DieScript>().Initialize(Die, Callback);
    }

    private GameObject GetDiePrefab(dFaces Die)
    {
        switch (Die)
        {
            case dFaces.d4:
                return d4;

            case dFaces.d6:
                return d6;

            case dFaces.d8:
                return d8;

            case dFaces.d10:
                return d10;

            case dFaces.d12:
                return d12;

            case dFaces.d20:
                return d20;
        }

        return null;
    }

    public delegate void RollCallback(int RollValue);

    public enum dFaces
    {
        d4  =  4,
        d6  =  6,
        d8  =  8,
        d10 = 10,
        d12 = 12,
        d20 = 20,
    }
}
