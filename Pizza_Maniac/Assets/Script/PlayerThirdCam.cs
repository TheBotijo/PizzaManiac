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

    [Header("Controls")]
    public float rotationSpeed;
    public GameObject crosshair;
    bool aimed;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerInput = new PlayerInputMap();
        _playerInput.Juego.Enable();

        CinemachineFreeLook vcam = thirdCam;
        vcam.m_CommonLens = true;
        vcam.m_Lens.FieldOfView = 35;
    }

    // Update is called once per frame
    void Update()
    {
        //Si volguessim que la direccio del player fos dependent de la orientació de la càmara
        //Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        //orientation.forward = viewDir.normalized;
        /*
        float horizontalInput = _playerInput.Juego.Move.ReadValue<Vector2>().x;
        float verticalInput = _playerInput.Juego.Move.ReadValue<Vector2>().y;
        float CamHInput = _playerInput.Juego.CameraMove.ReadValue<Vector2>().x;
        float CamVInput = _playerInput.Juego.CameraMove.ReadValue<Vector2>().y;
        //Camera Basic
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        Vector3 CamDir = orientation.forward * CamVInput + orientation.right * CamHInput;
        //Llegim els inputs de direcció i anem rotant l'orientació del player
        if (inputDir != Vector3.zero)
        {
            orientation.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            playerObj.forward = Vector3.Slerp(orientation.forward, CamDir.normalized, Time.deltaTime * rotationSpeed);
            
        } */
        Vector3 dirToCombat = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
        
        orientation.forward = dirToCombat.normalized;
        playerObj.forward = dirToCombat.normalized;

        CinemachineFreeLook vcam = thirdCam;
        vcam.m_CommonLens = true;

        //Aim OnPress
        if (_playerInput.Juego.Aim.IsPressed() && !aimed)
        {
            vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 25, 10f * Time.deltaTime);
            crosshair.SetActive(true);
        }
        else
        {
            vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 35, 10f * Time.deltaTime);
            crosshair.SetActive(false);
        }
            
    }
}
