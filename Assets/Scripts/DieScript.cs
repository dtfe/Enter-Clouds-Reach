using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScript : MonoBehaviour
{
    [SerializeField]
    private bool isGrounded = false;

    [SerializeField]
    private bool hasBeenRolled = false;

    [SerializeField]
    private int rolledNumber = 0;

    private Rigidbody rb;

    public DieFaceScript[] dieFaces;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        rb = GetComponent<Rigidbody>();
        dieFaces = GetComponentsInChildren<DieFaceScript>();
        Debug.Log("Rolling a " + dieFaces.Length + "-sided die!");

        rb.AddForce(new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f)) * 500, ForceMode.Force);
        rb.AddTorque(new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f)) * 100);
    }

    // Update is called once per frame
    void Update()
    {

        if(rb.velocity.magnitude == 0 && hasBeenRolled)
        {
            isGrounded = true;
        } else if (rb.velocity.magnitude > 0.1f)
        {
            hasBeenRolled = true;
            isGrounded = false;
        }


        if (isGrounded && hasBeenRolled)
        {
            getResult();
            hasBeenRolled = false; 
        }
    }


    private void getResult()
    {
        foreach(DieFaceScript number in dieFaces)
        {
            if (number.isTouchingGround() == true)
            {
                rolledNumber = (dieFaces.Length + 1) - number.number;
                break;
            }
        }
        Debug.Log(rolledNumber + " has been rolled!");
    }
}
