using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FrisbeeDetector), typeof(ConverterAnimator))]

public class Converter : MonoBehaviour
{
    public event UnityAction<int> CountFrisbeeChanged;
    public event UnityAction<int, int, int> CoinsCountChanged;

    [SerializeField] private Stickman _lastStickman;
    [SerializeField] private ParticleSystem _smookPoof;

    private FrisbeeDetector _frisbeeDetector;
    private ConverterAnimator _animator;
    private int _coinsGold;
    private int _coinsSilver;
    private int _coinsCopper;
    private int _frisbeeCount;
    private int _frisbeeToGoldCoins = 10 / 1;
    private int _frisbeeToSilverCoins = 5 / 1;
    private int _frisbeeToCopperCoins = 1 / 1;

    private void Awake()
    {
        _frisbeeDetector = GetComponent<FrisbeeDetector>();
        _animator = GetComponent<ConverterAnimator>();
    }

    private void OnEnable()
    {
        _frisbeeDetector.FrisbeeEntered += OnFrisbeeEntered;
        _lastStickman.TurnEnded += OnTurnEnded;
    }

    private void OnDisable()
    {
        _frisbeeDetector.FrisbeeEntered -= OnFrisbeeEntered;
        _lastStickman.TurnEnded -= OnTurnEnded;
    }

    private void OnFrisbeeEntered(FrisbeeMover frisbeeMover)
    {
        if (frisbeeMover is CoinMover coinMover == false)
        {
            _frisbeeCount++;
            _animator.Shacke();
            _smookPoof.Play();
            CountFrisbeeChanged?.Invoke(_frisbeeCount);
        }
    }

    private void OnTurnEnded(Stickman stickman)
    {
        _coinsGold = _frisbeeCount / _frisbeeToGoldCoins;
        _coinsSilver = _frisbeeCount % _frisbeeToGoldCoins / _frisbeeToSilverCoins;
        _coinsCopper = _frisbeeCount % _frisbeeToGoldCoins % _frisbeeToSilverCoins / _frisbeeToCopperCoins;
        CoinsCountChanged?.Invoke(_coinsGold, _coinsSilver, _coinsCopper);
    }
}
