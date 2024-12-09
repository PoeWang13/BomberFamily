using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Item/Bomb")]
public class Item_Bomb : Item_Recipe
{
    [SerializeField] private BombType myBombType;
    [SerializeField] private Item_Source myItemPower;
    [SerializeField] private Item_Source myItemLimit;
    [SerializeField] private Item_Source myItemFireTime;
    [SerializeField] private List<Item_Recipe> myRecipeList = new List<Item_Recipe>();

    public BombType MyBombType { get { return myBombType; } }
    public Item_Source MyItemPower { get { return myItemPower; } }
    public Item_Source MyItemLimit { get { return myItemLimit; } }
    public Item_Source MyItemFireTime { get { return myItemFireTime; } }
    public List<Item_Recipe> MyRecipeList { get { return myRecipeList; } }

    public override void AddMyItemToInventory(int bombPower, int bombLimit, float bombFireTime)
    {
        Warning_Manager.Instance.ShowMessage("You crafted " + myBombType + " Bomb.", 2);
        bool finded = false;
        for (int e = 0; e < Save_Load_Manager.Instance.gameData.allSpecialBomb.Count && !finded; e++)
        {
            if (Save_Load_Manager.Instance.gameData.allSpecialBomb[e].IsSameBomb(myBombType, bombPower, bombLimit, bombFireTime))
            {
                Save_Load_Manager.Instance.gameData.allSpecialBomb[e].bombAmount++;
                Canvas_Manager.Instance.SetBomb(e);
            }
        }
        if (!finded)
        {
            Canvas_Manager.Instance.AddBombPanel(myBombType, bombPower, bombLimit, bombFireTime);
        }
    }
}