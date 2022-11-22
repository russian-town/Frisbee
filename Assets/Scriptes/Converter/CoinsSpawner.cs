using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinsSpawner : MonoBehaviour
{
    public event UnityAction<FrisbeeMover> CoinSpawnEnded;
    public event UnityAction SpawnEnded;
    public event UnityAction GameOver;

    [SerializeField] private ParticleSystem _poofParticle;
    [SerializeField] private CoinPosition _target;
    [SerializeField] private Point _startPoint;
    [SerializeField] private Point _point1;
    [SerializeField] private Point _point2;
    [SerializeField] private float _delay;
    [SerializeField] private FollowCamera _followCamera;

    private List<CoinMover> _coins = new List<CoinMover>();
    private int _currentCountToMove;
    private int _currentCountToSpawn;
    private CameraTarget _coin;

    public void SetCurrentCount(int accumulateCount)
    {
        if(accumulateCount <= 0)
        {
            GameOver?.Invoke();
            return;
        }

        _currentCountToMove = accumulateCount;
        _currentCountToSpawn = accumulateCount;
    }

    public void Spawn(CoinMover coinTemplate, int count)
    {
        for (int i = 0; i < count; i++)
        {
            CoinMover newCoin = Instantiate(coinTemplate, transform.position, Quaternion.identity);

            if(_coin == null)
            {
                if(newCoin.TryGetComponent(out CameraTarget cameraTarget))
                {
                    _coin = cameraTarget;
                    _followCamera.SetTarget(_coin);
                }
            }

            newCoin.SetPreviousPosition(_startPoint.transform.position);
            newCoin.Init(_target);
            newCoin.MoveEnded += OnMoveEnded;
            _coins.Add(newCoin);
        }

        _currentCountToSpawn -= count;

        if (_currentCountToSpawn == 0)
        {
            _poofParticle.Play();
            Invoke(nameof(StartMoveCoin), _poofParticle.main.duration);
        }
            
    }

    private void StartMoveCoin()
    {
        StartCoroutine(MoveCoin());
    }

    private IEnumerator MoveCoin()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);

        foreach (var coin in _coins)
        {
            coin.StartMove(_target.transform.position, _point1.transform.position, _point2.transform.position, _startPoint.transform.position);
            yield return delay;
        }
    }

    private void OnMoveEnded(FrisbeeMover frisbeeMover)
    {
        CoinSpawnEnded?.Invoke(frisbeeMover);
        frisbeeMover.MoveEnded -= OnMoveEnded;
        _currentCountToMove--;

        if (_currentCountToMove == 0)
        {
            SpawnEnded?.Invoke();
            _followCamera.ResetTarget();
        }
    }
}
