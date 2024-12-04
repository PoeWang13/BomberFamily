using UnityEngine;

[CreateAssetMenu(fileName = "Source_Tool", menuName = "Item/Tool")]
public class Item_Tool : Item_Source
{
    [SerializeField] private Item_Recipe myRecipeItem;

    public Item_Recipe MyRecipeItem { get { return myRecipeItem; } }
}