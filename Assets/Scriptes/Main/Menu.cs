using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private const string Shop = "Shop";

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Audio _audio;
    [SerializeField] private Level _level;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _shopButton.onClick.AddListener(OnShopButtonClick);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _shopButton.onClick.RemoveListener(OnShopButtonClick);
    }

    private void OnPlayButtonClick()
    {
        _level.LoadLastLevel();
    }

    private void OnShopButtonClick()
    {
        _level.LoadLevelByName(Shop);
    }
}
