using System;
using Prime31;
using UnityEngine;

namespace Platformer
{
    public class Player : MonoBehaviour
    {
        [SerializeField] float walkingSpeed = 10;
        [SerializeField] float jumpImpulse;
        [SerializeField] float gravity = -10f;
        //[SerializeField] LayerMask collisionLayerMask;
        
        //BoxCollider2D box;
        CharacterController2D characterController2D;
        
        //RaycastHit2D[] boxcastResultsSingle = new RaycastHit2D[1];

        Vector2 velocity;

        void Awake()
        {
            //box = GetComponent<BoxCollider2D>();
            characterController2D = GetComponent<CharacterController2D>();
        }

        void FixedUpdate()
        {
            velocity.y += gravity;

            characterController2D.move(new Vector3(velocity.x * Time.deltaTime, velocity.y * Time.deltaTime));
            if (characterController2D.collisionState.below || characterController2D.collisionState.above)
            {
                velocity.y = 0;
            }

            /*
            TryMoveOneAxis(velocity.x * Time.deltaTime, 0);

            if (!TryMoveOneAxis(0, velocity.y * Time.deltaTime))
            {
                velocity.y = 0;
            }
            */
        }

        /*
        bool TryMoveOneAxis(float x, float y)
        {
            if ((x == 0) && (y == 0))
                return true;

            if ((x != 0) && (y != 0))
                throw new ArgumentException("Player.TryMoveOneAxis should only be called with either x or y");

            var moveDistance = Math.Abs(x + y);

            var completedMove = true;
            
            if (Physics2D.BoxCastNonAlloc(transform.TransformPoint(box.offset), box.size, 0f, new Vector2(x, y), boxcastResultsSingle, moveDistance, collisionLayerMask.value) > 0)
            {
                if (x != 0)
                {
                    x = Math.Sign(x) * boxcastResultsSingle[0].distance;
                    Debug.Log(x);
                }
                else
                {
                    y = Math.Sign(y) * boxcastResultsSingle[0].distance;
                }

                completedMove = false;
            }

            transform.Translate(x, y, 0);
            return completedMove;
        }
        */
        
        void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var jumpPressed = Input.GetButtonDown("Jump");

            velocity.x = horizontal * walkingSpeed;

            if (jumpPressed && characterController2D.isGrounded)
            {
                velocity.y = jumpImpulse;
            }
        }
    }
}