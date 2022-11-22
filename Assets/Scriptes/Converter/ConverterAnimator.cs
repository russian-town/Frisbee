using UnityEngine;

[RequireComponent(typeof(Animator))]

public class ConverterAnimator : MonoBehaviour
{
    private const string ShackeParametr = "Shacke";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Shacke()
    {
        _animator.SetTrigger(ShackeParametr);
    }
}
