using UnityEngine;
using System.Collections.Generic;

public class Inventory_Manager : Singletion<Inventory_Manager>
{
    [SerializeField] private Slot inventoryPrefab;
    [SerializeField] private Transform inventoryParent;
    [SerializeField] private All_Item_Holder allItemHolder;

    private List<Slot> myInventory = new List<Slot>();

    public List<Slot> MyInventory { get { return myInventory; } }

    private void Start()
    {
        // Inventory Slot oluştur
        AddInventorySlot(Save_Load_Manager.Instance.gameData.inventorySlotAmount);

        // Materialleri ver
        for (int e = 0; e < Save_Load_Manager.Instance.gameData.inventory.Count; e++)
        {
            if (Save_Load_Manager.Instance.gameData.inventory[e].inventoryType == InventoryType.Material)
            {
                myInventory[e].SlotFull(allItemHolder.MalzemeList[Save_Load_Manager.Instance.gameData.inventory[e].inventoryOrder],
                    Save_Load_Manager.Instance.gameData.inventory[e].inventoryAmount);
            }
            else if (Save_Load_Manager.Instance.gameData.inventory[e].inventoryType == InventoryType.Alet)
            {
                myInventory[e].SlotFull(allItemHolder.AletList[Save_Load_Manager.Instance.gameData.inventory[e].inventoryOrder],
                    Save_Load_Manager.Instance.gameData.inventory[e].inventoryAmount);
            }
        }
    }
    public void AddInventorySlot(int slotAmount)
    {
        // Inventory Slot oluştur
        for (int e = 0; e < slotAmount; e++)
        {
            myInventory.Add(Instantiate(inventoryPrefab, inventoryParent));
        }
    }
    /// <summary>
    /// İstenen itemden istenen kadar varmı diye bakar.
    /// </summary>
    /// <param name="item">İstenen item.</param>
    /// <param name="amount">istenen miktar.</param>
    /// <returns>Eksik kalan miktardır. 10 isteniyor, bizde 3 var yani geriye 7 döndürülür.</returns>
    public int HasItem(Item item, int amount)
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
    public void AddItem(MaterialHolder materialHolder)
    {
        Item_Source itemMaterial = materialHolder.recipeItem as Item_Source;
        int chekingAmount = materialHolder.recipeAmount;
        for (int e = 0; e < myInventory.Count; e++)
        {
            if (myInventory[e].CheckSlotForSameItem(itemMaterial))
            {
                if (itemMaterial.MyMax - myInventory[e].MyAmount >= chekingAmount)
                {
                    Save_Load_Manager.Instance.gameData.inventory[e].inventoryAmount += chekingAmount;
                    myInventory[e].AddItem(chekingAmount);
                    Save_Load_Manager.Instance.SaveGame();
                    return;
                }
                else
                {
                    int addingAmountToSlot = itemMaterial.MyMax - myInventory[e].MyAmount;
                    chekingAmount -= addingAmountToSlot;
                    Save_Load_Manager.Instance.gameData.inventory[e].inventoryAmount += addingAmountToSlot;
                    myInventory[e].AddItem(addingAmountToSlot);
                }
            }
        }
        if (chekingAmount > 0)
        {
            Warning_Manager.Instance.ShowMessage("Your Inventory is Full. You need buy some slots.", 2);
        }
        Save_Load_Manager.Instance.SaveGame();
    }
    public void RemoveItem(MaterialHolder materialHolder)
    {
        int chekingAmount = materialHolder.recipeAmount;
        for (int e = myInventory.Count - 1; e >= 0; e--)
        {
            if (myInventory[e].CheckSlotForSameItem(materialHolder.recipeItem))
            {
                if (myInventory[e].MyAmount >= chekingAmount)
                {
                    Save_Load_Manager.Instance.gameData.inventory[e].inventoryAmount -= chekingAmount;
                    myInventory[e].RemoveItem(chekingAmount);
                    Save_Load_Manager.Instance.SaveGame();
                    return;
                }
                else
                {
                    chekingAmount -= myInventory[e].MyAmount;
                    Save_Load_Manager.Instance.gameData.inventory[e] = new Inventory();
                    myInventory[e].SlotRelease();
                }
            }
        }
        Save_Load_Manager.Instance.SaveGame();
    }
}