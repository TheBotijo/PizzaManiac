using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerThirdCam : MonoBehaviour
{
    //new input system
    public PlayerInput _playerInput;
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
        _playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        //creem un nou vector per anar rotant el que segueix el player
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        

        float horizontalInput = _playerInput.actions["HorizontalMove"].ReadValue<Vector2>().x;
        float verticalInput = _playerInput.actions["VerticalMove"].ReadValue<Vector2>().y;
    }
}
