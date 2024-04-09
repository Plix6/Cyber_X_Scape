using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyColor : MonoBehaviour
{
    [SerializeField]private Color color;

    public void SetColor(Color newColor)
    {
        color = newColor;
    }

    public Color GetColor()
    {
        return color;
    }
}
