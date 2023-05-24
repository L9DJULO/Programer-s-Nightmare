using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Windows.Input;
using Photon.Pun;

public class playermovsolo : MonoBehaviour
{
    float playerHeight = 2f;
    private Animator animator;
    [SerializeField] Transform orientation;
    [Header("JumpLimit")]
    public int jumpLimit = 1;
    [Header("Movement")]
    float moveSpeed = 3f;
    float movementMultiplier = 20f;
    [SerializeField] float airMultiplier = 0.4f;
    [Header("Jumping")]
    public float jumpforce = 25f;
    public static bool canDouble = false;
    [Header("Drag")]
    float groundDrag = 6f;
    public static float airDrag = 3f;
    Rigidbody rb;
    Vector3 moveDirection;

    [Header("Sprinting")]
    public static bool IsSprinting;
    public float SprintMultiplier = 1.5f;
    [Header("GroundDetection")]
    [SerializeField] LayerMask groundMask;
    public static bool isGrounded = true;
    public bool islava = false;
    float groundDistance = 0.0001f;
    private bool nothing;

    float verticalMovement;
    float horizontalMovement;
    private RaycastHit slopeHit;
    Vector3 slopeMoveDirection;
    Vector3 oldPosition;
    public LayerMask Lava;
    public Camera cam;

    public void Awake()
    {
        PhotonView photonView = GetComponent<PhotonView>();
        if (!photonView || photonView.IsMine)
        {
            cam.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        oldPosition = rb.position;
        visible = false;
        countJump = 0;
    }

    private bool visible;
    private int countJump;

    private void Update()
    {
        animator.SetBool("Running", IsSprinting);
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                animator.SetBool("Front", true);
                animator.SetBool("Back", false);
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                animator.SetBool("Back", true);
                animator.SetBool("Front", false);
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                animator.SetBool("Back", false);
                animator.SetBool("Front", false);
                animator.SetBool("Left", true);
                animator.SetBool("Right", false);
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                animator.SetBool("Back", false);
                animator.SetBool("Front", false);
                animator.SetBool("Left", false);
                animator.SetBool("Right", true);
            }
            else
            {
                animator.SetBool("Back", false);
                animator.SetBool("Front", false);
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
            }
        }
        animator.SetBool("grounded", isGrounded);

            if (islava)
            {
                rb.position = oldPosition;
            }
            if (isGrounded)
            {
                if (countJump == 1)
                {
                    countJump = 0;
                }
                else if (countJump == 2)
                {
                    countJump = 0;
                }
            }
            Myinput();
            ControlDrag();
            if (Input.GetKeyDown(INPUTS.Jump))
            {
                if (isGrounded)
                {
                    countJump++;
                    Jump();
                    isGrounded = false;
                    canDouble = true;
                }
                else if (PowerUpDoubleJump.canDoubleJump && canDouble)
                {
                    countJump++;
                    Jump();
                    isGrounded = false;
                    canDouble = false;
                }
            }
            slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
            if (Input.GetKeyDown(INPUTS.sprint))
                Sprint();
            if (Input.GetKeyUp(INPUTS.sprint))
            {
                StopSprint();
            }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        if (collision.gameObject.tag == "LAVA")
        {
            islava = true;
        }
    }
    void Jump()
    {
        rb.AddForce(transform.up * jumpforce, ForceMode.Impulse);
    }

    private void Myinput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }
    void ControlDrag()
    {
        if (isGrounded)
            rb.drag = groundDrag;
        else rb.drag = airDrag;
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        if (isGrounded && !OnSlop())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlop())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier * movementMultiplier, ForceMode.Acceleration);
        }

    }

    private bool OnSlop()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
                return true;
            else
            {
                return false;
            }
        }
        return false;

    }
    void Sprint()
    {
        movementMultiplier *= SprintMultiplier;
        IsSprinting = true;
    }

    void StopSprint()
    {
        movementMultiplier = 20f;
        IsSprinting = false;
    }


}
