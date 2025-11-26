using UnityEngine;

namespace norbertcUtilities.PointAndClickMovement
{

    public class PhysicalObject : MonoBehaviour
    {
        public virtual void Update()
        {
            // setting z position in order to maintain proper layering in a 2D environment
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                transform.localPosition.y,
                transform.position.y);
        }
    }
}
