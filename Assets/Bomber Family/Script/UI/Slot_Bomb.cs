using UnityEngine;

public class Slot_Bomb : Slot
{
    [SerializeField] private Item_Bomb myBombItem;

    // Buttonun kendisine verildi
    public void LearnNeededRecipeList()
    {
        Canvas_Manager.Instance.SetRecipeList(myBombItem);
    }
}