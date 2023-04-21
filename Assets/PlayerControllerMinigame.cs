using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EnterCloudsReach.Combat
{
    public class PlayerControllerMinigame : MonoBehaviour
    {
        // Movement
        [SerializeField] private float speed = 1;
        private float movementX, movementY;
        private Rigidbody2D rb2d;

        // Raycast
        private int layermask;

        // Start is called before the first frame update
        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            layermask = 1 << 8;
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 movement = new Vector2(movementX, movementY);
            RaycastHit2D xDirHit = Physics2D.Raycast(transform.position, new Vector2(movementX * 100, 0), 50, layermask);
            Debug.Log(movement.magnitude);
            Debug.DrawRay(transform.position, new Vector2(movementX, 0) * 50, Color.yellow);
            RaycastHit2D yDirHit = Physics2D.Raycast(transform.position, new Vector2(0, movementY * 100), 50, layermask);
            Debug.DrawRay(transform.position, new Vector2(0, movementY) * 50, Color.green);
            if (xDirHit.collider != null && xDirHit.collider.CompareTag("CombatWall"))
            {
                movementX = 0;
            }
            if (yDirHit.collider != null && yDirHit.collider.CompareTag("CombatWall"))
            {
                movementY = 0;
            }
            movement = new Vector2(movementX, movementY);
            /*RaycastHit2D hit = Physics2D.Raycast(transform.position, movement, movement.magnitude * 50, layermask);
            Debug.DrawRay(transform.position, movement * 50, Color.red);

            if (hit.collider != null)
            {
                Debug.Log("hit a collider!");
                if (hit.collider.CompareTag("CombatWall"))
                {
                    Debug.Log("Hitting Wall!");
                    return;
                }
                transform.Translate(movement * speed);
            }*/
            transform.Translate(movement * speed);
        }

        private void OnMove(InputValue movementValue)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();

            movementX = movementVector.x;
            movementY = movementVector.y;
        }

        private void FixedUpdate()
        {

        }
    }
}
