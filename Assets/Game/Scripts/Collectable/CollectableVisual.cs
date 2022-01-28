using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableVisual : MonoBehaviour
{
    [SerializeField] private Animator collectableAnimator;

    private void Start()
    {
        SetRandomIdle();
    }
    private void ChangeAnimState(string name, bool value)
    {
        collectableAnimator.SetBool(name, value);
    }
    public void SetRandomIdle()
    {
        var rand = Random.Range(0, 4);
        collectableAnimator.SetFloat("IdleType", rand);
    }
    public void StackAnimation()
    {
        this.transform.DOLocalRotate(Vector3.zero, 1f, RotateMode.Fast);
        ChangeAnimState("IsWalking", true);
    }

    public void BattleAnimation()
    {
        this.transform.DOLocalRotate(Vector3.up * 180 , 1f, RotateMode.Fast);
        ChangeAnimState("IsBattleState", true);
    }
}
