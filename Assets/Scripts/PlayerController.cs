using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;

    private CharacterController _controller;

    private Vector3 _velocity;
    
    private float _playerSpeed = 4;
    private float _sprintMultiplier = 1.5f;
    private float _crouchMultiplier = 0.5f;

    private float _camCrouchPosYValue = -1;
    private Vector3 _camNormalPos;
    
    private bool _isGrounded;
    private float _groundDistance = 0.4f;
    private float _jumpSpeed = 2f;
    private float _gravity = -9.81f;

    private float _mouseSensitivity = 200f;
    private float _xRotation;

    private bool _inPauseMenu;


    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _camNormalPos = Camera.main.transform.localPosition;
    }

    private void Update()
    {
        if (_inPauseMenu)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        
        Move();
        RotatePlayer();
        Crouch();
    }

    private void Move()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            moveDir *= _sprintMultiplier;
        }


        _controller.Move(moveDir * _playerSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && _isGrounded)
        { 
            _velocity.y = Mathf.Sqrt(_jumpSpeed * -2f * _gravity);
        }

        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);

    }

    private void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);

        transform.Rotate(Vector3.up * mouseX);
    }

    private void Crouch()
    {

        //TODO SMOOTH THAT (IF WE WANT TO USE CROUCH)

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _playerSpeed *= _crouchMultiplier;
            Camera.main.transform.localPosition =
                new Vector3(_camNormalPos.x, _camNormalPos.y + _camCrouchPosYValue, _camNormalPos.z);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            _playerSpeed /= _crouchMultiplier;
            Camera.main.transform.localPosition = _camNormalPos;
        }
    }

    
    //Functions can be used to set some values
    public void SetMouseSensitivity(float mouseSensitivity)
    {
        _mouseSensitivity = mouseSensitivity;
    }

    public void SetInPauseMenu(bool inPauseMenu)
    {
        _inPauseMenu = inPauseMenu;
    }

}
