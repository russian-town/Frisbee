using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bag : MonoBehaviour
{
    public event UnityAction<int> CountFrisbeeChanged;

    private List<FrisbeeMover> _frisbeeMovers = new List<FrisbeeMover>();

    public IReadOnlyList<FrisbeeMover> FrisbeeMovers => _frisbeeMovers;

    public void AddFrisbee(FrisbeeMover frisbeeMover)
    {
        _frisbeeMovers.Add(frisbeeMover);
        CountFrisbeeChanged?.Invoke(_frisbeeMovers.Count);
    }

    public void RemoveFrisbee(FrisbeeMover frisbeeMover)
    {
        if (_frisbeeMovers.Contains(frisbeeMover))
        {
            _frisbeeMovers.Remove(frisbeeMover);
            CountFrisbeeChanged?.Invoke(_frisbeeMovers.Count);
        }
    }

    public void RemoveAll()
    {
        _frisbeeMovers.Clear();
    }
}
