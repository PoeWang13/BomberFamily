using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image imageSlotIcon;
    [SerializeField] private TextMeshProUGUI textSlotAmount;

    private Item_Material myItem;
    private int myAmount;

    public Item_Material MyItem { get { return myItem; } }
    public int MyAmount { get { return myAmount; } }

    public void SlotRelease()
    {
        myItem = null;
        imageSlotIcon.sprite = Canvas_Manager.Instance.EmptySlotIcon;
        textSlotAmount.text = "";
    }
    public void SlotFull(Item_Material item, int slotAmount)
    {
        myItem = item;
        myAmount = slotAmount;
        imageSlotIcon.sprite = myItem.MyIcon;
        textSlotAmount.text = myAmount.ToString();
    }
    public bool CheckSlotForSameItem(Item_Material item)
    {
        return myItem == item;
    }
    /// <summary>
    /// Slot boş mu? Kontrol eder
    /// </summary>
    /// <returns>Eğer slot boş ise true döner.</returns>
    public bool IsSlotEmpty()
    {
        return myItem is null;
    }
    public void AddItem(int slotAmount)
    {
        myAmount += slotAmount;
        textSlotAmount.text = myAmount.ToString();
    }
    public void RemoveItem(int slotAmount)
    {
        myAmount -= slotAmount;
        textSlotAmount.text = myAmount.ToString();
        if (myAmount <= 0)
        {
            SlotRelease();
        }
    }
}