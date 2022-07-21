using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class ArrowMovement : MonoBehaviour
{
    private Driver driver;
    private bool isPressed;
    private float verticalAmount = 1f;
    private float horizontalAmount = 1f;
    void Start()
    {
        driver = FindObjectOfType<Driver>();
    }
    
    
    public void MoveForward()
    {
        driver.MoveVertical(verticalAmount);
    }
    
    public void MoveBackwards()
    {
        driver.MoveVertical(-verticalAmount);
    }

    public void StopVerticalMovement()
    {
        driver.MoveVertical(0f);
    }
    public void RotateLeft()
    {
        driver.MoveHorizontal(-horizontalAmount);
    }
    
    public void RotateRight()
    {
        driver.MoveHorizontal(horizontalAmount);
    }

    public void StopHorizontalMovement()
    {
        driver.MoveHorizontal(0f);
    }
}
