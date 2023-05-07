using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimpleMove : MonoBehaviour
{
    [SerializeField] private UIManager uIManager;
    int pizzasLeft = 3;
    public bool isHit = false;
    //definim els inputs del usuari
    private float _vInput;
    private float _hInput;
    //Característiques físiques de la moto
    [SerializeField] Rigidbody rb;
    public Transform manillar;
    //Caracteristiques tecniques de la moto
    [SerializeField] Vector3 COG;
    [SerializeField] float horsePower = 5;
    [SerializeField] float brakeForce;
    bool brake; //isBraking?
    float currentbrakeForce;

    [SerializeField] float maxlayingAngle = 45f;
    public float targetlayingAngle;

    //Definim les capes de les rodes, el Transform i apart els Colliders
    [SerializeField] Transform frontWheelTransform;
    [SerializeField] Transform backWheelTransform;
    [SerializeField] WheelCollider frontWheel;
    [SerializeField] WheelCollider backWheel;

    //Definim les variables per la rotació del manillar
    [SerializeField] float currentSteeringAngle;
    [SerializeField] float maxSteeringAngle;
    //Variables definides amb un rang per poderles canviar facilment i que la rotacio i inclinacio de la moto es pugui experimentar milo
    [Range(0.000001f, 1)][SerializeField] float turnSmoothing;
    [Range(-40, 40)] public float layingammount;
    [Range(0.000001f, 1)][SerializeField] float leanSmoothing;
    [Range(0f, 0.1f)][SerializeField] float speedteercontrolTime;

    // Com fem servir fisiques utilitzem FixedUpdate
    void FixedUpdate()
    {
        //Començem a cridar a totes les funcions
        PlayerInputs();
        Motor();
        Steering();
        UpdateWheels();
        UpdateManillar();
        LayOnTurn();
        //DownPresureOnSpeed();

    }

    private void PlayerInputs()
    {
        //Guardem els inputs del usuari
        _vInput = Input.GetAxis("Vertical");
        _hInput = Input.GetAxis("Horizontal");
        brake = Input.GetKey(KeyCode.Space);
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Colision") //Detectem els objectes que ens interessen
        {
            if (!isHit) //Breu booleana per evitar o permetre l'accès al damage durant el temps de cooldown
            {
                isHit = true;
                Debug.Log("Has xocat");
                pizzasLeft--;
                //enviem el nombre de pizzes restants al manager per actualitzar els cors via index
                uIManager.Damage(pizzasLeft);
                //Definim un cooldown perque evitar multiples deteccions de colisió
                StartCoroutine(HitCooldown());
            }
            
            if (pizzasLeft == 0)
            {
                Debug.Log("Has mort");
                pizzasLeft = 3; //Reinicialitzem les pizzes
            }
        }
    }
    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        isHit = false;
    }
    private void Motor()
    {
        //Multipliquem 
        backWheel.motorTorque = _vInput * horsePower;
        currentbrakeForce = brake ? brakeForce : 0f;
        if (brake)
        {
            ApplyBrake();
        }
        else
        {
            ReleaseBrake();
        }
    }


    private void ApplyBrake()
    {
        frontWheel.brakeTorque = currentbrakeForce;
        backWheel.brakeTorque = currentbrakeForce;
    }

    private void ReleaseBrake()
    {
        frontWheel.brakeTorque = 0;
        backWheel.brakeTorque = 0;
    }
    public void DownPresureOnSpeed()
    {
        Vector3 downforce = Vector3.down;
        float downpressure;
        if (rb.velocity.magnitude > 5)
        {
            downpressure = rb.velocity.magnitude;
            rb.AddForce(downforce * downpressure, ForceMode.Force);
        }

    }
    public void SpeedSteerinReductor()
    {
        if (rb.velocity.magnitude < 5) //We set the limiting factor for the steering thus allowing how much steer we give to the player in relation to the speed
        {
            maxSteeringAngle = Mathf.LerpAngle(maxSteeringAngle, 50, speedteercontrolTime);
        }
        if (rb.velocity.magnitude > 5 && rb.velocity.magnitude < 10)
        {
            maxSteeringAngle = Mathf.LerpAngle(maxSteeringAngle, 30, speedteercontrolTime);
        }
        if (rb.velocity.magnitude > 10 && rb.velocity.magnitude < 15)
        {
            maxSteeringAngle = Mathf.LerpAngle(maxSteeringAngle, 15, speedteercontrolTime);
        }
        if (rb.velocity.magnitude > 15 && rb.velocity.magnitude < 20)
        {
            maxSteeringAngle = Mathf.LerpAngle(maxSteeringAngle, 10, speedteercontrolTime);
        }
        if (rb.velocity.magnitude > 20)
        {
            maxSteeringAngle = Mathf.LerpAngle(maxSteeringAngle, 5, speedteercontrolTime);
        }
    }
    private void Steering()
    {
        SpeedSteerinReductor();
        currentSteeringAngle = Mathf.Lerp(currentSteeringAngle, maxSteeringAngle * _hInput, turnSmoothing);
        frontWheel.steerAngle = currentSteeringAngle;
        
        //We set the target laying angle to the + or - input value of our steering 
        //We invert our input for rotating in the ocrrect axis
        targetlayingAngle = maxlayingAngle * -_hInput;
    }
    private void LayOnTurn()
    {
        Vector3 currentRot = transform.rotation.eulerAngles;

        if (rb.velocity.magnitude < 1)
        {
            layingammount = Mathf.LerpAngle(layingammount, 0f, 0.05f);
            transform.rotation = Quaternion.Euler(currentRot.x, currentRot.y, layingammount);
            return;
        }

        if (currentSteeringAngle < 0.5f && currentSteeringAngle > -0.5) //We're stright
        {
            layingammount = Mathf.LerpAngle(layingammount, 0f, leanSmoothing);
        }
        else //We're turning
        {
            layingammount = Mathf.LerpAngle(layingammount, targetlayingAngle, leanSmoothing);
            rb.centerOfMass = new Vector3(rb.centerOfMass.x, COG.y, rb.centerOfMass.z);
        }

        transform.rotation = Quaternion.Euler(currentRot.x, currentRot.y, layingammount);
    }

    public void UpdateWheels()
    {
        UpdateSingleWheel(frontWheel, frontWheelTransform);
        UpdateSingleWheel(backWheel, backWheelTransform);
    }
    public void UpdateManillar()
    {
        Quaternion sethandleRot;
        sethandleRot = frontWheelTransform.rotation;
        manillar.localRotation = Quaternion.Euler(manillar.localRotation.eulerAngles.x, currentSteeringAngle, manillar.localRotation.eulerAngles.z);
    }   
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
