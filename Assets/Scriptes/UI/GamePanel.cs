using UnityEngine;
using UnityEngine.UI;

public abstract class GamePanel : MonoBehaviour
{
    private const string Menu = "Menu";

    [SerializeField] private Button _targetButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Level _level;
    [SerializeField] private YandexAds _yandexAds;

    protected Level Level => _level;

    private void OnEnable()
    {
        _targetButton.onClick.AddListener(OnTargetButtonClick);
        _menuButton.onClick.AddListener(OnMenuButtonClick);
    }

    private void OnDisable()
    {
        _targetButton.onClick.RemoveListener(OnTargetButtonClick);
        _menuButton.onClick.RemoveListener(OnMenuButtonClick);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Disactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTargetButtonClick()
    {
        Action();
        _yandexAds.ShowInterstitial();
    }

    private void OnMenuButtonClick()
    {
        _level.LoadLevelByName(Menu);
    }

    protected abstract void Action();
}
