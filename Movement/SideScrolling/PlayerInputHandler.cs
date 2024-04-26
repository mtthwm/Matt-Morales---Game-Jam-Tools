using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] SideScrollingMovementController controller;
    [SerializeField] CollisionInteractionDriver interactionDriver;
    [SerializeField] InventoryView[] inventoryViews;

    [SerializeField][Range(-1, 1)] private int directionMultiplier = 1;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private string walkingParamName = "walking";


    private bool _inventoryOpen = false;

    public void Move (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (spriteRenderer != null)
            {
                if (GetXMovement(context) > 0)
                {
                    spriteRenderer.flipX = true;
                } else if (GetXMovement(context) < 0)
                {
                    spriteRenderer.flipX = false;
                }
            }
            if (animator != null)
            {
                animator.SetBool(walkingParamName, true);
            }
            controller.Move(GetXMovement(context));
        }
        else if (context.canceled)
        {
            if (animator != null)
            {
                animator.SetBool(walkingParamName, false);
            }
            controller.Stop();
        }
    }

    private float GetXMovement (InputAction.CallbackContext context)
    {
        return context.ReadValue<Vector2>().x * directionMultiplier;
    }

    public void Interact (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.Stop();
            interactionDriver.Interact();
        }
    }
    public void ToggleInventory (InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            if (_inventoryOpen)
            {
                foreach (InventoryView view in inventoryViews)
                {
                    view.Hide();
                }
            } else
            {
                foreach (InventoryView view in inventoryViews)
                {
                    view.Show();
                }
            }

            _inventoryOpen = !_inventoryOpen;
        }
    }
}
