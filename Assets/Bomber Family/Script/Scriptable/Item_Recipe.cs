using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RecipeItem
{
    public Item_Material recipeItem;
    public int recipeAmount;
}
[CreateAssetMenu(menuName = "Item/Recipe")]
public class Item_Recipe : ScriptableObject
{
    [SerializeField] private Item_Material myRecipeItem;
    [SerializeField] private List<RecipeItem> myRecipeList = new List<RecipeItem>();

    public Item_Material MyRecipeItem { get { return myRecipeItem; } }
    public List<RecipeItem> MyRecipeList { get { return myRecipeList; } }

    public virtual void AddMyItemToInventory()
    {
        Inventory_Manager.Instance.AddItem(myRecipeItem, 1);
    }
}