using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveJump : MonoBehaviour
{
    //Rigidbody del player = rb
    Rigidbody rb;

    //Definim variables de Moviment
    [Header("Movement")]
    public float moveSpeed;

    float horizontalInput;
    float verticalInput;
    public Transform orientation;

    Vector3 moveDirection;

    //Variables de Detecci� Ground
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public float groundDrag;

    //Variables de Jump
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation= true;

        readyToJump = true;
    }

    private void Update()
    {
        //per comprovar si toca terra amb un vector de la meitat de l'altura del personatge + un marge
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        UserInput();
        SpeedControl();

        //comprovem si toca el terra per aplicar un fregament al player
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }
    private void FixedUpdate()
    {
        PlayMove();
    }
    private void UserInput()
    {
        //recollir inputs de moviment en els eixos
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    private void PlayMove()
    {
        //moure's seguint el empty orientaci� endavant el eix vertical i orientaci� dreta el eix horitzontal
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        //apliquem una for�a al moviment quan esta tocant al terra
        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded) //a l'aire
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limitar la velocitat si aquesta es mes gran del que volem aconseguir
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            //tornem a aplicar aquesta nova velocitat al player
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // resetejar la velocitat de y per saltar sempre igual
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //apliquem impulse perque es nomes una vegada
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}