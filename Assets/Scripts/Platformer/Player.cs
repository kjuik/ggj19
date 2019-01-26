using System;
using Prime31;
using UnityEngine;
using UnityUtilities;

namespace Platformer
{
    public class Player : MonoBehaviour
    {
        [SerializeField] float walkingSpeed = 10;
        [SerializeField] float jumpImpulse;
        [SerializeField] float gravityJumpStillPressedMultiplier = 1f;
        [SerializeField] float gravityJumpReleasedMultiplier = 1f;
        [SerializeField] float gravityBase = -1f;
        [SerializeField] bool immediatelyStopUpwardMotionOnJumpRelease;
        [SerializeField] Transform cameraOffset;
        //[SerializeField] LayerMask collisionLayerMask;
        
        //BoxCollider2D box;
        CharacterController2D characterController2D;
        
        //RaycastHit2D[] boxcastResultsSingle = new RaycastHit2D[1];

        bool jumpStillPressed;

        Vector2 velocity;
        
        TheftManager theftManager;

        void Awake()
        {
            theftManager = TheftManager.Instance;
            
            //box = GetComponent<BoxCollider2D>();
            characterController2D = GetComponent<CharacterController2D>();
        }

        void FixedUpdate()
        {
            var gravityMultiplier = 1f;
            if (velocity.y > 0)
            {
                gravityMultiplier = jumpStillPressed ? gravityJumpStillPressedMultiplier : gravityJumpReleasedMultiplier;
            }

            velocity.y += gravityBase * gravityMultiplier * Time.deltaTime;

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
            if (!theftManager.Running)
            {
                velocity.x = 0;
                return;
            }
            
            var horizontal = Input.GetAxis("Horizontal");
            var jumpButtonDown = Input.GetButtonDown("Jump");
            var jumpButtonPressed = Input.GetButton("Jump");

            velocity.x = horizontal * walkingSpeed;

            if (jumpButtonDown && characterController2D.isGrounded)
            {
                velocity.y = jumpImpulse;
                jumpStillPressed = true;
            }

            if (jumpStillPressed && !jumpButtonPressed)
            {
                jumpStillPressed = false;
                if ((velocity.y > 0) && immediatelyStopUpwardMotionOnJumpRelease)
                {
                    velocity.y = 0;
                }
            }

            cameraOffset.SetPosition(x: transform.position.x);
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            var trigger = other.GetComponent<Trigger>();
            trigger.Execute();
        }
    }
}