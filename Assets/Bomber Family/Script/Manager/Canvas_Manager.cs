using TMPro;
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[Serializable]
public class BombRect
{
    public BombType bombType;
    public Button buttonBomb;
    public RectTransform rectBomb;

    public BombRect(BombRect bombRect)
    {
        this.bombType = bombRect.bombType;
        this.rectBomb = bombRect.rectBomb;
        this.buttonBomb = bombRect.buttonBomb;
    }
}
public enum BombType
{
    Simple,
    Clock,
    Nucleer,
    Area,
    Anti,
    Searcher
}
public class Canvas_Manager : Singletion<Canvas_Manager>
{
    public event EventHandler OnGameWin;
    public event EventHandler OnGameLost;

    [Header("Genel")]
    [SerializeField] private bool saveForGame;
    [SerializeField] private Transform sceneMaskedImage;
    [SerializeField] private All_Item_Holder all_Item_Holder;
    [SerializeField] private TextMeshProUGUI textMenuGoldAmount;
    [SerializeField] private TextMeshProUGUI textCreatorGoldAmount;

    [Header("Name")]
    [SerializeField] private GameObject panelName;
    [SerializeField] private TMP_InputField inputName;

    [Header("Menu")]
    [SerializeField] private GameObject panelGameFinish;
    [SerializeField] private GameObject objNextLevelButton;
    [SerializeField] private GameObject objReloadButton;
    [SerializeField] private TextMeshProUGUI textLevelResult;
    [SerializeField] private TextMeshProUGUI textLevelTime;
    [SerializeField] private TextMeshProUGUI textKillingEnemyAmontK;
    [SerializeField] private TextMeshProUGUI textBrokeBoxAmont;
    [SerializeField] private TextMeshProUGUI textUseBombAmont;
    [SerializeField] private TextMeshProUGUI textLoseLifeAmont;
    [SerializeField] private TextMeshProUGUI textActiveTrapAmont;
    [SerializeField] private TextMeshProUGUI textCaughtTrapAmont;
    [SerializeField] private TextMeshProUGUI textEarnGold;
    [SerializeField] private TextMeshProUGUI textEarnExp;

    [Header("Menu")]
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelShop;
    [SerializeField] private GameObject panelMap;
    [SerializeField] private Button buttonMyLevel;
    [SerializeField] private Transform mapButtonsParent;
    [SerializeField] private Transform myLevelButtonParent;
    [SerializeField] private List<GameObject> maps = new List<GameObject>();

    [Header("Creator")]
    [SerializeField] private GameObject panelCreator;
    [SerializeField] private GameObject panelBoardSize;
    [SerializeField] private GameObject panelProcess;
    [SerializeField] private TMP_InputField inputBoardSizeX;
    [SerializeField] private TMP_InputField inputBoardSizeY;
    [SerializeField] private Creator_Slot creator_Slot;
    [SerializeField] private Transform gateParent;
    [SerializeField] private Transform wallParent;
    [SerializeField] private Transform boxParent;
    [SerializeField] private Transform trapParent;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private Transform bossEnemyParent;
    [SerializeField] private Button buttonSaveMap;
    [SerializeField] private Image imageProcess;
    [SerializeField] private TextMeshProUGUI textProcess;
    [SerializeField] private GameObject objCheckingMapButton;
    [SerializeField] private GameObject panelObjectBehaviour;
    [SerializeField] private GameObject panelObjectMove;
    [SerializeField] private GameObject panelObjectDestroy;
    [SerializeField] private GameObject objSaveMapButtonsParent;
    [SerializeField] private GameObject objCreatorButtonTypeList;
    [SerializeField] private GameObject objCreatorTypeList;
    [SerializeField] private GameObject objChangeMapButton;

    [Header("Dungeon Create Setting")]
    [SerializeField] private Toggle toggleDungeonSetting;
    [SerializeField] private Button buttonDungeonCreate;
    [SerializeField] private Transform panelCreateSetting;
    [SerializeField] private Transform panelDungeonSetting;
    [SerializeField] private TMP_InputField inputDungeonSizeX;
    [SerializeField] private TMP_InputField inputDungeonSizeY;
    [SerializeField] private TMP_InputField inputWallAmount;
    [SerializeField] private TMP_InputField inputBoxAmount;
    [SerializeField] private TMP_InputField inputTrapAmount;
    [SerializeField] private TMP_InputField inputEnemyAmount;
    [SerializeField] private TMP_InputField inputBossEnemyAmount;
    [SerializeField] private TMP_InputField inputMagicStoneAmount;

    [Header("Player")]
    [SerializeField] private GameObject panelGame;
    [SerializeField] private GameObject panelPlayerSetting;
    [SerializeField] private Image imagePlayerIcon;
    [SerializeField] private TextMeshProUGUI textPlayerName;
    [SerializeField] private RectTransform rectPlayerLife;
    [SerializeField] private TextMeshProUGUI textPlayerLife;
    [SerializeField] private RectTransform rectPlayerPower;
    [SerializeField] private TextMeshProUGUI textPlayerPower;
    [SerializeField] private RectTransform rectPlayerSpeed;
    [SerializeField] private TextMeshProUGUI textPlayerSpeed;
    [SerializeField] private RectTransform rectPlayerBombFireLimit;
    [SerializeField] private TextMeshProUGUI textPlayerBombFireLimit;
    [SerializeField] private RectTransform rectPlayerSimpleBombAmount;
    [SerializeField] private TextMeshProUGUI textPlayerSimpleBombAmount;
    [SerializeField] private RectTransform rectPlayerClockBombAmount;
    [SerializeField] private TextMeshProUGUI textPlayerClockBombAmount;
    [SerializeField] private RectTransform rectPlayerNucleerBombAmount;
    [SerializeField] private TextMeshProUGUI textPlayerNucleerBombAmount;
    [SerializeField] private RectTransform rectPlayerAreaBombAmount;
    [SerializeField] private TextMeshProUGUI textPlayerAreaBombAmount;
    [SerializeField] private RectTransform rectPlayerAntiWallBombAmount;
    [SerializeField] private TextMeshProUGUI textPlayerAntiWallBombAmount;
    [SerializeField] private RectTransform rectPlayerSearcherBombAmount;
    [SerializeField] private TextMeshProUGUI textPlayerSearcherBombAmount;

    [Header("Bomb Button")]
    [SerializeField] private GameObject bombClockActiviter;
    [SerializeField] private List<BombRect> bombOrjRectList = new List<BombRect>();

    private BoardSaveType boardSaveType;
    private bool correctWallAmount;
    private bool correctBoxAmount;
    private bool correctTrapAmount;
    private bool correctEnemyAmount;
    private bool correctBossEnemyAmount;
    private bool correctMagicStoneAmount;
    private bool correctDungeonSize;

    private int goldChangedAmount;
    private int goldChangedStaertedAmount;
    private List<BombRect> bombRectList = new List<BombRect>();

    public bool ToggleDungeonSetting { get { return toggleDungeonSetting.isOn; } }

    private void Start()
    {
        //sceneMaskedImage.gameObject.SetActive(true);
        CloseMask(0.1f, () => {
            if (string.IsNullOrEmpty(Save_Load_Manager.Instance.gameData.playerName))
            {
                panelName.SetActive(true);
            }
            else
            {
                SetEverything();
            }
        });
    }
    // Canvas -> Panel-Name -> Button-Name'a atandý.
    public void SetName()
    {
        if (inputName.text.Length < 3)
        {
            Warning_Manager.Instance.ShowMessage("Your name should more than 3 letter.");
            return;
        }
        else if(inputName.text.Length > 15)
        {
            Warning_Manager.Instance.ShowMessage("Your name should less than 16 letter.");
            return;
        }
        else
        {
            Save_Load_Manager.Instance.gameData.playerName = inputName.text;
            SetEverything();
        }
    }
    private void SetEverything()
    {
        panelName.SetActive(false);
        panelMenu.SetActive(true);
        SetGold();
        SetGateCreatorSlot();
        SetWallCreatorSlot();
        SetBoxCreatorSlot();
        SetTrapCreatorSlot();
        SetEnemyCreatorSlot();
        SetBossEnemyCreatorSlot();
        SetBombRect();
        SetPlayerStats();
        SetMapButtons();
        SetMyLevelButtons();
        SetGameLevelButtons();
        SetDungeonSetting();
    }

    #region Menu
    // Canvas -> Panel-Menu -> Button-Continue'a atandý
    public void ContinueGame()
    {
        Warning_Manager.Instance.ShowMessage("Please wait. Level loading...", 3);
        panelMenu.SetActive(false);
        // Son haritanýn mapini oluþtur ve oyuna baþlat
        Game_Manager.Instance.SetGameType(GameType.Game);
        boardSaveType = BoardSaveType.GameLevelBoard;
        Map_Construct_Manager.Instance.ConstructMap(Save_Load_Manager.Instance.LoadBoard(boardSaveType, Save_Load_Manager.Instance.gameData.lastLevel));
    }
    private void SetMyLevelButtons()
    {
        for (int e = 0; e < Save_Load_Manager.Instance.gameData.maxMyLevel; e++)
        {
            Button but = Instantiate(buttonMyLevel, myLevelButtonParent);
            but.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level\n" + e;
            int order = e;
            but.onClick.AddListener(() => OpenMyLevel(order));
        }
    }
    private void OpenMyLevel(int order)
    {
        Warning_Manager.Instance.ShowMessage("Please wait. Level loading...", 3);
        // Panel kapanacak
        panelMenu.SetActive(false);
        myLevelButtonParent.parent.parent.gameObject.SetActive(false);
        Game_Manager.Instance.SetGameType(GameType.Game);
        boardSaveType = BoardSaveType.MyLevelBoard;
        Map_Construct_Manager.Instance.ConstructMap(Save_Load_Manager.Instance.LoadBoard(boardSaveType, order));
    }
    private void SetGameLevelButtons()
    {
        //for (int e = 0; e < Save_Load_Manager.Instance.gameData.maxGameLevel; e++)
        //{
        //    Button but = Instantiate(buttonMyLevel, myLevelButtonParent);
        //    but.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level\n" + e;
        //    int order = e;
        //    but.onClick.AddListener(() => OpenGameLevel(order));
        //}
    }
    private void OpenGameLevel(int order)
    {
        // Panel kapanacak
        panelMenu.SetActive(false);
        myLevelButtonParent.parent.parent.gameObject.SetActive(false);
        Game_Manager.Instance.SetGameType(GameType.Game);
        boardSaveType = BoardSaveType.GameLevelBoard;
        Map_Construct_Manager.Instance.ConstructMap(Save_Load_Manager.Instance.LoadBoard(boardSaveType, order));
    }
    private void SetMapButtons()
    {
        Transform butTr = mapButtonsParent.GetChild(0);
        butTr.gameObject.SetActive(true);
        Button buton = butTr.GetComponent<Button>();
        for (int e = 0; e < maps.Count; e++)
        {
            Button but = Instantiate(buton, mapButtonsParent);
            but.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "World - " + e;
            int order = e;
            but.onClick.AddListener(() => SetActiveMapPanel(order));
        }
        butTr.gameObject.SetActive(false);
    }
    private void SetActiveMapPanel(int order)
    {
        for (int e = 0; e < maps.Count; e++)
        {
            maps[e].SetActive(order == e);
        }
    }
    // Canvas -> Panel-Menu -> Button-Create-Map'a atandý
    public void OpenCreateMapPanel()
    {
        panelMenu.SetActive(false);
        panelCreator.SetActive(true);
        panelBoardSize.SetActive(true);
    }
    // Canvas -> Panel-Menu -> Button-Map'a atandý
    public void OpenMap()
    {
        panelMenu.SetActive(false);
        panelMap.SetActive(true);
    }
    // Canvas -> Panel-Menu -> Button-Shop'a atandý
    public void OpenShop()
    {
        panelShop.SetActive(true);
    }
    #endregion

    #region Set
    private void SetBombRect()
    {
        for (int e = 0; e < bombOrjRectList.Count; e++)
        {
            if (bombOrjRectList[e].bombType == BombType.Simple)
            {
                bombOrjRectList[e].rectBomb.GetComponent<Button>().onClick.AddListener(() => Player_Base.Instance.UseBombSimple());
                bombRectList.Add(new BombRect(bombOrjRectList[e]));
            }
            else if (bombOrjRectList[e].bombType == BombType.Clock)
            {
                bombOrjRectList[e].rectBomb.GetComponent<Button>().onClick.AddListener(() => Player_Base.Instance.UseBombClock());
                if (Save_Load_Manager.Instance.gameData.clockBombAmount> 0)
                {
                    bombRectList.Add(new BombRect(bombOrjRectList[e]));
                }
                else
                {
                    bombOrjRectList[e].rectBomb.gameObject.SetActive(false);
                }
            }
            else if (bombOrjRectList[e].bombType == BombType.Nucleer)
            {
                bombOrjRectList[e].rectBomb.GetComponent<Button>().onClick.AddListener(() => Player_Base.Instance.UseBombNucleer());
                if (Save_Load_Manager.Instance.gameData.nukleerBombAmount > 0)
                {
                    bombRectList.Add(new BombRect(bombOrjRectList[e]));
                }
                else
                {
                    bombOrjRectList[e].rectBomb.gameObject.SetActive(false);
                }
            }
            else if (bombOrjRectList[e].bombType == BombType.Area)
            {
                bombOrjRectList[e].rectBomb.GetComponent<Button>().onClick.AddListener(() => Player_Base.Instance.UseBombArea());
                if (Save_Load_Manager.Instance.gameData.areaBombAmount > 0)
                {
                    bombRectList.Add(new BombRect(bombOrjRectList[e]));
                }
                else
                {
                    bombOrjRectList[e].rectBomb.gameObject.SetActive(false);
                }
            }
            else if (bombOrjRectList[e].bombType == BombType.Anti)
            {
                bombOrjRectList[e].rectBomb.GetComponent<Button>().onClick.AddListener(() => Player_Base.Instance.UseBombAntiWall());
                if (Save_Load_Manager.Instance.gameData.antiWallBombAmount > 0)
                {
                    bombRectList.Add(new BombRect(bombOrjRectList[e]));
                }
                else
                {
                    bombOrjRectList[e].rectBomb.gameObject.SetActive(false);
                }
            }
            else if (bombOrjRectList[e].bombType == BombType.Searcher)
            {
                bombOrjRectList[e].rectBomb.GetComponent<Button>().onClick.AddListener(() => Player_Base.Instance.UseBombSearcher());
                if (Save_Load_Manager.Instance.gameData.searcherBombAmount > 0)
                {
                    bombRectList.Add(new BombRect(bombOrjRectList[e]));
                }
                else
                {
                    bombOrjRectList[e].rectBomb.gameObject.SetActive(false);
                }
            }
        }
    }
    public void SetGold()
    {
        textMenuGoldAmount.text = Save_Load_Manager.Instance.gameData.gold.ToString();
        textCreatorGoldAmount.text = Save_Load_Manager.Instance.gameData.gold.ToString();
    }
    public void SetGoldSmooth(int amount)
    {
        if (goldChangedAmount == 0)
        {
            goldChangedStaertedAmount = Save_Load_Manager.Instance.gameData.gold;
        }
        goldChangedAmount += amount;
        Save_Load_Manager.Instance.gameData.gold += amount;
        DOTween.To(value => {
            // goldu arttýr
            textMenuGoldAmount.text = (goldChangedStaertedAmount + (int)value).ToString();
            textCreatorGoldAmount.text = (goldChangedStaertedAmount + (int)value).ToString();
        }, startValue: 0, endValue: goldChangedAmount, duration: 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            goldChangedAmount = 0;
        });
    }
    #endregion

    #region Bomb
    // Canvas -> Panel-Game -> Panel-Joystick-Button -> Panel-Bomb-Settings -> Button-Bomb-Chance'a atandý
    public void UseBombChange()
    {
        int activeBomb = 0;
        for (int e = bombRectList.Count - 1; e >= 0; e--)
        {
            if (bombRectList[e].buttonBomb.interactable)
            {
                activeBomb = e;
                bombRectList[e].buttonBomb.interactable = false;
                bombRectList[e].rectBomb.sizeDelta = new Vector2(75, 75);
                if (bombRectList[e].bombType == BombType.Clock)
                {
                    if (Save_Load_Manager.Instance.gameData.clockBombAmount == 0)
                    {
                        bombRectList[e].rectBomb.gameObject.SetActive(false);
                        bombRectList.RemoveAt(e);
                        Player_Base.Instance.UseBombClockActiviter();
                    }
                }
                else if (bombRectList[e].bombType == BombType.Nucleer)
                {
                    if (Save_Load_Manager.Instance.gameData.nukleerBombAmount == 0)
                    {
                        bombRectList[e].rectBomb.gameObject.SetActive(false);
                        bombRectList.RemoveAt(e);
                    }
                }
                else if (bombRectList[e].bombType == BombType.Area)
                {
                    if (Save_Load_Manager.Instance.gameData.areaBombAmount == 0)
                    {
                        bombRectList[e].rectBomb.gameObject.SetActive(false);
                        bombRectList.RemoveAt(e);
                    }
                }
                else if (bombRectList[e].bombType == BombType.Anti)
                {
                    if (Save_Load_Manager.Instance.gameData.antiWallBombAmount == 0)
                    {
                        bombRectList[e].rectBomb.gameObject.SetActive(false);
                        bombRectList.RemoveAt(e);
                    }
                }
                else if (bombRectList[e].bombType == BombType.Searcher)
                {
                    if (Save_Load_Manager.Instance.gameData.searcherBombAmount == 0)
                    {
                        bombRectList[e].rectBomb.gameObject.SetActive(false);
                        bombRectList.RemoveAt(e);
                    }
                }
            }
        }
        int newBomb = (activeBomb - 1 + bombRectList.Count) % bombRectList.Count;
        bombRectList[newBomb].buttonBomb.interactable = true;
        bombRectList[newBomb].rectBomb.sizeDelta = new Vector2(150, 150);
        bombRectList[newBomb].rectBomb.SetAsLastSibling();
        bombClockActiviter.SetActive(bombRectList[newBomb].bombType == BombType.Clock);
    }
    // Canvas -> Panel-Game -> Panel-Joystick-Button -> Panel-Bomb-Settings -> Button-Bomb-Chance'a atandý
    public void UseBombClockActiviter()
    {
        Player_Base.Instance.UseBombClockActiviter();
    }
    public void AddBombRect(BombType bombType)
    {
        bool finded = false;
        for (int e = 0; e < bombOrjRectList.Count && !finded; e++)
        {
            if (bombOrjRectList[e].bombType == bombType)
            {
                finded = true;
                bombRectList.Add(new BombRect(bombOrjRectList[e]));
                bombOrjRectList[e].rectBomb.gameObject.SetActive(true);
            }
        }
    }
    public void RemoveBombRect(BombType bombType)
    {
        bool finded = false;
        for (int e = 0; e < bombRectList.Count && !finded; e++)
        {
            if (Save_Load_Manager.Instance.gameData.clockBombAmount == 0)
            {
                //Canvas_Manager.Instance.RemoveBombRect(BombType.Clock);
            }
            if (bombRectList[e].bombType == bombType)
            {
                finded = true;
                bombRectList.RemoveAt(e);
                bombOrjRectList[e].rectBomb.gameObject.SetActive(false);
            }
        }
    }
    #endregion

    #region Player Stats
    public void SetGamePanel(bool isActive)
    {
        panelGame.SetActive(isActive);
        Game_Manager.Instance.SetLevelStart();
    }
    public void SetPlayerSettingPanel(bool isActive)
    {
        panelPlayerSetting.SetActive(isActive);
    }
    public void SetPlayerStats()
    {
        Player_Base.Instance.ResetBase();
        SetPlayerSettingPanel(true);
        imagePlayerIcon.sprite = all_Item_Holder.PlayerIcon[Save_Load_Manager.Instance.gameData.playerIcon];
        textPlayerName.text = Save_Load_Manager.Instance.gameData.playerName;
        SetPlayerLifeText();
        SetPlayerPowerText();
        SetPlayerSpeedText();
        SetPlayerBombFireLimitText();
        SetPlayerSimpleBombAmountText();
        SetPlayerClockBombAmountText();
        SetPlayerNucleerBombAmountText();
        SetPlayerAreaBombAmountText();
        SetPlayerAntiWallBombAmountText();
        SetPlayerSearcherBombAmountText();
    }
    [ContextMenu("Life Text")]
    public void SetPlayerLifeText()
    {
        textPlayerLife.text = Player_Base.Instance.MyLife.ToString();
        textPlayerLife.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerLife.DOAnchorPos(new Vector3(60, 0, 0), 0.5f);
        });
    }
    public void SetPlayerPowerText()
    {
        textPlayerPower.text = Player_Base.Instance.BombFirePower.ToString();
        textPlayerPower.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerPower.DOAnchorPos(new Vector3(60, 0, 0), 0.5f);
        });
    }
    public void SetPlayerSpeedText()
    {
        textPlayerSpeed.text = Player_Base.Instance.MySpeed.ToString();
        textPlayerSpeed.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerSpeed.DOAnchorPos(new Vector3(60, 0, 0), 0.5f);
        });
    }
    public void SetPlayerBombFireLimitText()
    {
        textPlayerBombFireLimit.text = Player_Base.Instance.BombFireLimit.ToString();
        textPlayerBombFireLimit.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerBombFireLimit.DOAnchorPos(new Vector3(60, 0, 0), 0.5f);
        });
    }
    public void SetPlayerSimpleBombAmountText()
    {
        textPlayerSimpleBombAmount.text = Player_Base.Instance.SimpleBombAmount.ToString();
        textPlayerSimpleBombAmount.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerSimpleBombAmount.DOAnchorPos(new Vector3(60, 0, 0), 0.5f);
        });
    }
    public void SetPlayerClockBombAmountText()
    {
        textPlayerClockBombAmount.text = Save_Load_Manager.Instance.gameData.clockBombAmount.ToString();
        textPlayerClockBombAmount.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerClockBombAmount.DOAnchorPos(new Vector3(0, 0, 0), 0.5f);
        });
    }
    public void SetPlayerNucleerBombAmountText()
    {
        textPlayerNucleerBombAmount.text = Save_Load_Manager.Instance.gameData.nukleerBombAmount.ToString();
        textPlayerNucleerBombAmount.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerNucleerBombAmount.DOAnchorPos(new Vector3(0, 0, 0), 0.5f);
        });
    }
    public void SetPlayerAreaBombAmountText()
    {
        textPlayerAreaBombAmount.text = Save_Load_Manager.Instance.gameData.areaBombAmount.ToString();
        textPlayerAreaBombAmount.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerAreaBombAmount.DOAnchorPos(new Vector3(0, 0, 0), 0.5f);
        });
    }
    public void SetPlayerAntiWallBombAmountText()
    {
        textPlayerAntiWallBombAmount.text = Save_Load_Manager.Instance.gameData.antiWallBombAmount.ToString();
        textPlayerAntiWallBombAmount.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerAntiWallBombAmount.DOAnchorPos(new Vector3(0, 0, 0), 0.5f);
        });
    }
    public void SetPlayerSearcherBombAmountText()
    {
        textPlayerSearcherBombAmount.text = Save_Load_Manager.Instance.gameData.searcherBombAmount.ToString();
        textPlayerSearcherBombAmount.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerSearcherBombAmount.DOAnchorPos(new Vector3(0, 0, 0), 0.5f);
        });
    }
    #endregion

    #region Dungeon Create Setting
    private void SetDungeonSetting()
    {
        panelDungeonSetting.DOScaleX(0, 0.1f);
        panelCreateSetting.DOScaleY(0, 0.1f);
        buttonDungeonCreate.interactable = false;
    }
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Panel-InputField -> InputField-Map-Size-X ve InputField-Map-Size-Y'a atandý
    public void CheckDungeonSize()
    {
        correctDungeonSize = true;
        if (string.IsNullOrEmpty(inputBoardSizeX.text))
        {
            Warning_Manager.Instance.ShowMessage("Board Size Width input can not be empty.");
            correctDungeonSize = false;
        }
        else if (string.IsNullOrEmpty(inputBoardSizeY.text))
        {
            Warning_Manager.Instance.ShowMessage("Board Size Height input can not be empty.");
            correctDungeonSize = false;
        }
        if (int.TryParse(inputBoardSizeX.text, out int sizeX))
        {
            if (sizeX < 10)
            {
                correctDungeonSize = false;
                Warning_Manager.Instance.ShowMessage("Board Size Width can not be smallest than 10.", 3);
            }
            else if (sizeX % 2 == 0)
            {
                Warning_Manager.Instance.ShowMessage("Board Size Width can not be even. We make it odd.", 3);
                inputBoardSizeX.text = (sizeX + 1).ToString();
            }
            else if (sizeX > 100)
            {
                Warning_Manager.Instance.ShowMessage("Board Size Width can not be bigger than 100.", 3);
            }
        }
        else
        {
            correctDungeonSize = false;
            Warning_Manager.Instance.ShowMessage("Board Size Width must be number, NOT letter.");
        }
        if (int.TryParse(inputBoardSizeY.text, out int sizeY))
        {
            if (sizeY < 10)
            {
                correctDungeonSize = false;
                Warning_Manager.Instance.ShowMessage("Board Size Height can not be smallest than 10.", 3);
            }
            else if (sizeY % 2 == 0)
            {
                Warning_Manager.Instance.ShowMessage("Board Size Height can not be even. We make it odd.", 3);
                inputBoardSizeY.text = (sizeY + 1).ToString();
            }
            else if (sizeX > 100)
            {
                Warning_Manager.Instance.ShowMessage("Board Size Height can not be bigger than 100.", 3);
            }
        }
        else
        {
            correctDungeonSize = false;
            Warning_Manager.Instance.ShowMessage("Board Size Height must be number, NOT letter.");
        }
        CheckDungeonSettingCorrect();
    }
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Panel-Dungeon-Setting -> Create-Setting -> Panel-Wall-Container -> InputField-Wall-Object'a atandý
    public void SetDungeonWallObject()
    {
        correctWallAmount = false;
        if (string.IsNullOrEmpty(inputWallAmount.text))
        {
            Warning_Manager.Instance.ShowMessage("Wall input can not be empty.");
            return;
        }
        if (int.TryParse(inputWallAmount.text, out int wallAmount))
        {
            if (wallAmount < 0)
            {
                Warning_Manager.Instance.ShowMessage("The number of Wall can not be less than 0.");
                return;
            }
        }
        else
        {
            Warning_Manager.Instance.ShowMessage("The number of Wall must be number, NOT letter.");
            return;
        }
        correctWallAmount = true;
        Map_Creater_Manager.Instance.SetWallBoard(wallAmount);
        CheckDungeonSettingCorrect();
    }
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Panel-Dungeon-Setting -> Create-Setting -> Panel-Box-Container -> InputField-Box-Object'a atandý
    public void SetDungeonBoxObject()
    {
        correctMagicStoneAmount = true;
        if (string.IsNullOrEmpty(inputMagicStoneAmount.text))
        {
            Warning_Manager.Instance.ShowMessage("Magic Stone input can not be empty.");
            correctMagicStoneAmount = false;
        }
        if (int.TryParse(inputMagicStoneAmount.text, out int magicStoneAmount))
        {
            if (magicStoneAmount < 1)
            {
                Warning_Manager.Instance.ShowMessage("The number of Magic Stone must be more than 1");
                correctMagicStoneAmount = false;
            }
        }
        else
        {
            Warning_Manager.Instance.ShowMessage("The number of Magic Stone must be number, NOT letter.");
            correctMagicStoneAmount = false;
        }
        correctBoxAmount = true;
        if (string.IsNullOrEmpty(inputBoxAmount.text))
        {
            Warning_Manager.Instance.ShowMessage("Box input can not be empty.");
            correctBoxAmount = false;
        }
        if (int.TryParse(inputBoxAmount.text, out int boxAmount))
        {
            if (boxAmount < magicStoneAmount)
            {
                Warning_Manager.Instance.ShowMessage("The number of Boxes must be equal or more to the number of Magic Stone. (" + magicStoneAmount + ")");
                correctBoxAmount = false;
            }
        }
        else
        {
            Warning_Manager.Instance.ShowMessage("The number of Boxes must be number, NOT letter.");
            correctBoxAmount = false;
        }
        Map_Creater_Manager.Instance.SetBoxBoard(boxAmount);
        Map_Creater_Manager.Instance.SetMagicStoneBoard(magicStoneAmount);
        CheckDungeonSettingCorrect();
    }
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Panel-Dungeon-Setting -> Create-Setting -> Panel-Trap-Container -> InputField-Trap-Object'a atandý
    public void SetDungeonTrapObject()
    {
        correctTrapAmount = true;
        if (string.IsNullOrEmpty(inputTrapAmount.text))
        {
            Warning_Manager.Instance.ShowMessage("Trap input can not be empty.");
            correctTrapAmount = false;
        }
        if (int.TryParse(inputTrapAmount.text, out int trapAmount))
        {
            if (trapAmount < 0)
            {
                Warning_Manager.Instance.ShowMessage("The number of Trap must be more than 0");
                correctTrapAmount = false;
            }
        }
        else
        {
            Warning_Manager.Instance.ShowMessage("The number of Trap must be number, NOT letter.");
            correctTrapAmount = false;
        }
        Map_Creater_Manager.Instance.SetTrapBoard(trapAmount);
        CheckDungeonSettingCorrect();
    }
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Panel-Dungeon-Setting -> Create-Setting -> Panel-Enemy-Container -> InputField-Enemy-Object'a atandý
    public void SetDungeonEnemyObject()
    {
        correctEnemyAmount = true;
        if (string.IsNullOrEmpty(inputEnemyAmount.text))
        {
            Warning_Manager.Instance.ShowMessage("Enemy input can not be empty.");
            correctEnemyAmount = false;
        }
        if (int.TryParse(inputEnemyAmount.text, out int enemyAmount))
        {
            if (enemyAmount < 0)
            {
                Warning_Manager.Instance.ShowMessage("The number of Enemy must be more than 0");
                correctEnemyAmount = false;
            }
        }
        else
        {
            Warning_Manager.Instance.ShowMessage("The number of Enemy must be number, NOT letter.");
            correctEnemyAmount = false;
        }
        Map_Creater_Manager.Instance.SetEnemyBoard(enemyAmount);
        CheckDungeonSettingCorrect();
    }
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Panel-Dungeon-Setting -> Create-Setting -> Panel-Boss-Enemy-Container -> InputField-Boss-Enemy-Object'a atandý
    public void SetDungeonBossEnemyObject()
    {
        correctBossEnemyAmount = true;
        if (string.IsNullOrEmpty(inputBossEnemyAmount.text))
        {
            Warning_Manager.Instance.ShowMessage("Boss Enemy input can not be empty.");
            correctBossEnemyAmount = false;
        }
        if (int.TryParse(inputBossEnemyAmount.text, out int bossEnemyAmount))
        {
            if (bossEnemyAmount < 0)
            {
                Warning_Manager.Instance.ShowMessage("The number of Boss Enemy must be more than 0");
                correctBossEnemyAmount = false;
            }
        }
        else
        {
            Warning_Manager.Instance.ShowMessage("The number of Boss Enemy must be number, NOT letter.");
            correctBossEnemyAmount = false;
        }
        Map_Creater_Manager.Instance.SetBossEnemyBoard(bossEnemyAmount);
        CheckDungeonSettingCorrect();
    }
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Panel-Dungeon-Setting -> Create-Setting -> Panel-Magic-Stone-Container -> InputField-Magic-Stone-Object'a atandý
    public void SetDungeonMagicStoneObject()
    {
        correctMagicStoneAmount = true;
        if (string.IsNullOrEmpty(inputMagicStoneAmount.text))
        {
            Warning_Manager.Instance.ShowMessage("Magic Stone input can not be empty.");
            correctMagicStoneAmount = false;
        }
        if (int.TryParse(inputMagicStoneAmount.text, out int magicStoneAmount))
        {
            if (magicStoneAmount < 1)
            {
                Warning_Manager.Instance.ShowMessage("The number of Magic Stone must be more than 1");
                correctMagicStoneAmount = false;
            }
        }
        else
        {
            Warning_Manager.Instance.ShowMessage("The number of Magic Stone must be number, NOT letter.");
            correctMagicStoneAmount = false;
        }
        Map_Creater_Manager.Instance.SetMagicStoneBoard(magicStoneAmount);
        CheckDungeonSettingCorrect();
    }
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Panel-Dungeon-Setting -> Toggle-Free-Start-Random'a atandý
    public void DungeonCreateSetting(bool isOn)
    {
        CheckDungeonSettingCorrect();
        panelCreateSetting.DOScaleY(isOn ? 1 : 0, 1.0f);
    }
    private void CheckDungeonSettingCorrect()
    {
        bool isCorrect = false;
        if (toggleDungeonSetting.isOn)
        {
            if (correctWallAmount && correctMagicStoneAmount && correctTrapAmount && correctEnemyAmount
                && correctBossEnemyAmount && correctDungeonSize && correctBoxAmount)
            {
                isCorrect = true;
            }
        }
        else
        {
            if (correctMagicStoneAmount && correctDungeonSize)
            {
                isCorrect = true;
                panelDungeonSetting.DOScaleX(isCorrect ? 1 : 0, 1.0f);
            }
        }
        buttonDungeonCreate.interactable = isCorrect;
    }
    #endregion

    #region Creator
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Button-Create'a atandý.
    public void CreateMapBoard()
    {
        panelBoardSize.SetActive(false);
        objCheckingMapButton.SetActive(!saveForGame);
        buttonSaveMap.interactable = saveForGame;
        Map_Creater_Manager.Instance.SetBoardSize(int.Parse(inputBoardSizeX.text), int.Parse(inputBoardSizeY.text));
    }
    public void MapProcess(float process)
    {
        imageProcess.fillAmount = process;
        textProcess.text = (process * 100).ToString("N2");
    }
    public void SetActiveMapProcess(bool process)
    {
        panelProcess.SetActive(process);
    }
    public void SetSaveButtonForMyLevel()
    {
        if (!saveForGame)
        {
            buttonSaveMap.interactable = false;
        }
    }
    public void SetSaveButton()
    {
        panelGame.SetActive(false);
        panelCreator.SetActive(true);
        buttonSaveMap.interactable = true;
        Player_Base.Instance.SetEffectivePlayer(false);
    }
    // Canvas -> Panel-Creator -> Panel-Creator-Gold-Amount -> Button-Checking-Level'a atandý.
    public void CheckMapRules()
    {
        if (Map_Creater_Manager.Instance.CheckLevelMap())
        {
            panelMenu.SetActive(false);
            panelMap.SetActive(false);
            panelGame.SetActive(true);
            panelCreator.SetActive(false);
            SetPlayerStats();
            Player_Base.Instance.SetMove(true);
            Player_Base.Instance.SetEffectivePlayer(true);
            Player_Base.Instance.SetPosition(Vector3.zero);
            Warning_Manager.Instance.ShowMessage("Finish game for save.");
        }
    }
    // Canvas -> Panel-Creator -> Panel-Save-Map-Buttons -> Button-Change-Map'a atandý.
    public void ChangeMap()
    {
        // kayýt butonu kapatýlsýn
        buttonSaveMap.interactable = false;
        SetCreatorButton(true);

        objChangeMapButton.SetActive(false);
    }
    public void SetButtonChangeMap(bool isActive)
    {
        if (isActive)
        {
            SetCreatorButton(false);
        }
        objChangeMapButton.SetActive(isActive);
    }
    private void SetCreatorButton(bool isActive)
    {
        objCheckingMapButton.SetActive(isActive);
        // Objeler
        objCreatorButtonTypeList.SetActive(isActive);
        // Obje listesi
        objCreatorTypeList.SetActive(isActive);
    }
    // Canvas -> Panel-Creator -> Panel-Creator-Buttons -> Button-Map-Save'a atandý
    public void SaveMap()
    {
        // Save map for my level
        if (saveForGame)
        {
            // Yaptýðýmýz boardý deniyoruz.
            Map_Creater_Manager.Instance.SaveGameMap();
        }
        else
        {
            Map_Creater_Manager.Instance.SaveMyLevel();
        }
    }
    public void CanNotSaveMap()
    {
        panelGame.SetActive(false);
        panelCreator.SetActive(true);
        // kayýt butonu kapatýlsýn
        buttonSaveMap.interactable = false;
        objCheckingMapButton.SetActive(true);
        // Objeler
        objCreatorButtonTypeList.SetActive(true);
        // Obje listesi
        objCreatorTypeList.SetActive(true);
        objChangeMapButton.SetActive(false);
    }
    // Canvas -> Panel-Creator -> Panel-Creator-Buttons -> Button-Map-Release'a atandý
    public void ReleaseMap()
    {
        Map_Holder.Instance.ReleaseMap();
    }
    public void SetCreatorPanel(bool isActive)
    {
        panelCreator.SetActive(isActive);
    }
    public void SetCreatorButtons(bool isActive)
    {
        // Butonlar
        objSaveMapButtonsParent.SetActive(isActive);
        // Check level
        if (saveForGame)
        {
            objCheckingMapButton.SetActive(false);
        }
        else
        {
            objCheckingMapButton.SetActive(isActive);
        }
        // Objeler
        objCreatorButtonTypeList.SetActive(isActive);
        // OBje listesi
        objCreatorTypeList.SetActive(isActive);
    }
    public void SetPanelObjectBehaviourPanel(bool isActive, bool isActiveButtonMove = false, bool isActiveButtonDestroy = false)
    {
        panelObjectBehaviour.SetActive(isActive);
        panelObjectMove.SetActive(isActiveButtonMove);
        panelObjectDestroy.SetActive(isActiveButtonDestroy);
    }
    // Canvas -> Panel-Creator -> Panel-Objects-Behaviour -> Button-Move'a atandý
    public void MoveObject()
    {
        Map_Creater_Manager.Instance.MoveObject();
    }
    // Canvas -> Panel-Creator -> Panel-Objects-Behaviour -> Button-Turn'a atandý
    public void TurnObject()
    {
        Map_Creater_Manager.Instance.TurnObject();
    }
    // Canvas -> Panel-Creator -> Panel-Objects-Behaviour -> Button-Destroy'a atandý
    public void DestroyObject()
    {
        Map_Creater_Manager.Instance.DestroyObject();
    }
    #endregion

    #region Creator Slot
    private void SetGateCreatorSlot()
    {
        for (int e = 0; e < all_Item_Holder.GateList.Count; e++)
        {
            Creator_Slot slot = Instantiate(creator_Slot, gateParent);
            slot.SetSlot(all_Item_Holder.GateList[e]);
            slot.gameObject.SetActive(true);
        }
    }
    private void SetWallCreatorSlot()
    {
        for (int e = 0; e < all_Item_Holder.WallList.Count; e++)
        {
            Creator_Slot slot = Instantiate(creator_Slot, wallParent);
            slot.SetSlot(all_Item_Holder.WallList[e]);
            slot.gameObject.SetActive(true);
        }
    }
    private void SetBoxCreatorSlot()
    {
        for (int e = 0; e < all_Item_Holder.BoxList.Count; e++)
        {
            Creator_Slot slot = Instantiate(creator_Slot, boxParent);
            slot.SetSlot(all_Item_Holder.BoxList[e]);
            slot.gameObject.SetActive(true);
        }
    }
    private void SetTrapCreatorSlot()
    {
        for (int e = 0; e < all_Item_Holder.TrapList.Count; e++)
        {
            Creator_Slot slot = Instantiate(creator_Slot, trapParent);
            slot.SetSlot(all_Item_Holder.TrapList[e]);
            slot.gameObject.SetActive(true);
        }
    }
    private void SetEnemyCreatorSlot()
    {
        for (int e = 0; e < all_Item_Holder.EnemyList.Count; e++)
        {
            Creator_Slot slot = Instantiate(creator_Slot, enemyParent);
            slot.SetSlot(all_Item_Holder.EnemyList[e]);
            slot.gameObject.SetActive(true);
        }
    }
    private void SetBossEnemyCreatorSlot()
    {
        for (int e = 0; e < all_Item_Holder.BossEnemyList.Count; e++)
        {
            Creator_Slot slot = Instantiate(creator_Slot, bossEnemyParent);
            slot.SetSlot(all_Item_Holder.BossEnemyList[e]);
            slot.gameObject.SetActive(true);
        }
    }
    #endregion

    #region Finish Game
    public void GameWin()
    {
        LearnStats("Game Win");
        objNextLevelButton.SetActive(true);
        objReloadButton.SetActive(false);
        panelGameFinish.SetActive(true);
        OnGameWin?.Invoke(this, EventArgs.Empty);
        if (boardSaveType == BoardSaveType.GameLevelBoard)
        {
            Save_Load_Manager.Instance.gameData.lastLevel++;
        }
    }
    public void GameLost()
    {
        LearnStats("Game Lost");
        objNextLevelButton.SetActive(false);
        objReloadButton.SetActive(true);
        panelGameFinish.SetActive(true);
        OnGameLost?.Invoke(this, EventArgs.Empty);
    }
    private void LearnStats(string result)
    {
        textLevelResult.text = result;
        Player_Base.Instance.SetEffectivePlayer(false);
        int time = (int)Game_Manager.Instance.LevelTime;
        int hour = time / 3600;
        int minute = (time - (hour * 3600)) / 60;
        int second = time % 60;
        textLevelTime.text = "Time => " + hour + " : " + minute + " : " + second;
        textKillingEnemyAmontK.text = "Killing Enemy : " + Game_Manager.Instance.KillingEnemyAmont.ToString();
        textBrokeBoxAmont.text = "Broke Box : " + Game_Manager.Instance.BrokeBoxAmont.ToString();
        textUseBombAmont.text = "Use Bomb : " + Game_Manager.Instance.UseBombAmont.ToString();
        textLoseLifeAmont.text = "Lose Life : " + Game_Manager.Instance.LoseLifeAmont.ToString();
        textActiveTrapAmont.text = "Active Trap : " + Game_Manager.Instance.ActiveTrapAmont.ToString();
        textCaughtTrapAmont.text = "Caught Trap : " + Game_Manager.Instance.CaughtTrapAmont.ToString();
        textEarnGold.text = "Gold : " + Game_Manager.Instance.EarnGold.ToString();
        textEarnExp.text = "Exp : " + Game_Manager.Instance.EarnExp.ToString();
    }
    public void NextLevel()
    {
        panelGameFinish.SetActive(false);
        Map_Construct_Manager.Instance.ConstructMap(Save_Load_Manager.Instance.LoadBoard(BoardSaveType.GameLevelBoard, Save_Load_Manager.Instance.gameData.lastLevel));
    }
    public void GoMenu()
    {
        panelMenu.SetActive(true);
        panelGame.SetActive(false);
        panelGameFinish.SetActive(false);
        Map_Holder.Instance.SendToPoolAllObjects();
    }
    public void Reload()
    {
        panelGameFinish.SetActive(false);
        Map_Construct_Manager.Instance.ConstructMap(Save_Load_Manager.Instance.LoadBoard(BoardSaveType.GameLevelBoard, Save_Load_Manager.Instance.gameData.lastLevel));
    }
    #endregion

    #region Mask
    private void CloseMask(float waitingTime, Action action)
    {
        // Bekle
        DOVirtual.DelayedCall(waitingTime,
            () =>
            {
                // Close Mask
                DOTween.To(value => { sceneMaskedImage.localScale = Vector3.one * value; }, startValue: 2, endValue: 0, duration: 1.5f)
                    .SetEase(Ease.Linear).OnComplete(() =>
                    {
                        action?.Invoke();
                    });
            });
    }
    private void OpenMask(float waitingTime, Action action)
    {
        // Bekle
        DOVirtual.DelayedCall(waitingTime,
            () =>
            {
                action?.Invoke();
                // Open Mask
                DOTween.To(value => { sceneMaskedImage.localScale = Vector3.one * value; }, startValue: 0, endValue: 2, duration: 1.5f)
                .SetEase(Ease.Linear);
            });
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    #endregion
}