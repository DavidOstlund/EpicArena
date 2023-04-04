using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This script acts as a single point for all other scripts to get
// the current input from. It uses Unity's new Input System and
// functions should be mapped to their corresponding controls
// using a PlayerInput component with Unity Events.

[RequireComponent(typeof(PlayerInput))]
public class PlayerInput : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.zero;
    private bool interactPressed = false;
    private bool submitPressed = false;
    private bool attackPressed = false;
    [SerializeField] private Player player;

    private static PlayerInput instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;
    }

    public static PlayerInput GetInstance() 
    {
        return instance;
    }

    public void MovePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player.Move(context.ReadValue<Vector2>());
        }
        else if (context.canceled)
        {
            player.StopMoving();
        } 
    }

    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player.interacting();
            interactPressed = true;
        }
        else if (context.canceled)
        {
            interactPressed = false;
        } 
    }

    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        } 
    }

    public Vector2 GetMoveDirection() 
    {
        return moveDirection;
    }

    public void AttackButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player.TryToAttack(Mouse.current.position.ReadValue());
            attackPressed = true;
        }
        else if (context.canceled)
        {
            attackPressed = false;
        } 
    }

    // for any of the below 'Get' methods, if we're getting it then we're also using it,
    // which means we should set it to false so that it can't be used again until actually
    // pressed again.

    public bool GetInteractPressed() 
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }

    public bool GetSubmitPressed() 
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }

    public void RegisterSubmitPressed() 
    {
        submitPressed = false;
    }

    public bool GetAttackPressed() 
    {
        bool result = attackPressed;
        attackPressed = false;
        return result;
    }

}