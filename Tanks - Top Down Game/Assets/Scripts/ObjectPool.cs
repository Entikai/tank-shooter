using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    protected GameObject objectToPool;
    protected int poolSize = 10;
    protected Queue<GameObject> objectPool;

    private Transform spawnedObjectsParent;

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
            
            //Refactor this next line. There should be an event fired, and another script should do this.
            spawnedObject.AddComponent<DestroyIfDisabled>();
        }
        else
        {
            spawnedObject = objectPool.Dequeue();
            spawnedObject.transform.position = transform.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
        }
        objectPool.Enqueue(spawnedObject);
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

    //This should be in another script that should do this when the tank is killed.
    private void OnDestroy()
    {
        foreach (var item in objectPool)
        {
            if (item == null)
                continue;
            else if (item.activeSelf == false)
                Destroy(item);
            else
                item.GetComponent<DestroyIfDisabled>().SelfDestructionEnabled = true;
        }
    }
}