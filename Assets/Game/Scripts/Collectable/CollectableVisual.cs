using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableVisual : MonoBehaviour
{
    [SerializeField] private Animator collectableAnimator;

    private void OnEnable()
    {
        Observer.PlayerStartBattle += BattleAnimation;
        Observer.StopBattle += StopBattle;
    }

    private void OnDisable()
    {
        Observer.PlayerStartBattle -= BattleAnimation;
        Observer.StopBattle += StopBattle;
    }

    private void Start()
    {
        SetRandomIdle();
    }

    private void StopBattle()
    {
        ChangeAnimState("IsBattleState", false);
        StackAnimation();
    }

    public void StackAnimation()
    {
        if (this.gameObject.activeSelf)
        {
            this.transform.DOLocalRotate(Vector3.zero, 1f, RotateMode.Fast);
            ChangeAnimState("IsWalking", true);
        }
    }

    public void SetBattleResult(bool IsWin)
    {
        if (this.gameObject.activeSelf)
        {
            if (IsWin)
            {
                WinAnimation();
            }
            else
            {
                LoseAnimation();
            }
        }
    }

    private void ChangeAnimState(string name, bool value)
    {
        collectableAnimator.SetBool(name, value);
    }

    private void SetRandomIdle()
    {
        var rand = Random.Range(0, 4);
        collectableAnimator.SetFloat("IdleType", rand);
    }

    public void BattleAnimation()
    {
        ChangeAnimState("IsBattleState", true);
    }

    private void WinAnimation()
    {
        collectableAnimator.SetFloat("BattleResult", 1);
    }

    private void LoseAnimation()
    {
        this.transform.DOLocalRotate(Vector3.zero, 1f, RotateMode.Fast)
            .SetEase(Ease.OutSine)
            .OnComplete(() => collectableAnimator.SetFloat("BattleResult", 2));
    }
}
