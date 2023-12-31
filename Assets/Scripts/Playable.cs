using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playable : MonoBehaviour
{
    private const int Default = 0;
    private const int PlayerLayer = 3;
    /// <summary>
    /// Game objects which should not be rendered if the camera is on the current body
    /// </summary>
    [SerializeField] private List<GameObject> _body;
    [SerializeField] private Transform _cameraTransform;
    public Vector3 CameraPosition {get => _cameraTransform.position;}
    public void CullBody() {
        foreach (GameObject obj in _body) {
            obj.layer = PlayerLayer;
        }
    }
    public void RenderBody() {
        foreach(GameObject obj in _body) {
            obj.layer = Default;
        }
    }
}
