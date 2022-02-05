using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPlatform : MonoBehaviour
{
    private FinalPlatform nextPlatform;
    [SerializeField] private Transform bottom, top;
    private float platformWidth = 8f;
    public float Width => platformWidth;
    public Transform BottomPoint => bottom;
    public Transform TopPoint => top;
}
