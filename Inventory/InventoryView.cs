using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private GameObject prefab;

    public void Show ()
    {
        transform.parent.GetComponent<Canvas>().enabled = true;
    }

    public void Hide ()
    {
        transform.parent.GetComponent<Canvas>().enabled = false;
    }

    private void Start()
    {
        RenderInventory();
    }

    private void OnEnable()
    {
        inventoryManager.OnModifyInventory += RenderInventory;
    }

    private void OnDisable()
    {
        inventoryManager.OnModifyInventory -= RenderInventory;
    }

    private void RenderInventory()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (string itemName in inventoryManager.GetItems())
        {
            InventoryItem item = InventoryResolver.Instance.Resolve(itemName);
            GameObject g = Instantiate(prefab, transform);
            g.transform.localScale = Vector3.one;
            Image img = g.GetComponent<Image>();
            img.sprite = item.icon;
        }
    }

}
