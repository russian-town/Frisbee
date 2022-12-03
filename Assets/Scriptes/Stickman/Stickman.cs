using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(StickmanAnimator), typeof(StickmanRotater), typeof(Bag))]

public abstract class Stickman : MonoBehaviour
{
    public event UnityAction<FrisbeeMover> Threw;
    public event UnityAction<Stickman> TurnEnded;

    [SerializeField] private float _delay;
    [SerializeField] private FrisbeePoint _target;
    [SerializeField] private StartFrisbeePoint _startFrisbeePoint;
    [SerializeField] private FrisbeeMover _startFrisbee;
    [SerializeField] private FrisbeeSpawnPoint _nextFrisbeeSpawnPoint;
    [SerializeField] private Point _point1;
    [SerializeField] private Point _point2;

    private StickmanAnimator _animator;
    private StickmanRotater _rotater;
    private Bag _bag;
    private int _currentCount;
    private Coroutine _startThrowFrisbee;
    private Coroutine _startThrowCoins;

    private Material _material;

    protected StickmanAnimator Animator => _animator;
    protected StickmanRotater Rotater => _rotater;
    protected Bag Bag => _bag;
    protected FrisbeePoint Target => _target;
    protected Point Point1 => _point1;
    protected Point Point2 => _point2;
    protected FrisbeeMover StartFrisbee => _startFrisbee;

    private void Awake()
    {
        _animator = GetComponent<StickmanAnimator>();
        _rotater = GetComponent<StickmanRotater>();
        _bag = GetComponent<Bag>();
    }

    public void Action()
    {
        if (_bag.FrisbeeMovers.Count <= 0 || EventSystem.current.IsPointerOverGameObject())
            return;

        if (_startThrowFrisbee == null && _startThrowCoins == null)
        {
            if (Input.GetMouseButton(0))
                StartAction();
            else if (Input.GetMouseButtonUp(0))
                EndAction();
        }
    }

    public void Throw()
    {
        foreach (var frisbeeMover in _bag.FrisbeeMovers)
        {
            frisbeeMover.MoveEnded += OnMoveEnded;
            frisbeeMover.transform.parent = null;
        }

        _currentCount = _bag.FrisbeeMovers.Count;

        if (_bag.FrisbeeMovers[0] is CoinMover coin == false)
            _startThrowFrisbee = StartCoroutine(ThrowFrisbee());
        else
            _startThrowCoins = StartCoroutine(ThrowCoins());
    }

    public void Rotate()
    {
        _rotater.StartRotation();
    }

    protected abstract void StartAction();

    protected abstract void EndAction();

    private IEnumerator ThrowFrisbee()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);
        float distance = Vector3.Distance(transform.position, _target.transform.position);

        foreach (var frisbeeMover in _bag.FrisbeeMovers)
        {
            if (frisbeeMover.gameObject.activeSelf == false)
                continue;

            if (frisbeeMover == _startFrisbee)
                frisbeeMover.Init(_startFrisbeePoint);
            else
                frisbeeMover.Init(_nextFrisbeeSpawnPoint);

            frisbeeMover.SetPreviousPosition(frisbeeMover.transform.position);
            frisbeeMover.StartMove(_target.transform.position, _point1.transform.position, _point2.transform.position, transform.position);
            Threw?.Invoke(frisbeeMover);
            yield return delay;
        }
    }

    private IEnumerator ThrowCoins()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);

        foreach (var frisbeeMover in Bag.FrisbeeMovers)
        {
            if(frisbeeMover is CoinMover coin)
            {
                OnMoveEnded(coin);
                coin.Throw(transform.forward);
                yield return delay;
            }
        }
    }

    private void OnMoveEnded(FrisbeeMover frisbeeMover)
    {
        frisbeeMover.MoveEnded -= OnMoveEnded;

        _currentCount--;

        if (_currentCount <= 0)
            TurnEnded?.Invoke(this);
    }
}
