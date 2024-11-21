using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))] //this is a great thing to show people as it shows them how to make sure components will set up on objects.
public class FPSController : MonoBehaviour
{
    private float _xRotation;
    private Vector3 _playerVelocity;
    CharacterController _player;
    [SerializeField] private bool groundedPlayer;
    [SerializeField] private float _jumpForce = 1.0f;
    [SerializeField] private float mouseSensitivity = 200f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Camera camera;
    [SerializeField] private float xCameraBounds = 60f;
    [SerializeField] private float gravityValue = -9.81f;

    #region Smoothing code
    private Vector2 _currentMouseDelta;
    private Vector2 _currentMouseVelocity;
    [SerializeField] private float smoothTime = .1f;
    
    #endregion
  
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Awake()
    {
        _player = GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "SpecialArea")
        {
            Debug.Log("TriggerEnter");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "SpecialArea")
        {
            Debug.Log("TriggerExit");
        }
    }
    void Update()
    {
        Movement();
        Jump();
        Rotation();
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpForce * -3.0f * gravityValue);
        }
        _playerVelocity.y += gravityValue * Time.deltaTime;
        _player.Move(_playerVelocity * Time.deltaTime);
    }

    private void Movement()
    {
        _playerVelocity = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal"); //easier to explain after by using the forward and right vectors
        _playerVelocity.Normalize();
        _player.SimpleMove(_playerVelocity * speed );
    }

    private void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        Vector2 targetDelta = new Vector2(mouseX, mouseY);
        _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetDelta, ref _currentMouseVelocity, smoothTime);
        _xRotation -= _currentMouseDelta.y;
        _xRotation = Mathf.Clamp(_xRotation, -xCameraBounds, xCameraBounds);
        transform.Rotate(Vector3.up * _currentMouseDelta.x);
        camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }

    private void LateUpdate()
    {

        
    }
}
