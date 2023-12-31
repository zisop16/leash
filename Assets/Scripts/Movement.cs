using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public static Movement Instance {get; private set;}
    private PlayerInput _input;
    private InputAction _movement;
    private InputAction _possess;
    void Start()
    {
        if (Instance != null) {
            Debug.LogError("There can only be one Movement");
            Destroy(this);
            return;
        }
        Instance = this;
        _input = GetComponent<PlayerInput>();
        _movement = _input.actions.FindAction("Move");
        _possess = _input.actions.FindAction("Possess");
        // _throw = _input.actions.FindAction("Throw");
    }

    // Update is called once per frame
    void Update()
    {
        Playable target = PossessionTarget();
        if (target != null) {
            Possess(target);
        }
    }

    Playable PossessionTarget() {
        if (_possess.triggered) {
            // Shoots a ray towards the direction the player is looking
            Ray ray = CameraControl.Instance.PlayerCamera.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                GameObject obj = hit.collider.gameObject;
                Playable playable = obj.GetComponentInParent<Playable>();
                return playable;
            }
        }
        return null;
    }

    void Possess(Playable target) {
        CameraControl.Instance.Player = target;
    }



    /*
    void Move() {
        // (x, y) = (AD, WS) input
        Vector2 reading = _movement.ReadValue<Vector2>();
        Vector3 forward = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
        float _accel = 2f;
        Vector3 acceleration = (forward * reading.y + _cameraTransform.right * reading.x) * _accel;
        _playerBody.AddForce(acceleration, ForceMode.Acceleration);
    }
    */
}
