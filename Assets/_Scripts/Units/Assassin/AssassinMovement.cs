using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssassinMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveDirection;
    private bool isFacingLeft;
    public bool isOnMenu = false; 
    private bool isOnPauseMenu;

    [Header("Stats")]
    [SerializeField] float moveSpeed = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void PauseMenu(bool onPauseMenu)
    {
        isOnPauseMenu = onPauseMenu;
    }

    private void Update()
    {
        if (!isOnMenu && !isOnPauseMenu)
        {
            moveDirection = Vector2.zero;

            // Use the new Input System controls for movement
            moveDirection += Keyboard.current.wKey.isPressed ? Vector2.up : Vector2.zero;
            moveDirection += Keyboard.current.sKey.isPressed ? Vector2.down : Vector2.zero;
            moveDirection += Keyboard.current.dKey.isPressed ? Vector2.right : Vector2.zero;
            moveDirection += Keyboard.current.aKey.isPressed ? Vector2.left : Vector2.zero;

            // Running Logic
            if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
            {
                moveSpeed *= 2f;
                animator.SetBool("isRunning", true);
            }
            if (Keyboard.current.leftShiftKey.wasReleasedThisFrame)
            {
                moveSpeed /= 2f;
                animator.SetBool("isRunning", false);
            }

            moveDirection.Normalize();

            // Animations
            animator.SetBool("isWalking", moveDirection != Vector2.zero);

            // Handling if needs to rotate player left/right
            if (moveDirection.x < 0f && !isFacingLeft)
            {
                isFacingLeft = true;
                FlipPlayer();
            }
            else if (moveDirection.x > 0f && isFacingLeft)
            {
                isFacingLeft = false;
                FlipPlayer();
            }
        }
    }

    private void FlipPlayer()
    {
        // Rotate around y axis
        transform.Rotate(0f, 180f, 0f);
    }

    private void FixedUpdate()
    {
        if (!isOnMenu)
            rb.velocity = moveDirection * moveSpeed;
    }
}
