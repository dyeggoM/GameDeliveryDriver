using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Driver : MonoBehaviour
{
    [SerializeField] private float normalMoveSpeed = 10f;
    [SerializeField] private float reverseMultiplierSpeed = 0.8f;
    [SerializeField] private float steerSpeed = 90f;
    [SerializeField] private float sandSpeed = 7f;
    [SerializeField] private float waterSpeed = 5f;
    // [SerializeField] private float crashSpeed = 8f;
    [SerializeField] private float boostSpeed = 15f;
    
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Joystick verticalJoystick;
    [SerializeField] private Joystick horizontalJoystick;
    [SerializeField] private int crashesCountToResetSpeed = 10;
    [SerializeField] private bool isStandingRotationAllowed = true;

    private bool isMoving = false;
    private bool reverse = false;
    private float verticalMovement = 0f;
    private float horizontalMovement = 0f;
    private int currentHitCounter = 0;

    private bool isMovingForward = false;
    private bool isMovingBackwards = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private float arrowMovement = 1f;
    
    void Start()
    {
        
    }

    void Update()
    {
        ProcessRotation();
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        ProcessVerticalMovement();
        float moveAmount = verticalMovement * moveSpeed * Time.deltaTime;
        if (moveAmount < 0f)
        {
            moveAmount *= reverseMultiplierSpeed;
            reverse = true;
        }
        else
        {
            reverse = false;
        }
        transform.Translate(0f,moveAmount,0f);
        if (moveAmount > 0.001f || moveAmount < -0.001f)
            isMoving = true;
        else
            isMoving = false;
    }

    void ProcessVerticalMovement()
    {
        verticalMovement = Input.GetAxis("Vertical");
        if (verticalJoystick.Vertical != 0f)
        {
            verticalMovement = verticalJoystick.Vertical;
        }
        if (isMovingForward)
        {
            verticalMovement = arrowMovement;
        }
        if (isMovingBackwards)
        {
            verticalMovement = -arrowMovement;
        }
    }

    private void ProcessRotation()
    {
        ProcessHorizontalMovement();
        float reverseSteering = reverse ? 1 : -1;
        float steerAmount = horizontalMovement * steerSpeed * Time.deltaTime * reverseSteering;
        if(isMoving || isStandingRotationAllowed)
            transform.Rotate(0f,0f, steerAmount);
    }

    private void ProcessHorizontalMovement()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        if (horizontalJoystick.Horizontal != 0f)
        {
            horizontalMovement = horizontalJoystick.Horizontal;
        }
        if (isRotatingLeft)
        {
            horizontalMovement = -arrowMovement;
        }
        if (isRotatingRight)
        {
            horizontalMovement = arrowMovement;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boost"))
        {
            currentHitCounter = 0;
            moveSpeed = boostSpeed;
        }
        if (other.CompareTag("Water"))
        {
            moveSpeed = waterSpeed;
        }
        if (other.CompareTag("Sand"))
        {
            moveSpeed = sandSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sand"))
        {
            moveSpeed = normalMoveSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Untagged"))
        {
            // moveSpeed = crashSpeed;
            // normalMoveSpeed = crashSpeed;
            currentHitCounter++;
            if (currentHitCounter >= crashesCountToResetSpeed)
            {
                moveSpeed = normalMoveSpeed;
            }
        }
    }

    public void MoveVertical(float movementAmount)
    {
        if (movementAmount > 0.1f)
        {
            isMovingForward = true;
        }
        else if (movementAmount < -0.1f)
        {
            isMovingBackwards = true;
        }
        else
        {
            isMovingForward = false;
            isMovingBackwards = false;
        }
    }
    public void MoveHorizontal(float movementAmount)
    {
        if (movementAmount > 0.1f)
        {
            isRotatingRight = true;
        }
        else if (movementAmount < -0.1f)
        {
            isRotatingLeft = true;
        }
        else
        {
            isRotatingLeft = false;
            isRotatingRight = false;
        }
    }
}
