using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot_Trap : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Image imageOnayIcon;

    private Vector2Int objectCoor;
    private Trap_Trigger trapTrigger;

    public void SlotFull(Trap_Trigger trigger)
    {
        trapTrigger = trigger;
        objectCoor = trigger.MyCoor;
    }
    // Prefabe verildi.
    public void SetChoosingTrap()
    {
        //bool durum = imageOnayIcon.enabled;
        //imageOnayIcon.enabled = !durum;
        //if (durum)
        //{
        //    if (!trapTrigger.boardTrigger.triggerBoardObject.myAllCoor.Contains(objectCoor))
        //    {
        //        trapTrigger.boardTrigger.triggerBoardObject.myAllCoor.Add(objectCoor);
        //    }
        //}
        //else
        //{
        //    if (trapTrigger.boardTrigger.triggerBoardObject.myAllCoor.Contains(objectCoor))
        //    {
        //        trapTrigger.boardTrigger.triggerBoardObject.myAllCoor.Remove(objectCoor);
        //    }
        //}
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Camera_Manager.Instance.SendCamera(new Vector3(objectCoor.x, 0, objectCoor.y));
    }
}