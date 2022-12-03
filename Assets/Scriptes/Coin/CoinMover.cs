using UnityEngine;

[RequireComponent(typeof(Coin))]
public class CoinMover : FrisbeeMover
{
    [SerializeField] private float _force;

    private Coin _coin;

    private void Awake()
    {
        _coin = GetComponent<Coin>();
    }

    public void Throw(Vector3 direction)
    {
        _coin.UsePhysic(true);
        _coin.Throw(direction, _force);
    }
}
