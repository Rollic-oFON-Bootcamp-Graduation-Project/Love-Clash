using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase :  MonoBehaviour
{
    //BASE CLASS FOR EVERY UI PANEL
    [SerializeField] protected CanvasGroup canvasGroup;
    public void DisablePanel()
    {
        canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
    }

    public void EnablePanel()
    {
        canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }
}
