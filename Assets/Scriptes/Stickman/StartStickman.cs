using UnityEngine;

public class StartStickman : Stickman
{
    private void Start()
    {
        Bag.AddFrisbee(StartFrisbee);
    }

    protected override void StartAction()
    {
        Animator.StartThrow();
    }

    protected override void EndAction()
    {
        Animator.EndThrow();
    }
}
