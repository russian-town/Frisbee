using UnityEngine;

[RequireComponent(typeof(FrisbeeDetector), typeof(BoosterView))]

public abstract class Booster : MonoBehaviour
{
    [SerializeField] private int _boostCount;
    [SerializeField] private FrisbeeSpawner _frisbeeSpawner;
    [SerializeField] private Bag _previousBag;
    [SerializeField] private string _symbol;

    private FrisbeeDetector _frisbeeDetector;
    private BoosterView _boosterView;
    private int _currentFrisbee = 1;

    protected int BoostCount => _boostCount;
    protected int CurrentFrisbeeCount => _previousBag.LastCount(); 

    private void Awake()
    {
        _frisbeeDetector = GetComponent<FrisbeeDetector>();
        _boosterView = GetComponent<BoosterView>();
    }

    private void Start()
    {
        _boosterView.SetText(_symbol, _boostCount);
    }

    private void OnEnable()
    {
        _frisbeeDetector.FrisbeeEntered += OnFrisbeeEntered;
    }

    private void OnDisable()
    {
        _frisbeeDetector.FrisbeeEntered -= OnFrisbeeEntered;
    }

    protected abstract int GetBoostCount();

    private void OnFrisbeeEntered(FrisbeeMover frisbeeMover)
    {
        _frisbeeSpawner.Spawn(GetBoostCount() - _currentFrisbee);
        transform.parent.gameObject.SetActive(false);
    }
}
