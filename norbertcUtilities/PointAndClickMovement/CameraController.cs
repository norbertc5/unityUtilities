
namespace norbertcUtilities.PointAndClickMovement
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using norbertcUtilities.ActionOnTime;

    public class CameraController : MonoBehaviour
    {
        static CameraController instance;

        [SerializeField] int maxDeflection = 2;
        [SerializeField] int deflectionSpeed = 10;
        const int CAMERA_Z_POSITON = -100;
        public static Vector2 mousePos;

        [SerializeField] float shakeAmplitude = 0.1f;
        [SerializeField] float shakeFrequency = 20;
        [SerializeField] float shakeDuration = 0.1f;
        static bool isShaking;

        void Start()
        {
            instance = this;
        }

        void FixedUpdate()
        {
            mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            // apply deflection
            transform.localPosition = Vector3.Slerp(
                transform.localPosition,
                mousePos,
                Time.time) * Time.fixedDeltaTime * deflectionSpeed;

        }

        private void Update()
        {

            if (isShaking)
            {
                float shake = Mathf.Sin(shakeFrequency * Time.time) * shakeAmplitude;
                transform.localPosition += Vector3.up * shake;
            }

            // clamp cameras position and provide z axis offset
            transform.localPosition = new Vector3(
                Mathf.Clamp(transform.localPosition.x, -maxDeflection, maxDeflection),
                Mathf.Clamp(transform.localPosition.y, -maxDeflection, maxDeflection),
                CAMERA_Z_POSITON);
        }

        public static void Shake()
        {
            isShaking = true;

            ActionOnTime.Create(() =>
            {
                isShaking = false;
            }, instance.shakeDuration);
        }

    }
}
