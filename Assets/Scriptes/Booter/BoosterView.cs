using UnityEngine;
using TMPro;

public class BoosterView : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;

    public void SetText(string symbol, int boostCount)
    {
        _countText.text = symbol + boostCount.ToString();
    }
}
