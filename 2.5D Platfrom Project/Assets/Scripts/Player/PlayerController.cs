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
    int climbState;
    public static float playerPositionX;
    public static bool atTheDoor;
    public bool ableToMakeADoubleJump = true, isGrounded, ableToMakeClimb;
   
    public Animator animator;
    public Transform playerModel;
    public GameObject climbButton;


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
        float hInput = Input.GetAxis("Horizontal");
        direction.x = hInput * speed;

        animator.SetFloat("speed", Mathf.Abs(hInput));

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded)
        {
            ableToMakeADoubleJump = true;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                animator.SetTrigger("fireBallAttack");
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
            if (ableToMakeADoubleJump & Input.GetButtonDown("Jump"))
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
            ableToMakeClimb = false;   
    }
}
