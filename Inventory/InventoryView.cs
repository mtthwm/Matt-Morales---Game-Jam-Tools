using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    public delegate void InventoryShowEvent();
    public event InventoryShowEvent OnInventoryShow;

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
        OnInventoryShow?.Invoke();
        string[] items = sourceInventoryManager.GetItems();
        for (int i = 0; i < slots.Length; i++) {

            InventorySlot slot = slots[i];
            Image img = slot.handle.GetComponent<Image>();
            img.enabled = false;
            InventoryButton invBtn = slot.handle.GetComponent<InventoryButton>();
            invBtn.Item = "";
            Button btn = slot.handle.GetComponent<Button>();
            btn.enabled = false;

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
                    invBtn.Item = itemName;
                    btn.enabled = true;
                }
                else
                {
                    Debug.LogWarning(itemName + " Not a valid inventory item!");
                }
            }
        }
    }

}
