using System.Net;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum GameType
{
    None = 0,
    Menu = 1,
    Game = 2,
    MapCreate = 3
}
public class Game_Manager : Singletion<Game_Manager>
{
    [SerializeField] private List<ParticleSystem> level1 = new List<ParticleSystem>();
    [SerializeField] private List<ParticleSystem> level2 = new List<ParticleSystem>();
    [SerializeField] private List<ParticleSystem> level3 = new List<ParticleSystem>();
    [ContextMenu("Level 1 Particle")]
    private void SetParticle1()
    {
        level1.ForEach(x => x.Play());
    }
    [ContextMenu("Level 2 Particle")]
    private void SetParticle2()
    {
        level2.ForEach(x => x.Play());
    }
    [ContextMenu("Level 3 Particle")]
    private void SetParticle3()
    {
        level3.ForEach(x => x.Play());
    }


    [Header("Genel")]
    [SerializeField] private Pooler magicStone;
    [SerializeField] private Transform gameElements;
    [SerializeField] private Transform playerWaitingPoint;
    [SerializeField] private GameObject onlineController;
    [SerializeField] private All_Item_Holder all_Item_Holder;

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        DayTime();
    }
    private bool CheckAreWeOnline()
    {
        // Internet var mı?
        if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            //Debug.Log("İnternet adsl ile var.");
            SetOnlineController(true);
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            //Debug.Log("İnternet telefon hattı ile var.");
            SetOnlineController(true);
        }
        else if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //Debug.Log("İnternet yok.");
            SetOnlineController(false);
        }
        return areWeOnline;
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
        Debug.LogWarning(pos);
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