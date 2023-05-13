using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerThirdCam : MonoBehaviour
{
    //new input system
    private PlayerInputMap _playerInput;
    [Header("References")]
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerInput = new PlayerInputMap();
        _playerInput.Juego.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //creem un nou vector per anar rotant el que segueix el player
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        

        float horizontalInput = _playerInput.Juego.CameraMove.ReadValue<Vector2>().x;
        float verticalInput = _playerInput.Juego.CameraMove.ReadValue<Vector2>().y;
    }
}
