using System;
using UnityEngine;

[Serializable]
public class ItemData
{
    private float _colorR;
    private float _colorG;
    private float _colorB;
    private float _colorA;

    public ItemData(Color color)
    {
        _colorR = color.r;
        _colorG = color.g;
        _colorB = color.b;
        _colorA = color.a;
    }

    public Color GetColor()
    {
        return new Color(_colorR, _colorG, _colorB, _colorA);
    }
}
