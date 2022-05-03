using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("Item",
                "Change sprite",
                "Changes the sprite of the sprite renderer to a new sprite")]
    [AddComponentMenu("")]

    public class ChangeSprite : Command
    {
        //[Tooltip("reference to an InventoryItem scriptable object that fills the ItemSlots in the inventory")]
        [SerializeField] protected GameObject ObjectHolder;
        [SerializeField] protected SpriteRenderer rend;
        [SerializeField] protected Sprite newSprite;


        //[Tooltip("If add is true, item will be added to Inventory, if add is false, item will be removed")]
        [SerializeField] protected bool ShowItem;
        [SerializeField] protected GameObject ObjectsToShow;

        public override void OnEnter()
        {
            rend = ObjectHolder.gameObject.GetComponent<SpriteRenderer>();
            rend.sprite = newSprite;

            if (ShowItem) { ObjectsToShow.SetActive(true); }

            Continue();
        }

        public override string GetSummary()
        {
            if (ObjectHolder == null)
            {
                return "Error: no item selected";
            }

            return "hi";
        }
    }
}
   
