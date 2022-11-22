using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ObstacleView), typeof(FrisbeeDetector))]

public class Obstacle : MonoBehaviour
{
    public event UnityAction GameOver;

    [SerializeField] private int _count;
    [SerializeField] private Bag _previousBag;
    [SerializeField] private FrisbeeMover _startFrisbee;

    private ObstacleView _obstacleView;
    private FrisbeeDetector _frisbeeDetector;

    private void Awake()
    {
        _obstacleView = GetComponent<ObstacleView>();
        _frisbeeDetector = GetComponent<FrisbeeDetector>();
    }

    private void OnEnable()
    {
        _frisbeeDetector.FrisbeeEntered += OnFrisbeeEntered;
    }

    private void Start()
    {
        _obstacleView.SetCount(_count);
    }
    
    private void OnFrisbeeEntered(FrisbeeMover frisbeeMover)
    {
        if (frisbeeMover != _startFrisbee)
            return;

        _frisbeeDetector.FrisbeeEntered -= OnFrisbeeEntered;

        if (_count >= _previousBag.FrisbeeMovers.Count)
        {
            GameOver?.Invoke();
            return;
        }

        for (int i = _previousBag.FrisbeeMovers.Count - 1; i >= _previousBag.FrisbeeMovers.Count - _count; i--)
        {
            FrisbeeMover frisbee = _previousBag.FrisbeeMovers[i];
            frisbee.gameObject.SetActive(false);
        }
    }
}
