using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 direction;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public float speed = 8, jumpForce = 10, climbSpeed = 100, gravity = -20;
    int climbState, jumpState, attackState;
    float runState, hInput;
    public static float playerPositionX;
    public static bool atTheDoor;
    public bool ableToMakeADoubleJump = true, isGrounded, ableToMakeClimb;
   
    public Animator animator;
    public Transform playerModel;
    [SerializeField] GameObject climbButton, jumpButton, attackButton;


    private void Start()
    {
        atTheDoor = false;
    }
    void Update()
    {
        gravity = -20 + (climbState * 20);

        if (PlayerManager.gameOver)
        {
            animator.SetTrigger("isDead");
            this.enabled = false;
        }

        Run();
        direction.x = hInput * speed;

        animator.SetFloat("speed", Mathf.Abs(hInput));

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded)
        {
            ableToMakeADoubleJump = true;
            if (Input.GetButtonDown("Jump") || jumpState == 1)
            {
                JumpDecision(0);
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.F) || attackState == 1)
            {
                AttackDecision(0);
                animator.SetTrigger("fireBallAttack");
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
            if (ableToMakeADoubleJump & (Input.GetButtonDown("Jump") || jumpState == 1))
            {
                DoubleJump();
            }
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fireball Attack"))
            return;


        if (hInput != 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(hInput, 0, 0));
            playerModel.rotation = newRotation;
        }

        if (ableToMakeClimb)
        {
            climbButton.SetActive(true);
            animator.SetBool("isClimbing", true);
            direction.y = climbState * climbSpeed * Time.deltaTime;
        }
        else
        {
            climbButton.SetActive(false);
            ClimbLadderDecision(0);
            animator.SetBool("isClimbing", false);
        }

        controller.Move(direction * Time.deltaTime);
    }

    private void Run()
    {
        

        if (runState == 1 || runState == -1)
        {
            hInput = runState;
        }
        else
        {
            hInput = Input.GetAxis("Horizontal");

        }
    }

    private void DoubleJump()
    {
        animator.SetTrigger("doubleJump");
        direction.y = jumpForce;
        ableToMakeADoubleJump = false;
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    public void ClimbLadderDecision(int climbState)
    {
        this.climbState = climbState;
    }

    public void JumpDecision (int jumpState)
    {
        this.jumpState = jumpState;
    }

    public void AttackDecision(int attackState)
    {
        this.attackState = attackState;
    }

    public void RunDecision(int runState)
    {
        this.runState = runState;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyCastle")
            atTheDoor = true;

        if (other.gameObject.tag == "DeathPoint")
        { 
            PlayerManager.reducingHealth = true;
            animator.SetTrigger("isFalling");
            playerPositionX = controller.transform.position.x;
        }

        if (other.gameObject.tag == "Ladder")
        {
            jumpButton.SetActive(false);
            attackButton.SetActive(false);

            ableToMakeClimb = true;
            animator.SetTrigger("isClimbing0");
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "EnemyCastle")
            atTheDoor = true;

        if (other.gameObject.tag == "Ladder")
            ableToMakeClimb = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyCastle")
            atTheDoor = false;

        if (other.gameObject.tag == "Ladder")
        { 
            ableToMakeClimb = false;

            jumpButton.SetActive(true);
            attackButton.SetActive(true);
        }
    }
}
