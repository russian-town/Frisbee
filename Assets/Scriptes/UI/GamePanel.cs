using UnityEngine;

public class GamePanel : MonoBehaviour
{
    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Disactivate()
    {
        gameObject.SetActive(false);
    }
}
