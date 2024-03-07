using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private InventoryView inventoryView;
    [SerializeField] private Mode mode;
    [SerializeField] public string Item;
     
    private enum Mode
    {
        Move,
        Add,
        Remove
    }

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(InventoryAction);
    }

    private void InventoryAction ()
    {
        if (Item.Equals(""))
        {
            return;
        }

        switch (mode)
        {
            case Mode.Move:
                inventoryView.sourceInventoryManager.MoveItem(inventoryView.targetInventoryManager, Item);
                break;
            case Mode.Add:
                inventoryView.sourceInventoryManager.AddItem(Item);
                break;
            case Mode.Remove:
                inventoryView.sourceInventoryManager.RemoveItem(Item);
                break;
        }
    }
}
