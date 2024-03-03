using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public delegate void InventoryAction ();
    public event InventoryAction OnModifyInventory;

    [SerializeField] private List<string> items = new();

    [SerializeField] private int maxCapacity;

    public bool AddItem (string item)
    {
        if (items.Count <= maxCapacity)
        {
            items.Add(item);
            OnModifyInventory?.Invoke();
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool CheckItem (string item)
    {
        return items.Contains(item);
    }

    public bool RemoveItem(string item)
    {
        bool result = items.Remove(item);
        OnModifyInventory?.Invoke();
        return result;
    }

    public IEnumerable<string> GetItems ()
    {
        return items.ToArray();
    }

    public bool CheckCapacity (int countToAdd)
    {
        return items.Count + countToAdd <= maxCapacity;
    }
}
