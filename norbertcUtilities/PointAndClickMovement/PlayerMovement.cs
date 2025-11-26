using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace norbertcUtilities.PointAndClickMovement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : PhysicalObject
    {
        public static Action OnNewDestinationSet;
        NavMeshAgent agent;
        Vector2 gotoPos;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

        private void OnEnable()
        {
            OnNewDestinationSet += SetAgentsDestionation;
        }

        private void OnDisable()
        {
            OnNewDestinationSet -= SetAgentsDestionation;
        }


        public override void Update()
        {
            base.Update();

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                gotoPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                OnNewDestinationSet?.Invoke();
            }
        }

        void SetAgentsDestionation()
        {
            agent.SetDestination(gotoPos);
        }
    }
}
