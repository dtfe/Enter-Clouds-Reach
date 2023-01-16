using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFaceScript : MonoBehaviour
{
    private bool isGrounded;
    public int number;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }


    public bool isTouchingGround()
    {
        return isGrounded;
    }
}
