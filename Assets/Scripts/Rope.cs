using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Generates an animation of a rope throw,
/// Where one end of the rope is free to move, and the other end is fixed in place
/// </summary>
public class Rope : MonoBehaviour
{
    private const float DistancePerPoint = .3f;
    private bool _drewEnd;
    private List<Vector3> _pastPoints;
    [SerializeField] private LineRenderer _ropeLine;
    [SerializeField] private Transform _hook;

    void Start() {
        _pastPoints = new List<Vector3> {};
        AddPoint(_hook.position);
        const float width = .02f;
        _ropeLine.startWidth = width;
        _ropeLine.endWidth = width;
        _drewEnd = false;
    }

    void AddPoint(Vector3 p) {
        _pastPoints.Add(p);
        _ropeLine.positionCount = _pastPoints.Count;
        _ropeLine.SetPosition(_pastPoints.Count - 1, p);
    }

    void Update()
    {
        if (_drewEnd) {
            _ropeLine.positionCount = _pastPoints.Count;
            _ropeLine.SetPositions(_pastPoints.ToArray());
        }
        _drewEnd = false;
        Vector3 endPoint = _hook.position;
        Vector3 recentlyDrawnPoint = _pastPoints[_pastPoints.Count - 1];
        float distance = (endPoint - recentlyDrawnPoint).magnitude;
        if (distance >= DistancePerPoint) {
            AddPoint(endPoint);
        } else {
            _ropeLine.positionCount += 1;
            _ropeLine.SetPosition(_pastPoints.Count, endPoint);
            _drewEnd = true;
        }
    }
}
