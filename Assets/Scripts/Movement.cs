using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _playerBody;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _accel;
    void Start()
    {
        _playerBody = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 forward = _cameraTransform.forward;
    }
}
