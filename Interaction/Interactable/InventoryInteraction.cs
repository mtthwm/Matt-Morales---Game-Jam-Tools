using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryInteraction : BaseInteraction
{
    [SerializeField] private InventoryManager inventoryManager;

    public delegate void InventoryHoverAction (string[] items);
    public static event InventoryHoverAction OnHoverEnter;
    public static event InventoryHoverAction OnHoverExit;

    private enum InventoryModificationMode
    {
        Add,
        Remove
    }

    [SerializeField] private InventoryModificationMode mode;
    [SerializeField] private string[] items;

    protected override void Action()
    {
        if (mode == InventoryModificationMode.Add)
        {
            foreach (string item in items)
            {
                inventoryManager.AddItem(item);
            }
        }
        else
        {
            foreach (string item in items)
            {
                inventoryManager.RemoveItem(item);
            }
        }
    }

    protected override void HoverEnter ()
    {
        OnHoverEnter?.Invoke(items);
    }

    protected override void HoverExit ()
    {
        OnHoverExit?.Invoke(items);
    }

    public string[] GetItems ()
    {
        return items.ToArray();
    }
}
