using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    private void LoadNextScene()
    {
        if (SceneManager.GetSceneAt(SceneManager.GetActiveScene().buildIndex + 1) == null)
            return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
