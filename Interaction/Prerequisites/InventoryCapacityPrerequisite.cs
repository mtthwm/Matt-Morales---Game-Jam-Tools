using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCapacityPrerequisite : BasePrerequisite
{
    [SerializeField] private InventoryManager inventoryManager;
    private InventoryInteraction _inventoryInteraction;

    private void Start()
    {
        _inventoryInteraction = GetComponent<InventoryInteraction>();
    }

    public override bool Check()
    {
        return inventoryManager.CheckCapacity(_inventoryInteraction.GetItems().Length);
    }
}
