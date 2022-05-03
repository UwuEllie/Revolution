using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using TMPro;

public class Verb : MonoBehaviour
{
    public string walkString = "Walk to ";
    public string useString = "Use ";

    public string currentClickable;
    public InventoryItem currentItem;
    public string hoveredItemSlot;
    public bool combinability;

    public Inventory inventory;

    public enum Action { walk, use };
    public Action verb = Action.walk;
    private TextMeshProUGUI verbTextBox;

    private Flowchart[] flowcharts;

    void Start()
    {
        verbTextBox = GetComponentInChildren <TextMeshProUGUI>();
        verbTextBox.text = "";
        flowcharts = FindObjectsOfType<Flowchart>();
        inventory = FindObjectOfType<Inventory>();
    }

    public void UpdateVerbTextBox(string currentClickable)
    {
        SetVerbInFlowchart();
        if (verb == Action.walk)
        {
            combinability = false;
            verbTextBox.text = walkString + currentClickable;
        }
        else if (verb == Action.use)
        {
            if (inventory.canvasGroup.interactable == true)
            {
                combinability = true; 
                verbTextBox.text = useString + " " + currentItem.itemName + " with " + hoveredItemSlot; 
            }
            else if (currentClickable == null)
            {
                verbTextBox.text = useString + " " + currentItem.itemName + " with ";
            }
            else
            {
                combinability = false;
                verbTextBox.text = useString + " " + currentItem.itemName + "with " + currentClickable;
            }
        }
    }

    public void SetVerbInFlowchart()
    {
        foreach (Flowchart flowchart in flowcharts)
        {
            if (flowchart.HasVariable("verb")) { flowchart.SetStringVariable("verb", verb.ToString()); }
            if (currentItem == null) { return; }
            if (flowchart.HasVariable("currentItem")) { flowchart.SetStringVariable("currentItem", currentItem.itemName); }
        }
    }
}
