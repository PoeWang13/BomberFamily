using System;
using System.Net;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public enum GameType
{
    None = 0,
    Menu = 1,
    Game = 2,
    MapCreate = 3
}
public class Game_Manager : Singletion<Game_Manager>
{
    public event EventHandler OnGameStart;

    [Header("Genel")]
    [SerializeField] private Pooler magicStone;
    [SerializeField] private Transform gameElements;
    [SerializeField] private Transform playerWaitingPoint;
    [SerializeField] private GameObject onlineController;
    [SerializeField] private All_Item_Holder all_Item_Holder;
    [SerializeField] private Level_Exp_Holder level_Exp_Holder;
    [SerializeField] private List<GameObject> finishParticles = new List<GameObject>();

    private bool levelStart;
    private bool areWeOnline;
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
    public Vector3 PlayerWaitingPoint { get { return playerWaitingPoint.position; } }
    public bool LevelStart { get { return levelStart; } }
    public bool AreWeOnline { get { return areWeOnline; } }
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
        DayTime();
    }
    [ContextMenu("Day Time")]
    private void DayTime()
    {
        // Yerel saati veriyor.
        try
        {
            using (var response = WebRequest.Create("http://www.google.com").GetResponse())
            {
                SetOnlineController(true);
            }
        }
        catch (WebException)
        {
            SetOnlineController(false);
        }
    }
    private void SetOnlineController(bool online)
    {
        areWeOnline = online;
        onlineController.SetActive(!areWeOnline);
        if (!areWeOnline)
        {
            Debug.Log("Nete bağlı değiliz. Her saniye kontrol et. Bağlanınca sayfayı yeniden yükle.");
            StartCoroutine(ControlConnection());
        }
    }
    IEnumerator ControlConnection()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Start()
    {
        Canvas_Manager.Instance.OnGameWin += Instance_OnGameWin;
        Canvas_Manager.Instance.OnGameLost += Instance_OnGameLost;
    }
    public void GameWin()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject rndObj = finishParticles[Random.Range(0, finishParticles.Count)];
            Vector3 rndPos = new Vector3(Player_Base.Instance.transform.position.x + Random.Range(-5, 5), 5,
                Player_Base.Instance.transform.position.z + Random.Range(-5 , 5));
            Destroy(Instantiate(rndObj, rndPos, Quaternion.identity));
        }
    }
    private void Instance_OnGameLost(object sender, EventArgs e)
    {
        GameFinish();
    }
    private void Instance_OnGameWin(object sender, EventArgs e)
    {
        GameFinish();
    }
    private void GameFinish()
    {
        levelStart = false;
        levelTime = Time.time - beginingLevelTime;
    }
    public void StartLevel()
    {
        levelStart = true;
        SetLevelStats();
        OnGameStart?.Invoke(this, EventArgs.Empty);
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
        beginingLevelTime = Time.time;
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
        int exp = Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerExp;
        int expMax = Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerExpMax;
        int level = Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerLevel;

        exp += amount;
        if (exp >= expMax)
        {
            if (level_Exp_Holder.CanIncreaseMyLevel(level))
            {
                level++;
                exp -= expMax;
            }
        }
        expMax = level_Exp_Holder.LearnMyLevelMaxExp(level);

        Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerExp = exp;
        Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerExpMax = expMax;
        Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerLevel = level;
        if (exp >= expMax)
        {
            AddExpAmount(0);
        }
        Canvas_Manager.Instance.SetLevel();
    }
    #endregion
}