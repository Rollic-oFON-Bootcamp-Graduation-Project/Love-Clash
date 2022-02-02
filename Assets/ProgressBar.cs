using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private int maximum, _current;
    [SerializeField] private Image imageToFill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GetCurrentFill();
    }

    public void SetCurrentFill(float current)
    {
        float fillAmount = ((float)current / (float)maximum);
        imageToFill.fillAmount = fillAmount;
    }
}
