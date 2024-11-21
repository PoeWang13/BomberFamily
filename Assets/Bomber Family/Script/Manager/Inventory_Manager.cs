using UnityEngine;
using System.Collections.Generic;

public class Inventory_Manager : Singletion<Inventory_Manager>
{
    [SerializeField] private List<Slot> myInventory = new List<Slot>();

    public List<Slot> MyInventory { get { return myInventory; } }

    /// <summary>
    /// İstenen itemden istenen kadar varmı diye bakar.
    /// </summary>
    /// <param name="item">İstenen item.</param>
    /// <param name="amount">istenen miktar.</param>
    /// <returns>Eksik kalan miktardır. 10 isteniyor, bizde 3 var yani geriye 7 döndürülür.</returns>
    public int HasItem(Item_Material item, int amount)
    {
        int chekingAmount = amount;
        for (int e = 0; e < myInventory.Count; e++)
        {
            if (myInventory[e].CheckSlotForSameItem(item))
            {
                chekingAmount -= myInventory[e].MyAmount;
                if (chekingAmount <= 0)
                {
                    return 0;
                }
            }
        }
        return chekingAmount;
    }
    public void AddItem(Item_Material item, int amount)
    {
        int chekingAmount = amount;
        for (int e = 0; e < myInventory.Count; e++)
        {
            if (myInventory[e].CheckSlotForSameItem(item))
            {
                if (item.MyMax - myInventory[e].MyAmount >= chekingAmount)
                {
                    myInventory[e].AddItem(chekingAmount);
                    return;
                }
                else
                {
                    int addingAmountToSlot = item.MyMax - myInventory[e].MyAmount;
                    chekingAmount -= addingAmountToSlot;
                    myInventory[e].AddItem(addingAmountToSlot);
                }
            }
        }
    }
    public void RemoveItem(Item_Material item, int amount)
    {
        int chekingAmount = amount;
        for (int e = 0; e < myInventory.Count; e++)
        {
            if (myInventory[e].CheckSlotForSameItem(item))
            {
                if (myInventory[e].MyAmount >= chekingAmount)
                {
                    myInventory[e].RemoveItem(chekingAmount);
                    return;
                }
                else
                {
                    chekingAmount -= myInventory[e].MyAmount;
                    myInventory[e].SlotRelease();
                }
            }
        }
    }
}