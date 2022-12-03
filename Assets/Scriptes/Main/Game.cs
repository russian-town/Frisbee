using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameView))]

public class Game : MonoBehaviour
{
    [SerializeField] private List<Obstacle> _obstacles;
    [SerializeField] private RampMover _rampMover;
    [SerializeField] private float _delayBeforeDefeat;
    [SerializeField] private float _delayBetweenNextLevel;
    [SerializeField] private float _delayBetweenStartLevel;

    private GameView _gameView;

    private void Awake()
    {
        _gameView = GetComponent<GameView>();
    }

    private void OnEnable()
    {
        if (_obstacles.Count <= 0)
            return;

        foreach (var obstacle in _obstacles)
        {
            obstacle.GameOver += OnGameOver;
        }

        _rampMover.LevelComplete += StartLevelComplete;
    }

    private void OnDisable()
    {
        if (_obstacles.Count <= 0)
            return;

        foreach (var obstacle in _obstacles)
        {
            obstacle.GameOver -= OnGameOver;
        }

        _rampMover.LevelComplete -= StartLevelComplete;
    }

    private void Start()
    {
        StartCoroutine(StartDelay());
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

    private void StartLevelComplete()
    {
        StartCoroutine(LevelComplete());
    }

    private IEnumerator LevelComplete()
    {
        yield return new WaitForSecondsRealtime(_delayBetweenNextLevel);
        _gameView.ShowLevelCompletePanel();
    }

    private IEnumerator StartDelay()
    {
        Time.timeScale = 0;
        float delay = Time.realtimeSinceStartup + _delayBetweenStartLevel;

        while(Time.realtimeSinceStartup < delay)
        {
            yield return 0;
        }

        Time.timeScale = 1;
    }
}
