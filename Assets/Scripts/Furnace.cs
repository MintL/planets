using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Planets
{
    public class Furnace : Factory
    {
        protected override Recipe GetRecipe()
        {
            return new Recipe(new Slot
            {
                Item = FindObjectOfType<ItemDatabase>().GetItem("Iron Ore"),
                Count = 2,
            }, new Slot
            {
                Item = FindObjectOfType<ItemDatabase>().GetItem("Iron Ingot"),
                Count = 1,
            },
            4f);
        }
    }
}
