using UnityEngine;
using UnityUtilities;

namespace Platformer
{
    public class Patroller : MonoBehaviour
    {
        [SerializeField] float offsetX;
        [SerializeField] float speed = 0.5f;

        Countdown time;

        Vector3 fromPosition;
        Vector3 toPosition;

        void Awake()
        {
            time = new Countdown(true, Mathf.Abs(offsetX) / speed);
           
            fromPosition = transform.position;
            toPosition = fromPosition;
            toPosition.x += offsetX;
            UpdateDirection();
        }

        void Update()
        {
            transform.position = Vector3.Lerp(fromPosition, toPosition, time.PercentElapsed);

            if (time.Progress())
            {
                time.Reset();
                var temp = fromPosition;
                fromPosition = toPosition;
                toPosition = temp;
                UpdateDirection();
            }
        }
        
        void UpdateDirection()
        {
            transform.SetLocalScale(x: Mathf.Sign(toPosition.x - fromPosition.x));
        }

        void OnDrawGizmos()
        {
            var target = transform.position;
            target.x += offsetX;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target);
            Gizmos.DrawSphere(transform.position, 0.05f);
            Gizmos.DrawSphere(target, 0.05f);
        }
    }
}