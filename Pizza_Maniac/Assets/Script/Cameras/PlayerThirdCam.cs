using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using Cinemachine;
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
    public Transform combatLookAt;
    public CinemachineFreeLook thirdCam;
    public PlayerMoveJump playermove;

    [Header("Controls")]
    public float rotationSpeed;
    bool aimed;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerInput = new PlayerInputMap();
        _playerInput.Juego.Enable();

        //GameObject playerr = GameObject.FindGameObjectWithTag("Player");
        //playermove.GetComponent<PlayerMoveJump>();

        CinemachineFreeLook vcam = thirdCam;
        vcam.m_CommonLens = true;
        vcam.m_Lens.FieldOfView = 35;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Si volguessim que la direccio del player fos dependent de la orientació de la càmara
        //Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        //orientation.forward = viewDir.normalized;

        /*
        float CamHInput = _playerInput.Juego.CameraMove.ReadValue<Vector2>().x;
        float CamVInput = _playerInput.Juego.CameraMove.ReadValue<Vector2>().y;
        
        Vector3 CamDir = orientation.forward * CamVInput + orientation.right * CamHInput;
        */
        Vector3 dirToCombat = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);

        orientation.forward = dirToCombat.normalized;
        playerObj.forward = dirToCombat.normalized;

        float horizontalInput = _playerInput.Juego.Move.ReadValue<Vector2>().x;
        float verticalInput = _playerInput.Juego.Move.ReadValue<Vector2>().y;
         
        //Camera Basic
        Vector3 inputDir = (orientation.forward * verticalInput + orientation.right * horizontalInput) * Time.deltaTime;
        
        //Llegim els inputs de direcció i anem rotant l'orientació del player
        if (inputDir != Vector3.zero)
        {
            orientation.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            playerObj.forward = Vector3.Slerp(orientation.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);

        }
        


        CinemachineFreeLook vcam = thirdCam;
        vcam.m_CommonLens = true;

        //Aim OnPress
        if (_playerInput.Juego.Aim.IsPressed())
        {
            vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 25, 10f * Time.deltaTime);
            playermove.aiming = true;
        }
        else
        {
            vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 35, 10f * Time.deltaTime);
            playermove.aiming = false;
        }

        /*
        //Aim OnClick
        if (_playerInput.Juego.Aim.WasPressedThisFrame() && !aimed)
        {
            vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 25, 10f * Time.deltaTime);
            aimed = true;
        }
        else if (_playerInput.Juego.Aim.WasPressedThisFrame() && aimed)
        {
            vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 35, 10f * Time.deltaTime);
            aimed = false;
        }*/

    }
}
