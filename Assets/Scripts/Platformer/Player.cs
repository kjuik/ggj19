using System;
using Prime31;
using UnityEngine;
using UnityUtilities;

namespace Platformer
{
    public class Player : SingletonMonoBehaviour<Player>
    {
        [SerializeField] float walkingSpeed = 10;
        [SerializeField] float jumpImpulse;
        [SerializeField] float gravityJumpStillPressedMultiplier = 1f;
        [SerializeField] float gravityJumpReleasedMultiplier = 1f;
        [SerializeField] float gravityBase = -1f;
        [SerializeField] bool immediatelyStopUpwardMotionOnJumpRelease;
        [SerializeField] Transform cameraOffset;

        [SerializeField] GameObject carryingObjectObject;
        //[SerializeField] LayerMask collisionLayerMask;
        
        //BoxCollider2D box;
        CharacterController2D characterController2D;
        
        //RaycastHit2D[] boxcastResultsSingle = new RaycastHit2D[1];

        bool jumpStillPressed;

        Vector2 velocity;
        
        TheftManager theftManager;

        Animator animator;
        //readonly int PropertyWalking = Animator.StringToHash("walking");
        readonly int PropertyWalkingSpeed = Animator.StringToHash("walkingSpeed");

        void Awake()
        {
            theftManager = TheftManager.Instance;

            animator = GetComponent<Animator>();
            
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

            var isWalkingLeft = (velocity.x < 0) && !characterController2D.collisionState.left;
            var isWalkingRight = (velocity.x > 0) && !characterController2D.collisionState.right;
            var isWalking = characterController2D.isGrounded && (isWalkingLeft || isWalkingRight);
            //animator.SetBool(PropertyWalking, isWalking);
            animator.SetFloat(PropertyWalkingSpeed, isWalking ? Mathf.Abs(velocity.x) : 0f);

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

            if ((velocity.x != 0) && Mathf.Sign(velocity.x) != Mathf.Sign(transform.localScale.x))
            {
                transform.SetLocalScale(x: Mathf.Sign(velocity.x));
            }
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            var trigger = other.GetComponent<Trigger>();
            trigger.Execute();
        }

        public void Bounce(float bounceImpulse)
        {
            velocity.y = bounceImpulse;
        }

        public void UnlockCarryingObject()
        {
            carryingObjectObject.SetActive(true);
        }

        public bool IsCarryingObjectUnlocked
        {
            get { return carryingObjectObject.activeSelf; }
        }
    }
}