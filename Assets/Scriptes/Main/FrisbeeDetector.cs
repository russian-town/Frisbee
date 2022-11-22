using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]

public class FrisbeeDetector : MonoBehaviour
{
    public event UnityAction<FrisbeeMover> FrisbeeEntered;

    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        _boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out FrisbeeMover frisbeeMover))
        {
            FrisbeeEntered?.Invoke(frisbeeMover);
        }
    }
}
