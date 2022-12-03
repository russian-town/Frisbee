using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public event UnityAction<int> MoneyChanged;
    private const string MoneyKey = "Money";

    [SerializeField] private bool _isClear;

    private int _money;

    public int Money => _money;

    private void OnValidate()
    {
        if (_isClear == true)
            ClearSaveData();
    }

    private void Start()
    {
        if(PlayerPrefs.HasKey(MoneyKey))
            _money = PlayerPrefs.GetInt(MoneyKey);

        MoneyChanged?.Invoke(_money);
    }

    public void AddMoney(int money)
    {
        if (money <= 0)
            return;

        _money += money;
        SaveMoneyCount();

        MoneyChanged?.Invoke(_money);
    }

    public void DecreaseMoney(int value)
    {
        if (value <= 0)
            return;

        _money -= value;
        SaveMoneyCount();

        MoneyChanged?.Invoke(_money);
    }

    private void SaveMoneyCount()
    {
        PlayerPrefs.SetInt(MoneyKey, _money);
        PlayerPrefs.Save();
    }

    public void ClearSaveData()
    {
        PlayerPrefs.DeleteAll();
    }
}
