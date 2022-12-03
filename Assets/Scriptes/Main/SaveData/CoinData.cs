using System;
using UnityEngine;

[Serializable]
public class CoinData
{
    private float _positionX;
    private float _positionY;
    private float _positionZ;
    private float _rotationX;
    private float _rotationY;
    private float _rotationZ;
    private float _rotationW;

    public string CoinType { get; private set; }

    public CoinData(string coinType, Vector3 position, Quaternion rotation)
    {
        CoinType = coinType;
        _positionX = position.x;
        _positionY = position.y;
        _positionZ = position.z;
        _rotationX = rotation.x;
        _rotationY = rotation.y;
        _rotationZ = rotation.z;
        _rotationW = rotation.w;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(_positionX, _positionY, _positionZ);
    }

    public Quaternion GetRotation()
    {
        return new Quaternion(_rotationX, _rotationY, _rotationZ, _rotationW);
    }
}
