using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocationInventoryDisplay : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Transform[] locations;
    [SerializeField] private Transform container;

    private void OnEnable()
    {
        inventoryManager.OnModifyInventory += UpdateDisplay;
    }

    private void OnDisable()
    {
        inventoryManager.OnModifyInventory -= UpdateDisplay;
    }

    private void UpdateDisplay ()
    {
        ClearContainer();
        string[] items = inventoryManager.GetItems().ToArray();
        for (int i = 0; i < items.Length; i++)
        {
            GenerateSpriteObject(items[i], locations[i]);
            i++;
        }
    }

    private GameObject GenerateSpriteObject (string itemName, Transform t)
    {
        InventoryItem item = InventoryResolver.Instance.Resolve(itemName);
        GameObject obj = new GameObject(item.name);
        SpriteRenderer spr = obj.AddComponent<SpriteRenderer>();
        spr.sprite = item.icon;
        obj.transform.parent = container;

        obj.transform.position = t.position;
        obj.transform.rotation = t.rotation;
        obj.transform.localScale = t.localScale;

        return obj;
    }

    private void ClearContainer ()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}
