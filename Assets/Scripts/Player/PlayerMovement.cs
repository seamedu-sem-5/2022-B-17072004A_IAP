using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float jumpForce;
    float currentSpeed;
    float currentJumpForce;


    [Header("Combat")]
    public bool isAttackRequested;
    public bool isDefendRequested;
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask enemyLayer;
    Gamepad pad;
    public int damage;
    private HealthManager healthManager;

    [Header("Physics")]
    [SerializeField] protected bool useGravity;
    [SerializeField] protected float gravityScale;

    public Vector2 movementInput;
    [SerializeField] protected Rigidbody2D rb;
    public bool isGrounded;
    [SerializeField] protected bool isJumpRequested;
    public CameraShake camShake;

    [Header("GroundCheck")]
    [SerializeField] protected Transform groundCheckTransform;
    [SerializeField] protected float groundCheckRadius;
    [SerializeField] protected LayerMask groundLayer;


    [Header("Inputs")]
    private PlayerControls controls;

    [Header("Sprites")]
    [SerializeField] protected SpriteRenderer spriteRenderer;
    private HashSet<Collider2D> detectedEnemies = new HashSet<Collider2D>();
    private Slider healthSlider;
    private void OnEnable()
    {
        controls = new PlayerControls();
        controls.Player.Move.performed += OnMove;
        controls.Player.Jump.performed += OnJump;
        controls.Player.Attack.performed += OnAttack;
        controls.Player.Defend.performed += OnDefend;
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Move.canceled -= OnMove;
        controls.Player.Jump.canceled -= OnJump;
        controls.Player.Attack.canceled -= OnAttack;
        controls.Player.Defend.canceled -= OnDefend;
        controls.Player.Disable();
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        camShake = GameObject.Find("MainCamera").GetComponent<CameraShake>();
        pad = Gamepad.current;
        healthSlider = gameObject.GetComponentInChildren<Slider>();
        healthManager = GetComponent<HealthManager>();
        currentSpeed = moveSpeed;
        currentJumpForce = jumpForce;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed && isGrounded)
        {
            isJumpRequested = true;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed && isGrounded)
        {
            isAttackRequested = true;
        }
    }

    public void OnDefend(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isDefendRequested = true;
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.JoystickButton4))
        {
            isDefendRequested = false;
        }

        if(isDefendRequested)
        {
            moveSpeed = 0f;
            jumpForce = 0f;
        }
        else
        {
            moveSpeed = currentSpeed;
            jumpForce = currentJumpForce;
        }
        
    }

    private void FixedUpdate()
    {
            Move();
            CheckGround();
            if(isJumpRequested)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x,jumpForce);
                isJumpRequested = false;
            }

            if(!isGrounded && useGravity)
            {
                rb.linearVelocity += Vector2.down * gravityScale * Time.fixedDeltaTime;
            }
            if(isAttackRequested)
            {
                //GamepadVibrate(0.25f, 0.75f, 0.2f);
                camShake.StartShake();
                Attack();
                isAttackRequested = false;
            }
            //if (!isAttackRequested)
            //{
            //    pad.SetMotorSpeeds(0f, 0f);
            //}

    }

    void Attack()
    {
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
       
        foreach(Collider2D enemy in hitEnemy)
        {
            if(!detectedEnemies.Contains(enemy))
            {
                detectedEnemies.Add(enemy);
            }
            else if(detectedEnemies.Contains(enemy) && !enemy.GetComponent<PlayerMovement>().isDefendRequested)
            {
                Debug.Log(enemy.name);
                enemy.GetComponent<HealthManager>().TakeDamage(damage);
            }
            else if(enemy.GetComponent<PlayerMovement>().isDefendRequested)
            {
                enemy.GetComponent<HealthManager>().TakeDamage(0);
            }
        }

    }

    public void GamepadVibrate(float lowFrequency,float highFrequency,float duration)
    {

        if(pad != null)
        {
            pad.SetMotorSpeeds(lowFrequency,highFrequency);
            Invoke(nameof(StopGamepadVibration),duration);
        }
    }

    public void StopGamepadVibration()
    {
        isAttackRequested = false;
        pad.SetMotorSpeeds(0f,0f);
    }

    void Move()
    {
        Vector2 targetVelocity = new Vector2(movementInput.x * moveSpeed * Time.fixedDeltaTime * 10f, rb.linearVelocity.y);
        rb.linearVelocity = targetVelocity;

        if(movementInput.x > 0)
        {
            transform.localScale = new Vector2(1f, 1f);
            healthSlider.transform.localScale = new Vector2(0.01f, 0.01f);
        }
        else if(movementInput.x < 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
            healthSlider.transform.localScale = new Vector2(-0.01f, 0.01f);
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position,groundCheckRadius,groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if(groundCheckTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(groundCheckTransform.position,groundCheckRadius);
        }
        if(attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(attackPoint.position,attackRadius);
        }
    }
}
