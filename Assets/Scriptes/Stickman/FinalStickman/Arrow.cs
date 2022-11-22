using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void Start()
    {
        Disactivate();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Disactivate()
    {
        gameObject.SetActive(false);
    }
}
