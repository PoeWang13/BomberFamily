using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class NeededItemHolder
{
    public Item recipeItem;
    public int recipeAmount;
    public InventoryType inventoryType;

    public NeededItemHolder()
    {
        this.recipeAmount = 1;
    }
    public NeededItemHolder(Item recipeItem, int recipeAmount, InventoryType inventoryType)
    {
        this.recipeItem = recipeItem;
        this.recipeAmount = recipeAmount;
        this.inventoryType = inventoryType;
    }
}
[CreateAssetMenu(menuName = "Item/Recipe")]
public class Item_Recipe : Item
{
    [SerializeField] private int myRecipePrice;
    [SerializeField] private Item myRecipeItem;
    [SerializeField] private List<NeededItemHolder> myNeededItemList = new List<NeededItemHolder>();

    public int MyRecipePrice { get { return myRecipePrice; } }
    public Item MyRecipeItem { get { return myRecipeItem; } }
    public List<NeededItemHolder> MyNeededItemList { get { return myNeededItemList; } }
    
    public virtual void AddMyItemToInventory(int bombPower, int bombLimit, float bombFireTime)
    {
        Inventory_Manager.Instance.AddItem(new NeededItemHolder(myRecipeItem, 1, InventoryType.Alet));
    }
}