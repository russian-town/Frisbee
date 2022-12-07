using TMPro;
using UnityEngine;

public class LocalizationData : MonoBehaviour
{
    [SerializeField] private TMP_Text _translationText;
    [SerializeField] private string _ruText;
    [SerializeField] private string _enText;
    [SerializeField] private string _trText;

    private void Start()
    {
        Translate();
    }

    public void Translate()
    {
        if (PlayerPrefs.HasKey(Localization.LanguageKey))
        {
            switch (PlayerPrefs.GetString(Localization.LanguageKey))
            {
                case Localization.Ru:
                    _translationText.text = _ruText;
                    break;
                case Localization.En:
                    _translationText.text = _enText;
                    break;
                case Localization.Tr:
                    _translationText.text = _trText;
                    break;
                default:
                    _translationText.text = _enText;
                    break;
            }
        }
    }
}
