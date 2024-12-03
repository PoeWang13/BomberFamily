using UnityEngine;
using System.Collections.Generic;

public class Inventory_Manager : Singletion<Inventory_Manager>
{
    [SerializeField] private Slot inventoryPrefab;
    [SerializeField] private Transform inventoryParent;
    [SerializeField] private All_Item_Holder allItemHolder;
    [SerializeField] private List<Slot> myInventory = new List<Slot>();

    public List<Slot> MyInventory { get { return myInventory; } }

    private void Start()
    {
        // Inventory Slot oluştur
        for (int e = 0; e < Save_Load_Manager.Instance.gameData.inventorySlotAmount; e++)
        {
            myInventory.Add(Instantiate(inventoryPrefab, inventoryParent));
        }

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
    [ContextMenu("Add Material")]
    private void AddMaterial()
    {
        Save_Load_Manager.Instance.gameData.inventory.Add(new Inventory(0, 5, InventoryType.Material));
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
        Item_Material itemMaterial = materialHolder.recipeItem as Item_Material;
        int chekingAmount = materialHolder.recipeAmount;
        for (int e = 0; e < myInventory.Count; e++)
        {
            if (myInventory[e].CheckSlotForSameItem(itemMaterial))
            {
                if (itemMaterial.MyMax - myInventory[e].MyAmount >= chekingAmount)
                {
                    myInventory[e].AddItem(chekingAmount);
                    return;
                }
                else
                {
                    int addingAmountToSlot = itemMaterial.MyMax - myInventory[e].MyAmount;
                    chekingAmount -= addingAmountToSlot;
                    myInventory[e].AddItem(addingAmountToSlot);
                }
            }
        }
        if (chekingAmount > 0)
        {
            Warning_Manager.Instance.ShowMessage("Your Inventory is Full. You need buy some slots.", 2);
        }
    }
    public void RemoveItem(MaterialHolder materialHolder)
    {
        int chekingAmount = materialHolder.recipeAmount;
        for (int e = 0; e < myInventory.Count; e++)
        {
            if (myInventory[e].CheckSlotForSameItem(materialHolder.recipeItem))
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