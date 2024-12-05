using UnityEngine;
using UnityEngine.UI;

public class Slot_Material : Slot
{
    [SerializeField] private Button myButton;
    [SerializeField] private NeededItemHolder myRecipeItem;

    private int havingAmount = 0;

    public void SetMaterialSlot(NeededItemHolder recipeMaterial)
    {
        myRecipeItem = recipeMaterial;
        SlotFull(myRecipeItem.recipeItem, recipeMaterial.recipeAmount);
        havingAmount = Inventory_Manager.Instance.HasItem(myRecipeItem.recipeItem, recipeMaterial.recipeAmount);
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