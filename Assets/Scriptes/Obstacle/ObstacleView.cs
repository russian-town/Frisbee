using UnityEngine;
using TMPro;

public class ObstacleView : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;

    public void SetCount(int count)
    {
        _countText.text = count.ToString();
    }
}
