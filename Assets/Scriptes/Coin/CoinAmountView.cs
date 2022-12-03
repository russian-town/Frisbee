using System.Collections;
using TMPro;
using UnityEngine;

public class CoinAmountView : MonoBehaviour
{
    private const float MinAlpha = 0f;

    [SerializeField] private CanvasGroup _amountTextCanvasGroup;
    [SerializeField] private float _switchAlphaSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private TMP_Text _amountText;

    private void Start()
    {
        StartCoroutine(SwithAlpha());
    }

    public void Initialize(string amount)
    {
        _amountText.text = amount;
    }

    private IEnumerator SwithAlpha()
    {
        while(_amountTextCanvasGroup.alpha != 0)
        {
            _amountTextCanvasGroup.alpha = Mathf.MoveTowards(_amountTextCanvasGroup.alpha, MinAlpha, Time.deltaTime * _switchAlphaSpeed);
            transform.position = Vector3.MoveTowards(transform.position,transform.position + Vector3.up, Time.deltaTime * _moveSpeed);
            yield return null;
        }

        Destroy(gameObject);
    }
}
