using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : UIBase
{
    
    public void Restart()
    {
        //UIManager.Instance.DisableUI(UIManager.Instance.GameOverScreen, UIManager.Instance.StartScreen);
        UIManager.Instance.StartScreen.EnablePanel();
        UIManager.Instance.GameOverScreen.DisablePanel();
        MySceneManager.Instance.RestartActiveScene();
        
    }
    //GAMEOVER SCREEN FUNCTIONS, RESTART, SHOW SCORE ETC
}
