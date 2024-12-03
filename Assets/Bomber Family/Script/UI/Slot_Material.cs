using UnityEngine;
using UnityEngine.UI;

public class Slot_Material : Slot
{
    [SerializeField] private Button myButton;
    [SerializeField] private MaterialHolder myRecipeItem;

    private int havingAmount = 0;

    public void SetMaterialSlot(MaterialHolder recipeMaterial)
    {
        myRecipeItem = recipeMaterial;
        SlotFull(myRecipeItem.recipeItem, 1);
        havingAmount = Inventory_Manager.Instance.HasItem(myRecipeItem.recipeItem, myRecipeItem.recipeAmount);
        myButton.interactable = havingAmount > 0;
    }
    // Buttonun kendisine verildi
    public void LearnNeededMaterial()
    {
        if (myRecipeItem is null)
        {
            return;
        }
        Warning_Manager.Instance.ShowMessage("Need another " + havingAmount + " items.", 2);
    }
    public bool HasSlotNeededAmount()
    {
        return havingAmount == 0;
    }
}