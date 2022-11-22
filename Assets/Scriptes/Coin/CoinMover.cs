using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class CoinMover : FrisbeeMover
{
    [SerializeField] private float _force;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    public void Throw(Vector3 direction)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(new Vector3(0f, transform.position.y, 0f) + direction * _force, ForceMode.VelocityChange);
    }
}
