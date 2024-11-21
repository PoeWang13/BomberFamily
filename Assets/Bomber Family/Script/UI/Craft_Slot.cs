using UnityEngine;
using UnityEngine.UI;

public class Craft_Slot : Slot
{
    [SerializeField] private Button buttonSlot;

    private int neededAmount;

    public void SlotFull(RecipeItem recipeItem)
    {
        SlotFull(recipeItem.recipeItem, recipeItem.recipeAmount);

        neededAmount = Inventory_Manager.Instance.HasItem(MyItem, MyAmount);
        buttonSlot.interactable = neededAmount != 0;
    }
    public void SlotFixButton()
    {
        buttonSlot.interactable = false;
    }
    // Prefabın kendisine verildi
    public void LearnNeededAmount()
    {
        Warning_Manager.Instance.ShowMessage("You need find another </color=red>" + neededAmount + "</color> item.", 2);
    }
    public bool SlotNeededAmount()
    {
        return neededAmount != 0;
    }
}