using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerThirdCam : MonoBehaviour
{
    //new input system
    private PlayerInputMap _playerInput;
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform combatLookAt;

    public CameraStyle currentStyle;

    public enum CameraStyle
    {
        Basic, 
        Combat
    }
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
        //Si volguessim que la direccio del player fos dependent de la orientaci� de la c�mara
        //Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        //orientation.forward = viewDir.normalized;

        if (currentStyle== CameraStyle.Basic)
        {
            float horizontalInput = _playerInput.Juego.Move.ReadValue<Vector2>().x;
            float verticalInput = _playerInput.Juego.Move.ReadValue<Vector2>().y;
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
            //Llegim els inputs de direcci� i anem rotant l'orientaci� del player
            if (inputDir != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
                orientation.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        } 
        else if(currentStyle== CameraStyle.Combat) 
        {
            Vector3 dirToCombat = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombat.normalized;

            playerObj.forward = dirToCombat.normalized;
        }

    }
}
