using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameScreen : UIBase
{
    [SerializeField] private TextMeshProUGUI levelText;

    private void SetLevelText(int level)
    {
        levelText.SetText($"Level {level.ToString()}");
    }
    public override void EnablePanel()
    {
        base.EnablePanel();
        SetLevelText(MySceneManager.Instance.CurrentLevel);
    }
}
