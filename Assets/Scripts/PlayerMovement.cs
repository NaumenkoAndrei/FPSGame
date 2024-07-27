using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 12f;
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private Transform _groundCheckerPivot;
    [SerializeField] private float _checkGroundRadius = 0.4f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _cameraTransform;


    private CharacterController _controller;
    private Vector3 _moveDirection;
    private float _velocity;
    private float _gravity = -9.81f * 3;

    private bool _isGrounded;
    private bool _isMoving;

    private Vector3 _lastPosition = new Vector3(0f, 0f, 0f);

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_isGrounded)
        {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        _isGrounded = IsOnGround();

        if (_isGrounded && _velocity < 0)
        {
            _velocity = -2;
        }

        Move(_moveDirection);
        DoGravity();
    }

    private bool IsOnGround()
    {
        bool result = Physics.CheckSphere(_groundCheckerPivot.position, _checkGroundRadius, _groundMask);

        return result;
    }

    private bool IsMoving()
    {
        if (_lastPosition != gameObject.transform.position && _isGrounded)
        {
            return true;
        }
        return false;
    }

    private void Move(Vector3 direction)
    {
        Vector3 forward = new Vector3(_cameraTransform.forward.x, 0f, _cameraTransform.forward.z).normalized;
        Vector3 right = new Vector3(_cameraTransform.right.x, 0f, _cameraTransform.right.z).normalized;

        Vector3 move = (direction.x * right + direction.z * forward).normalized;

        _controller.Move(move * _speed * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        _velocity = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
    }

    private void DoGravity()
    {
        _velocity += _gravity * Time.deltaTime;

        _controller.Move(Vector3.up * _velocity * Time.fixedDeltaTime);
    }
}