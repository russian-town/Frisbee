using UnityEngine;
using TMPro;

[RequireComponent(typeof(Converter), typeof(CoinsSpawner))]

public class ConverterDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _countFrisbeeText;
    [SerializeField] private TMP_Text _coinsGoldCountText;
    [SerializeField] private TMP_Text _coinsSilverCountText;
    [SerializeField] private TMP_Text _coinsCopperCountText;
    [SerializeField] private FrisbeeCountPanel _frisbeeCountPanel;
    [SerializeField] private CoinsCountPanel _coinsCountPanel;
    [SerializeField] private CoinMover _coinGoldTemplate;
    [SerializeField] private CoinMover _coinSilverTemplate;
    [SerializeField] private CoinMover _coinCopperTemplate;

    private Converter _converter;
    private CoinsSpawner _coinsSpawner;
    private int _coinsGoldToSpawnCount;
    private int _coinsSilverToSpawnCount;
    private int _coinsCopperSpawnCount;

    private void Awake()
    {
        _converter = GetComponent<Converter>();
        _coinsSpawner = GetComponent<CoinsSpawner>();
    }

    private void Start()
    {
        _frisbeeCountPanel.gameObject.SetActive(true);
        _coinsCountPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _converter.CountFrisbeeChanged += OnCountFrisbeeChanged;
        _converter.CoinsCountChanged += OnCoinsCountChanged;
    }

    private void OnDisable()
    {
        _converter.CountFrisbeeChanged -= OnCountFrisbeeChanged;
        _converter.CoinsCountChanged -= OnCoinsCountChanged;
    }

    private void OnCountFrisbeeChanged(int count)
    {
        _countFrisbeeText.text = count.ToString();
    }

    private void OnCoinsCountChanged(int goldCoinCount, int silverCoinCount, int copperCoinCount)
    {
        _coinsGoldCountText.text = goldCoinCount.ToString();
        _coinsSilverCountText.text = silverCoinCount.ToString();
        _coinsCopperCountText.text = copperCoinCount.ToString();
        _coinsGoldToSpawnCount = goldCoinCount;
        _coinsSilverToSpawnCount = silverCoinCount;
        _coinsCopperSpawnCount = copperCoinCount;
        Invoke(nameof(ShowCoinsCount), 1f);
    }

    private void ShowCoinsCount()
    {
        _frisbeeCountPanel.gameObject.SetActive(false);
        _coinsCountPanel.gameObject.SetActive(true);
        int accumulateCount = _coinsCopperSpawnCount + _coinsSilverToSpawnCount + _coinsGoldToSpawnCount;

        _coinsSpawner.SetCurrentCount(accumulateCount);
        _coinsSpawner.Spawn(_coinCopperTemplate, _coinsCopperSpawnCount);
        _coinsSpawner.Spawn(_coinSilverTemplate, _coinsSilverToSpawnCount);
        _coinsSpawner.Spawn(_coinGoldTemplate, _coinsGoldToSpawnCount);
    }
}
