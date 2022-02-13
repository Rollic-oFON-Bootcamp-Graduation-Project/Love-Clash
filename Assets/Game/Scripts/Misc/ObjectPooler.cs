using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoSingleton<ObjectPooler>
{

    //TODO GENERALIZE THIS
    [SerializeField] private Transform parentProjectile;
    [SerializeField] private Projectile projectileObject;
    [SerializeField] public List<Projectile> pooledProjectiles;
    [SerializeField] public int projectilePoolAmount;


    private void Start()
    {
        CreateProjectiles();
    }
    private void CreateProjectiles()
    {
        for (int i = 0; i < projectilePoolAmount; i++)
        {
            var obj = Instantiate(projectileObject, parentProjectile);
            pooledProjectiles.Add(obj);
            obj.gameObject.SetActive(false);
        }
    }
    public Projectile GetPooledProjectile()
    {

        for (int i = 0; i < pooledProjectiles.Count; i++)
        {
            if (!pooledProjectiles[i].gameObject.activeInHierarchy)
            {
                return pooledProjectiles[i];
            }
        }

        var obj = Instantiate(projectileObject, parentProjectile);
        pooledProjectiles.Add(obj);

        
        return obj;
    }
}
