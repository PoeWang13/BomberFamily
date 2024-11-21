using UnityEngine;
using System.Collections.Generic;

public class Craft_Manager : Singletion<Craft_Manager>
{
    [SerializeField] private List<Craft_Slot> craft_Slot = new List<Craft_Slot>();

    private Item_Recipe item_Recipe;
    public void ChooseRecipe(Item_Recipe recipe)
    {
        item_Recipe = recipe;
        for (int e = 0; e < craft_Slot.Count; e++)
        {
            craft_Slot[e].SlotRelease();
            craft_Slot[e].SlotFixButton();
        }
        for (int e = 0; e < recipe.MyRecipeList.Count; e++)
        {
            craft_Slot[e].SlotFull(recipe.MyRecipeList[e]);
        }
    }
    public void CraftRecipe()
    {
        bool canCraft = true;
        for (int e = 0; e < craft_Slot.Count && canCraft; e++)
        {
            if (craft_Slot[e].IsSlotEmpty())
            {
                continue;
            }
            if (craft_Slot[e].SlotNeededAmount())
            {
                canCraft = false;
            }
        }
        if (canCraft)
        {
            for (int h = 0; h < item_Recipe.MyRecipeList.Count; h++)
            {
                bool allRemoved = false;
                int removeAmount = item_Recipe.MyRecipeList[h].recipeAmount;
                for (int e = 0; e < craft_Slot.Count && !allRemoved; e++)
                {
                    if (craft_Slot[e].CheckSlotForSameItem(item_Recipe.MyRecipeList[h].recipeItem))
                    {
                        int amount = craft_Slot[e].MyAmount;
                        if (amount >= removeAmount)
                        {
                            Inventory_Manager.Instance.RemoveItem(item_Recipe.MyRecipeList[h].recipeItem, removeAmount);
                            removeAmount = 0;
                            // Sıradaki item
                            allRemoved = true;
                        }
                        else
                        {
                            removeAmount -= amount;
                            Inventory_Manager.Instance.RemoveItem(item_Recipe.MyRecipeList[h].recipeItem, amount);
                        }
                    }
                }
            }
            item_Recipe.AddMyItemToInventory();
        }
        else
        {
            Warning_Manager.Instance.ShowMessage("You Recipe need some items.", 2);
        }
    }
}