using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _amount;

    public int Amount => _amount;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
