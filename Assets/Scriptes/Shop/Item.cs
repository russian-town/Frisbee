using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items", order = 51)]
public class Item : ScriptableObject
{
    [SerializeField] private Color _iconColor;
    [SerializeField] private bool _isRewarded;
    [SerializeField] private int _price;

    public bool IsRewarded => _isRewarded;
    public Color IconColor => _iconColor;
    public int Price => _price;
}
