using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Trigger_Slot : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Image imageOnayIcon;

    private Vector2Int objectCoor;
    private Trap_Trigger trapTrigger;

    /// <summary>
    /// Objeyi etkileyebilecek triggeri belirler ve duruma göre onu setler.
    /// </summary>
    /// <param name="trigger">Objeyi etkilen triger</param>
    public void SlotFull(Trap_Trigger trigger)
    {
        trapTrigger = trigger;
        objectCoor = Map_Creater_Manager.Instance.TrapBase.MyCoor;
        // True : Trigger bu objeyi etkiliyor, False : objeyi etkilemiyor.
        imageOnayIcon.enabled = trigger.boardTrigger.triggerBoardObject.HasCoor(objectCoor);
    }
    // Prefabe verildi.
    public void SetChoosingTrap()
    {
        bool durum = !imageOnayIcon.enabled;
        imageOnayIcon.enabled = durum;
        if (durum)
        {
            trapTrigger.SetActivatedObject(objectCoor);
        }
        else
        {
            trapTrigger.SetDeActivatedObject(objectCoor);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Camera_Manager.Instance.SendCamera(new Vector3(trapTrigger.MyCoor.x, 0, trapTrigger.MyCoor.y));
    }
}