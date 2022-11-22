using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TrailRenderer))]

public class FrisbeeMover : MonoBehaviour
{
    public event UnityAction<FrisbeeMover> MoveEnded;

    [SerializeField] private float _aspectRatio;

    private Transform _parent;
    private Vector3 _position;
    private Vector3 _previousPosition;
    private Quaternion _rotation;
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void OnDisable()
    {
        MoveEnded?.Invoke(this);
    }

    public void SetPreviousPosition(Vector3 previousPosition)
    {
        _previousPosition = previousPosition;
    }

    public void Init(StartFrisbeePoint startFrisbeePoint)
    {
        _parent = startFrisbeePoint.transform;
        _position = Vector3.zero;
        _rotation = Quaternion.identity;
    }

    public void Init(FrisbeeSpawnPoint frisbeeSpawnPoint)
    {
        _parent = frisbeeSpawnPoint.transform;
        _position = frisbeeSpawnPoint.SortPosition(this);
        _rotation = frisbeeSpawnPoint.transform.localRotation;
        frisbeeSpawnPoint.IncreaseLastPosition();
    }

    public void StartMove(Vector3 targetPosition, Vector3 point1, Vector3 point2, Vector3 startPositon)
    {
        _trailRenderer.enabled = true;
        StartCoroutine(Move(targetPosition, point1, point2, startPositon));
    }

    private IEnumerator Move(Vector3 targetPosition, Vector3 point1, Vector3 point2, Vector3 startPositon)
    {
        float time = 0;

        while (time < 1)
        {
            Vector3 target = Bezier.GetPoint(_previousPosition, point1, point2, targetPosition, time);
            float distance = Vector3.Distance(startPositon, targetPosition);
            time += distance / _aspectRatio * Time.deltaTime;
            transform.position = target;
            transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(_previousPosition, point1, point2, targetPosition, time));
            yield return null;
        }

        time = 0;
        transform.parent = _parent;
        transform.localPosition = _position;
        transform.localRotation = _rotation;
        _trailRenderer.enabled = false;

        MoveEnded?.Invoke(this);
    }
}
