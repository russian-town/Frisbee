using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    private const string LastLevelIndexKey = "LastSceneIndex";
    private const int FirstLevelIndex = 2;

    private int _currentLevelIndex;

    private void Start()
    {
        _currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextLevel()
    {
        int nextLevelIndex = _currentLevelIndex + 1;

        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(FirstLevelIndex);
            PlayerPrefs.DeleteKey(LastLevelIndexKey);
        }
        else
        {
            SceneManager.LoadScene(nextLevelIndex);
            SaveNextLevel(nextLevelIndex);
        }
    }

    public void LoadLastLevel()
    {
        if (PlayerPrefs.HasKey(LastLevelIndexKey))
            SceneManager.LoadScene(PlayerPrefs.GetInt(LastLevelIndexKey));
        else
            SceneManager.LoadScene(FirstLevelIndex);
    }

    public void LoadLevelByName(string name)
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;

        SceneManager.LoadScene(name);
    }

    public void ReloadCurrentLevel()
    {
        if (Time.timeScale < 1f)
            Time.timeScale = 1f;

        SceneManager.LoadScene(_currentLevelIndex);
    }

    private void SaveNextLevel(int nextLevel)
    {
        if (PlayerPrefs.HasKey(LastLevelIndexKey))
        {
            if (nextLevel > PlayerPrefs.GetInt(LastLevelIndexKey))
                PlayerPrefs.SetInt(LastLevelIndexKey, nextLevel);
        }
        else
        {
            PlayerPrefs.SetInt(LastLevelIndexKey, FirstLevelIndex);
        }
    }
}
