using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Coin : MonoBehaviour
{
    [SerializeField] private float _pushForce;
    [SerializeField] private int _amount;
    [SerializeField] private string _type;
    [SerializeField] private CoinAmountView _coinAmountView;

    private Rigidbody _rigidbody;

    public string Type => _type;
    public int Amount => _amount;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ShowAmount()
    {
        Instantiate(_coinAmountView, transform.position, Quaternion.identity).Initialize(_amount.ToString());
    }

    public void UsePhysic(bool usePhysic)
    {
        _rigidbody.isKinematic = !usePhysic;
    }

    public void Throw(Vector3 direction, float throwForce)
    {
        _rigidbody.AddForce(new Vector3(0f, transform.position.y, 0f) + direction * throwForce, ForceMode.VelocityChange);
    }

    public void Push()
    {
        _rigidbody.AddForce(Vector3.back * _pushForce);
    }
}
