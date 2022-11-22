using UnityEngine;

[RequireComponent(typeof(Animator))]

public class StickmanAnimator : MonoBehaviour
{
    private const string StartThrowParametr = "StartThrow";
    private const string EndThrowParametr = "EndThrow";
    private const string KeepParametr = "Keep";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Idle()
    {
        _animator.SetBool(KeepParametr, false);
    }

    public void Keep()
    {
        _animator.SetBool(KeepParametr, true);
    }

    public void StartThrow()
    {
        _animator.SetBool(StartThrowParametr, true);
    }

    public void EndThrow()
    {
        _animator.SetBool(StartThrowParametr, false);
        _animator.SetTrigger(EndThrowParametr);
    }
}
