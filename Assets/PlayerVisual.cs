using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Avatar[]  avatars;
    [SerializeField] private GameObject[] playerVisuals;
    [SerializeField] private Transform[] rightHands;
    [SerializeField] private GameObject currentVisual;
    [SerializeField] private Weapon weapon;

    [SerializeField] private int visualIndex = 0;

    private void Start()
    {
        weapon.transform.position = rightHands[visualIndex].position;
        weapon.transform.SetParent(rightHands[visualIndex]);
    }
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

    public void ChangeVisual()
    {
        visualIndex++;
        currentVisual.SetActive(false);
        currentVisual = playerVisuals[visualIndex];
        playerAnimator.avatar = avatars[visualIndex];
        currentVisual.SetActive(true);
        weapon.transform.position = rightHands[visualIndex].position;
        weapon.transform.SetParent(rightHands[visualIndex]);

        ChangeAnimState("WalkType", visualIndex);
    }

}
