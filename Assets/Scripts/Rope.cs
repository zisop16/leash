using System;
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
    private List<Vector3> _pastPoints;

    private float _length;

    private bool _connected;
    public bool Connected {
        get => _connected;
        set {_connected = value;}
    }
    [SerializeField] private LineRenderer _ropeLine;
    [SerializeField] private Transform _hook;
    private Transform _origin;

    public void setOrigin(Transform t) {
        _origin = t;
    }

    void Start() {
        _pastPoints = new List<Vector3> {};
        AddPoint(_origin.position);
        const float width = .02f;
        _ropeLine.startWidth = width;
        _ropeLine.endWidth = width;
    }

    private void AddPoint(Vector3 p) {
        _pastPoints.Add(p);
    }
    /// <summary>
    /// As the line gets longer and longer, we want to draw fewer and fewer vertices
    /// Returns the desired distance per vertex based on current line length
    /// d(l) = A * (l^b + 1)
    /// Where A is (0, inf)
    /// B is (0, 1]
    /// </summary>
    /// <returns>Float from 0 -> a</returns>
    private float VertexDistance() {
        float a = .1f;
        float b = .75f;
        float distance = a * (Mathf.Pow(_length, b) + 1);
        return distance;
    }

    void FixedUpdate() {
        _pastPoints[0] = _origin.position;
        if (!Connected) {
            DisconnectedDraw();
        }
        else {
            RemoveCloseVertices();
            SimplifyVertices();
        }
        _ropeLine.SetPositions(_pastPoints.ToArray());
        _ropeLine.positionCount = _pastPoints.Count + 1;
        _ropeLine.SetPosition(_pastPoints.Count, _hook.position);
    }

    void Update()
    {
        
    }

    private void RemoveCloseVertices() {
        for (int i = 1; i < _pastPoints.Count; i++) {
            Vector3 curr = _pastPoints[i];
            float currDist = (curr - _hook.position).sqrMagnitude;
            float playerDist = (_origin.position - _hook.position).sqrMagnitude;
            if (currDist >= playerDist) {
                _pastPoints.RemoveAt(i);
                i--;
            } else {
                break;
            }
        }
    }
    /// <summary>
    /// Moves all points within pastPoints towards a line directly from the origin to the hook
    /// If the point is close enough to the line, it will be removed from the vertex list
    /// </summary>
    private void SimplifyVertices() {
        Vector3 direction = (_hook.position - _origin.position).normalized;
        for (int i = 1; i < _pastPoints.Count; i++) {
            Vector3 curr = _pastPoints[i];
            Vector3 fromOrigin = curr - _origin.position;
            Vector3 target = Vector3.Dot(fromOrigin, direction) * direction;
            Vector3 displacement = (fromOrigin - target);
            float distance = displacement.magnitude;
            const float minDistance = .4f;
            if (distance < minDistance) {
                _pastPoints.RemoveAt(i);
                i--;
                continue;
            }
            // Move the point 30% of the way towards the target
            const float lerp = .70f;
            Vector3 modified = _origin.position + target + displacement * lerp;
            _pastPoints[i] = modified;
        }
    }

    private void DisconnectedDraw() {
        Vector3 endPoint = _hook.position;
        Vector3 recentlyDrawnPoint = _pastPoints[_pastPoints.Count - 1];
        float distance = (endPoint - recentlyDrawnPoint).magnitude;
        
        if (distance >= VertexDistance()) {
            AddPoint(endPoint);
            _length += distance;
        }
    }
}
