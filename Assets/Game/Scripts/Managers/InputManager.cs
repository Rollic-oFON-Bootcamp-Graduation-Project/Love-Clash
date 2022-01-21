using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector2 mouseInput;
    public Vector2 MouseInput => mouseInput;
    private Vector2 previousMousePosition;
    private static InputManager instance;
    public static InputManager Instance => instance ?? (instance = instance = FindObjectOfType<InputManager>());
    private void Awake()
    {
        instance = instance ??= this;
        Debug.Log(instance);
    }
    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }
    private Vector2 mousePositionCM
    {
        get
        {
            Vector2 pixels = Input.mousePosition;
            var inches = pixels / Screen.dpi;
            var centimeters = inches * 2.54f;
            return centimeters;
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Clicked");
            previousMousePosition = mousePositionCM;

        }
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("Swirling");
            var newPos = mousePositionCM - previousMousePosition;
            previousMousePosition = mousePositionCM;
            mouseInput = newPos;
        }
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("Released");
            mouseInput = Vector2.zero;
        }
    }
}
