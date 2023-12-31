using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance {get; private set;}
    public Camera PlayerCamera {get; private set;}
    float _sens;
    [SerializeField] Playable _initial;

    public Playable Player {
        get => _player; 
        set {
            // Include the player body we were previously looking through
            _player?.RenderBody();
            _player = value;
            // Exclude the current player body
            _player.CullBody();
        }
    }
    Playable _player;
    float _yRotation;
    void Start()
    {
        if (Instance != null) {
            Debug.LogError("There can only be one CameraControl");
            Destroy(this);
            return;
        }
        PlayerCamera = GetComponent<Camera>();
        Instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        _yRotation = 0f;
        _sens = 1;
        Player = _initial;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.CameraPosition;
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
