using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FrisbeeDetector))]

public class CoinsChecker : MonoBehaviour
{
    public event UnityAction<int> CoinEntered;

    private FrisbeeDetector _frisbeeDetector;

    private void Awake()
    {
        _frisbeeDetector = GetComponent<FrisbeeDetector>();
    }

    private void OnEnable()
    {
        _frisbeeDetector.FrisbeeEntered += OnFrisbeeEntered;
    }

    private void OnDisable()
    {
        _frisbeeDetector.FrisbeeEntered -= OnFrisbeeEntered;
    }

    private void OnFrisbeeEntered(FrisbeeMover frisbeeMover)
    {
        if(frisbeeMover is CoinMover coinMover)
        {
            if(coinMover.TryGetComponent(out Coin coin))
            {
                CoinEntered?.Invoke(coin.Amount);
                Destroy(coin.gameObject);
            }
        }
    }
}
