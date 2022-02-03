using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableUI : MonoBehaviour
{
    [SerializeField] private LoveBar loveBar;
    public void StartBattleUI()
    {
        SetActiveUI(UIType.LOVEBAR, true);
    }
    public void StopBattleUI()
    {
        SetActiveUI(UIType.LOVEBAR, false);
    }

    public void SetActiveUI(UIType type, bool value)
    {
        switch (type)
        {
            case UIType.LOVEBAR:
                loveBar.gameObject.SetActive(value);
                break;
        }
    }

    public void SetMaxLove(float value)
    {
        loveBar.SetMaxLove(value);
    }

    public void UpdateLoveBar(float value)
    {
        loveBar.UpdateLoveBar(value);
    }
}
