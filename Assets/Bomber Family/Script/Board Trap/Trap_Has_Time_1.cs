using UnityEngine;

public class Trap_Has_Time_1 : Trap_Base, ISetWaitingTime
{
    // Boarddaki herhangi bir enemyden üretir.
    public float waitingTime = 5;
    public float waitingTimeNext;

    public BoardTrapTimer1 boardTrapTimer1 = new BoardTrapTimer1();

    public override void SetTrap()
    {
        if (string.IsNullOrEmpty(Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardSpecial))
        {
            return;
        }
        BoardContainer boardContainer = JsonUtility.FromJson<BoardContainer>(Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardSpecial);

        boardTrapTimer1 = JsonUtility.FromJson<BoardTrapTimer1>(boardContainer.boardData.boardString);

        waitingTime = boardTrapTimer1.BoardObject.myWaitingTime;
    }
    public void SetGameBoardString()
    {
        BoardContainer boardContainer = new BoardContainer();
        boardContainer.boardData = new BoardData();

        boardContainer.boardData.trapType = TrapType.Home;
        string jsonTriggerData = JsonUtility.ToJson(boardTrapTimer1, true);
        boardContainer.boardData.boardString = jsonTriggerData;

        string jsonBoardTriggerData = JsonUtility.ToJson(boardContainer, true);
        Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardSpecial = jsonBoardTriggerData;
    }
    public void SetWaitingTime(float waitingTime)
    {
        boardTrapTimer1.BoardObject.myWaitingTime = waitingTime;
        SetGameBoardString();
    }
    public override void SetMouseButton()
    {
        Map_Creater_Manager.Instance.ChooseStuckObject(this);
        Map_Creater_Manager.Instance.ChooseTrap(this);
        Canvas_Manager.Instance.OpenBaseSetting(true);
        Canvas_Manager.Instance.CloseSettingPanels();
        Canvas_Manager.Instance.SetForTimer1(true);
    }
}