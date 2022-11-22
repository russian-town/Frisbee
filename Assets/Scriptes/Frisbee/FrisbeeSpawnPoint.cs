using UnityEngine;

public class FrisbeeSpawnPoint : MonoBehaviour
{
    private int _lastPosition;

    public Vector3 SortPosition(FrisbeeMover frisbeeMover)
    {
        Vector3 sortPosition = transform.localPosition;
        float fullScale = 2f;
        sortPosition.y = frisbeeMover.transform.localScale.y * fullScale * _lastPosition;
        return sortPosition;
    }

    public void SetLastPosition(int lastPosition)
    {
        _lastPosition = lastPosition;
    }

    public void IncreaseLastPosition()
    {
        _lastPosition++;
    }
}
