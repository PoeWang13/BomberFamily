using UnityEngine;

public class Slot_Recipe : Slot
{
    private Item_Recipe myRecipeItem;

    public void SetRecipeSlot(Item_Recipe recipeItem)
    {
        myRecipeItem = recipeItem;
        SlotFull(recipeItem, 1);
    }
    // Buttonun kendisine verildi
    public void LearnNeededRecipeMaterial()
    {
        if (myRecipeItem is null)
        {
            return;
        }
        Canvas_Manager.Instance.SetMaterialList(myRecipeItem);
    }
}