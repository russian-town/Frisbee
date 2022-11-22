using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [SerializeField] private float _lenght;
    [SerializeField] private float _targetPositionX;
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.position = new Vector3(_targetPositionX + Mathf.PingPong(Time.time * _speed, _lenght), transform.position.y, transform.position.z);
    }
}
