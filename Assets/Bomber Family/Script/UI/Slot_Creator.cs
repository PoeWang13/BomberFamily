using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Creator : MonoBehaviour
{
    private Item_Board myItem;

    public void SetSlot(Item_Board myItem)
    {
        this.myItem = myItem;
        GetComponentInChildren<TextMeshProUGUI>().text = myItem.MyPrice.ToString();
        GetComponent<Image>().sprite = myItem.MyIcon;
        GetComponent<Button>().onClick.AddListener(() => CreateMyObject());
    }
    private void CreateMyObject()
    {
        if (myItem.MyBoardType == BoardType.Gate)
        {
            if (Map_Creater_Manager.Instance.HasGate)
            {
                Warning_Manager.Instance.ShowMessage("Your Map has a GATE.", 2);
                return;
            }
        }
        if (Canvas_Manager.Instance.IsMultiple)
        {
            Canvas_Manager.Instance.SetMultiplePlacementObject(myItem);
        }
        else
        {
            if (Save_Load_Manager.Instance.gameData.gold >= myItem.MyPrice)
            {
                Map_Creater_Manager.Instance.SetCreatedObject(myItem);
                Canvas_Manager.Instance.SetGoldSmooth(-myItem.MyPrice);
                // Hareket sistemi
                if (myItem.MyBoardType == BoardType.Trap)
                {
                    if ((myItem as Item_Trap).MyTrapType == TrapType.None)
                    {
                        Canvas_Manager.Instance.OpenBaseSetting(false);
                        Canvas_Manager.Instance.CloseSettingPanels();
                    }
                    else if ((myItem as Item_Trap).MyTrapType == TrapType.Trigger)
                    {
                        Canvas_Manager.Instance.OpenBaseSetting(false);
                        Canvas_Manager.Instance.CloseSettingPanels();
                    }
                    else if ((myItem as Item_Trap).MyTrapType > (TrapType)99 && (myItem as Item_Trap).MyTrapType < (TrapType)1000)
                    {
                        Canvas_Manager.Instance.OpenBaseSetting(false);
                        Canvas_Manager.Instance.CloseSettingPanels();
                    }
                    else if ((myItem as Item_Trap).MyTrapType > (TrapType)999)
                    {
                        Canvas_Manager.Instance.OpenBaseSetting(false);
                        Canvas_Manager.Instance.CloseSettingPanels();
                    }
                }
                else
                {
                    Canvas_Manager.Instance.OpenBaseSetting(false);
                    Canvas_Manager.Instance.CloseSettingPanels();
                }
            }
            else
            {
                Warning_Manager.Instance.ShowMessage("You dont have enough Gold.", 2);
            }
        }
    }
}