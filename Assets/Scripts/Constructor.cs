using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Planets
{
    public class Constructor : Factory
    {
        protected override Recipe GetRecipe()
        {
            return new Recipe(new Slot
            {
                Item = FindObjectOfType<ItemDatabase>().GetItem("Iron Ingot"),
                Count = 1,
            }, new Slot
            {
                Item = FindObjectOfType<ItemDatabase>().GetItem("Wood"),
                Count = 1,
            },
            2f);
        }
    }
}
