using System;
using System.Net;
using UnityEngine;
using System.Collections;
using System.Globalization;
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
    public event EventHandler OnGameFinish;

    [Header("Genel")]
    [SerializeField] private Pooler magicStone;
    [SerializeField] private GameObject objLevelLost;
    [SerializeField] private Transform gameElements;
    [SerializeField] private Transform playerWaitingPoint;
    [SerializeField] private GameObject onlineController;
    [SerializeField] private GameObject levelLostParticle;
    [SerializeField] private Material materialLevelFinish;
    [SerializeField] private Material materialLevelOpen;
    [SerializeField] private Material materialLevelLost;
    [SerializeField] private All_Item_Holder all_Item_Holder;
    [SerializeField] private Level_Exp_Holder level_Exp_Holder;
    [SerializeField] private List<GameObject> finishParticles = new List<GameObject>();
    public List<Level_Opener> levelOpenerList = new List<Level_Opener>();

    [Header("Pooler")]
    [SerializeField] private Pooler antiPooler;
    [SerializeField] private Pooler areaPooler;
    [SerializeField] private Pooler clockPooler;
    [SerializeField] private Pooler nucleerPooler;
    [SerializeField] private Pooler searcherPooler;
    [SerializeField] private Pooler elektroPooler;
    [SerializeField] private Pooler lavPooler;
    [SerializeField] private Pooler buzPooler;
    [SerializeField] private Pooler sisPooler;
    [SerializeField] private Pooler zehirPooler;

    private bool levelStart;
    private bool areWeOnline;
    private float levelTime;
    private float beginingLevelTime;

    private int earnExp;
    private int earnGold;
    private int brokeBoxAmont;
    private int useBombAmont;
    private int loseLifeAmont;
    private int activeTrapAmont;
    private int caughtTrapAmont;
    private int killingEnemyAmont;

    private GameType gameType;
    private DateTime dateTime;
    private Transform bombParent;
    private Transform boardLootParent;
    private List<PoolObje> lootObjects = new List<PoolObje>();

    public GameType GameType { get { return gameType; } }
    public Vector3 PlayerWaitingPoint { get { return playerWaitingPoint.position; } }
    public Material MaterialLevelOpen { get { return materialLevelOpen; } }
    public bool LevelStart { get { return levelStart; } }
    public bool AreWeOnline { get { return areWeOnline; } }
    public float LevelTime { get { return levelTime; } }
    public int DayOfYear { get { return dateTime.DayOfYear; } }
    public int Year { get { return dateTime.Year; } }
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
        bombParent = Utils.MakeChieldForGameElement("Board_Bomb");
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
                dateTime = DateTime.ParseExact(response.Headers["date"], "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal);
                SetOnlineController(true);
            }
        }
        catch (WebException)
        {
            SetOnlineController(false);
        }
    }
    [ContextMenu("Set Level Open Order")]
    private void SetLevelOpenOrder()
    {

        for (int e = 0; e < levelOpenerList.Count; e++)
        {
            levelOpenerList[e].SetOpenerOrder(e);
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
    public List<Pooler> LearnBossEnemyForSameWorld(WorldType worldType)
    {
        List<Pooler> bossEnemies = new List<Pooler>();
        for (int e = 0; e < all_Item_Holder.BossEnemyList.Count; e++)
        {
            if (worldType == (all_Item_Holder.BossEnemyList[e] as Item_Character).MyWorldType)
            {
                bossEnemies.Add(all_Item_Holder.BossEnemyList[e].MyPool);
            }
        }
        return bossEnemies;
    }
    public List<Pooler> LearnEnemyForSameWorld(WorldType worldType)
    {
        List<Pooler> enemies = new List<Pooler>();
        for (int e = 0; e < all_Item_Holder.EnemyList.Count; e++)
        {
            if (worldType == (all_Item_Holder.EnemyList[e] as Item_Character).MyWorldType)
            {
                enemies.Add(all_Item_Holder.EnemyList[e].MyPool);
            }
        }
        return enemies;
    }
    public void GameWin()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject rndObj = finishParticles[Random.Range(0, finishParticles.Count)];
            Vector3 rndPos = new Vector3(Player_Base.Instance.transform.position.x + Random.Range(-5, 5), 5,
                Player_Base.Instance.transform.position.z + Random.Range(-5 , 5));
            Destroy(Instantiate(rndObj, rndPos, Quaternion.identity), 3);
        }
        int earnGold = Random.Range(2, 10);
        AddGoldAmount(earnGold);
    }
    private void Instance_OnGameLost(object sender, EventArgs e)
    {
        // Kuru kafayı göster ve yerleştir
        levelLostParticle.SetActive(true);
        levelLostParticle.transform.position = levelOpenerList[Save_Load_Manager.Instance.gameData.lastLevel].transform.position;
        // Materiali değiştir
        levelOpenerList[Save_Load_Manager.Instance.gameData.lastLevel].LevelFinishLost(materialLevelLost);
        GameFinish();
    }
    private void Instance_OnGameWin(object sender, EventArgs e)
    {
        levelOpenerList[Save_Load_Manager.Instance.gameData.lastLevel].LevelFinish(materialLevelOpen);
        levelOpenerList[Save_Load_Manager.Instance.gameData.lastLevel + 1].SetOpener(materialLevelFinish);
        GameFinish();
    }
    private void GameFinish()
    {
        levelStart = false;
        OnGameFinish?.Invoke(this, EventArgs.Empty);
        levelTime = Time.time - beginingLevelTime;
    }
    public void StartLevel()
    {
        levelStart = true;
        SetLevelStats();
        beginingLevelTime = Time.time;
        levelLostParticle.SetActive(false);
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
    }
    #endregion

    #region Stats
    private void SetLevelStats()
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
        Canvas_Manager.Instance.SetGoldSmooth(amount);
    }
    [ContextMenu("Add Exp")]
    private void ExpAdd()
    {
        AddExpAmount(33);
    }
    public void AddExpAmount(int amount)
    {
        earnExp += amount;
        GiveToPlayerExp(amount);
    }
    public void GiveToPlayerExp(int exp)
    {
        int playerExp = Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerExp;
        int playerExpMax = Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerExpMax;
        int playerLevel = Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerLevel;

        playerExp += exp;
        bool canUpgrade = true;
        while (canUpgrade)
        {
            if (level_Exp_Holder.CanIncreaseMyLevel(playerLevel))
            {
                if (playerExp >= playerExpMax)
                {
                    playerLevel++;
                    playerExp -= playerExpMax;
                    playerExpMax = level_Exp_Holder.LearnMyLevelMaxExp(playerLevel);
                    Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerStatAmount++;
                    canUpgrade = playerExp >= playerExpMax;
                }
                else
                {
                    canUpgrade = false;
                }
            }
            else
            {
                canUpgrade = false;
            }
        }
        Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerExp = playerExp;
        Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerExpMax = playerExpMax;
        Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerLevel = playerLevel;

        Canvas_Manager.Instance.SetLevel();
    }
    #endregion

    #region Use Special Bomb
    public void UseAntiBomb(Character_Base owner)
    {
        SetBomb(owner, antiPooler);
    }
    public void UseAreaBomb(Character_Base owner)
    {
        SetBomb(owner, areaPooler);
    }
    public void UseClockBomb(Character_Base owner)
    {
        SetBomb(owner, clockPooler);
    }
    public void UseNuckleerBomb(Character_Base owner)
    {
        SetBomb(owner, nucleerPooler);
    }
    public void UseSearcherBomb(Character_Base owner)
    {
        SetBomb(owner, searcherPooler);
    }
    public void UseElektroBomb(Character_Base owner)
    {
        SetBomb(owner, elektroPooler);
    }
    public void UseLavBomb(Character_Base owner)
    {
        SetBomb(owner, lavPooler);
    }
    public void UseBuzBomb(Character_Base owner)
    {
        SetBomb(owner, buzPooler);
    }
    public void UseSisBomb(Character_Base owner)
    {
        SetBomb(owner, sisPooler);
    }
    public void UseZehirBomb(Character_Base owner)
    {
        SetBomb(owner, zehirPooler);
    }
    private void SetBomb(Character_Base owner, Pooler pool, bool isSearcher = false)
    {
        Vector3Int ownerPos = owner.LearnIntDirection(owner.transform.position);
        (PoolObje, bool) bombItem = pool.HavuzdanObjeIste_Kontrol(ownerPos);
        Bomb_Base bomb = bombItem.Item1.GetComponent<Bomb_Base>();
        bomb.SetBomb(owner, isSearcher);
        bomb.SetBoardCoor(new Vector2Int(ownerPos.x, ownerPos.z));
        if (bombItem.Item2)
        {
            bombParent.transform.SetParent(bombParent);
        }
    }
    #endregion
}