using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 500f;
    [SerializeField] private float _topClamp = -90f;
    [SerializeField] private float _bottomClamp = 90f;

    private float _xRotation = 0f;
    private float _yRotation = 0f;

    private Vector3 _mousePosition;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        GetMouseInputs();
    }

    private void FixedUpdate()
    {
        RotatinAround();
    }

    private void GetMouseInputs()
    {
        _mousePosition = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
        _mousePosition *= _mouseSensitivity * Time.fixedDeltaTime; ;
    }

    private void RotatinAround()
    {
        _xRotation -= _mousePosition.y;
        _xRotation = Mathf.Clamp(_xRotation, _topClamp, _bottomClamp);

        _yRotation += _mousePosition.x;
        transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
    }
}
