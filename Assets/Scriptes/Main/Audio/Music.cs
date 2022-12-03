using UnityEngine;

public class Music : MonoBehaviour
{
    private void Awake()
    {
        Music[] musics = FindObjectsOfType<Music>();

        if (musics.Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
