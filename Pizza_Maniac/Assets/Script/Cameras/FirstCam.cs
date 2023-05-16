using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FirstCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    public Transform playerObj;

    float xRotation;
    float yRotation;
    public Camera cam;

    private PlayerInputMap _playerInput;
    public PlayerMoveJump playermove;
    // Start is called before the first frame update
    private void Start()
    {
        _playerInput = new PlayerInputMap();
        _playerInput.Juego.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        UserInput();

        if (_playerInput.Juego.Aim.IsPressed())
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 25, 10f * Time.deltaTime);
            playermove.aiming = true;
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 35, 10f * Time.deltaTime);
            playermove.aiming = false;
        }
    }

    private void UserInput()
    {
        float mouseX = _playerInput.Juego.CameraMove.ReadValue<Vector2>().x * Time.deltaTime * sensX;
        float mouseY = _playerInput.Juego.CameraMove.ReadValue<Vector2>().y * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        playerObj.rotation = Quaternion.Euler(0, yRotation, 0);
    }
} 
