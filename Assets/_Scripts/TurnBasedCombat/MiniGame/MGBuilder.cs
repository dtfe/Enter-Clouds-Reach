using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



namespace EnterCloudsReach.Combat
{
    public class MGBuilder : MonoBehaviour
    {
        private enum ActiveMode
        {
            Create,
            Move,
            Delete
        }

        // Selectable Prefabs
        [Header("Selectable Prefabs")]
        [SerializeField] private Button selectPoint;
        [SerializeField] private GameObject pointPrefab;
        [SerializeField] private Button selectSlide;
        [SerializeField] private GameObject slidePrefab;

        // Time Parameters
        [Header("Time Parameters")]
        [SerializeField] private TMP_InputField inputTime;
        [SerializeField] private TMP_Text time;
        [SerializeField] private float timeFloat;
        [SerializeField] private bool isPlaying;

        // Tools
        [Header("Tool Buttons")]
        [SerializeField] private Button create;
        [SerializeField] private Button move;
        [SerializeField] private Button delete;

        // Data

        private void Update()
        {
            if (isPlaying)
            {
                timeFloat += Time.deltaTime;
            }
        }




        private void CreatePrefab(GameObject GOtoSpawn, float WhenToSpawn, Vector3 WhereToSpawn)
        {
            

        }
    }
}
