using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.Combat
{
    public class MGBuilderSimple : MonoBehaviour
    {
        [SerializeField] private GameObject point;
        [SerializeField] private Canvas ui;
        [SerializeField] private MGLoader loader;
        public List<MGPoint> points = new List<MGPoint>();

        private void Start()
        {
            loader.QueueUp(points.ToArray());
        }
    }

    [System.Serializable]
    public class MGPoint
    {
        public enum TypeOfPoint
        {
            Point,
            Slide
        }
        [SerializeField] public TypeOfPoint type;
        [SerializeField] public GameObject prefabToSpawn;
        [SerializeField] public Vector3 position;
        [SerializeField] public Vector3 endPos;
        [SerializeField] public float whenToSpawn;
        [SerializeField] public bool showParameters = true;
        [SerializeField] public bool hasSpawned = false;
    }
}
