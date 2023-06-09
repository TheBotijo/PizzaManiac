using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveJump : MonoBehaviour
{
    //Rigidbody del player = rb
    Rigidbody rb;

    //Definim variables de Moviment
    [Header("Movement")]
    public float moveSpeed;
    private float lateSpeed;

    float yRotation;
    float horizontalInput;
    float verticalInput;
    public Transform orientation;
    private PlayerInputMap _playerInput;
    public Health_Damage HealthDamage;
    public bool aiming;
    

    Vector3 moveDirection;

    //Animations
    public Animator animator;

    //Variables de Detecci� Ground
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public float groundDrag;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    private void Start()
    {
        lateSpeed = moveSpeed;
        _playerInput = new PlayerInputMap();
        _playerInput.Juego.Enable();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation= true;
       
        

        readyToJump = true;
    }

    public void Update()
    {
        //per comprovar si toca terra amb un vector de la meitat de l'altura del personatge + un marge
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, whatIsGround);
        
            UserInput();
            SpeedControl();
            PlayMove();
        
        
        //Animations
        //walk
        if (_playerInput.Juego.Move.IsPressed())
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);
        }
        else { animator.SetBool("Walk", false); }
        //run
        if (_playerInput.Juego.Run.IsPressed())
        {            
            animator.SetBool("Run", true);
            animator.SetBool("Walk", false);
            
        }
        else { animator.SetBool("Run", false); }
        //Jump
        if (_playerInput.Juego.Jump.IsPressed() && animator.GetBool("Run")==true)
        {
            animator.SetBool("RunJump",true);          
        }
        if (_playerInput.Juego.Jump.IsPressed() && animator.GetBool("Run") == false)
        {
            animator.SetBool("Jump", true);  
        }
           
        //comprovem si toca el terra per aplicar un fregament al player
        if (grounded)
        {
            rb.drag = groundDrag;
        }        
        else
            rb.drag = 0;
    }
    private void UserInput()
    {
        //recollir inputs de moviment en els eixos
        horizontalInput = _playerInput.Juego.Move.ReadValue<Vector2>().x;
        verticalInput = _playerInput.Juego.Move.ReadValue<Vector2>().y;

        if (_playerInput.Juego.Jump.WasPressedThisFrame() && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    private void PlayMove()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //moure's seguint el empty orientaci� endavant el eix vertical i orientaci� dreta el eix horitzontal
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;


        //apliquem una for�a al moviment quan esta tocant al terra
        if (aiming)
            moveSpeed = lateSpeed / 2;
        else
            moveSpeed = lateSpeed;

        if (grounded && _playerInput.Juego.Run.IsPressed())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 20f, ForceMode.Force);
            
        }
        else if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            
        }
        else if(!grounded && flatVel.magnitude == 0)
        {
            rb.AddForce(moveDirection.normalized, ForceMode.Force);
        }
        else if (!grounded && flatVel.magnitude > (moveSpeed * 8)) //a l'aire
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 20f * airMultiplier, ForceMode.Force);
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