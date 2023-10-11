using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMetrics _playerMetrics;
    [SerializeField] private float maxSpeed = 1f;
    [SerializeField] private float maxFallingSpeed = -15f;

    [SerializeField] private float maxAcceleration = 1f;
    [SerializeField] private float maxDeceleration = 1f;
    [SerializeField] private float maxAirAcceleration;
    [SerializeField] private float maxAirDeceleration;
    [SerializeField] private float maxTurnSpeed;
    [SerializeField] private float maxAirTurnSpeed;
    [SerializeField] private float friction;
    private string accelUsed = ""; // DONT USE (Just for debug)
    private Vector2 desiredVelocity;
    private float maxSpeedChange;

    private Rigidbody2D rb;
    private PlayerJump playerJump;
    private bool turning; //DONT USE (Just for debug )

    public string MovementDebugInfo
    {
        get =>
            $" Movenment info \n " +
            $"\n" +
            $" Desired Horizontal Velocity: {desiredVelocity.x} \n " +
            $" Velocity: {rb.velocity.x} \n " +
            $" Horizontal Acceleration: {maxSpeedChange / Time.deltaTime} \n " +
            $" Horizontal AccelUsed: {accelUsed} \n " +
            $" turning? {turning}"; // 
        set => throw new NotImplementedException();
    }

    public float MaxSpeed
    {
        get => maxSpeed;
        set => maxSpeed = value;
    }

    public float MaxAcceleration
    {
        get => maxAcceleration;
        set => maxAcceleration = value;
    }

    public float MaxDeceleration
    {
        get => maxDeceleration;
        set => maxDeceleration = value;
    }
    public float TurnSpeed
    {
        get => maxTurnSpeed;
        set => maxTurnSpeed = value;
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerJump = GetComponent<PlayerJump>();
        //LoadData();
    }


    private void Update()
    {
        desiredVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * maxSpeed, 0f);
    }

    private void FixedUpdate()
    {
        bool onGround = playerJump.OnGround;

        float acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        float deceleration = onGround ? maxDeceleration : maxAirDeceleration;
        float turnSpeed = onGround ? maxTurnSpeed : maxAirTurnSpeed;
        
        turning = false;
        if (desiredVelocity.x != 0)
        {
            if (Mathf.Sign(desiredVelocity.x) != Mathf.Sign(rb.velocity.x) && rb.velocity.x != 0)
            {
                turning = true;
                accelUsed = "Turn Accel";
                maxSpeedChange = turnSpeed * Time.fixedDeltaTime;
                //Debug.Log("horizontal y rb velocity distintos signos: Turn speed aplicado");
            }
            else
            {
                maxSpeedChange = acceleration * Time.fixedDeltaTime;
                accelUsed = "maxAccel";
                //Debug.Log("horizontal y rb velocity mismo signo: aceleration aplicada");
            }
        }
        else
        {
            if (rb.velocity.x != 0)
            {
                maxSpeedChange = deceleration * Time.fixedDeltaTime;
                accelUsed = "maxDeAccel";
                //Debug.Log("Desacelerando, ninguna tecla presionada");
            }
            else
            {
                accelUsed = "No one";
                maxSpeedChange = 0;
                //Debug.Log("Idle");
                //return;
            }
        }

       
        Vector2 velocity = new(Mathf.MoveTowards(rb.velocity.x, desiredVelocity.x, maxSpeedChange), rb.velocity.y < maxFallingSpeed ? maxFallingSpeed: rb.velocity.y);
        rb.velocity = new Vector2(velocity.x, velocity.y);
    }

    public void LoadData()
    {
        maxSpeed = _playerMetrics.maxSpeed;
        maxAcceleration = _playerMetrics.maxAcceleration;
        maxDeceleration = _playerMetrics.maxDeceleration;
        maxTurnSpeed = _playerMetrics.turnSpeed;
    }
    
    
}