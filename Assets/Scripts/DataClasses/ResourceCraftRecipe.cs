using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataClasses
{
    public class ResourceCraftRecipe 
    {
        public ResourceItem craftedItem;
        public List<ResourceCraftRecipeToolIngredient> recipe;
    }

    [Serializable]
    public class ResourceCraftRecipeToolIngredient
    {
        public ToolItem tool;
        public int amount;
    }
    
    [Serializable]
    public class ResourceCraftRecipeResourceIngredient
    {
        public ResourceItem resourceItem;
        public int amount;
    }
}