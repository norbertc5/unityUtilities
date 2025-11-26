using System.Collections;
using UnityEngine;

namespace norbertcUtilities.PointAndClickMovement
{
    public class HumanAnimationController : MonoBehaviour
    {
        public AnimationStates AnimationState { get; private set; }
        [SerializeField] float measureInterval = 0.1f;

        Animator animator;
        Vector2 previousHumanPosition;

        public enum AnimationStates
        {
            Idle,
            Walk
        }

        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            StartCoroutine(CheckPreviousPosition());
        }

        IEnumerator CheckPreviousPosition()
        {
            while (true)
            {
                previousHumanPosition = new Vector2(transform.position.x, transform.position.y);
                yield return new WaitForSeconds(measureInterval);

                if ((previousHumanPosition - (Vector2)transform.position).magnitude > 0.1f)
                {
                    animator.CrossFade("walk", 0);
                    AnimationState = AnimationStates.Walk;
                }
                else
                {
                    animator.CrossFade("idle", 0);
                    AnimationState = AnimationStates.Idle;
                }
            }
        }
    }
}
