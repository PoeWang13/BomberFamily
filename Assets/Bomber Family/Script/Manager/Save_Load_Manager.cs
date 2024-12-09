﻿using System;
using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

[Serializable]
public class LevelBoard
{
    public int magicStone;
    public Vector2Int levelSize;
    public List<Board> levelBoard = new List<Board>();
}
[Serializable]
public class MyBoard
{
    public List<LevelBoard> allLevels = new List<LevelBoard>();
}
public enum BoardSaveType
{
    GameLevel,
    MyLevel,
    SecretLevel
}
[Serializable]
public class CharacterStat
{
    public int myLife = 1;
    public int mySpeed = 1;
    public int myBombAmount = 1;
    public int myBombPower = 1;
    public int myBombFireLimit = 1;
    public float myBombFireTime = 1;
    public int myBombBoxPassing = 1;
    public float myBombPushingTime = 1;
}
[Serializable]
public class PlayerData
{
    /* 
     * Player_Simple        : Normal başlangıç karakteri
     * Player_Natural       : Daha çok can toplayabilen
     * Player_Stronger      : Daha çok vuran
     * Player_Bomber        : Daha çok bomba atan
     * Player_Flash         : Daha çok hızlanabilen
     * Player_Arsonist      : Daha çok ateş bırabilen
     * Player_Defender      : Daha çok shield kullanabilen
     * 
     * Player_Ghost         : 1 Kutunun içinden geçebilen
     * Player_Broker        : 1 Kutunun ötesinede ateş bırakabilen
     * Player_Thrower       : Fırlatılan bomba atabilen
     * Player_Timer         : Sadece clock bombası bırakan
     * Player_Radioactiv    : Sadece nucleer bombası bırakan
     * Player_Area          : Sadece area bombası bırakan
     * Player_Anti_Wall     : Sadece anti wall bombası bırakan
     * Player_Searcher      : Sadece searcher bombası bırakan
     * 
     */

    [Header("Player")]
    public bool playerBuyed;
    public int playerStatAmount;
    public int playerLevel;
    public int playerExp;
    public int playerExpMax = 100;

    [Header("Player Stat")]
    public CharacterStat playerStat = new CharacterStat();
    public PlayerData()
    {
        playerExpMax = 100;
        playerStat.myLife = 1;
        playerStat.mySpeed = 100;
        playerStat.myBombAmount = 1;
        playerStat.myBombPower = 1;
        playerStat.myBombFireLimit = 1;
        playerStat.myBombBoxPassing = 0;
        playerStat.myBombPushingTime = 3;
    }
}
public enum DailyType
{
    Gold = 0,
    Exp = 1,
    Malzeme = 2,
    Alet = 3,
    Bomb = 4,
    Bilet = 5,
}
[Serializable]
public class DailyReward
{
    public bool dailyTaked;
    public DailyType dailyType;
    public int dailyRewardOrder;
    public int dailyRewardAmount;

    public DailyReward(DailyType daily, bool taked, int rewardOrder, int rewardAmount)
    {
        dailyType = daily;
        dailyTaked = taked;
        dailyRewardOrder = rewardOrder;
        dailyRewardAmount = rewardAmount;
    }
}
[Serializable]
public class BombAmount
{
    public BombType bombType;
    public int bombAmount;
    public int bombPower;
    public int bombLimit;
    public float bombFireTime;

    public BombAmount(BombType bombType, int bombAmount)
    {
        this.bombType = bombType;
        this.bombAmount = bombAmount;
        this.bombPower = 1;
        this.bombLimit = 1;
        this.bombFireTime = 1;
    }
    public BombAmount(BombType bombType, int bombAmount, int bombPower, int bombLimit, float bombFireTime)
    {
        this.bombType = bombType;
        this.bombAmount = bombAmount;
        this.bombPower = bombPower;
        this.bombLimit = bombLimit;
        this.bombFireTime = bombFireTime;
    }
    public void SetPower(int bombPower)
    {
        this.bombPower = bombPower;
    }
    public void SetLimit(int bombLimit)
    {
        this.bombLimit = bombLimit;
    }
    public void SetFireTime(float bombFireTime)
    {
        this.bombFireTime = bombFireTime;
    }
    public bool IsSameBomb(BombType type, int power, int limit, float fireTime)
    {
        if (bombType != type)
        {
            return false;
        }
        if (bombPower != power)
        {
            return false;
        }
        if (bombLimit != limit)
        {
            return false;
        }
        if (bombFireTime != fireTime)
        {
            return false;
        }
        return true;
    }
}
public enum InventoryType
{
    Material = 0,
    Alet = 1,
}
[Serializable]
public class Inventory
{
    public int inventoryOrder;
    public int inventoryAmount;
    public InventoryType inventoryType;

    public Inventory()
    {
        this.inventoryOrder = -1;
    }
    public Inventory(int inventoryOrder, InventoryType inventoryType)
    {
        this.inventoryOrder = inventoryOrder;
        this.inventoryAmount = 0;
        this.inventoryType = inventoryType;
    }
    public Inventory(int inventoryOrder, int inventoryAmount, InventoryType inventoryType)
    {
        this.inventoryOrder = inventoryOrder;
        this.inventoryAmount = inventoryAmount;
        this.inventoryType = inventoryType;
    }
    public void AddAmount(int amount)
    {
        inventoryAmount += amount;
    }
}
[Serializable]
public class GameData
{
    [Header("Game Stat")]
    public int gold;
    public int lastLevel;
    public int maxMyLevel;
    public int maxGameLevel;
    public int lastDay;
    public int year;

    [Header("Player Account")]
    public int playerOrder;
    public int inventorySlotAmount;
    public string accountName;
    public List<Inventory> inventory = new List<Inventory>();
    public List<BombAmount> allSpecialBomb = new List<BombAmount>();
    public List<DailyReward> dailyReward = new List<DailyReward>();
    public List<PlayerData> allPlayers = new List<PlayerData>();
    public GameData()
    {
        lastDay = -9999;

        inventorySlotAmount = 10;
        for (int e = 0; e < 10; e++)
        {
            inventory.Add(new Inventory());
        }

        // 1 gün -> Altın -> 100 - 500
        dailyReward.Add(new DailyReward(DailyType.Gold, false, 0, 100));
        // 2 gün -> Exp -> 50 - 250
        dailyReward.Add(new DailyReward(DailyType.Exp, false, 0, 50));
        // 3 gün -> Malzeme -> 1 - 10 Malzeme
        dailyReward.Add(new DailyReward(DailyType.Malzeme, false, 0, 1));
        // 4 gün -> Altın -> 500 - 1000
        dailyReward.Add(new DailyReward(DailyType.Gold, false, 0, 200));
        // 5 gün -> Exp -> 250 - 500
        dailyReward.Add(new DailyReward(DailyType.Exp, false, 0, 100));
        // 6 gün -> Malzeme -> 1 - 2 Alet
        dailyReward.Add(new DailyReward(DailyType.Alet, false, 0, 1));
        // 7 gün -> Bomba
        dailyReward.Add(new DailyReward(DailyType.Bomb, false, 1, 1));
    }
}
[Serializable]
public class LevelDatas
{
    public List<string> LevelLinks = new List<string>();
    public List<string> SecretLevelLinks = new List<string>();
}
[Serializable]
public class LevelDataContainer
{
    public LevelDatas GameDatas;
}
public class Save_Load_Manager : Singletion<Save_Load_Manager>
{
    [Header("Save-Load")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useSifre;

    [Header("Save-Type")]
    [SerializeField] private int saveGameOrder;
    [SerializeField] private BoardSaveType saveType;
    [SerializeField] private All_Item_Holder all_Item_Holder;
    public GameData gameData;

    private Save_Load_File_Data_Handler save_Load_File_Data_Handler;
    private LevelDataContainer allLevelDatas = new LevelDataContainer();
    private string driveJsonLink = "1eDluhDH_KcaMFIQgg1xkoVTePs9R_JxQ";// Jsonun download linki
    private string driveStartLink = "https://drive.google.com/uc?export=download&id=";

    public BoardSaveType SaveType { get { return saveType; } }

    public override void OnAwake()
    {
        DontDestroyOnLoad(gameObject);
        if (string.IsNullOrEmpty(fileName))
        {
            #if UNITY_EDITOR_64
            Debug.LogError("Save_Load scriptinde fileName boş olamaz.");
            //UnityEditor.EditorApplication.isPaused = true;
            #elif UNITY_EDITOR
            Debug.LogError("Save_Load scriptinde fileName boş olamaz.");
            //UnityEditor.EditorApplication.isPaused = true;
            #endif
            return;
        }
        save_Load_File_Data_Handler =
                    new Save_Load_File_Data_Handler(Application.persistentDataPath, fileName, useSifre);
        LoadGame();
    }
    private void Start()
    {
        if (Game_Manager.Instance.AreWeOnline)
        {
            StartCoroutine(GetGamesData(driveStartLink + driveJsonLink));
        }
    }
    #region Download Board Fonksiyon
    IEnumerator GetGamesData(string url)
    {
        if (!string.IsNullOrEmpty(gameData.accountName))
        {
            Warning_Manager.Instance.ShowMessage("We are checking your levels. Wait a second please...", 3);
        }
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);

        yield return unityWebRequest.SendWebRequest();
        UnityWebRequest.Result result = unityWebRequest.result;
        if (result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Bilgi gelmedi. Hata : " + unityWebRequest.error);
        }
        else
        {
            try
            {
                allLevelDatas = JsonUtility.FromJson<LevelDataContainer>(unityWebRequest.downloadHandler.text);
                gameData.maxGameLevel = allLevelDatas.GameDatas.LevelLinks.Count;
                StartCoroutine(GetLastLevelsData(gameData.lastLevel, allLevelDatas.GameDatas.LevelLinks[gameData.lastLevel]));
                StartCoroutine(GetLevelsData());
                StartCoroutine(GetSecretLevelsData());
            }
            catch
            {
                Debug.LogError(gameData.lastLevel + " nolu level bulunmuyor. Bu sorunu düzeltmen lazım.");
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        unityWebRequest.Dispose();
    }
    IEnumerator GetLevelsData()
    {
        int order = 0;
        while (allLevelDatas.GameDatas.LevelLinks.Count > 0)
        {
            if (File.Exists(Application.persistentDataPath + "/Game-Level/Game-Level-" + order + ".kimex"))
            {
                allLevelDatas.GameDatas.LevelLinks.RemoveAt(0);
                order++;
            }
            else
            {
                UnityWebRequest unityWebRequest = UnityWebRequest.Get(driveStartLink + allLevelDatas.GameDatas.LevelLinks[0]);
                yield return unityWebRequest.SendWebRequest();
                UnityWebRequest.Result result = unityWebRequest.result;
                if (result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("Bilgi gelmedi. Hata : " + unityWebRequest.error);
                }
                else
                {
                    try
                    {
                        // Dosya var yani resim indirilmiş.
                        LevelBoard levelBoard = JsonUtility.FromJson<LevelBoard>(unityWebRequest.downloadHandler.text);
                        string levelJson = JsonUtility.ToJson(levelBoard);
                        File.WriteAllText(Application.persistentDataPath + "/Game-Level/Game-Level-" + order + ".kimex", levelJson);
                        allLevelDatas.GameDatas.LevelLinks.RemoveAt(0);
                        order++;
                    }
                    catch
                    {
                        Debug.LogError("Bilgi gelmedi. Hata : " + unityWebRequest.error);
                    }
                }
                unityWebRequest.Dispose();
            }
        }
    }
    IEnumerator GetSecretLevelsData()
    {
        int order = 0;
        while (allLevelDatas.GameDatas.SecretLevelLinks.Count > 0)
        {
            if (File.Exists(Application.persistentDataPath + "/Secret-Level/Secret-Level-" + order + ".kimex"))
            {
                allLevelDatas.GameDatas.LevelLinks.RemoveAt(0);
                order++;
            }
            else
            {
                UnityWebRequest unityWebRequest = UnityWebRequest.Get(driveStartLink + allLevelDatas.GameDatas.SecretLevelLinks[0]);
                yield return unityWebRequest.SendWebRequest();
                UnityWebRequest.Result result = unityWebRequest.result;
                if (result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("Bilgi gelmedi. Hata : " + unityWebRequest.error);
                }
                else
                {
                    try
                    {
                        // Dosya var yani resim indirilmiş.
                        LevelBoard levelBoard = JsonUtility.FromJson<LevelBoard>(unityWebRequest.downloadHandler.text);
                        string levelJson = JsonUtility.ToJson(levelBoard);
                        File.WriteAllText(Application.persistentDataPath + "/Secret-Level/Secret-Level-" + order + ".kimex", levelJson);
                        allLevelDatas.GameDatas.LevelLinks.RemoveAt(0);
                        order++;
                    }
                    catch
                    {
                        Debug.LogError("Bilgi gelmedi. Hata : " + unityWebRequest.error);
                    }
                }
                unityWebRequest.Dispose();
            }
        }
    }
    IEnumerator GetLastLevelsData(int lastLevel, string linkLevel)
    {
        if (File.Exists(Application.persistentDataPath + "/Game-Level/Game-Level-" + lastLevel + ".kimex"))
        {
            // Canvası başlat
            Canvas_Manager.Instance.GameStart();
        }
        else
        {
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(driveStartLink + linkLevel);
            yield return unityWebRequest.SendWebRequest();
            UnityWebRequest.Result result = unityWebRequest.result;
            if (result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogWarning("Bilgi gelmedi. Hata : " + unityWebRequest.error);
            }
            else
            {
                try
                {
                    // Dosya var yani resim indirilmiş.
                    LevelBoard levelBoard = JsonUtility.FromJson<LevelBoard>(unityWebRequest.downloadHandler.text);
                    string levelJson = JsonUtility.ToJson(levelBoard);
                    File.WriteAllText(Application.persistentDataPath + "/Game-Level/Game-Level-" + lastLevel + ".kimex", levelJson);
                    // Canvası başlat
                    Canvas_Manager.Instance.GameStart();
                    if (!string.IsNullOrEmpty(gameData.accountName))
                    {
                        Warning_Manager.Instance.ShowMessage("You can play to continue last LEVEL.", 3);
                    }
                }
                catch
                {
                    Debug.LogError("Bilgi gelmedi. Hata : " + unityWebRequest.error);
                }
            }
            unityWebRequest.Dispose();
        }
    }
    #endregion

    #region Save-Load Board Fonksiyon
    public void SaveBoard(LevelBoard levelBoard)
    {
        // Dosya yazılıyor.
        string levelJson = JsonUtility.ToJson(levelBoard);
        if (saveType == BoardSaveType.MyLevel)
        {
            if (File.Exists(Application.persistentDataPath + "/My-Level/My-Level-" + gameData.maxMyLevel + ".kimex"))
            {
                Warning_Manager.Instance.ShowMessage("Your level order is wrong check save data files.", 2);
                return;
            }
            Warning_Manager.Instance.ShowMessage("Saved level board.", 2);
            File.WriteAllText(Application.persistentDataPath + "/My-Level/My-Level-" + gameData.maxMyLevel + ".kimex", levelJson);
            gameData.maxMyLevel++;
        }
        else if (saveType == BoardSaveType.SecretLevel)
        {
            if (File.Exists("C:/Users/90545/Desktop/Secret-Level-" + saveGameOrder + ".kimex"))
            {
                Warning_Manager.Instance.ShowMessage("Your level order is wrong check save data files.", 2);
                return;
            }
            Warning_Manager.Instance.ShowMessage("Saved level board. Check level files.", 2);
            File.WriteAllText("C:/Users/90545/Desktop/Secret-Level-" + saveGameOrder + ".kimex", levelJson);
        }
        else if (saveType == BoardSaveType.GameLevel)
        {
            if (File.Exists("C:/Users/90545/Desktop/Game-Level-" + saveGameOrder + ".kimex"))
            {
                Warning_Manager.Instance.ShowMessage("Your level order is wrong check save data files.", 2);
                return;
            }
            Warning_Manager.Instance.ShowMessage("Saved level board. Check level files.", 2);
            File.WriteAllText("C:/Users/90545/Desktop/Game-Level-" + saveGameOrder + ".kimex", levelJson);
        }
        SaveGame();
    }
    public LevelBoard LoadBoard(BoardSaveType boardSaveType, int order)
    {
        if (boardSaveType == BoardSaveType.MyLevel)
        {
            if (File.Exists(Application.persistentDataPath + "/My-Level/My-Level-" + order + ".kimex"))
            {
                // Dosya var.
                string levelJson = File.ReadAllText(Application.persistentDataPath + "/My-Level/My-Level-" + order + ".kimex");
                LevelBoard levelBoard = JsonUtility.FromJson<LevelBoard>(levelJson);
                return levelBoard;
            }
            else
            {
                Warning_Manager.Instance.ShowMessage("We can not found your Level so we give Last Your Level. Check save data files.", 2);
                string levelJson = File.ReadAllText(Application.persistentDataPath + "/My-Level/My-Level-" + gameData.maxMyLevel + ".kimex");
                LevelBoard levelBoard = JsonUtility.FromJson<LevelBoard>(levelJson);
                return levelBoard;
            }
        }
        else if (boardSaveType == BoardSaveType.SecretLevel)
        {
            if (File.Exists(Application.persistentDataPath + "/Secret-Level/Secret-Level-" + order + ".kimex"))
            {
                // Dosya var.
                string levelJson = File.ReadAllText(Application.persistentDataPath + "/Secret-Level/Secret-Level-" + order + ".kimex");
                LevelBoard levelBoard = JsonUtility.FromJson<LevelBoard>(levelJson);
                return levelBoard;
            }
            else
            {
                Warning_Manager.Instance.ShowMessage("We can not found your Level so we give Last Secret Level. Check save data files.", 2);
                string levelJson = File.ReadAllText(Application.persistentDataPath + "/Secret-Level/Secret-Level-" + order + ".kimex");
                LevelBoard levelBoard = JsonUtility.FromJson<LevelBoard>(levelJson);
                return levelBoard;
            }
        }
        else if (boardSaveType == BoardSaveType.GameLevel)
        {
            if (File.Exists(Application.persistentDataPath + "/Game-Level/Game-Level-" + order + ".kimex"))
            {
                // Dosya var.
                string levelJson = File.ReadAllText(Application.persistentDataPath + "/Game-Level/Game-Level-" + order + ".kimex");
                LevelBoard levelBoard = JsonUtility.FromJson<LevelBoard>(levelJson);
                return levelBoard;
            }
            else
            {
                Warning_Manager.Instance.ShowMessage("You have finished the game. The last level has been reloaded.", 2);
                string levelJson = File.ReadAllText(Application.persistentDataPath + "/Game-Level/Game-Level-" + (gameData.maxGameLevel - 1) + ".kimex");
                LevelBoard levelBoard = JsonUtility.FromJson<LevelBoard>(levelJson);
                return levelBoard;
            }
        }
        return null;
    }
    #endregion

    #region Save-Load Game Fonksiyon
    private void LoadGame()
    {
        gameData = save_Load_File_Data_Handler.LoadGame();
        if (gameData == null)
        {
            gameData = new GameData();
            Directory.CreateDirectory(Application.persistentDataPath + "/Game-Level");
            Directory.CreateDirectory(Application.persistentDataPath + "/My-Level");
            Directory.CreateDirectory(Application.persistentDataPath + "/Secret-Level");

            // Karakter tiplerini ekle
            for (int e = 0; e < all_Item_Holder.PlayerSourceList.Count; e++)
            {
                gameData.allPlayers.Add(new PlayerData());
            }
            gameData.allPlayers[0].playerBuyed = true;

            saveType = BoardSaveType.MyLevel;
        }
        else
        {
            // Karakter tiplerini ekle
            for (int e = gameData.allPlayers.Count; e < all_Item_Holder.PlayerSourceList.Count; e++)
            {
                gameData.allPlayers.Add(new PlayerData());
            }
        }
    }
    [ContextMenu("Save Game")]
    public void SaveGame()
    {
        save_Load_File_Data_Handler.SaveGame(gameData);
    }
    private void OnApplicationQuit()
    {
        //int Year = DateTime.UtcNow.Year;
        //int Month = DateTime.UtcNow.Month;
        //int Day = DateTime.UtcNow.Day;
        ////int Hour = DateTime.UtcNow.Hour + hoursFark;
        //int Hour = DateTime.UtcNow.Hour;
        //int Minute = DateTime.UtcNow.Minute;
        //int Second = DateTime.UtcNow.Second;

        //if (Hour > 24)
        //{
        //    Hour -= 24;
        //    Day += 1;
        //}
        //if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12)
        //{
        //    if (Day > 31)
        //    {
        //        Day -= 31;
        //        Month += 1;
        //    }
        //}
        //if (Month == 4 || Month == 6 || Month == 9 || Month == 11)
        //{
        //    if (Day > 30)
        //    {
        //        Day -= 30;
        //        Month += 1;
        //    }
        //}
        //if (Month == 2)
        //{
        //    if (Day > 28)
        //    {
        //        Day -= 28;
        //        Month += 1;
        //    }
        //}
        //if (Month > 12)
        //{
        //    Month -= 12;
        //    Year += 1;
        //}
        //DateTime dateTime = new DateTime(Year, Month, Day, Hour, Minute, Second);
        ////gameData.lastOnlineDayTime = new DayTime(dateTime);

        SaveGame();
    }
    #endregion
}
public class Save_Load_File_Data_Handler
{
    // Nereye ve nasıl kayıt yapıalacağını belirleyen class
    private string directoryPath;
    private string fileName;
    private bool useSifre;
    private readonly string sifreName = "HuseyinEmreCAN";
    public Save_Load_File_Data_Handler(string directoryPath, string fileName, bool useSifre)
    {
        this.directoryPath = directoryPath;
        this.fileName = fileName;
        this.useSifre = useSifre;
    }
    public GameData LoadGame()
    {
        string fullDataPath = Path.Combine(directoryPath, fileName + ".kimex");
        GameData loadedData = null;
        if (File.Exists(fullDataPath))
        {
            try
            {
                string jsonData = "";
                using (FileStream stream = new FileStream(fullDataPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        jsonData = reader.ReadToEnd();
                    }
                }
                if (useSifre)
                {
                    jsonData = SifrelemeYap(jsonData);
                }
                loadedData = JsonUtility.FromJson<GameData>(jsonData);
            }
            catch (Exception e)
            {

                Debug.LogError("Error happining when we try to load in " + fullDataPath + "\n" + "Error is " + e);
                throw;
            }
        }
        return loadedData;
    }
    public void SaveGame(GameData gameData)
    {
        string fullDataPath = Path.Combine(directoryPath, fileName + ".kimex");
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullDataPath));
            string jsonData = JsonUtility.ToJson(gameData, true);
            if (useSifre)
            {
                jsonData = SifrelemeYap(jsonData);
            }
            using (FileStream stream = new FileStream(fullDataPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(jsonData);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error happining when we try to save in " + fullDataPath + "\n" + "Error is " + e);
        }
    }
    public string SifrelemeYap(string gameData)
    {
        string sifreliData = "";
        for (int e = 0; e < gameData.Length; e++)
        {
            sifreliData += (char)(gameData[e] ^ sifreName[e % sifreName.Length]);
        }
        return sifreliData;
    }
}