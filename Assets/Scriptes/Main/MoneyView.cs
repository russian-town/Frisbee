using UnityEngine;
using TMPro;

[RequireComponent(typeof(Wallet))]

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;

    private Wallet _wallet;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
    }

    private void OnEnable()
    {
        _wallet.MoneyChanged += OnMoneyChange;
    }

    private void OnDisable()
    {
        _wallet.MoneyChanged -= OnMoneyChange;
    }

    private void OnMoneyChange(int money)
    {
        _moneyText.text = money.ToString();
    }
}
