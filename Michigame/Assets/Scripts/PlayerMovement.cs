using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 1f;
    [SerializeField] private float maxAcceleration = 1f;
    [SerializeField] private float maxDeccaleration = 1f;
    [SerializeField] private float friction;
    [SerializeField] private float turnSpeed;
    private string accelUsed = ""; // DONT USE (Just for debug)
    private Vector2 desiredVelocity;
    private float maxSpeedChange;

    private Rigidbody2D rb;
    private bool turning; //DONT USE (Just for debug )

    public string PlayerStats =>
        $"  Desired Velocity: {desiredVelocity.x} \n " +
        $" Velocity: {rb.velocity.x} \n " +
        $" Acceleration: {maxSpeedChange / Time.deltaTime} \n " +
        $" AccelUsed: {accelUsed} \n " +
        $" turning? {turning}"; // 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        desiredVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * maxSpeed, 0f);
    }

    private void FixedUpdate()
    {
        turning = false;
        if (desiredVelocity.x != 0)
        {
            if (Mathf.Sign(desiredVelocity.x) != Mathf.Sign(rb.velocity.x) && rb.velocity.x != 0)
            {
                turning = true;
                accelUsed = "Turn Accel";
                maxSpeedChange = turnSpeed * Time.fixedDeltaTime;
                Debug.Log("horizontal y rb velocity distintos signos: Turn speed aplicado");
            }
            else
            {
                maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;
                accelUsed = "maxAccel";
                Debug.Log("horizontal y rb velocity mismo signo: aceleration aplicada");
            }
        }
        else
        {
            if (rb.velocity.x != 0)
            {
                maxSpeedChange = maxDeccaleration * Time.fixedDeltaTime;
                accelUsed = "maxDeAccel";
                Debug.Log("Desacelerando, ninguna tecla presionada");
            }
            else
            {
                accelUsed = "No one";
                maxSpeedChange = 0;
                Debug.Log("Idle");
                return;
            }
        }

        Vector2 velocity = new(Mathf.MoveTowards(rb.velocity.x, desiredVelocity.x, maxSpeedChange), 0f);
        rb.velocity = new Vector2(velocity.x, 0);
    }
}