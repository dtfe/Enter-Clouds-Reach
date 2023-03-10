using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEffect : MonoBehaviour
{
    public GameObject effectToSpawn;
    public Transform spawnFromWhere;
    public float timeBeforeDestructon = 3;

    public void instantiateEffect()
    {
        GameObject spawnedEffect = Instantiate(effectToSpawn, spawnFromWhere);
        Destroy(spawnedEffect, timeBeforeDestructon);
    }
}
