using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public event UnityAction<int> MoneyChanged;
    private const string MoneyKey = "Money";

    [SerializeField] private CoinsChecker _coinsChecker;

    private int _money;

    private void Start()
    {
        if(PlayerPrefs.HasKey(MoneyKey))
            _money = PlayerPrefs.GetInt(MoneyKey);

        MoneyChanged?.Invoke(_money);
    }

    private void OnEnable()
    {
        _coinsChecker.CoinEntered += OnCoinEntered;
    }

    private void OnDisable()
    {
        _coinsChecker.CoinEntered -= OnCoinEntered;
    }

    private void OnCoinEntered(int amount)
    {
        AddMoney(amount);
    }

    private void AddMoney(int money)
    {
        if (money <= 0)
            return;

        _money += money;
        SaveMoneyCount();

        MoneyChanged?.Invoke(_money);
    }

    private void SaveMoneyCount()
    {
        PlayerPrefs.SetInt(MoneyKey, _money);
        PlayerPrefs.Save();
    }
}
