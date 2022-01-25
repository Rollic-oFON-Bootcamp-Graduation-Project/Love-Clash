using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    public void EnableVisual()
    {
        this.gameObject.SetActive(true);
    }
    public void DisableVisual()
    {
        this.gameObject.SetActive(false);
    }
    public void ChangeAnimState(string name, bool value)
    {
        playerAnimator.SetBool(name, value);
    }
    public void ChangeAnimState(string name, int value)
    {
        playerAnimator.SetFloat(name, value);
    }

}
