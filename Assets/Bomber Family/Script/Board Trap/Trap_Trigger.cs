using UnityEngine;
using System.Collections.Generic;

public class Trap_Trigger : Trap_Base, ISetWorkTime, ISetActivatedObject, ISetDeActivatedObject
{
    private Animator myAnimator;
    private List<Trap_Diken> myDikens = new List<Trap_Diken>();

    public BoardTrigger boardTrigger = new BoardTrigger();
    public override void OnStart()
    {
        myAnimator = GetComponent<Animator>();
        Physics.IgnoreCollision(MyNotTriggeredCollider, Player_Base.Instance.MyNotTriggeredCollider);
    }
    public override void SetTrap()
    {
        if (string.IsNullOrEmpty(Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardSpecial))
        {
            return;
        }
        BoardContainer boardContainer = JsonUtility.FromJson<BoardContainer>(Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardSpecial);

        boardTrigger = JsonUtility.FromJson<BoardTrigger>(boardContainer.boardData.boardString);

        for (int e = 0; e < boardTrigger.triggerBoardObject.myAllCoor.Count; e++)
        {
            if (Map_Holder.Instance.GameBoard[boardTrigger.triggerBoardObject.myAllCoor[e].x, boardTrigger.triggerBoardObject.myAllCoor[e].y]
                .board_Object.TryGetComponent(out Trap_Diken trap_Diken))
            {
                AddDiken(trap_Diken);
            }
        }
    }
    public void SetGameBoardString()
    {
        BoardContainer boardContainer = new BoardContainer();
        boardContainer.boardData = new BoardData();

        boardContainer.boardData.trapType = TrapType.Trigger;
        string jsonTriggerData = JsonUtility.ToJson(boardTrigger, true);
        boardContainer.boardData.boardString = jsonTriggerData;

        string jsonBoardTriggerData = JsonUtility.ToJson(boardContainer, true);
        Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardSpecial = jsonBoardTriggerData;
    }
    public void SetWorkTime(bool isOneTime)
    {
        boardTrigger.triggerBoardObject.isOneTime = isOneTime;
        SetGameBoardString();
    }
    public void SetActivatedObject(Vector2Int objCoor)
    {
        if (boardTrigger.triggerBoardObject.AddCoor(objCoor))
        {
            SetGameBoardString();
        }
    }
    public void SetDeActivatedObject(Vector2Int objCoor)
    {
        if (boardTrigger.triggerBoardObject.RemoveCoor(objCoor))
        {
            SetGameBoardString();
        }
    }
    public override void SetMouseButton()
    {
        Map_Creater_Manager.Instance.ChooseStuckObject(this);
        Map_Creater_Manager.Instance.ChooseTrigger(this);
        Canvas_Manager.Instance.OpenBaseSetting(true);
        Canvas_Manager.Instance.CloseSettingPanels();
        Canvas_Manager.Instance.OpenPanelSettingTimerActive(false);
        Canvas_Manager.Instance.OpenPanelSettingTimer(false);
        Canvas_Manager.Instance.OpenTriggerSetting();
        Canvas_Manager.Instance.SetTrigger(false);
    }
    public void AddDiken(Trap_Diken diken)
    {
        if (!myDikens.Contains(diken))
        {
            myDikens.Add(diken);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myDikens.ForEach(d => d.SetDiken());
            Game_Manager.Instance.AddActiveTrapAmount();
            myAnimator.SetBool("IsActive", !myAnimator.GetBool("IsActive"));
        }
        if (other.CompareTag("Enemy"))
        {
            myDikens.ForEach(d => d.SetDiken());
            myAnimator.SetBool("IsActive", !myAnimator.GetBool("IsActive"));
        }
    }
}