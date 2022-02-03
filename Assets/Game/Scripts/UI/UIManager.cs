using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] public StartScreen StartScreen;
    [SerializeField] public GameOverScreen GameOverScreen;
    [SerializeField] public InGameScreen InGameScreen;
    private HashSet<UIBase> uiSet;

    private void Awake()
    {

    }

    public void DisableUI(params UIBase[] uisToDisable)
    {
        uiSet.Add(StartScreen);
        uiSet.Add(GameOverScreen);
        foreach (UIBase ui in uiSet)
        {
            for(int i = 0; i < uisToDisable.GetLength(0);i++)
            {
                if(ui == uisToDisable[i])
                {
                    ui.DisablePanel();
                }
            }
        }
    }
    //THIS WILL ENABLE/DISABLE UI 
}
