using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public enum GameType
{
    None = 0,
    Menu = 1,
    Game = 2,
    MapCreate = 3
}
public class Game_Manager : Singletion<Game_Manager>
{
    [Header("Genel")]
    [SerializeField] private Pooler magicStone;
    [SerializeField] private Player_Base player;
    [SerializeField] private Transform gameElements;

    private bool levelStart;
    private float levelTime;
    private float beginingLevelTime;

    private int killingEnemyAmont;
    private int brokeBoxAmont;
    private int useBombAmont;
    private int loseLifeAmont;
    private int activeTrapAmont;
    private int caughtTrapAmont;
    private int earnGold;
    private int earnExp;

    private GameType gameType;
    private Transform boardLootParent;
    private List<PoolObje> lootObjects = new List<PoolObje>();

    public GameType GameType { get { return gameType; } }
    public bool LevelStart { get { return levelStart; } }
    public float LevelTime { get { return levelTime; } }
    public int KillingEnemyAmont { get { return killingEnemyAmont; } }
    public int BrokeBoxAmont { get { return brokeBoxAmont; } }
    public int UseBombAmont { get { return useBombAmont; } }
    public int LoseLifeAmont { get { return loseLifeAmont; } }
    public int ActiveTrapAmont { get { return activeTrapAmont; } }
    public int CaughtTrapAmont { get { return caughtTrapAmont; } }
    public int EarnGold { get { return earnGold; } }
    public int EarnExp { get { return earnExp; } }

    public override void OnAwake()
    {
        Utils.SetAllParents(gameElements);
        boardLootParent = Utils.MakeChieldForGameElement("Board_Loot");
    }
    private void Start()
    {
        Canvas_Manager.Instance.OnGameWin += Instance_OnGameWin;
        Canvas_Manager.Instance.OnGameLost += Instance_OnGameLost;
    }
    private void Instance_OnGameLost(object sender, System.EventArgs e)
    {
        levelStart = false;
        levelTime = Time.time - beginingLevelTime;
    }
    private void Instance_OnGameWin(object sender, System.EventArgs e)
    {
        levelStart = false;
        levelTime = Time.time - beginingLevelTime;
    }
    public void SetLevelStart()
    {
        levelStart = true;
    }
    public void CreateMagicStone(Vector3 pos)
    {
        Map_Holder.Instance.MagicStoneObjects.Add(magicStone.HavuzdanObjeIste(pos));
    }
    public void AddLootObhjectList(PoolObje poolObje)
    {
        poolObje.transform.SetParent(boardLootParent);
        lootObjects.Add(poolObje);
    }
    public void SendToPoolAllObjects()
    {
        for (int e = 0; e < lootObjects.Count; e++)
        {
            if (lootObjects[e].gameObject.activeSelf)
            {
                lootObjects[e].EnterHavuz();
            }
        }
    }


    #region Set
    public void SetGameType(GameType type)
    {
        gameType = type;
    }
    public void SetPlayer(bool isActive)
    {
        player.gameObject.SetActive(isActive);
    }
    #endregion

    #region Stats
    public void SetLevelStats()
    {
        killingEnemyAmont = 0;
        brokeBoxAmont = 0;
        useBombAmont = 0;
        loseLifeAmont = 0;
        earnGold = 0;
        earnExp = 0;
        levelTime = Time.time;
    }
    public void AddEnemyAmount()
    {
        killingEnemyAmont++;
    }
    public void AddBoxAmount()
    {
        brokeBoxAmont++;
    }
    public void AddBombAmount()
    {
        useBombAmont++;
    }
    public void AddLoseLifeAmount(int amount)
    {
        loseLifeAmont += amount;
    }
    public void AddActiveTrapAmount()
    {
        activeTrapAmont++;
    }
    public void AddCaughtTrapAmount()
    {
        caughtTrapAmont++;
    }
    public void AddGoldAmount(int amount)
    {
        earnGold += amount;
    }
    public void AddExpAmount(int amount)
    {
        earnExp += amount;
    }
    #endregion
}