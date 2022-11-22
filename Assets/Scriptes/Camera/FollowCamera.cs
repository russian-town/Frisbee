using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private CameraTarget _target;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offSet;

    private void Start()
    {
        transform.position = _target.transform.position + _offSet;
    }

    private void LateUpdate()
    {
        if (_target == null)
            return;

        Vector3 targetPosition = _target.transform.position + _offSet;

        if (transform.position != targetPosition)
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _speed);
    }

    public void SetTarget(CameraTarget cameraTarget)
    {
        _target = cameraTarget;
    }

    public void ResetTarget()
    {
        _target = null;
    }
}
