using UnityEngine;
using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;

public class Localization : MonoBehaviour
{
    public const string LanguageKey = "language";
    public const string Ru = "ru";
    public const string En = "en";
    public const string Tr = "tr";

    [SerializeField] private List<LocalizationData> _localizationDatas = new List<LocalizationData>();

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        yield return YandexGamesSdk.Initialize();

        switch (YandexGamesSdk.Environment.i18n.lang)
        {
            case Ru:
                PlayerPrefs.SetString(LanguageKey, Ru);
                break;
            case En:
                PlayerPrefs.SetString(LanguageKey, En);
                break;
            case Tr:
                PlayerPrefs.SetString(LanguageKey, Tr);
                break;
            default:
                PlayerPrefs.SetString(LanguageKey, En);
                break;
        }

        foreach (var localizationData in _localizationDatas)
        {
            localizationData.Translate();
        }
    }
}
