using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableVisual : MonoBehaviour
{
    [SerializeField] private Animator collectableAnimator;
    [SerializeField] private MaleAnimState currentState;

    public void UpdateAnimState(MaleAnimState newState, HitType type = HitType.OBSTACLE)
    {
        if (currentState == newState) return;
        currentState = newState;
        switch (newState)
        {
            case MaleAnimState.IDLE:
                PlayRandomIdle();
                break;
            case MaleAnimState.WALKING:
                StackAnimation();
                break;
            case MaleAnimState.ONBATTLE:
                ChangeAnimState("Battle", true);
                break;
            case MaleAnimState.OFFBATTLE:
                ChangeAnimState("Battle", false);
                ChangeAnimState("Win", false);
                break;
            case MaleAnimState.TAKEN:
                ChangeAnimState("Taken", true);
                PlayTakenAnim(type);
                break;
            case MaleAnimState.WIN:
                ChangeAnimState("Win", true);
                break;
            default:
                break;
        }

    }
    private void Start()
    {
        PlayRandomIdle();
    }

    public void StackAnimation()
    {
        if (this.gameObject.activeSelf)
        {
            this.transform.DOLocalRotate(Vector3.zero, 1f, RotateMode.Fast);
            ChangeAnimState("Walking", true);
        }
    }

    private void ChangeAnimState(string name, bool value)
    {
        collectableAnimator.SetBool(name, value);
    }

    private void PlayRandomIdle()
    {
        var rand = Random.Range(0, 4);
        collectableAnimator.SetFloat("IdleType", rand);
    }

    private void PlayLoveAnim()
    {
        collectableAnimator.SetFloat("BattleResult", 1);
    }

    private void PlayTakenAnim(HitType type)
    {
        switch (type)
        {
            case HitType.OBSTACLE:
                collectableAnimator.SetFloat("TakenType", 0);
                break;
            case HitType.ARENA:
                collectableAnimator.SetFloat("TakenType", 1);
                break;
            default:
                break;
        }

    }

    private void PlayHateAnim()
    {
        this.transform.DOLocalRotate(Vector3.zero, 1f, RotateMode.Fast)
            .SetEase(Ease.OutSine)
            .OnComplete(() => collectableAnimator.SetFloat("BattleResult", 2));
    }
    public void PlayBattle()
    {
        //UpdateAnimState(MaleAnimState.ONBATTLE);
    }
    public void StopBattle()
    {
        UpdateAnimState(MaleAnimState.OFFBATTLE);
    }
}
