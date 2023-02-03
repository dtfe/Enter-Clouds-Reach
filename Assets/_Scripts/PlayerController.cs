using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private GameObject targetPos;
    private bool isMoving;

    public Camera cam;

    public NavMeshAgent agent;

    public GameObject navmeshPoint;

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0) && !targetPos)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject targetGO = Instantiate(navmeshPoint, hit.point, Quaternion.identity);
                targetPos = targetGO;
            }
        }*/

        if (targetPos == null)
        {
            return;
        }

        if (!isMoving)
        {
            agent.SetDestination(targetPos.transform.position);
            isMoving = true;
        }

        if (transform.position.x > targetPos.transform.position.x - 0.2f && transform.position.x < targetPos.transform.position.x + 0.2f && transform.position.y - 1 > targetPos.transform.position.y - 0.2f && transform.position.y - 1 < targetPos.transform.position.y + 0.2f && transform.position.z > targetPos.transform.position.z - 0.2f && transform.position.z < targetPos.transform.position.z + 0.2f)
        {
            targetPos.GetComponent<NavigationPointScript>().TriggerEvent();
            targetPos = null;
            isMoving = false;
            Debug.Log(2);
        }
    }

    public void SetTargetPos(GameObject pos)
    {
        targetPos = pos;
    }
}
