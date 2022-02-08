using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
[ExecuteInEditMode]
public class FinalPlatform : MonoBehaviour
{
    private MaterialPropertyBlock block;
    [SerializeField] private Color color;
    [SerializeField] private Renderer renderer;
    [SerializeField] private Transform bottom, top;
    [SerializeField] private TextMeshProUGUI platformText;
    private float platformWidth = 8f;
    public float Width => platformWidth;
    public Transform BottomPoint => bottom;
    public Transform TopPoint => top;
    private void Awake()
    {
        SetMaterial(color);
    }
    public void SetMaterial(Color color)
    {
        block = new MaterialPropertyBlock();
        var colorID = Shader.PropertyToID("_Color");
        block.SetColor(colorID, color);
        renderer.SetPropertyBlock(block);
    }

    public void SetColor(Color platformColor)
    {
        color = platformColor;
    }
    public void SetPlatformText(string text)
    {
        platformText.SetText(text);
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
            return;
        Awake();
    }
#endif
}
