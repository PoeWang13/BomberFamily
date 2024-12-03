using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Bomb")]
public class Item_Bomb : Item_Recipe
{
    [SerializeField] private BombType myBombType;
    [SerializeField] private List<Item_Recipe> myRecipeList = new List<Item_Recipe>();

    public List<Item_Recipe> MyRecipeList { get { return myRecipeList; } }

    public override void AddMyItemToInventory()
    {
        Debug.Log("Item_Bomb");
        Save_Load_Manager.Instance.gameData.allBombAmount[(int)myBombType].bombAmount++;
        Canvas_Manager.Instance.SetBomb(myBombType);
    }
}