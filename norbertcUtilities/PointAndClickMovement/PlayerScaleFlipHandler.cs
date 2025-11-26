using UnityEngine;
using UnityEngine.InputSystem;

namespace norbertcUtilities.PointAndClickMovement
{
    public class PlayerScaleFlipHandler : MonoBehaviour
    {
        int screenWidth;
        HumanAnimationController humanAnimationController;

        void Start()
        {
            screenWidth = Screen.width;
            humanAnimationController = GetComponentInParent<HumanAnimationController>();
        }

        private void OnEnable()
        {
            PlayerMovement.OnNewDestinationSet += SetScaleX;
        }

        private void OnDisable()
        {
            PlayerMovement.OnNewDestinationSet -= SetScaleX;
        }

        void Update()
        {
            // stop performing flip when animation is not idle 
            if (humanAnimationController.AnimationState != HumanAnimationController.AnimationStates.Idle)
            {
                return;
            }

            SetScaleX();
        }

        void SetScaleX()
        {
            if (isMouseOnLeftScreenSide())
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        bool isMouseOnLeftScreenSide()
        {
            float mousePosX = Mouse.current.position.ReadValue().x;
            return mousePosX < (screenWidth / 2);
        }
    }
}
