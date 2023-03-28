using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScript : MonoBehaviour
{
    [SerializeField] private int rolledNumber = 0;
    [SerializeField] private Rigidbody diceRB;
    public DieFaceScript[] dieFaces;

    DieRoller.dFaces die;
    DieRoller.RollCallback callback;

    private bool isGrounded = false;
    private bool isRolling = true;

    private bool hasBeenRolled = false;

    public void Initialize(DieRoller.dFaces Die, DieRoller.RollCallback Callback)
    {
        dieFaces = GetComponentsInChildren<DieFaceScript>();

        die = Die;
        callback = Callback;

        RollDie();
    }

    private void RollDie()
    {
        isGrounded = false;
        hasBeenRolled = false;

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Debug.Log("Rolling a " + die + " die!");
        diceRB.velocity = Quaternion.Euler(0, Random.Range(0f, 90f), 0) * Vector3.forward * Random.Range(25f, 75f);
        diceRB.angularVelocity = new Vector3(Random.Range(-90f, 90f), Random.Range(-90f, 90f), Random.Range(-90f, 90f));
    }

    void Update()
    {
        if (!isRolling) { return; }

        if(diceRB.velocity.magnitude <= 0.025f && hasBeenRolled)
        {
            isGrounded = true;
        }
        else if (diceRB.velocity.magnitude > 0.025f)
        {
            hasBeenRolled = true;
            isGrounded = false;
        }

        if (isRolling && isGrounded && hasBeenRolled)
        {
            rolledNumber = GetResult();

            if (rolledNumber <= 0)
            {
                RollDie();
                return;
            }

            Debug.Log(rolledNumber + " has been rolled!");
            callback?.Invoke(rolledNumber);
            //Destroy(gameObject, 1);
            isRolling = false;
        }
    }

    private void FixedUpdate()
    {
        diceRB.useGravity = false;
        diceRB.AddForce(Physics.gravity * 2.5f, ForceMode.Acceleration);
    }


    private int GetResult()
    {
        foreach(DieFaceScript number in dieFaces)
        {
            if (number.isTouchingGround() == true)
            {
                return (dieFaces.Length + 1) - number.number;
            }
        }

        return -1;
    }
}
