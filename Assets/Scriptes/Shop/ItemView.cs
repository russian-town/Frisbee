using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    private const string Bought = "Bought";
    private const string Selected = "Selected";

    public event UnityAction<ItemView> BuyButtonClicked;
    public event UnityAction<ItemView> RewardedButtonClicked;

    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _rewardedButton;
    [SerializeField] private Color _boughtColor;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Image _checkImage;
    [SerializeField] private Image _buyButtonImage;

    public string ID { get; private set; }
    public bool IsBought => PlayerPrefs.HasKey(ID + Bought);
    public bool IsSelected => PlayerPrefs.HasKey(ID + Selected);
    public Color IconColor { get; private set; }
    public int Price { get; private set; }

    private void Start()
    {
        _checkImage.enabled = false;

        if (IsBought == true)
        {
            DisableRewardedState();
            EnableBoghtState();
        }

        if (IsSelected == true)
        {
            DisableRewardedState();
            EnableSelectedState();
        }
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClicked);
        _rewardedButton.onClick.AddListener(OnRewardedButtonClicked);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
        _rewardedButton.onClick.RemoveListener(OnRewardedButtonClicked);
    }

    public void Initialize(Color iconColor, int price, bool isRewarded)
    {
        IconColor = iconColor;
        _icon.color = IconColor;
        ID = $"{IconColor.r}{IconColor.g}{IconColor.b}{IconColor.a}";

        if (isRewarded == false)
        {
            Price = price;
            _priceText.text = Price.ToString();
            DisableRewardedState();
        }
        else
        {
            EnableRewardedState();
        }
    }

    public void EnableBoghtState()
    {
        _priceText.enabled = false;
        _checkImage.enabled = true;
        _buyButtonImage.color = _boughtColor;

        if (IsBought == false)
            Save(Bought);
    }

    public void EnableSelectedState()
    {
        _buyButtonImage.color = _selectedColor;

        if (IsSelected == false)
            Save(Selected);
    }

    public void DisableSelectedState()
    {
        PlayerPrefs.DeleteKey(ID + Selected);
        _buyButtonImage.color = _boughtColor;
    }
    public void DisableRewardedState()
    {
        _buyButton.gameObject.SetActive(true);
        _rewardedButton.gameObject.SetActive(false);
    }    

    private void EnableRewardedState()
    {
        if (IsBought == true || IsSelected == true)
            return;

        _buyButton.gameObject.SetActive(false);
        _buyButton.gameObject.SetActive(true);
    }

    private void OnBuyButtonClicked()
    {
        BuyButtonClicked?.Invoke(this);
    }

    private void OnRewardedButtonClicked()
    {
        RewardedButtonClicked?.Invoke(this);
    }

    private void Save(string key)
    {
        PlayerPrefs.SetString(ID + key, key);
        PlayerPrefs.Save();
    }
}
