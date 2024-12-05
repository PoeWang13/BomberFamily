using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image imageSlotIcon;
    [SerializeField] private GameObject panelPrice;
    [SerializeField] private TextMeshProUGUI textSlotAmount;

    private Item myItem;
    private int myAmount;

    public Item MyItem { get { return myItem; } }
    public int MyAmount { get { return myAmount; } }

    public void SlotRelease()
    {
        myItem = null;
        myAmount = 0;
        imageSlotIcon.sprite = Canvas_Manager.Instance.EmptySlotIcon;
        textSlotAmount.text = "";
        panelPrice.SetActive(false);
    }
    public void SlotFull(Item item, int slotAmount)
    {
        myItem = item;
        myAmount = slotAmount;
        panelPrice.SetActive(true);
        imageSlotIcon.sprite = myItem.MyIcon;
        textSlotAmount.text = myAmount.ToString();
    }
    public bool CheckSlotForSameItem(Item item)
    {
        return myItem == item;
    }
    public bool CheckSlotHasAmount(int slotAmount)
    {
        return myAmount >= slotAmount;
    }
    public bool CheckSlotForEmptyItem()
    {
        return myAmount <= 0;
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
        panelPrice.SetActive(myAmount > 1);
        textSlotAmount.text = myAmount.ToString();
    }
    public void RemoveItem(int slotAmount)
    {
        myAmount -= slotAmount;
        panelPrice.SetActive(myAmount > 1);
        textSlotAmount.text = myAmount.ToString();
        if (myAmount <= 0)
        {
            SlotRelease();
        }
    }
}