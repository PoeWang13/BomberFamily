using UnityEngine;
using System.Collections.Generic;

public class Trap_Has_Time_2 : Trap_Base, IHasTrigger, IAlwaysActivite, ISetEffectTime, ISetActivited, ISetWaitingTime, ISetWorkingTime
{
    public BoardTrapTimer2 boardTrapTimer2 = new BoardTrapTimer2();
    public bool activeted;
    public bool alwaysActivated;
    public float trapTimeNext;
    public float myEffectTime;
    public Vector2 myStopWorkTime;
    public List<Character_Base> myCharacterList = new List<Character_Base>();
    public override void OnStart()
    {
        if (MyNotTriggeredCollider != null)
        {
            Physics.IgnoreCollision(MyNotTriggeredCollider, Player_Base.Instance.MyNotTriggeredCollider);
        }
    }
    private void Update()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (boardTrapTimer2.BoardObject.isTrigger)
        {
            return;
        }
        trapTimeNext += Time.deltaTime;
        if (alwaysActivated)
        {
            if (trapTimeNext > myEffectTime)
            {
                BehaviourChange(true);
            }
        }
        else
        {
            if (activeted)
            {
                if (trapTimeNext > myStopWorkTime.y)
                {
                    BehaviourChange(false);
                }
            }
            else
            {
                if (trapTimeNext > myStopWorkTime.x)
                {
                    BehaviourChange(true);
                }
            }
        }
    }
    public virtual void BehaviourChange(bool giveDamage)
    {
        trapTimeNext = 0;
        activeted = giveDamage;
    }
    public override void SetTrap()
    {
        if (string.IsNullOrEmpty(Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardSpecial))
        {
            return;
        }
        BoardContainer boardContainer = JsonUtility.FromJson<BoardContainer>(Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardSpecial);

        boardTrapTimer2 = JsonUtility.FromJson<BoardTrapTimer2>(boardContainer.boardData.boardString);

        activeted = boardTrapTimer2.BoardObject.isActivited;
        alwaysActivated = boardTrapTimer2.BoardObject.isAlwaysActivited;
        myStopWorkTime = boardTrapTimer2.BoardObject.myStopWorkTime;
        myEffectTime = boardTrapTimer2.BoardObject.myEffectTime;
        SetTrapForSpecial();
    }
    public virtual void SetTrapForSpecial()
    {

    }
    public void SetGameBoardString()
    {
        BoardContainer boardContainer = new BoardContainer();
        boardContainer.boardData = new BoardData();

        boardContainer.boardData.trapType = TrapType.Diken;
        string jsonTriggerData = JsonUtility.ToJson(boardTrapTimer2, true);
        boardContainer.boardData.boardString = jsonTriggerData;

        string jsonBoardTriggerData = JsonUtility.ToJson(boardContainer, true);
        Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardSpecial = jsonBoardTriggerData;
    }
    public void SetHasTrigger(bool isTrigger)
    {
        boardTrapTimer2.BoardObject.isTrigger = isTrigger;
        SetGameBoardString();
    }
    public void SetAlwaysActivite(bool isAlwaysActivite)
    {
        boardTrapTimer2.BoardObject.isAlwaysActivited = isAlwaysActivite;
        SetGameBoardString();
    }
    public void SetEffectTime(float effectTime)
    {
        boardTrapTimer2.BoardObject.myEffectTime = effectTime;
        SetGameBoardString();
    }
    public void SetActivited(bool isActivited)
    {
        boardTrapTimer2.BoardObject.isActivited = isActivited;
        SetGameBoardString();
    }
    public void SetWaitingTime(float waitingTime)
    {
        boardTrapTimer2.BoardObject.myStopWorkTime = new Vector2(waitingTime, boardTrapTimer2.BoardObject.myStopWorkTime.y);
        SetGameBoardString();
    }
    public void SetWorkingTime(float workingTime)
    {
        boardTrapTimer2.BoardObject.myStopWorkTime = new Vector2(boardTrapTimer2.BoardObject.myStopWorkTime.x, workingTime);
        SetGameBoardString();
    }
    public override void SetMouseButton()
    {
        Map_Creater_Manager.Instance.ChooseStuckObject(this);
        Map_Creater_Manager.Instance.ChooseTrap(this);
        Canvas_Manager.Instance.OpenBaseSetting(true);
        Canvas_Manager.Instance.CloseSettingPanels();
        Canvas_Manager.Instance.OpenPanelSettingTimerActive(false);
        Canvas_Manager.Instance.OpenPanelSettingTimer(true);
        Canvas_Manager.Instance.CloseSettingPanels();
        Canvas_Manager.Instance.OpenTriggerForObject(true);
        Canvas_Manager.Instance.SetForTrigger(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (other.transform.TryGetComponent(out Character_Base charBase))
        {
            if (!myCharacterList.Contains(charBase))
            {
                if (activeted || alwaysActivated)
                {
                    CharacterEntered(charBase);
                }
                myCharacterList.Add(charBase);
            }
        }
    }
    public virtual void CharacterEntered(Character_Base charBase)
    {
    }
    private void OnTriggerExit(Collider other)
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (other.transform.TryGetComponent(out Character_Base charBase))
        {
            if (myCharacterList.Contains(charBase))
            {
                myCharacterList.Remove(charBase);
            }
        }
    }
}