using UnityEngine;

public class FinalStickman : Stickman
{
    [SerializeField] private CoinsSpawner _coinsSpawner;
    [SerializeField] private CoinPosition _coinPosition;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Arrow _arrow;

    private void OnEnable()
    {
        _coinsSpawner.CoinSpawnEnded += OnCoinSpawnEnded;
        _coinsSpawner.SpawnEnded += OnSpawnEnded;
    }

    private void OnDisable()
    {
        _coinsSpawner.CoinSpawnEnded -= OnCoinSpawnEnded;
        _coinsSpawner.SpawnEnded -= OnSpawnEnded;
    }

    protected override void StartAction()
    {
        Rotater.Rotate(_rotationSpeed);
    }

    protected override void EndAction()
    {
        _arrow.Disactivate();
        Animator.Idle();
        Animator.EndThrow();
    }

    private void OnCoinSpawnEnded(FrisbeeMover frisbeeMover)
    {
        Bag.AddFrisbee(frisbeeMover);
    }

    private void OnSpawnEnded()
    {
        Bag.FrisbeeMovers[0].transform.parent = _coinPosition.transform;
        Bag.FrisbeeMovers[0].transform.localRotation = Quaternion.Euler(Vector3.zero);
        Bag.FrisbeeMovers[0].transform.localPosition = Vector3.zero;
        Animator.Keep();
        _arrow.Activate();
    }
}
