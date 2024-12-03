using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MaterialHolder
{
    public Item recipeItem;
    public int recipeAmount;

    public MaterialHolder(Item recipeItem, int recipeAmount)
    {
        this.recipeItem = recipeItem;
        this.recipeAmount = recipeAmount;
    }
}
[CreateAssetMenu(menuName = "Item/Recipe")]
public class Item_Recipe : Item
{
    [SerializeField] private Item myRecipeItem;
    [SerializeField] private int  myRecipePrice;
    [SerializeField] private List<MaterialHolder> myNeededMaterialList = new List<MaterialHolder>();

    public Item MyRecipeItem { get { return myRecipeItem; } }
    public int MyRecipePrice { get { return myRecipePrice; } }
    public List<MaterialHolder> MyNeededMaterialList { get { return myNeededMaterialList; } }

    public virtual void AddMyItemToInventory()
    {
        Inventory_Manager.Instance.AddItem(new MaterialHolder(myRecipeItem, 1));
    }
}