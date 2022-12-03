using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Saver _saver;
    [SerializeField] private Content _content;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private ItemView _itemViewTemplate;
    [SerializeField] private ShopScroll _snapScroll;
    [SerializeField] private List<Item> _items = new List<Item>();
    [SerializeField] private YandexAds _yandexAds;
    [SerializeField] private ItemView _defaultItemView;
    [SerializeField] private Color _defaultColor;

    private ItemView _rewardedItemView;
    private List<ItemView> _itemView = new List<ItemView>();

    private void Start()
    {
        SpawnItemView();
        _itemView.Add(_defaultItemView);
        _defaultItemView.BuyButtonClicked += OnBuyButtonClicked;
        _snapScroll.Initialize(_itemView);

        if (_saver.LoadItemData() == null)
        {
            _defaultItemView.DisableRewardedState();
            _defaultItemView.EnableBoghtState();
            _defaultItemView.EnableSelectedState();
        }
    }

    private void OnDisable()
    {
        foreach (var itemView in _itemView)
        {
            itemView.BuyButtonClicked -= OnBuyButtonClicked;
            itemView.RewardedButtonClicked -= OnRewardedButtonCliked;
        }

        _defaultItemView.BuyButtonClicked -= OnBuyButtonClicked;
    }

    private void SpawnItemView()
    {
        foreach (var item in _items)
        {
            ItemView newItemView = Instantiate(_itemViewTemplate, _content.transform);
            newItemView.Initialize(item.IconColor, item.Price, item.IsRewarded);
            newItemView.BuyButtonClicked += OnBuyButtonClicked;
            newItemView.RewardedButtonClicked += OnRewardedButtonCliked;
            _itemView.Add(newItemView);
        }
    }

    private void OnBuyButtonClicked(ItemView itemView)
    {
        if (itemView == _defaultItemView)
            _saver.Save(new ItemData(_defaultColor));

        if (itemView.IsBought == true)
            SelectItemView(itemView);
        else if (itemView.IsBought == false && _wallet.Money >= itemView.Price)
            BuyItemView(itemView);
    }

    private void OnRewardedButtonCliked(ItemView itemView)
    {
        _yandexAds.RewardedCallback += OnRewardedCallback;
        _yandexAds.ErrorCallback += OnErrorCallback;
        _yandexAds.CloseCallback += OnCloseCollback;
        _yandexAds.OpenCallback += OnOpenCallBack;
        _rewardedItemView = itemView;
        _yandexAds.ShowVideo();
    }

    private void OnRewardedCallback()
    {
        UnsubscribeAds();
        _rewardedItemView.DisableRewardedState();
        BuyItemView(_rewardedItemView);
        _rewardedItemView = null;
    }

    private void OnErrorCallback(string error)
    {
        UnsubscribeAds();
        _rewardedItemView = null;
    }

    private void OnCloseCollback()
    {
        UnsubscribeAds();
        _rewardedItemView = null;
    }

    private void OnOpenCallBack()
    {
        UnsubscribeAds();
    }

    private void UnsubscribeAds()
    {
        _yandexAds.OpenCallback -= OnOpenCallBack;
        _yandexAds.RewardedCallback -= OnRewardedCallback;
        _yandexAds.ErrorCallback -= OnErrorCallback;
        _yandexAds.CloseCallback -= OnCloseCollback;
    }

    private void BuyItemView(ItemView itemView)
    {
        itemView.EnableBoghtState();
        _wallet.DecreaseMoney(itemView.Price);
        SelectItemView(itemView);
    }

    private void SelectItemView(ItemView itemView)
    {
        DeselectItemsView(itemView);
        itemView.EnableSelectedState();

        if (itemView == _defaultItemView)
            return;

        _saver.Save(new ItemData(itemView.IconColor));
    }

    private void DeselectItemsView(ItemView currentSelectionItemView)
    {
        foreach (var itemView in _itemView)
        {
            if (itemView == currentSelectionItemView)
                continue;

            if (itemView.IsSelected == true)
                itemView.DisableSelectedState();
        }
    }
}
