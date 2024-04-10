using UnityEngine;

namespace norbertcUtilities.ObjectsPooling
{
    [CreateAssetMenu(menuName = "norbertcUtilities/Objects Pool")]
    public class ObjectsPool : ScriptableObject
    {
        public GameObject objectPrefab;
        public int instantionsAmount = 10;
    }
}