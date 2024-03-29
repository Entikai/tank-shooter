using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPool : MonoBehaviour
{
    protected GameObject objectToPool;
    protected int poolSize = 10;
    protected Queue<GameObject> objectPool;

    private Transform spawnedObjectsParent;

    [SerializeField] private UnityEvent<GameObject> OnPooledObjectInstantiated = new UnityEvent<GameObject>();
    [SerializeField] private UnityEvent<Queue<GameObject>> OnObjectPoolUpdated = new UnityEvent<Queue<GameObject>>();

    private void Awake()
    {
        objectPool = new Queue<GameObject>();
    }

    public void Initialize(GameObject objectToPool, int poolSize = 10)
    {
        this.objectToPool = objectToPool;
        this.poolSize = poolSize;
    }

    public GameObject CrateObject()
    {
        CreateObjectParentIfNeeded();

        GameObject spawnedObject = null;

        if (objectPool.Count < poolSize)
        {
            spawnedObject = Instantiate(objectToPool, transform.position, Quaternion.identity);
            spawnedObject.name = transform.root.name + "_" + objectToPool.name + "_" + objectPool.Count;
            spawnedObject.transform.SetParent(spawnedObjectsParent);

            OnPooledObjectInstantiated?.Invoke(spawnedObject);
            OnObjectPoolUpdated?.Invoke(objectPool);
        }
        else
        {
            spawnedObject = objectPool.Dequeue();
            spawnedObject.transform.position = transform.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
            OnObjectPoolUpdated?.Invoke(objectPool);
        }
        objectPool.Enqueue(spawnedObject);
        OnObjectPoolUpdated?.Invoke(objectPool);
        return spawnedObject;
    }

    private void CreateObjectParentIfNeeded()
    {
        if (spawnedObjectsParent == null)
        {
            string name = "ObjectPool_" + objectToPool.name;

            //Check if any other Object Pool has already created pool parent.
            var parentObject = GameObject.Find(name);

            if (parentObject != null)
                spawnedObjectsParent = parentObject.transform;
            else
                spawnedObjectsParent = new GameObject(name).transform;
        }
    }
}