using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("Item", 
                "Change Item", 
                "Adds or removes an item from the inventory")]
    [AddComponentMenu("")]
    public class ChangeItem : Command
    {
        [Tooltip("reference to an InventoryItem scriptable object that fills the ItemSlots in the inventory")]
        [SerializeField] protected InventoryItem item;

        [Tooltip("If add is true, item will be added to Inventory, if add is false, item will be removed")]
        [SerializeField] protected bool add;

        public override void OnEnter()
        {
            if (item != null)
            {
                if (add) { item.itemOwned = true; }
                else { item.itemOwned = false; }
            }

            Continue();
        }

        public override string GetSummary()
        {
            if (item == null)
            {
                return "Error: no item selected";
            }

            return item.itemName;
        }
    }
}
