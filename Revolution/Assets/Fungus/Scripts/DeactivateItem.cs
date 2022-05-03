using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("Item",
                "Deactivate or activate sprite",
                "Turns a GameObject off or on in the scene")]
    [AddComponentMenu("")]

    public class DeactivateItem : Command
    {
        [SerializeField] protected GameObject GameObjectToChange;
        [SerializeField] protected bool Show;
        

        public override void OnEnter()
        {
            if (Show) { GameObjectToChange.SetActive(true); }
            else { GameObjectToChange.SetActive(false); }
            
            Continue();
        }

        public override string GetSummary()
        {
            if (GameObjectToChange == null)
            {
                return "Error: no item selected";
            }

            return "hi";
        }
    }

}
