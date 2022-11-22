using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameView))]

public class Game : MonoBehaviour
{
    [SerializeField] private List<Obstacle> _obstacles;
    [SerializeField] private CoinsSpawner _coinsSpawner;
    [SerializeField] private RampMover _rampMover;
    [SerializeField] private float _delayBeforeDefeat;
    [SerializeField] private float _delayBetweenNextLevels;

    private GameView _gameView;

    private void Awake()
    {
        _gameView = GetComponent<GameView>();
    }

    private void OnEnable()
    {
        _coinsSpawner.GameOver += OnGameOver;

        if (_obstacles.Count <= 0)
            return;

        foreach (var obstacle in _obstacles)
        {
            obstacle.GameOver += OnGameOver;
        }

        _rampMover.LevelComplete += OnLevelComplete;
    }

    private void OnDisable()
    {
        _coinsSpawner.GameOver += OnGameOver;

        if (_obstacles.Count <= 0)
            return;

        foreach (var obstacle in _obstacles)
        {
            obstacle.GameOver -= OnGameOver;
        }

        _rampMover.LevelComplete -= OnLevelComplete;
    }

    private void Pause()
    {
        Time.timeScale = 0;
    }

    private void OnGameOver()
    {
        StartCoroutine(GameOver());
        Pause();
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(_delayBeforeDefeat);
        _gameView.ShowGameOverPanel();
    }

    private void OnLevelComplete()
    {
        StartCoroutine(LevelComplete());
    }

    private IEnumerator LevelComplete()
    {
        yield return new WaitForSecondsRealtime(_delayBetweenNextLevels);
        _gameView.ShowLevelCompletePanel();
    }
}
