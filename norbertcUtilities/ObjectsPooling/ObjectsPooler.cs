using System.Collections.Generic;
using UnityEngine;

namespace norbertcUtilities.ObjectsPooling
{
    public class ObjectsPooler : MonoBehaviour
    {
        [SerializeField] ObjectsPool[] pools;
        [SerializeField] int maxObjectsAmountInPool = 100;
        [HideInInspector] public List<GameObject> objectsList;  // idk why but it must be public
        GameObject[,] objectsToPool;
        int[] objectsIndexes;

        void Awake()
        {
            objectsToPool = new GameObject[pools.Length, maxObjectsAmountInPool];
            objectsIndexes = new int[pools.Length];

            // here all objects needed by pools are created and assigned to an array
            foreach (ObjectsPool pool in pools)
            {
                // create an object and assign it to the list
                for (int i = 0; i < pool.instantionsAmount; i++)
                {
                    GameObject newObject = Instantiate(pool.objectPrefab);
                    objectsList.Add(newObject);
                    newObject.transform.parent = transform;
                    newObject.transform.position = Vector3.down;
                }
                // convert list to array
                GameObject[] array = objectsList.ToArray();

                // place the object into the objectsToPool array
                for (int j = 0; j < pool.instantionsAmount; j++)
                {
                    objectsToPool[System.Array.IndexOf(pools, pool), j] = array[j];
                }
                objectsList.Clear();
            }
        }

        /// <summary> Return an object from the pool and move the pool. </summary>
        /// <param name="poolIndex"></param>
        /// <returns>Object from pool.</returns>
        public GameObject GetObjectFromPool(int poolIndex)
        {
            // move the pool
            objectsIndexes[poolIndex]++;
            if (objectsIndexes[poolIndex] >= pools[poolIndex].instantionsAmount)
                objectsIndexes[poolIndex] = 0;

            return objectsToPool[poolIndex, objectsIndexes[poolIndex]];
        }

        /// <summary>
        /// Turn off all object in pool with index from argument.
        /// </summary>
        /// <param name="poolIndex"></param>
        public void TurnOffAllInPool(int poolIndex)
        {
            for (int i = 0; i < pools[poolIndex].instantionsAmount; i++)
            {
                objectsToPool[poolIndex, i].SetActive(false);
            }
        }
    }
}