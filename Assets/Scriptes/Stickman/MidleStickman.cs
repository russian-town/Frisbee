using UnityEngine;

[RequireComponent(typeof(TrajectoryDrawer))]

public class MidleStickman : Stickman
{
    [SerializeField] private Stickman _previousStickman;

    private TrajectoryDrawer _trajectoryDrawer;

    private void Start()
    {
        _trajectoryDrawer = GetComponent<TrajectoryDrawer>();
    }

    private void OnEnable()
    {
        _previousStickman.Threw += OnThrew;
    }

    private void OnDisable()
    {
        _previousStickman.Threw -= OnThrew;
    }

    protected override void StartAction()
    {
        Animator.StartThrow();
        _trajectoryDrawer.Draw(Point1, Point2, Target.transform.position);
    }

    protected override void EndAction()
    {
        Animator.EndThrow();
        _trajectoryDrawer.Clear();
    }

    private void OnThrew(FrisbeeMover frisbeeMover)
    {
        if (frisbeeMover.gameObject.activeSelf == true)
            Bag.AddFrisbee(frisbeeMover);
    }
}
