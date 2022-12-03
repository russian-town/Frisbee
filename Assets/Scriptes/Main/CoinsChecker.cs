using UnityEngine;

[RequireComponent(typeof(FrisbeeDetector))]

public class CoinsChecker : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;

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
                coin.ShowAmount();
                _wallet.AddMoney(coin.Amount);
                Destroy(coin.gameObject);
            }
        }
    }
}
