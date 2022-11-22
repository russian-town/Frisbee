using System.Collections.Generic;
using UnityEngine;

public class StickmanChanger : MonoBehaviour
{
    [SerializeField] private List<Stickman> _stickmans;
    [SerializeField] private CoinsSpawner _coinsSpawner;

    private int _currentStickman;

    private void Start()
    {
        _currentStickman = 0;
    }

    private void OnEnable()
    {
        foreach (var stickman in _stickmans)
        {
            stickman.TurnEnded += OnTurnEnded;
        }

        _coinsSpawner.SpawnEnded += OnSpawnEnded;
    }

    private void OnDisable()
    {
        _coinsSpawner.SpawnEnded -= OnSpawnEnded;
    }

    private void Update()
    {
        if (_stickmans[_currentStickman] != null)
            _stickmans[_currentStickman].Action();
    }

    private Stickman GetNextStickman()
    {
        if (_currentStickman >= _stickmans.Count - 1)
            return null;

        _currentStickman++;
        Stickman nextSticman = _stickmans[_currentStickman];

        if (nextSticman != null)
        {
            nextSticman.Rotate();
            return nextSticman;
        }

        return null;
    }

    private void OnTurnEnded(Stickman stickman)
    {
        stickman.TurnEnded -= OnTurnEnded;
        GetNextStickman();
    }

    private void OnSpawnEnded()
    {
        GetNextStickman();
    }
}
