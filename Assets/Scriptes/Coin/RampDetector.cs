using UnityEngine;

[RequireComponent(typeof(Coin))]
public class RampDetector : MonoBehaviour
{
    private Coin _coin;

    private void Awake()
    {
        _coin = GetComponent<Coin>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out RampMover rampMover))
        {
            _coin.Push();
        }
    }
}
