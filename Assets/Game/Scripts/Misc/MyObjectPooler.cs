using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyObjectPooler : MonoSingleton<MyObjectPooler>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public Transform parent;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(pool.parent);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;


        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }


    public Projectile GetProjectileFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + " doesn't exist.");
            return null;
        }

        GameObject projectile = poolDictionary[tag].Dequeue();
        projectile.SetActive(true);
        poolDictionary[tag].Enqueue(projectile);

        return projectile.GetComponent<Projectile>();
    }
}
