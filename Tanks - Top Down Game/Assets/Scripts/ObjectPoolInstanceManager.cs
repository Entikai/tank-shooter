using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolInstanceManager : MonoBehaviour
{
    Queue<GameObject> objectPool;

    private void Awake()
    {
        objectPool = new Queue<GameObject>();
    }
    public void AttachDestroyIfDisabledScript(GameObject spawnedObject)
    {
        spawnedObject.AddComponent<DestroyIfDisabled>();
    }

    public void UpdateTheStateOfTheObjectPool(Queue<GameObject> objectPool)
    {
        this.objectPool = objectPool;
    }

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