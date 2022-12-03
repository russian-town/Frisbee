using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SnapScroll : MonoBehaviour
{
    private List<ItemView> _itemViews;
    private RectTransform _rectTransform;
    private int _selectedItemID;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(List<ItemView> itemViews)
    {
        _itemViews = itemViews;
    }

    private void Update()
    {
    }
}
