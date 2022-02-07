using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WinScreen : UIBase
{
    [SerializeField] private Image heart, glow, winText;
    private Sequence sequence;

    public void NextLevel()
    {
        UIManager.Instance.StartScreen.EnablePanel();
        UIManager.Instance.WinScreen.DisablePanel();
        MySceneManager.Instance.LoadNextLevel();
    }
    public override void EnablePanel()
    {
        base.EnablePanel();
        NextLevelAnimation();
    }
    public override void DisablePanel()
    {
        base.DisablePanel();
        ResetUITransform();
        DOTween.Kill(sequence);
    }

    private void NextLevelAnimation()
    {
        sequence = DOTween.Sequence();
        winText.transform.localScale = heart.transform.localScale =  Vector3.zero;
        winText.transform.DOScale(1, 1f);
        heart.transform.DOScale(1, 1f);
        sequence.Append(glow.transform.DORotate(new Vector3(0, 0, 10), 1f)
            .SetEase(Ease.Linear)
            .SetRelative());
        sequence.SetLoops(-1, LoopType.Incremental);


        //sequence.Play();
    }

    private void ResetUITransform()
    {
        winText.transform.localScale = heart.transform.localScale =  Vector3.one;
        glow.transform.localRotation = Quaternion.identity;
    }

}
