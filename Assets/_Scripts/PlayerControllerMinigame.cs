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
        public float rayLengthMultiplier;
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
            Vector2 movement = new Vector3(movementX, movementY, 0);
            RaycastHit2D xDirHit = Physics2D.Raycast(transform.position, new Vector3(movementX, 0, 0), 1.5f, layermask);
            Debug.DrawRay(transform.position, new Vector3(movementX*rayLengthMultiplier, 0, 0), Color.yellow);
            RaycastHit2D zDirHit = Physics2D.Raycast(transform.position, new Vector3(0, movementY, 0), 1.5f, layermask);
            Debug.DrawRay(transform.position, new Vector3(0, movementY*rayLengthMultiplier, 0), Color.green);
            if (xDirHit.collider != null && xDirHit.collider.CompareTag("CombatWall"))
            {
                movementX = 0;
            }
            if (zDirHit.collider != null && zDirHit.collider.CompareTag("CombatWall"))
            {
                movementY = 0;
            }
            if(movementX == 0 && movementY != 0)
            {
                if(movementY > 0)
                {
                    movementY = 1;
                }
                else if (movementY < 0)
                {
                    movementY = -1;
                }
            }
            if (movementY == 0 && movementX != 0)
            {
                if (movementY > 0)
                {
                    movementY = 1;
                }
                else if (movementY < 0)
                {
                    movementY = -1;
                }
            }
            movement = new Vector2(-movementX, movementY);
            transform.Translate(movement * speed);
        }

        private void OnMove(InputValue movementValue)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();

            movementX = movementVector.x;
            movementY = movementVector.y;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Hit Trigger");
            if (collision.gameObject.CompareTag("MGAttack"))
            {
                Debug.Log("Got hit!");
            }
        }
    }
}
