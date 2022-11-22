using System.Collections;
using UnityEngine;

public class StickmanRotater : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _maxAngle;

    private float _mouseX;

    public void Rotate(float sensetivity)
    {
        _mouseX += Input.GetAxis("Mouse X") * sensetivity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.x, Mathf.Clamp(_mouseX, -_maxAngle, _maxAngle), transform.rotation.z);
    }

    public void StartRotation()
    {
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        while (transform.rotation != Quaternion.identity)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * _rotateSpeed);
            yield return null;
        }
    }
}
