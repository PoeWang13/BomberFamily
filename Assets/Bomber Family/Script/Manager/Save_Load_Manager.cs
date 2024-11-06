using System;
using System.IO;
using UnityEngine;
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
    GameLevelBoard,
    MyLevelBoard
}
[Serializable]
public class GameData
{
    [Header("Game Stat")]
    public int gold;
    public int lastLevel;
    public int maxMyLevel;
    public int maxGameLevel;

    [Header("Player")]
    public string playerName;
    public int playerIcon;
    public int playerLevel;
    public int playerExp;
    public int playerExpMax;

    [Header("Player Stat")]
    public int life = 1;
    public int speed = 100;
    public int bombFirePower = 1;
    public int bombFireLimit = 1;

    [Header("Bomb Amount")]
    public int simpleBombAmount;
    public int clockBombAmount;
    public int nukleerBombAmount;
    public int areaBombAmount;
    public int antiWallBombAmount;
    public int searcherBombAmount;
    public GameData()
    {
    }
}
public class Save_Load_Manager : Singletion<Save_Load_Manager>
{
    [Header("Save-Load")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useSifre;
    public GameData gameData;
    private Save_Load_File_Data_Handler save_Load_File_Data_Handler;
    public override void OnAwake()
    {
        DontDestroyOnLoad(gameObject);
        if (string.IsNullOrEmpty(fileName))
        {
            #if UNITY_2022_1_OR_NEWER
            Debug.LogError("Save_Load scriptinde fileName boş olamaz.");
            UnityEditor.EditorApplication.isPaused = true;
            #endif
            return;
        }
        save_Load_File_Data_Handler =
                    new Save_Load_File_Data_Handler(Application.persistentDataPath, fileName, useSifre);
        LoadGame();
    }
    public void SaveBoard(BoardSaveType saveType, LevelBoard levelBoard)
    {
        // Dosya yazılıyor.
        string levelJson = JsonUtility.ToJson(levelBoard);
        if (saveType == BoardSaveType.MyLevelBoard)
        {
            if (File.Exists(Application.persistentDataPath + "/My-Level/My-Level-" + gameData.maxMyLevel + ".kimex"))
            {
                Warning_Manager.Instance.ShowMessage("Your level order is wrong check save data files.", 2);
                return;
            }
            File.WriteAllText(Application.persistentDataPath + "/My-Level/My-Level-" + gameData.maxMyLevel + ".kimex", levelJson);
            gameData.maxMyLevel++;
        }
        else
        {
            if (File.Exists(Application.persistentDataPath + "/Game-Level/Game-Level-" + gameData.maxGameLevel + ".kimex"))
            {
                Warning_Manager.Instance.ShowMessage("Your level order is wrong check save data files.", 2);
                return;
            }
            File.WriteAllText(Application.persistentDataPath + "/Game-Level/Game-Level-" + gameData.maxGameLevel + ".kimex", levelJson);
            gameData.maxGameLevel++;
        }
        SaveGame();
    }
    public LevelBoard LoadBoard(BoardSaveType saveType, int order)
    {
        if (saveType == BoardSaveType.MyLevelBoard)
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
                Warning_Manager.Instance.ShowMessage("We can not found your Level so we give Last Your Level. Check save data files..", 2);
                string levelJson = File.ReadAllText(Application.persistentDataPath + "/My-Level/My-Level-" + gameData.maxMyLevel + ".kimex");
                LevelBoard levelBoard = JsonUtility.FromJson<LevelBoard>(levelJson);
                return levelBoard;
            }
        }
        else
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
                Warning_Manager.Instance.ShowMessage("We can not found your Level so we give Last Game Level. Check save data files..", 2);
                string levelJson = File.ReadAllText(Application.persistentDataPath + "/Game-Level/Game-Level-" + gameData.maxGameLevel + ".kimex");
                LevelBoard levelBoard = JsonUtility.FromJson<LevelBoard>(levelJson);
                return levelBoard;
            }
        }
    }

    #region Save-Load Game Fonksiyon
    private void LoadGame()
    {
        gameData = save_Load_File_Data_Handler.LoadGame();
        if (gameData == null)
        {
            gameData = new GameData();
            Directory.CreateDirectory(Application.persistentDataPath + "/Game-Level");
            Directory.CreateDirectory(Application.persistentDataPath + "/My-Level");
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