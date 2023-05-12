using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.Combat
{
    public class MGLoader : MonoBehaviour
    {
        [SerializeField] private Canvas ui;
        [SerializeField] private MGSequence sequence;
        [SerializeField] private CombatMinigameManager manager;
        [SerializeField] private float whenToStart = 3;
        private bool startMinigame = false;
        private Queue<MGPoint> pointQueue = new Queue<MGPoint>();
        private List<float> whenToSpawn = new List<float>();
        private float time;
        private int curIndex = 0;
        private int curSortingOrder = 999;


        // Start is called before the first frame update
        void Start()
        {
            if (sequence != null)
            {
                QueueUp(sequence.points.ToArray());
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (startMinigame)
            {
                time += Time.deltaTime;
                if (curIndex < whenToSpawn.Count && whenToSpawn[curIndex] < time)
                {
                    GameObject spawnedGO;
                    spawnedGO = Instantiate(pointQueue.Peek().prefabToSpawn, ui.transform);
                    spawnedGO.SetActive(true);
                    spawnedGO.GetComponent<RectTransform>().anchoredPosition = pointQueue.Peek().position;
                    spawnedGO.GetComponent<Renderer>().sortingOrder = curSortingOrder;
                    spawnedGO.GetComponent<MGTimingController>().manager = manager;
                    spawnedGO.transform.SetAsFirstSibling();
                    curSortingOrder--;
                    //Destroy(spawnedGO, 3);
                    pointQueue.Dequeue();
                    curIndex++;
                }
                if (pointQueue.Count == 0)
                {
                    startMinigame = false;
                }
            }
        }

        public void LoadSequence(MGSequence seq)
        {
            QueueUp(seq.points.ToArray());
        }

        public void QueueUp(MGPoint[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                pointQueue.Enqueue(points[i]);
                whenToSpawn.Add(points[i].whenToSpawn);
            }
            StartCoroutine(LoadInMinigame());
        }

        private IEnumerator LoadInMinigame()
        {
            curSortingOrder = 999;
            time = 0;
            curIndex = 0;
            yield return new WaitForSeconds(whenToStart);
            startMinigame = true;
        }

        public void testStart()
        {
            List<MGPoint> points = new List<MGPoint>();
            points = sequence.points;
            for (int i = 0; i < points.Count; i++)
            {
                pointQueue.Enqueue(points[i]);
                whenToSpawn.Add(points[i].whenToSpawn);
            }
            StartCoroutine(LoadInMinigame());
        }
    }
}
