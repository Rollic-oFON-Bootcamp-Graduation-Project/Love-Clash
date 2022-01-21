using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector2 mouseInput;
    private Vector2 rawMouseInput;
    public Vector2 MouseInput => mouseInput;
    public Vector2 RawMouseInput => rawMouseInput;
    private Vector2 previousMousePosition;
    private Vector2 previousRawMousePosition;
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
            previousRawMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("Swirling");

            //RAW MOUSE POS
            rawMouseInput = (Vector2)Input.mousePosition - previousRawMousePosition;
            previousRawMousePosition = Input.mousePosition;

            //CENTIMETER MOUSE POS
            var newPos = mousePositionCM - previousMousePosition;
            previousMousePosition = mousePositionCM;
            mouseInput = newPos;
        }
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("Released");
            mouseInput = rawMouseInput = Vector2.zero;
        }
    }
}
