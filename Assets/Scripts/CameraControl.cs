using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float _sens;
    private float _yRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _yRotation = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
    }

    private void HandleRotation() {
        float horizontalRotation = Input.GetAxisRaw("Mouse X") * _sens;
        float verticalRotation = Input.GetAxisRaw("Mouse Y") * _sens;

        _yRotation += verticalRotation;
        if (_yRotation > 90) {
            float diff = _yRotation - 90;
            _yRotation = 90;
            verticalRotation -= diff;
        }
        if (_yRotation < -90) {
            float diff = _yRotation + 90;
            _yRotation = -90;
            verticalRotation -= diff;
        }
        transform.Rotate(Vector3.up, horizontalRotation, Space.World);
        transform.Rotate(Vector3.right, -verticalRotation);
    }
}
