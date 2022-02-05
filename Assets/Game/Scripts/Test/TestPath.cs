using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestPath : MonoBehaviour
{
    [SerializeField] FinalPathGenerator finalpath;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOPath(finalpath.PathPoints, 25f)
            .SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
