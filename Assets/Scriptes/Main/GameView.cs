using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] private LevelCompletePanel _levelCompletePanel;
    [SerializeField] private GameOverPanel _gameOverPanel;

    private void Start()
    {
        _levelCompletePanel.Disactivate();
        _gameOverPanel.Disactivate();
    }

    public void ShowLevelCompletePanel()
    {
        _levelCompletePanel.Activate();
    }

    public void ShowGameOverPanel()
    {
        _gameOverPanel.Activate();
    }
}
