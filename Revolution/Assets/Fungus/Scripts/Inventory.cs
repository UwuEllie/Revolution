using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System.Linq;

public class Inventory : MonoBehaviour
{
    private MenuDialog[] menuDialogue;
    private SayDialog[] sayDialogue;
    public CanvasGroup canvasGroup;
    private WalkToTarget target;

    public InventoryItem[] inventoryItem;
    public ItemSlot[] itemSlots;

    private Flowchart[] flowcharts;
    
    void Start()
    {
        menuDialogue = FindObjectsOfType<MenuDialog>();
        sayDialogue = FindObjectsOfType<SayDialog>();
        canvasGroup = GetComponent<CanvasGroup>();
        target = FindObjectOfType<WalkToTarget>();
        flowcharts = FindObjectsOfType<Flowchart>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            ToggleInventory(!canvasGroup.interactable);
        }
    }

    private void ToggleInventory(bool setting)
    {
        ToggleCanvasGroup(canvasGroup, setting);
        InitialiseItemSlots();

        if (!target.cutSceneInProgress)
        {
            target.InDialogue = setting;
        }

        foreach (MenuDialog menuDialog in menuDialogue)
        {
            ToggleCanvasGroup(menuDialog.GetComponent<CanvasGroup>(), !setting);
        }
        foreach (SayDialog sayDialog in sayDialogue)
        {
            sayDialog.dialogueEnabled = !setting;
            if (setting) { Time.timeScale = 0f; }
            else { Time.timeScale = 1f; }
            ToggleCanvasGroup(sayDialog.GetComponent<CanvasGroup>(), !setting);
            
        }
    }

    private void InitialiseItemSlots()
    {
        List<InventoryItem> ownedItems = GetOwnedItems(inventoryItem.ToList());

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < ownedItems.Count)
            {
                itemSlots[i].DisplayItem(ownedItems[i]);
            }
            else
            {
                itemSlots[i].ClearItem();
            }
        }
    }

    private void ToggleCanvasGroup(CanvasGroup canvasGroup, bool setting)
    {
        canvasGroup.alpha = setting ? 1f : 0f;
        canvasGroup.interactable = setting;
        canvasGroup.blocksRaycasts = setting;
    }

    public List<InventoryItem> GetOwnedItems(List<InventoryItem> inventoryItems)
    {
        List<InventoryItem> ownedItems = new List<InventoryItem>();

        foreach (InventoryItem item in inventoryItems)
        {
            if (item.itemOwned)
            {
                ownedItems.Add(item);
            }
        }

        return ownedItems;
    }

    public void CombineItems(InventoryItem item1, InventoryItem item2)
    {
        if (item1.combinable == true && item2.combinable == true)
        {
            for (int i = 0; i < item1.combinableItems.Length; i++)
            {
                if (item1.combinableItems[i] == item2)
                {
                    foreach (Flowchart flowchart in flowcharts)
                    {
                        if (flowchart.HasBlock(item1.successBlockNames[i]))
                        {
                            ToggleInventory(false);
                            target.EnterDialogue();
                            flowchart.ExecuteBlock(item1.successBlockNames[i]);
                            return;
                        }
                    }
                }
            }
        }
        foreach (Flowchart flowchart in flowcharts)
        {
            if (flowchart.HasBlock(item1.failBlockNames))
            {
                ToggleInventory(false);
                target.EnterDialogue();
                flowchart.ExecuteBlock(item1.failBlockNames);
            }
        }
    }
}
