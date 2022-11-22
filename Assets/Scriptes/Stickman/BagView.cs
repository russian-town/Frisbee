using UnityEngine;
using TMPro;

[RequireComponent(typeof(Bag))]

public class BagView : MonoBehaviour
{
    [SerializeField] private TMP_Text _countFrisbeeText;

    private Bag _bag;

    private void Awake()
    {
        _bag = GetComponent<Bag>();
    }

    private void Start()
    {
        _countFrisbeeText.enabled = false;
    }

    private void OnEnable()
    {
        _bag.CountFrisbeeChanged += OnCountFrisbeeChange;
    }

    private void OnDisable()
    {
        _bag.CountFrisbeeChanged -= OnCountFrisbeeChange;
    }

    private void OnCountFrisbeeChange(int count)
    {
        if (_countFrisbeeText.enabled == false)
            _countFrisbeeText.enabled = true;

        _countFrisbeeText.text = count.ToString();
    }
}
