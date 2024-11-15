using TMPro;
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Diagnostics;

[Serializable]
public class BombRect
{
    public BombType bombType;
    public Button buttonBomb;
    public RectTransform rectBomb;

    public BombRect(BombRect bombRect)
    {
        bombType = bombRect.bombType;
        rectBomb = bombRect.rectBomb;
        buttonBomb = bombRect.buttonBomb;
    }
}
public class Canvas_Manager : Singletion<Canvas_Manager>
{
    public event EventHandler OnGameWin;
    public event EventHandler OnGameLost;

    [Header("Genel")]
    [SerializeField] private Transform sceneMaskedImage;
    [SerializeField] private All_Item_Holder all_Item_Holder;
    [SerializeField] private TextMeshProUGUI textMenuGoldAmount;
    [SerializeField] private TextMeshProUGUI textCreatorGoldAmount;

    [Header("Name")]
    [SerializeField] private GameObject panelName;
    [SerializeField] private TMP_InputField inputName;

    [Header("Game Finish")]
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
    [SerializeField] private Button buttonChooser;
    [SerializeField] private Transform wallChooseParent;
    [SerializeField] private Transform boxChooseParent;
    [SerializeField] private Transform trapChooseParent;
    [SerializeField] private Transform enemyChooseParent;
    [SerializeField] private Transform bossEnemyChooseParent;
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
    [SerializeField] private Transform panelHelp;
    [SerializeField] private Button buttonHelpCloser;
    [SerializeField] private GameObject panelPlayerSetting;
    [SerializeField] private Image imagePlayerIcon;
    [SerializeField] private Image imagePlayerLevelPercent;
    [SerializeField] private TextMeshProUGUI textPlayerExp;
    [SerializeField] private TextMeshProUGUI textPlayerLevel;
    [SerializeField] private TextMeshProUGUI textPlayerName;
    [SerializeField] private RectTransform rectPlayerLife;
    [SerializeField] private TextMeshProUGUI textPlayerLife;
    [SerializeField] private RectTransform rectPlayerPower;
    [SerializeField] private TextMeshProUGUI textPlayerPower;
    [SerializeField] private RectTransform rectPlayerSpeed;
    [SerializeField] private TextMeshProUGUI textPlayerSpeed;
    [SerializeField] private RectTransform rectPlayerBombFireLimit;
    [SerializeField] private TextMeshProUGUI textPlayerBombFireLimit;
    [SerializeField] private RectTransform rectPlayerBombAmount;
    [SerializeField] private TextMeshProUGUI textPlayerBombAmount;

    [Header("Bomb Button")]
    [SerializeField] private GameObject bombClockActiviter;

    [Header("Player Choose")]
    [SerializeField] private Joystick joystickMove;
    [SerializeField] private Button buttonPlayerBuy;
    [SerializeField] private Button buttonPlayerChoose;
    [SerializeField] private Image imageBombFire;
    [Space]
    [SerializeField] private TextMeshProUGUI textShopPlayerName;
    [SerializeField] private TextMeshProUGUI textShopPlayerPrice;
    [SerializeField] private TextMeshProUGUI textShopPlayerLevel;
    [SerializeField] private TextMeshProUGUI textShopPlayerBombType;
    [Space]
    [SerializeField] private TextMeshProUGUI textShopPlayerLife;
    [SerializeField] private TextMeshProUGUI textShopPlayerSpeed;
    [SerializeField] private TextMeshProUGUI textShopPlayerBombAmount;
    [SerializeField] private TextMeshProUGUI textShopPlayerBombPower;
    [SerializeField] private TextMeshProUGUI textShopPlayerBombFireLimit;
    [SerializeField] private TextMeshProUGUI textShopPlayerBoxPassing;
    [SerializeField] private TextMeshProUGUI textShopPlayerBoxPushingTime;
    [Space]
    [SerializeField] private TextMeshProUGUI textShopPlayerUpgradeLife;
    [SerializeField] private TextMeshProUGUI textShopPlayerUpgradeSpeed;
    [SerializeField] private TextMeshProUGUI textShopPlayerUpgradeBombAmount;
    [SerializeField] private TextMeshProUGUI textShopPlayerUpgradeBombPower;
    [SerializeField] private TextMeshProUGUI textShopPlayerUpgradeBombFireLimit;
    [SerializeField] private TextMeshProUGUI textShopPlayerUpgradeBoxPassing;
    [SerializeField] private TextMeshProUGUI textShopPlayerUpgradeBoxPushingTime;

    private bool isBuyed;
    private bool correctWallAmount;
    private bool correctBoxAmount;
    private bool correctTrapAmount;
    private bool correctEnemyAmount;
    private bool correctBossEnemyAmount;
    private bool correctMagicStoneAmount;
    private bool correctDungeonSize;

    private int playerOrder;
    private int goldChangedAmount;
    private int goldChangedStaertedAmount;

    private Player_Base player_Base;

    private List<bool> buttonCreaterWallList = new List<bool>();
    private List<bool> buttonCreaterBoxList = new List<bool>();
    private List<bool> buttonCreaterTrapList = new List<bool>();
    private List<bool> buttonCreaterEnemyList = new List<bool>();
    private List<bool> buttonCreaterBossEnemyList = new List<bool>();

    public bool ToggleDungeonSetting { get { return toggleDungeonSetting.isOn; } }

    #region Menu
    private void Start()
    {
        Game_Manager.Instance.OnGameStart += Instance_OnGameStart;
    }
    public void GameStart()
    {
        sceneMaskedImage.gameObject.SetActive(true);
        CloseMask(0.1f, () => {
            if (string.IsNullOrEmpty(Save_Load_Manager.Instance.gameData.accountName))
            {
                panelName.SetActive(true);
                panelHelp.gameObject.SetActive(true);
            }
            else
            {
                CloseHTP(false);
                SetEverything();
            }
        });
    }
    // Canvas -> Panel-Menu -> Button-Continue'a atandı
    public void ContinueGame()
    {
        Audio_Manager.Instance.PlayGameStart();
        Warning_Manager.Instance.ShowMessage("Please wait. Level loading...", 3);
        panelMenu.SetActive(false);
        // Son haritanın mapini oluştur ve oyuna başlat
        Game_Manager.Instance.SetGameType(GameType.Game);
        Map_Construct_Manager.Instance.ConstructMap(Save_Load_Manager.Instance.LoadBoard(BoardSaveType.GameLevel, Save_Load_Manager.Instance.gameData.lastLevel));
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
        Audio_Manager.Instance.PlayGameStart();
        Warning_Manager.Instance.ShowMessage("Please wait. Level loading...", 3);
        // Panel kapanacak
        panelMenu.SetActive(false);
        myLevelButtonParent.parent.parent.gameObject.SetActive(false);
        Game_Manager.Instance.SetGameType(GameType.Game);
        Map_Construct_Manager.Instance.ConstructMap(Save_Load_Manager.Instance.LoadBoard(BoardSaveType.MyLevel, order));
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
        Audio_Manager.Instance.PlayGameStart();
        // Panel kapanacak
        panelMenu.SetActive(false);
        myLevelButtonParent.parent.parent.gameObject.SetActive(false);
        Game_Manager.Instance.SetGameType(GameType.Game);
        Map_Construct_Manager.Instance.ConstructMap(Save_Load_Manager.Instance.LoadBoard(BoardSaveType.GameLevel, order));
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
    // Canvas -> Panel-Menu -> Button-Create-Map'a atandı
    public void OpenCreateMapPanel()
    {
        panelMenu.SetActive(false);
        panelCreator.SetActive(true);
        panelBoardSize.SetActive(true);
    }
    // Canvas -> Panel-Menu -> Button-Map'a atandı
    public void OpenMap()
    {
        panelMenu.SetActive(false);
        panelMap.SetActive(true);
    }
    // Canvas -> Panel-Menu -> Button-Shop'a atandı
    public void OpenShop()
    {
        panelShop.SetActive(true);
    }
    // Canvas -> Panel-Game -> Panel-Help -> Button-Close'a atandı
    public void CloseHTP(bool isOpen)
    {
        buttonHelpCloser.interactable = isOpen;
        if (isOpen)
        {
            panelHelp.gameObject.SetActive(true);
            panelHelp.DOScale(Vector3.one, 1);
        }
        else
        {
            panelHelp.DOScale(Vector3.zero, 1).OnComplete(() => panelHelp.gameObject.SetActive(false));
        }
    }
    #endregion

    #region Player Choose
    public void SetPlayerInfo()
    {
        bombClockActiviter.SetActive(all_Item_Holder.PlayerSourceList[Save_Load_Manager.Instance.gameData.playerOrder].MyBombType == BombType.Clock);
        player_Base.SetPlayerStat(joystickMove);
        isBuyed = Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerBuyed;
        buttonPlayerBuy.gameObject.SetActive(!isBuyed);
        buttonPlayerChoose.gameObject.SetActive(isBuyed);
        buttonPlayerChoose.interactable = playerOrder != Save_Load_Manager.Instance.gameData.playerOrder;
        all_Item_Holder.PlayerSourceList[playerOrder].SetSpriteOrder();
        imageBombFire.sprite = all_Item_Holder.PlayerSourceList[playerOrder].SetImageBombFireLimit(0);
        textShopPlayerName.text = all_Item_Holder.PlayerSourceList[playerOrder].MyName;
        textShopPlayerPrice.text = isBuyed ? "Buyed" : all_Item_Holder.PlayerSourceList[playerOrder].MyPrice.ToString();

        textShopPlayerBombType.text = all_Item_Holder.PlayerSourceList[playerOrder].MyBombingType;
        textShopPlayerLevel.text = Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerLevel.ToString();

        textShopPlayerLife.text = Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myLife.ToString();
        textShopPlayerSpeed.text = (Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.mySpeed * 0.01f).ToString();
        textShopPlayerBombAmount.text = Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myBombAmount.ToString();
        textShopPlayerBombPower.text = Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myBombPower.ToString();
        textShopPlayerBombFireLimit.text = Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myBombFireLimit.ToString();
        textShopPlayerBoxPassing.text = Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myBombBoxPassing.ToString();
        textShopPlayerBoxPushingTime.text = Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myBombPushingTime.ToString();

        textShopPlayerUpgradeLife.text = all_Item_Holder.PlayerSourceList[playerOrder].MyUpgrade.myLife.ToString();
        textShopPlayerUpgradeSpeed.text = all_Item_Holder.PlayerSourceList[playerOrder].MyUpgrade.mySpeed.ToString();
        textShopPlayerUpgradeBombAmount.text = all_Item_Holder.PlayerSourceList[playerOrder].MyUpgrade.myBombAmount.ToString();
        textShopPlayerUpgradeBombPower.text = all_Item_Holder.PlayerSourceList[playerOrder].MyUpgrade.myBombPower.ToString();
        textShopPlayerUpgradeBombFireLimit.text = all_Item_Holder.PlayerSourceList[playerOrder].MyUpgrade.myBombFireLimit.ToString();
        textShopPlayerUpgradeBoxPassing.text = all_Item_Holder.PlayerSourceList[playerOrder].MyUpgrade.myBombBoxPassing.ToString();
        textShopPlayerUpgradeBoxPushingTime.text = all_Item_Holder.PlayerSourceList[playerOrder].MyUpgrade.myBombPushingTime.ToString();
    }
    // Canvas -> Panel-Menu -> Panel-Shop -> Panel-Shop-Player-Container -> Panel-Player-Info-Container -> Panel-Player-Bomb -> Button-Left ve Button-Right'a atandı
    public void ShowBombFireLimit(int next)
    {
        imageBombFire.sprite = all_Item_Holder.PlayerSourceList[playerOrder].SetImageBombFireLimit(next);
    }
    // Canvas -> Panel-Menu -> Panel-Shop -> Panel-Shop-Player-Container -> Image-PlayerBg -> Button-Left ve Button-Right'a atandı
    public void PlayerNext(int next)
    {
        player_Base.EnterHavuz();
        playerOrder += next;
        if (playerOrder == -1)
        {
            playerOrder = Save_Load_Manager.Instance.gameData.allPlayers.Count - 1;
        }
        else if (playerOrder == Save_Load_Manager.Instance.gameData.allPlayers.Count)
        {
            playerOrder = 0;
        }
        player_Base = all_Item_Holder.PlayerSourceList[playerOrder].MyPool.HavuzdanObjeIste(Game_Manager.Instance.PlayerWaitingPoint).GetComponent<Player_Base>();
        SetPlayerInfo();
        buttonPlayerBuy.gameObject.SetActive(!Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerBuyed);
        buttonPlayerChoose.gameObject.SetActive(Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerBuyed);
    }
    // Canvas -> Panel-Menu -> Panel-Shop -> Panel-Shop-Player-Container -> Button-Player-Buy'a atandı
    public void PlayerBuy()
    {
        if (Save_Load_Manager.Instance.gameData.gold >= all_Item_Holder.PlayerSourceList[playerOrder].MyPrice)
        {
            Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerBuyed = true;
            buttonPlayerBuy.gameObject.SetActive(!Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerBuyed);
            buttonPlayerChoose.gameObject.SetActive(Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerBuyed);
            SetGoldSmooth(-all_Item_Holder.PlayerSourceList[playerOrder].MyPrice);
        }
        else
        {
            Warning_Manager.Instance.ShowMessage("You dont have enough Gold.", 2);
        }
    }
    // Canvas -> Panel-Menu -> Panel-Shop -> Panel-Shop-Player-Container -> Button-Player-Choose'a atandı
    public void PlayerChoose()
    {
        Save_Load_Manager.Instance.gameData.playerOrder = playerOrder;
        player_Base.SetInstance();
    }
    #endregion

    #region Set
    // Canvas -> Panel-Name -> Button-Name'a atandı.
    public void SetName()
    {
        if (inputName.text.Length < 3)
        {
            Warning_Manager.Instance.ShowMessage("Your name should more than 3 letter.");
            return;
        }
        else if (inputName.text.Length > 15)
        {
            Warning_Manager.Instance.ShowMessage("Your name should less than 16 letter.");
            return;
        }
        else
        {
            Save_Load_Manager.Instance.gameData.accountName = inputName.text;
            SetEverything();
        }
    }
    private void SetEverything()
    {
        panelName.SetActive(false);
        panelMenu.SetActive(true);
        player_Base = all_Item_Holder.PlayerSourceList[Save_Load_Manager.Instance.gameData.playerOrder].MyPool.HavuzdanObjeIste(Game_Manager.Instance.PlayerWaitingPoint).GetComponent<Player_Base>();
        player_Base.SetInstance();
        playerOrder = Save_Load_Manager.Instance.gameData.playerOrder;
        SetPlayerInfo();
        SetLevel();
        SetGold();
        SetGateCreatorSlot();
        SetWallCreatorSlot();
        SetBoxCreatorSlot();
        SetTrapCreatorSlot();
        SetEnemyCreatorSlot();
        SetBossEnemyCreatorSlot();
        SetMapButtons();
        SetMyLevelButtons();
        SetGameLevelButtons();
        SetDungeonSetting();
        CreateAllDungeonBoardCreatorChooser();
    }
    public void SetGold()
    {
        Audio_Manager.Instance.PlayGoldChance();
        textMenuGoldAmount.text = Save_Load_Manager.Instance.gameData.gold.ToString();
        textCreatorGoldAmount.text = Save_Load_Manager.Instance.gameData.gold.ToString();
    }
    public void SetGoldSmooth(int amount)
    {
        if (goldChangedAmount == 0)
        {
            Audio_Manager.Instance.PlayGoldChance();
            goldChangedStaertedAmount = Save_Load_Manager.Instance.gameData.gold;
        }
        goldChangedAmount += amount;
        Save_Load_Manager.Instance.gameData.gold += amount;
        DOTween.To(value => {
            // goldu arttır
            textMenuGoldAmount.text = (goldChangedStaertedAmount + (int)value).ToString();
            textCreatorGoldAmount.text = (goldChangedStaertedAmount + (int)value).ToString();
        }, startValue: 0, endValue: goldChangedAmount, duration: 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            goldChangedAmount = 0;
        });
    }
    #endregion

    #region Bomb
    // Canvas -> Panel-Game -> Button-Bomb-Clock-Activiter'a atandı
    public void UseBombClockActiviter()
    {
        Player_Base.Instance.UseBombClockActiviter();
    }
    // Canvas -> Panel-Game -> Button-Use-Bomb'a atandı
    public void UseBomb()
    {
        Player_Base.Instance.UseBomb();
    }
    #endregion

    #region Player Stats
    private void Instance_OnGameStart(object sender, EventArgs e)
    {
        Player_Base.Instance.ResetBase();
        SetPlayerSettingPanel(true);
        imagePlayerIcon.sprite = all_Item_Holder.PlayerSourceList[Save_Load_Manager.Instance.gameData.playerOrder].MyIcon;
        textPlayerName.text = Save_Load_Manager.Instance.gameData.accountName;
        SetPlayerLifeText();
        SetPlayerPowerText();
        SetPlayerSpeedText();
        SetPlayerBombFireLimitText();
        SetPlayerBombAmountText();
        panelGame.SetActive(true);
        SetActiveMapProcess(false);
    }
    public void SetLevel()
    {
        int exp = Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerExp;
        int expMax = Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerExpMax;
        int level = Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerLevel;

        textPlayerExp.text = "Exp : " + exp.ToString();
        float percent = 1.0f * exp / expMax;
        textPlayerLevel.text = "Level : " + level + " - %" + (percent * 100).ToString();
        imagePlayerLevelPercent.fillAmount = percent;
    }
    public void SetPlayerSettingPanel(bool isActive)
    {
        panelPlayerSetting.SetActive(isActive);
    }
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
        textPlayerPower.text = Player_Base.Instance.CharacterStat.myBombPower.ToString();
        textPlayerPower.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerPower.DOAnchorPos(new Vector3(60, 0, 0), 0.5f);
        });
    }
    public void SetPlayerSpeedText()
    {
        textPlayerSpeed.text = (Player_Base.Instance.MySpeed * 0.01f).ToString();
        textPlayerSpeed.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerSpeed.DOAnchorPos(new Vector3(60, 0, 0), 0.5f);
        });
    }
    public void SetPlayerBombFireLimitText()
    {
        textPlayerBombFireLimit.text = Player_Base.Instance.CharacterStat.myBombFireLimit.ToString();
        textPlayerBombFireLimit.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerBombFireLimit.DOAnchorPos(new Vector3(60, 0, 0), 0.5f);
        });
    }
    public void SetPlayerBombAmountText()
    {
        textPlayerBombAmount.text = Player_Base.Instance.MyBombAmount.ToString();
        textPlayerBombAmount.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerBombAmount.DOAnchorPos(new Vector3(60, 0, 0), 0.5f);
        });
    }
    #endregion

    #region Dungeon Create Setting
    private void SetDungeonSetting()
    {
        panelDungeonSetting.DOScaleX(0, 0.1f);
        panelCreateSetting.DOScaleY(0, 0.1f);
        buttonDungeonCreate.interactable = false;
        CheckDungeonSize();
    }
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Panel-InputField -> InputField-Map-Size-X ve InputField-Map-Size-Y'a atandı
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
            if (sizeX < 11)
            {
                correctDungeonSize = false;
                Warning_Manager.Instance.ShowMessage("Board Size Width can not be smallest than 11.", 3);
            }
            else if (sizeX > 25)
            {
                correctDungeonSize = false;
                Warning_Manager.Instance.ShowMessage("Board Size Width can not be bigger than 25.", 3);
            }
            else if (sizeX % 2 == 0)
            {
                Warning_Manager.Instance.ShowMessage("Board Size Width can not be even. We make it odd.", 3);
                inputBoardSizeX.text = (sizeX + 1).ToString();
            }
        }
        else
        {
            correctDungeonSize = false;
            Warning_Manager.Instance.ShowMessage("Board Size Width must be number, NOT letter.");
        }
        if (int.TryParse(inputBoardSizeY.text, out int sizeY))
        {
            if (sizeY < 11)
            {
                correctDungeonSize = false;
                Warning_Manager.Instance.ShowMessage("Board Size Height can not be smallest than 11.", 3);
            }
            else if (sizeY > 25)
            {
                correctDungeonSize = false;
                Warning_Manager.Instance.ShowMessage("Board Size Height can not be bigger than 25.", 3);
            }
            else if (sizeY % 2 == 0)
            {
                Warning_Manager.Instance.ShowMessage("Board Size Height can not be even. We make it odd.", 3);
                inputBoardSizeY.text = (sizeY + 1).ToString();
            }
        }
        else
        {
            correctDungeonSize = false;
            Warning_Manager.Instance.ShowMessage("Board Size Height must be number, NOT letter.");
        }
        if (sizeX * sizeY > 500)
        {
            correctDungeonSize = false;
            Warning_Manager.Instance.ShowMessage("Board Size Area can not be bigger than 500.");
        }
        CheckDungeonSettingCorrect();
    }
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Panel-Dungeon-Setting -> Create-Setting -> Panel-Wall-Container -> InputField-Wall-Object'a atandı
    public void SetDungeonWallObject()
    {
        correctWallAmount = true;
        if (string.IsNullOrEmpty(inputWallAmount.text))
        {
            Warning_Manager.Instance.ShowMessage("Wall input can not be empty.");
            correctWallAmount = false;
        }
        if (int.TryParse(inputWallAmount.text, out int wallAmount))
        {
            if (wallAmount < 0)
            {
                Warning_Manager.Instance.ShowMessage("The number of Wall can not be less than 0.");
                correctWallAmount = false;
            }
            if (wallAmount > 0)
            {
                if (Map_Creater_Manager.Instance.ChoosedWallList.Count == 0)
                {
                    Warning_Manager.Instance.ShowMessage("You must choose some Wall.");
                    correctWallAmount = false;
                }
            }
        }
        else
        {
            Warning_Manager.Instance.ShowMessage("The number of Wall must be number, NOT letter.");
            correctWallAmount = false;
        }
        Map_Creater_Manager.Instance.SetWallBoard(wallAmount);
        CheckDungeonSettingCorrect();
    }
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Panel-Dungeon-Setting -> Create-Setting -> Panel-Box-Container -> InputField-Box-Object'a atandı
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
            if (magicStoneAmount > 0)
            {
                if (Map_Creater_Manager.Instance.ChoosedBoxList.Count == 0)
                {
                    Warning_Manager.Instance.ShowMessage("You must choose some Box.");
                    correctMagicStoneAmount = false;
                }
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
            if (boxAmount > 0)
            {
                if (Map_Creater_Manager.Instance.ChoosedBoxList.Count == 0)
                {
                    Warning_Manager.Instance.ShowMessage("You must choose some Box.");
                    correctBoxAmount = false;
                }
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
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Panel-Dungeon-Setting -> Create-Setting -> Panel-Trap-Container -> InputField-Trap-Object'a atandı
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
            if (trapAmount > 0)
            {
                if (Map_Creater_Manager.Instance.ChoosedTrapList.Count == 0)
                {
                    Warning_Manager.Instance.ShowMessage("You must choose some Trap.");
                    correctTrapAmount = false;
                }
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
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Panel-Dungeon-Setting -> Create-Setting -> Panel-Enemy-Container -> InputField-Enemy-Object'a atandı
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
            if (enemyAmount > 0)
            {
                if (Map_Creater_Manager.Instance.ChoosedEnemyList.Count == 0)
                {
                    Warning_Manager.Instance.ShowMessage("You must choose some Enemy.");
                    correctEnemyAmount = false;
                }
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
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Panel-Dungeon-Setting -> Create-Setting -> Panel-Boss-Enemy-Container -> InputField-Boss-Enemy-Object'a atandı
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
            if (bossEnemyAmount > 0)
            {
                if (Map_Creater_Manager.Instance.ChoosedBossEnemyList.Count == 0)
                {
                    Warning_Manager.Instance.ShowMessage("You must choose some Boss Enemy.");
                    correctBossEnemyAmount = false;
                }
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
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Panel-Dungeon-Setting -> Create-Setting -> Panel-Magic-Stone-Container -> InputField-Magic-Stone-Object'a atandı
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
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Panel-Dungeon-Setting -> Toggle-Free-Start-Random'a atandı
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
    private void CreateAllDungeonBoardCreatorChooser()
    {
        CreaterWallChooseButton();
        CreaterBoxChooseButton();
        CreaterTrapChooseButton();
        CreaterEnemyChooseButton();
        CreaterBossEnemyChooseButton();
    }
    private void CreaterWallChooseButton()
    {
        for (int e = 0; e < all_Item_Holder.WallList.Count; e++)
        {
            buttonCreaterWallList.Add(false);
            Button chooser = Instantiate(buttonChooser, wallChooseParent);
            chooser.transform.GetChild(0).GetComponent<Image>().sprite = all_Item_Holder.WallList[e].MyIcon;
            int order = e;
            Transform buton = chooser.transform;
            chooser.onClick.AddListener(() => CreaterWallOrder(buton, order));
        }
    }
    private void CreaterWallOrder(Transform buton, int order)
    {
        buttonCreaterWallList[order] = !buttonCreaterWallList[order];
        buton.GetChild(1).GetChild(0).gameObject.SetActive(buttonCreaterWallList[order]);
        if (buttonCreaterWallList[order])
        {
            if (!Map_Creater_Manager.Instance.ChoosedWallList.Contains(order))
            {
                Map_Creater_Manager.Instance.ChoosedWallList.Add(order);
            }
        }
        else
        {
            if (Map_Creater_Manager.Instance.ChoosedWallList.Contains(order))
            {
                Map_Creater_Manager.Instance.ChoosedWallList.Remove(order);
            }
        }
        SetDungeonWallObject();
    }
    private void CreaterBoxChooseButton()
    {
        for (int e = 0; e < all_Item_Holder.BoxList.Count; e++)
        {
            buttonCreaterBoxList.Add(false);
            Button chooser = Instantiate(buttonChooser, boxChooseParent);
            chooser.transform.GetChild(0).GetComponent<Image>().sprite = all_Item_Holder.BoxList[e].MyIcon;
            int order = e;
            Transform buton = chooser.transform;
            chooser.onClick.AddListener(() => CreaterBoxOrder(buton, order));
        }
    }
    private void CreaterBoxOrder(Transform buton, int order)
    {
        buttonCreaterBoxList[order] = !buttonCreaterBoxList[order];
        buton.GetChild(1).GetChild(0).gameObject.SetActive(buttonCreaterBoxList[order]);
        if (buttonCreaterBoxList[order])
        {
            if (!Map_Creater_Manager.Instance.ChoosedBoxList.Contains(order))
            {
                Map_Creater_Manager.Instance.ChoosedBoxList.Add(order);
            }
        }
        else
        {
            if (Map_Creater_Manager.Instance.ChoosedBoxList.Contains(order))
            {
                Map_Creater_Manager.Instance.ChoosedBoxList.Remove(order);
            }
        }
        SetDungeonBoxObject();
    }
    private void CreaterTrapChooseButton()
    {
        for (int e = 0; e < all_Item_Holder.TrapList.Count; e++)
        {
            buttonCreaterTrapList.Add(false);
            Button chooser = Instantiate(buttonChooser, trapChooseParent);
            chooser.transform.GetChild(0).GetComponent<Image>().sprite = all_Item_Holder.TrapList[e].MyIcon;
            int order = e;
            Transform buton = chooser.transform;
            chooser.onClick.AddListener(() => CreaterTrapOrder(buton, order));
        }
    }
    private void CreaterTrapOrder(Transform buton, int order)
    {
        buttonCreaterTrapList[order] = !buttonCreaterTrapList[order];
        buton.GetChild(1).GetChild(0).gameObject.SetActive(buttonCreaterTrapList[order]);
        if (buttonCreaterTrapList[order])
        {
            if (!Map_Creater_Manager.Instance.ChoosedTrapList.Contains(order))
            {
                Map_Creater_Manager.Instance.ChoosedTrapList.Add(order);
            }
        }
        else
        {
            if (Map_Creater_Manager.Instance.ChoosedTrapList.Contains(order))
            {
                Map_Creater_Manager.Instance.ChoosedTrapList.Remove(order);
            }
        }
        SetDungeonTrapObject();
    }
    private void CreaterEnemyChooseButton()
    {
        for (int e = 0; e < all_Item_Holder.EnemyList.Count; e++)
        {
            buttonCreaterEnemyList.Add(false);
            Button chooser = Instantiate(buttonChooser, enemyChooseParent);
            chooser.transform.GetChild(0).GetComponent<Image>().sprite = all_Item_Holder.EnemyList[e].MyIcon;
            int order = e;
            Transform buton = chooser.transform;
            chooser.onClick.AddListener(() => CreaterEnemyOrder(buton, order));
        }
    }
    private void CreaterEnemyOrder(Transform buton, int order)
    {
        buttonCreaterEnemyList[order] = !buttonCreaterEnemyList[order];
        buton.GetChild(1).GetChild(0).gameObject.SetActive(buttonCreaterEnemyList[order]);
        if (buttonCreaterEnemyList[order])
        {
            if (!Map_Creater_Manager.Instance.ChoosedEnemyList.Contains(order))
            {
                Map_Creater_Manager.Instance.ChoosedEnemyList.Add(order);
            }
        }
        else
        {
            if (Map_Creater_Manager.Instance.ChoosedEnemyList.Contains(order))
            {
                Map_Creater_Manager.Instance.ChoosedEnemyList.Remove(order);
            }
        }
        SetDungeonEnemyObject();
    }
    private void CreaterBossEnemyChooseButton()
    {
        for (int e = 0; e < all_Item_Holder.BossEnemyList.Count; e++)
        {
            buttonCreaterBossEnemyList.Add(false);
            Button chooser = Instantiate(buttonChooser, bossEnemyChooseParent);
            chooser.transform.GetChild(0).GetComponent<Image>().sprite = all_Item_Holder.BossEnemyList[e].MyIcon;
            int order = e;
            Transform buton = chooser.transform;
            chooser.onClick.AddListener(() => CreaterBossEnemyOrder(buton, order));
        }
    }
    private void CreaterBossEnemyOrder(Transform buton, int order)
    {
        buttonCreaterBossEnemyList[order] = !buttonCreaterBossEnemyList[order];
        buton.GetChild(1).GetChild(0).gameObject.SetActive(buttonCreaterBossEnemyList[order]);
        if (buttonCreaterBossEnemyList[order])
        {
            if (!Map_Creater_Manager.Instance.ChoosedBossEnemyList.Contains(order))
            {
                Map_Creater_Manager.Instance.ChoosedBossEnemyList.Add(order);
            }
        }
        else
        {
            if (Map_Creater_Manager.Instance.ChoosedBossEnemyList.Contains(order))
            {
                Map_Creater_Manager.Instance.ChoosedBossEnemyList.Remove(order);
            }
        }
        SetDungeonBossEnemyObject();
    }
    #endregion

    #region Creator
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Button-Create'a atandı.
    public void CreateMapBoard()
    {
        panelBoardSize.SetActive(false);
        objCheckingMapButton.SetActive(Save_Load_Manager.Instance.SaveType == BoardSaveType.MyLevel);
        buttonSaveMap.interactable = Save_Load_Manager.Instance.SaveType == BoardSaveType.GameLevel;
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
        buttonSaveMap.interactable = Save_Load_Manager.Instance.SaveType == BoardSaveType.GameLevel;
    }
    public void SetSaveButton()
    {
        panelGame.SetActive(false);
        panelCreator.SetActive(true);
        buttonSaveMap.interactable = true;
        Player_Base.Instance.SetEffectivePlayer(false);
    }
    // Canvas -> Panel-Creator -> Panel-Creator-Gold-Amount -> Button-Checking-Level'a atandı.
    public void CheckMapRules()
    {
        if (Map_Creater_Manager.Instance.CheckLevelMap())
        {
            panelMenu.SetActive(false);
            panelMap.SetActive(false);
            panelGame.SetActive(true);
            panelCreator.SetActive(false);
            Camera_Manager.Instance.SetCameraPos(new Vector3Int(Map_Holder.Instance.BoardSize.x, 0, Map_Holder.Instance.BoardSize.y));
            Warning_Manager.Instance.ShowMessage("Finish game for save.");
        }
    }
    // Canvas -> Panel-Creator -> Panel-Save-Map-Buttons -> Button-Change-Map'a atandı.
    public void ChangeMap()
    {
        // kayıt butonu kapatılsın
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
    // Canvas -> Panel-Creator -> Panel-Creator-Buttons -> Button-Map-Save'a atandı
    public void SaveMap()
    {
        // Save map for my level
        if (Save_Load_Manager.Instance.SaveType == BoardSaveType.GameLevel)
        {
            // Yaptığımız boardı deniyoruz.
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
        // kayıt butonu kapatılsın
        buttonSaveMap.interactable = false;
        objCheckingMapButton.SetActive(Save_Load_Manager.Instance.SaveType == BoardSaveType.MyLevel);
        // Objeler
        objCreatorButtonTypeList.SetActive(true);
        // Obje listesi
        objCreatorTypeList.SetActive(true);
        objChangeMapButton.SetActive(false);
    }
    // Canvas -> Panel-Creator -> Panel-Creator-Buttons -> Button-Map-Release'a atandı
    public void ReleaseMap()
    {
        Map_Creater_Manager.Instance.DestroyObject();
        Map_Holder.Instance.ReleaseMap();
    }
    // Canvas -> Panel-Creator -> Button-Close'a atandı
    public void CloseMap()
    {
        Map_Creater_Manager.Instance.DestroyObject();
        Map_Holder.Instance.CloseMap();
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
        if (Save_Load_Manager.Instance.SaveType == BoardSaveType.GameLevel)
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
    // Canvas -> Panel-Creator -> Panel-Objects-Behaviour -> Button-Move'a atandı
    public void MoveObject()
    {
        Map_Creater_Manager.Instance.MoveObject();
    }
    // Canvas -> Panel-Creator -> Panel-Objects-Behaviour -> Button-Turn'a atandı
    public void TurnObject()
    {
        Map_Creater_Manager.Instance.TurnObject();
    }
    // Canvas -> Panel-Creator -> Panel-Objects-Behaviour -> Button-Destroy'a atandı
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
        Audio_Manager.Instance.PlayGameSuccess();
        OnGameWin?.Invoke(this, EventArgs.Empty);
        LearnStats("Game Win");
        objNextLevelButton.SetActive(true);
        objReloadButton.SetActive(false);
        panelGameFinish.SetActive(true);
        if (Save_Load_Manager.Instance.SaveType == BoardSaveType.GameLevel)
        {
            Save_Load_Manager.Instance.gameData.lastLevel++;
        }
    }
    public void GameLost()
    {
        Audio_Manager.Instance.PlayGameFailed();
        OnGameLost?.Invoke(this, EventArgs.Empty);
        LearnStats("Game Lost");
        objNextLevelButton.SetActive(false);
        objReloadButton.SetActive(true);
        panelGameFinish.SetActive(true);
    }
    private void LearnStats(string result)
    {
        textLevelResult.text = result;
        Player_Base.Instance.SetEffectivePlayer(false);
        int time = Mathf.RoundToInt(Game_Manager.Instance.LevelTime);
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
        Map_Construct_Manager.Instance.ConstructMap(Save_Load_Manager.Instance.LoadBoard(BoardSaveType.GameLevel, Save_Load_Manager.Instance.gameData.lastLevel));
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
        Map_Construct_Manager.Instance.ConstructMap(Save_Load_Manager.Instance.LoadBoard(BoardSaveType.GameLevel, Save_Load_Manager.Instance.gameData.lastLevel));
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