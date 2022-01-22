using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScreen : UIBase
{



    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}
