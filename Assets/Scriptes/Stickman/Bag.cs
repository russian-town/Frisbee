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

    public int LastCount()
    {
        int count = 0;

        for (int i = 0; i < _frisbeeMovers.Count; i++)
        {
            if (_frisbeeMovers[i].gameObject.activeSelf == false)
                continue;
            else
                count++;
        }

        return count;
    }
}
