using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class StartScreen : UIBase
{
    [SerializeField] private Image tapToPlayImage;
    private Tween tapToPlayTween;
    private void Start()
    {
        TapToPlayAnimation();
    }
    public override void DisablePanel()
    {
        base.DisablePanel();
        DisableTapToPlayAnimation();
    }
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
    private void TapToPlayAnimation()
    {
        tapToPlayTween = tapToPlayImage.transform.DOScale(1.2f, 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetUpdate(true);
    }
    private void DisableTapToPlayAnimation()
    {
        DOTween.Kill(tapToPlayTween);
    }
}
