using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Animator animator;
    protected PlayerMovement playerMovement;
    protected HealthManager healthManager;
    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        healthManager = GetComponent<HealthManager>();
    }

    private void Update()
    {
        HandleJumpAnimation();
        HandleMoveAnimation();
        HandleAttackAnimation();
        if(healthManager.currentHealth <= 0)
        {
            animator.SetTrigger("Death");
            Destroy(this.gameObject,1.7f);
        }
    }

    private void FixedUpdate()
    {
        HandleDefendAnimation();
    }
    void HandleDefendAnimation()
    {
        if(playerMovement.isDefendRequested)
        {
            animator.SetBool("Defend",true);
        }
        else if(!playerMovement.isDefendRequested)
        {
            animator.SetBool("Defend",false);
        }
    }

    void HandleMoveAnimation()
    {
        if(playerMovement.movementInput.x == 0)
        {
            animator.SetFloat("Speed", 0f);
        }
        else if(playerMovement.movementInput.x > 0 || playerMovement.movementInput.x < 0)
        {
            animator.SetFloat("Speed", 0.5f);
        }
    }
    void HandleJumpAnimation()
    {
        if(playerMovement.isGrounded)
        {
            animator.SetBool("IsGrounded", true);
        }
        else if(!playerMovement.isGrounded) 
        {
            animator.SetBool("IsGrounded", false);
        }
    }

    void HandleAttackAnimation()
    {
        if(playerMovement.isAttackRequested)
        {
            animator.SetTrigger("Attack");
        }

        if(!playerMovement.isAttackRequested)
        {
            animator.ResetTrigger("Attack");
        }
    }

}
