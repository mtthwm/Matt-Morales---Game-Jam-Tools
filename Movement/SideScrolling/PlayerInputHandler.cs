using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] SideScrollingMovementController controller;
    [SerializeField] CollisionInteractionDriver interactionDriver;
    [SerializeField] InventoryView[] inventoryViews;

    private bool _inventoryOpen = false;

    public void Move (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.Move(context.ReadValue<Vector2>().x);
        }
        else if (context.canceled)
        {
            controller.Stop();
        }
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
