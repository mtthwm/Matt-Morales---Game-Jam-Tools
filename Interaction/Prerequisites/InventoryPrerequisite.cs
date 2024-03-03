using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPrerequisite : BasePrerequisite
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private string[] inventoryItems;
    [SerializeField] private bool remove;
    [SerializeField] private bool oneTime;

    private bool m_hasBeenSatisfied;

    public override bool Check()
    {
        if (oneTime && m_hasBeenSatisfied)
        {
            return true;
        }

        bool result = true;
        foreach (string item in inventoryItems)
        {
            if (!inventoryManager.CheckItem(item))
            {
                result = false;
                break;
            }
        }

        if (!result)
        {
            return false;
        }
        else
        {
            foreach (string item in inventoryItems)
            {
                inventoryManager.RemoveItem(item);
            }
            m_hasBeenSatisfied = true;
            return true;
        }
    }
}
