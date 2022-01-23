using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBall : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] float fireRate;
    [SerializeField] float force;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    private void Shoot()
    {
        var obj = Instantiate(ball);
        var rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force);
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / fireRate);
            Shoot();
        }
    }
}
