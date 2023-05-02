using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.Combat
{
    public class MGBuilderSimple : MonoBehaviour
    {
        [SerializeField] private GameObject point;
        [SerializeField] private Canvas ui;
        public List<MGPoint> points = new List<MGPoint>();
        private float time;

        private void Update()
        {
            time += Time.deltaTime;
            foreach(MGPoint p in points)
            {
                GameObject spawnedGO;
                if(p.whenToSpawn < time)
                {
                    spawnedGO = Instantiate(p.prefabToSpawn, ui.transform);
                    spawnedGO.GetComponent<RectTransform>().anchoredPosition = p.position;
                    Destroy(spawnedGO, 3);

                }
            }
        }
    }

    [System.Serializable]
    public class MGPoint
    {
        [SerializeField] public GameObject prefabToSpawn;
        [SerializeField] public Vector3 position;
        [SerializeField] public float whenToSpawn;
        [SerializeField] public bool showParameters = true;
        [SerializeField] public bool hasSpawned = false;
    }
}
