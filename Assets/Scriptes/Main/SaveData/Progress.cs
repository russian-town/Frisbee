using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Saver))]
public class Progress : MonoBehaviour
{
    private const string GoldCoin = "Gold";
    private const string SilverCoin = "Silver";
    private const string CopperCoin = "Copper";

    [SerializeField] private RampMover _rampMover;
    [SerializeField] private CoinsSpawner _coinsSpawner;
    [SerializeField] private Coin _goldCoin;
    [SerializeField] private Coin _silverCoin;
    [SerializeField] private Coin _copperCoin;
    [SerializeField] private Material _stickmanMaterial;
    [SerializeField] private Color _defaultStickmanColor;

    private List<CoinData> _coinDatas = new List<CoinData>();
    private Saver _saver;

    private void Awake()
    {
        _saver = GetComponent<Saver>();
    }

    private void OnEnable()
    {
        _rampMover.LevelComplete += OnLevelComplete;
    }

    private void OnDisable()
    {
        _rampMover.LevelComplete -= OnLevelComplete;
    }

    private void Start()
    {
        LoadProgress();
        SetStickmanColor();
    }

    private void OnLevelComplete()
    {
        Coin[] coins = FindObjectsOfType<Coin>();

        foreach (var coin in coins)
        {
            _coinDatas.Add(new CoinData(coin.Type, coin.transform.position, coin.transform.rotation));
        }

        _saver.Save(_coinDatas);
    }

    private void LoadProgress()
    {
        if (_saver.LoadCoinDatas() == null)
            return;

        foreach (var coinData in _saver.LoadCoinDatas())
        {
            if (coinData.CoinType == GoldCoin)
                _coinsSpawner.Spawn(_goldCoin, coinData.GetPosition(), coinData.GetRotation());
            else if (coinData.CoinType == SilverCoin)
                _coinsSpawner.Spawn(_silverCoin, coinData.GetPosition(), coinData.GetRotation());
            else if (coinData.CoinType == CopperCoin)
                _coinsSpawner.Spawn(_copperCoin, coinData.GetPosition(), coinData.GetRotation());
        }
    }

    private void SetStickmanColor()
    {
        if (_saver.LoadItemData() != null)
            _stickmanMaterial.color = _saver.LoadItemData().GetColor();
        else
            _stickmanMaterial.color = _defaultStickmanColor;
    }
}
