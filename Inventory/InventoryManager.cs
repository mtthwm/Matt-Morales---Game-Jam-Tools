using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public delegate void InventoryAction ();
    public event InventoryAction OnModifyInventory;

    [SerializeField] private List<string> items = new();

    [SerializeField] private int maxCapacity;

    public bool AddItem (string item)
    {
        if (items.Count <= maxCapacity && !item.Equals(""))
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

    public bool MoveItem (InventoryManager dest, string item)
    {
        if (!CheckItem(item) || !dest.CheckCapacity(1))
        {
            return false;
        }

        RemoveItem(item);
        return dest.AddItem(item);
    }

    public string ItemAt (int i)
    {
        return items[i];
    }

    public bool CheckItem (string item)
    {
        return items.Contains(item);
    }

    public bool RemoveItem(string item)
    {
        if (!CheckItem(item))
        {
            return false;
        }

        bool result = items.Remove(item);
        OnModifyInventory?.Invoke();
        return result;
    }

    public string[] GetItems ()
    {
        return items.ToArray();
    }

    public bool CheckCapacity (int countToAdd)
    {
        return items.Count + countToAdd <= maxCapacity;
    }
}
