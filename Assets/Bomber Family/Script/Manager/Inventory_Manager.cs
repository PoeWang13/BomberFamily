using UnityEngine;
using System.Collections.Generic;

public class Inventory_Manager : Singletion<Inventory_Manager>
{
    [SerializeField] private List<NeededItemHolder> neededItemList = new List<NeededItemHolder>();
    [ContextMenu("Add New Item")]
    private void AddNewItem()
    {
        for (int i = 0; i < neededItemList.Count; i++)
        {
            AddItem(neededItemList[i]);
        }
    }
    [SerializeField] private Slot inventoryPrefab;
    [SerializeField] private Transform inventoryParent;
    [SerializeField] private All_Item_Holder allItemHolder;

    private List<Slot> myInventory = new List<Slot>();

    public List<Slot> MyInventory { get { return myInventory; } }

    private void Start()
    {
        // Inventory Slot oluştur
        for (int e = 0; e < Save_Load_Manager.Instance.gameData.inventory.Count; e++)
        {
            myInventory.Add(Instantiate(inventoryPrefab, inventoryParent));
        }

        // Materialleri ver
        for (int e = 0; e < Save_Load_Manager.Instance.gameData.inventory.Count; e++)
        {
            if (Save_Load_Manager.Instance.gameData.inventory[e].inventoryAmount < 1)
            {
                continue;
            }
            if (Save_Load_Manager.Instance.gameData.inventory[e].inventoryType == InventoryType.Material)
            {
                myInventory[e].SlotFull(allItemHolder.MalzemeList[Save_Load_Manager.Instance.gameData.inventory[e].inventoryOrder],
                    Save_Load_Manager.Instance.gameData.inventory[e].inventoryAmount);
            }
            else if (Save_Load_Manager.Instance.gameData.inventory[e].inventoryType == InventoryType.Alet)
            {
                myInventory[e].SlotFull(allItemHolder.ToolList[Save_Load_Manager.Instance.gameData.inventory[e].inventoryOrder],
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
            Save_Load_Manager.Instance.gameData.inventory.Add(new Inventory());
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
    public void AddItem(NeededItemHolder addedItem)
    {
        Item_Source itemMaterial = addedItem.recipeItem as Item_Source;
        int chekingAmount = addedItem.recipeAmount;
        int itemOrder = -1;
        bool isFinded = false;
        if (addedItem.inventoryType == InventoryType.Material)
        {
            itemOrder = allItemHolder.LearnMalzemeOrder(itemMaterial);
        }
        else
        {
            itemOrder = allItemHolder.LearnToolOrder(itemMaterial);
        }
        for (int e = 0; e < myInventory.Count && !isFinded; e++)
        {
            if (myInventory[e].CheckSlotForEmptyItem())
            {
                Save_Load_Manager.Instance.gameData.inventory[e] = new Inventory(itemOrder, addedItem.inventoryType);
                if (itemMaterial.MyMax - myInventory[e].MyAmount >= chekingAmount)
                {
                    Save_Load_Manager.Instance.gameData.inventory[e].AddAmount(chekingAmount);
                    myInventory[e].SlotFull(itemMaterial, chekingAmount);
                    chekingAmount = 0;
                    isFinded = true;
                }
                else
                {
                    int addingAmountToSlot = itemMaterial.MyMax - myInventory[e].MyAmount;
                    chekingAmount -= addingAmountToSlot;
                    Save_Load_Manager.Instance.gameData.inventory[e].AddAmount(addingAmountToSlot);
                    myInventory[e].SlotFull(itemMaterial, addingAmountToSlot);
                }
            }
            else if (myInventory[e].CheckSlotForSameItem(itemMaterial))
            {
                if (itemMaterial.MyMax - myInventory[e].MyAmount >= chekingAmount)
                {
                    Save_Load_Manager.Instance.gameData.inventory[e].AddAmount(chekingAmount);
                    myInventory[e].AddItem(chekingAmount);
                    chekingAmount = 0;
                    isFinded = true;
                }
                else
                {
                    int addingAmountToSlot = itemMaterial.MyMax - myInventory[e].MyAmount;
                    chekingAmount -= addingAmountToSlot;
                    Save_Load_Manager.Instance.gameData.inventory[e].AddAmount(addingAmountToSlot);
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
    public void RemoveItem(NeededItemHolder removedHolder)
    {
        int chekingAmount = removedHolder.recipeAmount;
        for (int e = 0; e < myInventory.Count; e++)
        {
            if (myInventory[e].CheckSlotForSameItem(removedHolder.recipeItem))
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