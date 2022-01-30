using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyVisual : MonoBehaviour
{
    [SerializeField] private Animator enemyAnimator;

    private void Start()
    {
        PlayRandomDance();
    }

    private void PlayRandomDance()
    {
        var rand = Random.Range(0, 6);
        enemyAnimator.SetFloat("DanceType", rand);
    }
}
