using UnityEngine;

[RequireComponent(typeof(Bag))]

public class FrisbeeSpawner : MonoBehaviour
{
    [SerializeField] private FrisbeeSpawnPoint _frisbeeSpawnPoint;
    [SerializeField] private FrisbeeMover _frisbeeTemplate;

    private Bag _bag;

    public FrisbeeSpawnPoint FrisbeeSpawnPoint => _frisbeeSpawnPoint;

    private void Awake()
    {
        _bag = GetComponent<Bag>();
    }

    public void Spawn(int count)
    {
        for (int i = 0; i <= count; i++)
        {
            FrisbeeMover newFrisbee = Instantiate(_frisbeeTemplate, _frisbeeSpawnPoint.transform);
            newFrisbee.transform.localPosition = _frisbeeSpawnPoint.SortPosition(_frisbeeTemplate);
            newFrisbee.transform.rotation = Quaternion.identity;
            _bag.AddFrisbee(newFrisbee);
            _frisbeeSpawnPoint.SetLastPosition(i);
        }
    }
}
