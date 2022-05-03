using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryItem item;
    private Inventory inventory;

    public Image itemImage;
    private TextMeshProUGUI textBox;

    private Verb verb;
    private WalkToTarget target;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        textBox = GetComponentInChildren<TextMeshProUGUI>();
        verb = FindObjectOfType<Verb>();
        target = FindObjectOfType<WalkToTarget>();
    }

    public void DisplayItem(InventoryItem thisItem)
    {
        item = thisItem;
        textBox.text = item.itemName;
        itemImage.sprite = item.itemIcon;
        gameObject.SetActive(true);
    }

    public void ClearItem()
    {
        item = null;
        itemImage.sprite = null;
        gameObject.SetActive(false);
    }

    public void OnItemClick()
    {
        if (target.cutSceneInProgress) { return; }
        if (verb.verb == Verb.Action.use && verb.currentItem != null)
        {
            inventory.CombineItems(verb.currentItem, item);
        }

        verb.verb = Verb.Action.use;
        verb.currentItem = item;
        verb.UpdateVerbTextBox(null);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        verb.hoveredItemSlot = item.itemName;
        verb.UpdateVerbTextBox(null);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        verb.hoveredItemSlot = null;
        verb.UpdateVerbTextBox(null);
    }
}
