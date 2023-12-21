using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody _playerBody;

    private PlayerInput _input;
    // private InputAction _movement;
    private InputAction _throw;
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private Transform _throwLocation;
    [SerializeField] private GameObject _hook;
    void Start()
    {
        _playerBody = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
        // _movement = _input.actions.FindAction("Move");
        _throw = _input.actions.FindAction("Throw");
    }

    // Update is called once per frame
    void Update()
    {
        if (_throw.triggered) {
            throwHook();
        }
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

    void throwHook() {
        Vector3 forward = _cameraTransform.forward;
        GameObject hook = Instantiate(_hook, _throwLocation.position, Quaternion.identity);
        hook.GetComponentInChildren<Rope>().setOrigin(_throwLocation);
        Rigidbody hookBody = hook.GetComponentInChildren<Rigidbody>();
        float speed = 20f;
        Vector3 throwVelocity = speed * forward;
        hookBody.AddForce(throwVelocity, ForceMode.VelocityChange);
    }
}
