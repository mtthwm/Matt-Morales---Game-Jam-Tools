using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryResolver : MonoBehaviour
{
    [SerializeField] private string inventoryResourcePath = "Inventory";

    InventoryItem[] _inventoryItems = null;

    public static InventoryResolver Instance;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            _inventoryItems = Resources.LoadAll<InventoryItem>(inventoryResourcePath);
        }
    }



    public InventoryItem Resolve (string name)
    {
        Debug.Log(name);
        for (int i = 0; i < _inventoryItems.Length; i++)
        {
            if (_inventoryItems[i].name == name)
            {
                return _inventoryItems[i];
            }
        }

        return null;
    }
}
