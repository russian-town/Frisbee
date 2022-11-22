using System.Collections.Generic;
using UnityEngine;

public class TrajectoryDrawer : MonoBehaviour
{
    [SerializeField] private float _maxPositionX;
    [SerializeField] private int _segmentNumber;
    [SerializeField] private Point _pointTemplate;

    private List<Point> _points = new List<Point>();

    private void Start()
    {
        for (int i = 0; i <= _segmentNumber; i++)
        {
            Point newPoint = Instantiate(_pointTemplate, transform);
            newPoint.gameObject.SetActive(false);
            _points.Add(newPoint);
        }
    }

    public void Draw(Point point1, Point point2, Vector3 targetPosition)
    {
        MoveMidlePoints(point1, point2, targetPosition);

        for (int i = 0; i <= _segmentNumber; i++)
        {
            float step = (float)i / _segmentNumber;
            Vector3 pointPosition = Bezier.GetPoint(transform.position, point1.transform.position, point2.transform.position, targetPosition, step);
            _points[i].gameObject.SetActive(true);
            _points[i].transform.position = pointPosition;
        }
    }

    public void Clear()
    {
        foreach (var point in _points)
        {
            point.gameObject.SetActive(false);
        }
    }

    private void MoveMidlePoints(Point point1, Point point2, Vector3 targetPosition)
    {
        float radius = Vector3.Distance(transform.position, targetPosition) / 2f;
        Vector3 midlePosition = point1.transform.position;
        midlePosition.z = transform.position.z + radius;
        midlePosition.y = transform.position.y;
        midlePosition.x = Mathf.Clamp(midlePosition.x + Input.GetAxis("Mouse X"), -_maxPositionX, _maxPositionX);
        point1.transform.position = midlePosition;
        point2.transform.position = midlePosition;
    }
}
