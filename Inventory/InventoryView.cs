using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [Serializable]
    private class InventorySlot
    {
        public string contents;
        public GameObject handle;
    }
    [SerializeField] public InventoryManager sourceInventoryManager;
    [SerializeField] public InventoryManager targetInventoryManager;
    [SerializeField] private UnityEvent onShow;
    [SerializeField] private UnityEvent onHide;
    [SerializeField] private InventorySlot[] slots;

    public void Show ()
    {
        RenderInventory();
        gameObject.SetActive(true);
        onShow.Invoke();
    }

    public void Hide ()
    {
        gameObject.SetActive(false);
        onHide.Invoke();
    }

    private void Start()
    {
        RenderInventory();
        Hide();
    }

    private void OnEnable()
    {
        sourceInventoryManager.OnModifyInventory += RenderInventory;
    }

    private void OnDisable()
    {
        sourceInventoryManager.OnModifyInventory -= RenderInventory;
    }

    private void RenderInventory()
    {
        string[] items = sourceInventoryManager.GetItems();
        for (int i = 0; i < slots.Length; i++) {

            InventorySlot slot = slots[i];
            Image img = slot.handle.GetComponent<Image>();
            img.enabled = false;
            InventoryButton btn = slot.handle.GetComponent<InventoryButton>();
            btn.Item = "";

            if (i < items.Length)
            {
                string itemName = items[i];
                slot.contents = itemName;
                InventoryItem item = InventoryResolver.Instance.Resolve(itemName);
                if (item is not null)
                {
                    slot.handle.transform.localScale = Vector3.one;
                    img.sprite = item.icon;
                    img.enabled = true;
                    btn.Item = itemName;
                }
                else
                {
                    Debug.LogWarning(itemName + " Not a valid inventory item!");
                }
            }
        }
    }

}
