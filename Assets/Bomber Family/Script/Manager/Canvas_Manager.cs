using TMPro;
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEditor;

[Serializable]
public class BombList
{
    public RectTransform rectBombParent;
    public TextMeshProUGUI textBombAmount;
    public TextMeshProUGUI textBombPower;
    public TextMeshProUGUI textBombLimit;
    public TextMeshProUGUI textBombFireTime;
    public BombList(RectTransform rectBombParent, TextMeshProUGUI textBombAmount, TextMeshProUGUI textBombPower
        , TextMeshProUGUI textBombLimit, TextMeshProUGUI textBombFireTime)
    {
        this.rectBombParent = rectBombParent;
        this.textBombAmount = textBombAmount;
        this.textBombPower = textBombPower;
        this.textBombLimit = textBombLimit;
        this.textBombFireTime = textBombFireTime;
    }
}
public class Canvas_Manager : Singletion<Canvas_Manager>
{
    public event EventHandler OnGameWin;
    public event EventHandler OnGameLost;

    // Genel
    [SerializeField] private Sprite emptySlotIcon;
    [SerializeField] private Transform sceneMaskedImage;
    [SerializeField] private All_Item_Holder all_Item_Holder;
    [SerializeField] private TextMeshProUGUI textMenuGoldAmount;
    [SerializeField] private TextMeshProUGUI textCraftGoldAmount;
    [SerializeField] private TextMeshProUGUI textCreatorGoldAmount;

    // Name
    [SerializeField] private GameObject panelName;
    [SerializeField] private TMP_InputField inputName;

    // Game Finish
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
    [SerializeField] private Button buttonDoubleReward;
    [SerializeField] private Button buttonOffer1;
    [SerializeField] private Button buttonOffer2;
    [SerializeField] private Button buttonOffer3;

    // Menu
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelLevelsMap;
    [SerializeField] private GameObject panelMyLevels;
    [SerializeField] private Button buttonMyLevel;
    [SerializeField] private Transform myLevelButtonParent;
    [SerializeField] private List<Button> dailyButtons = new List<Button>();

    // Creator
    [SerializeField] private GameObject panelCreator;
    [SerializeField] private GameObject panelBoardSize;
    [SerializeField] private GameObject panelProcessHolder;
    [SerializeField] private GameObject panelProcess;
    [SerializeField] private TMP_InputField inputBoardSizeX;
    [SerializeField] private TMP_InputField inputBoardSizeY;
    [SerializeField] private Slot_Creator slotCreator;
    [SerializeField] private Transform gateParent;
    [SerializeField] private Transform wallParent;
    [SerializeField] private Transform boxParent;
    [SerializeField] private Transform trapParent;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private Transform bossEnemyParent;
    [SerializeField] private Button buttonSaveMap;
    [SerializeField] private Image imageProcess;
    [SerializeField] private TextMeshProUGUI textProcess;
    [SerializeField] private TextMeshProUGUI textProcessBase;
    [SerializeField] private GameObject objCheckingMapButton;
    [SerializeField] private GameObject objSaveMapButtonsParent;
    [SerializeField] private GameObject objCreatorButtonTypeList;
    [SerializeField] private GameObject objCreatorTypeList;
    [SerializeField] private GameObject objChangeMapButton;
    [SerializeField] private GameObject objEmptyArea;
    [SerializeField] private Image imageMultiplePlacement;
    [SerializeField] private GameObject objCreatingMultipleObject;
    [SerializeField] private TextMeshProUGUI textPlacementType;
    [SerializeField] private TextMeshProUGUI textCreateObjectSetting;
    [SerializeField] private TMP_InputField inputMultiplePlacementAmount;
    [SerializeField] private GameObject objPlacement;
    [SerializeField] private List<GameObject> allPanelObjectTypes = new List<GameObject>();

    // Creator Object Setting
    [SerializeField] private GameObject panelSettingBase;
    [SerializeField] private GameObject objButtonMove;
    [SerializeField] private Toggle toggleSettingTrigger;
    [SerializeField] private Toggle toggleSettingAlwaysActive;
    [SerializeField] private Toggle toggleSettingTimer;
    [SerializeField] private Slot_Trigger slotTrigger;
    [SerializeField] private GameObject panelTriggerList;
    [SerializeField] private Transform panelTriggerListParent;
    [SerializeField] private GameObject panelSettingForObjectTimer1;
    [SerializeField] private Toggle toggleStartingWithActivated;
    [SerializeField] private GameObject panelSettingForObjectTimer2;
    [SerializeField] private GameObject panelSettingForObjectEffect;
    [SerializeField] private TextMeshProUGUI textSettingInfo;

    // Dungeon Create Setting
    [SerializeField] private Toggle toggleDungeonFree;
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

    // Player
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
    [SerializeField] private RectTransform rectPlayerSpeed;
    [SerializeField] private TextMeshProUGUI textPlayerSpeed;
    [SerializeField] private RectTransform rectPlayerBombAmount;
    [SerializeField] private TextMeshProUGUI textPlayerBombAmount;
    [SerializeField] private RectTransform rectPlayerPower;
    [SerializeField] private TextMeshProUGUI textPlayerPower;
    [SerializeField] private RectTransform rectPlayerBombFireLimit;
    [SerializeField] private TextMeshProUGUI textPlayerBombFireLimit;
    [SerializeField] private RectTransform rectPlayerBoxPassing;
    [SerializeField] private TextMeshProUGUI textPlayerBoxPassing;
    [SerializeField] private RectTransform rectPlayerBoxPushingTime;
    [SerializeField] private TextMeshProUGUI textPlayerBoxPushingTime;

    // Bomb
    [SerializeField] private GameObject bombClockActiviter;
    [SerializeField] private RectTransform rectBombButtonSimple;
    [SerializeField] private Transform objBombButtonHolder;

    // Player Choose
    [SerializeField] private Joystick joystickMove;
    [SerializeField] private Button buttonPlayerBuy;
    [SerializeField] private Button buttonPlayerChoose;
    [SerializeField] private Image imageBombFire;
    [SerializeField] private TextMeshProUGUI textShopPlayerName;
    [SerializeField] private TextMeshProUGUI textShopPlayerPrice;
    [SerializeField] private TextMeshProUGUI textShopPlayerLevel;
    [SerializeField] private TextMeshProUGUI textShopPlayerUpgrade;
    [SerializeField] private TextMeshProUGUI textShopPlayerBombType;
    [SerializeField] private TextMeshProUGUI textShopPlayerLife;
    [SerializeField] private TextMeshProUGUI textShopPlayerSpeed;
    [SerializeField] private TextMeshProUGUI textShopPlayerBombAmount;
    [SerializeField] private TextMeshProUGUI textShopPlayerBombPower;
    [SerializeField] private TextMeshProUGUI textShopPlayerBombFireLimit;
    [SerializeField] private TextMeshProUGUI textShopPlayerBoxPassing;
    [SerializeField] private TextMeshProUGUI textShopPlayerBoxPushingTime;
    [SerializeField] private TextMeshProUGUI textShopPlayerUpgradeLife;
    [SerializeField] private TextMeshProUGUI textShopPlayerUpgradeSpeed;
    [SerializeField] private TextMeshProUGUI textShopPlayerUpgradeBombAmount;
    [SerializeField] private TextMeshProUGUI textShopPlayerUpgradeBombPower;
    [SerializeField] private TextMeshProUGUI textShopPlayerUpgradeBombFireLimit;
    [SerializeField] private TextMeshProUGUI textShopPlayerUpgradeBoxPassing;
    [SerializeField] private TextMeshProUGUI textShopPlayerUpgradeBoxPushingTime;
    [SerializeField] private Button buttonShopPlayerLife;
    [SerializeField] private Button buttonShopPlayerSpeed;
    [SerializeField] private Button buttonShopPlayerBombAmount;
    [SerializeField] private Button buttonShopPlayerBombPower;
    [SerializeField] private Button buttonShopPlayerBombFireLimit;
    [SerializeField] private Button buttonShopPlayerBoxPassing;
    [SerializeField] private Button buttonShopPlayerBoxPushingTime;

    // Level Start Help
    [SerializeField] private RectTransform rectLevelHelp;
    [SerializeField] private Image imageLevelHelpPlayerIcon;
    [SerializeField] private Button buttonLevelHelpLife;
    [SerializeField] private Button buttonLevelHelpAmount;
    [SerializeField] private Button buttonLevelHelpPower;
    [SerializeField] private GameObject cameraMenu;
    [SerializeField] private GameObject cameraMap;

    // Craft
    [SerializeField] private Button buttonCraft;
    [SerializeField] private Image buttonCraftIcon;
    [SerializeField] private Sprite craftSlotIcon;
    [SerializeField] private GameObject objBombStatPanel;
    [SerializeField] private Image imageBombPowerIcon;
    [SerializeField] private Image imageBombLimitIcon;
    [SerializeField] private Image imageBombFireTimeIcon;
    [SerializeField] private TextMeshProUGUI textBombPowerAmount;
    [SerializeField] private TextMeshProUGUI textBombLimitAmount;
    [SerializeField] private TextMeshProUGUI textBombFireTimeAmount;
    [SerializeField] private Button butonIncBombPower;
    [SerializeField] private Button butonIncBombPowerAds;
    [SerializeField] private Button butonDecBombPower;
    [SerializeField] private Button butonIncBombLimit;
    [SerializeField] private Button butonIncBombLimitAds;
    [SerializeField] private Button butonDecBombLimit;
    [SerializeField] private Button butonIncBombFireTime;
    [SerializeField] private Button butonIncBombFireTimeAds;
    [SerializeField] private Button butonDecBombFireTime;
    [SerializeField] private List<Slot_Material> myMaterialList = new List<Slot_Material>();
    [SerializeField] private List<Slot_Recipe> myRecipeList = new List<Slot_Recipe>();

    // Shop
    [SerializeField] private GameObject panelShop;
    [SerializeField] private Transform objTool;
    [SerializeField] private Transform objToolParent;
    [SerializeField] private Transform objMaterial;
    [SerializeField] private Transform objMaterialParent;
    [SerializeField] private List<GameObject> shopPanelList = new List<GameObject>();

    private bool isBuyed;
    private bool correctWallAmount;
    private bool correctBoxAmount;
    private bool correctTrapAmount;
    private bool correctEnemyAmount;
    private bool correctBossEnemyAmount;
    private bool correctMagicStoneAmount;
    private bool correctDungeonSize;
    private bool isMultiple;
    private bool changeTime = false;

    private int amountMultiple;
    private int bombOrder;
    private int playerOrder;
    private int goldChangedAmount;
    private int goldChangedStaertedAmount;
    private int bombPower;
    private int bombLimit;
    private float bombFireTime;

    private Player_Base player_Base;
    private Image imageLevelHelpLife;
    private Image imageLevelHelpPower;
    private Image imageLevelHelpAmount;
    private Item_Recipe myRecipeItem;
    private RectTransform choosedBomb;

    private List<BombList> allBombs = new List<BombList>();
    private List<bool> buttonCreaterWallList = new List<bool>();
    private List<bool> buttonCreaterBoxList = new List<bool>();
    private List<bool> buttonCreaterTrapList = new List<bool>();
    private List<bool> buttonCreaterEnemyList = new List<bool>();
    private List<bool> buttonCreaterBossEnemyList = new List<bool>();

    public Sprite EmptySlotIcon { get { return emptySlotIcon; } }
    public bool ToggleDungeonSetting { get { return toggleDungeonSetting.isOn; } }
    public bool IsMultiple { get { return isMultiple; } }
    public int AmountMultiple { get { return amountMultiple; } }

    #region Menu
    [ContextMenu("Close Panels")]
    private void ClosePanels()
    {
        panelName.SetActive(false);
        panelMenu.SetActive(false);
        panelGame.SetActive(false);
        panelShop.SetActive(false);
        panelLevelsMap.SetActive(false);
    }
    private void Start()
    {
        sceneMaskedImage.gameObject.SetActive(true);
        Game_Manager.Instance.OnGameStart += Instance_OnGameStart;
    }
    public void GameStart()
    {
        CloseMask(0.1f, () => {
            if (string.IsNullOrEmpty(Save_Load_Manager.Instance.gameData.accountName))
            {
                panelName.SetActive(true);
                panelMenu.SetActive(false);
                panelHelp.gameObject.SetActive(true);
            }
            else
            {
                CloseHTP(false);
                SetEverything();
            }
        });
    }
    private void SetDaily()
    {
        for (int e = 0; e < Save_Load_Manager.Instance.gameData.dailyReward.Count; e++)
        {
            int dailyOrder = e;
            dailyButtons[dailyOrder].onClick.AddListener(() =>
            {
                DailyReward(dailyOrder);
            });
            Sprite dailySprite = null;
            if (Save_Load_Manager.Instance.gameData.dailyReward[e].dailyType == DailyType.Gold)
            {
                dailySprite = all_Item_Holder.GenelIcons[0];
            }
            if (Save_Load_Manager.Instance.gameData.dailyReward[e].dailyType == DailyType.Exp)
            {
                dailySprite = all_Item_Holder.GenelIcons[1];
            }
            if (Save_Load_Manager.Instance.gameData.dailyReward[e].dailyType == DailyType.Malzeme)
            {
                dailySprite = all_Item_Holder.MalzemeList[Save_Load_Manager.Instance.gameData.dailyReward[e].dailyRewardOrder].MyIcon;
            }
            if (Save_Load_Manager.Instance.gameData.dailyReward[e].dailyType == DailyType.Alet)
            {
                dailySprite = all_Item_Holder.ToolList[Save_Load_Manager.Instance.gameData.dailyReward[e].dailyRewardOrder].MyIcon;
            }
            if (Save_Load_Manager.Instance.gameData.dailyReward[e].dailyType == DailyType.Bomb)
            {
                dailySprite = all_Item_Holder.BombList[Save_Load_Manager.Instance.gameData.dailyReward[e].dailyRewardOrder].MyIcon;
            }
            if (Save_Load_Manager.Instance.gameData.dailyReward[e].dailyType == DailyType.Bilet)
            {
                dailySprite = all_Item_Holder.BiletIcons[Save_Load_Manager.Instance.gameData.dailyReward[e].dailyRewardOrder];
            }
            SetDailyReward(e, Save_Load_Manager.Instance.gameData.dailyReward[e].dailyTaked, Save_Load_Manager.Instance.gameData.dailyReward[e].dailyRewardAmount, 
                Save_Load_Manager.Instance.gameData.dailyReward[e].dailyRewardOrder, Save_Load_Manager.Instance.gameData.dailyReward[e].dailyType, dailySprite);
        }
        // Oyuna ilk kez girmişler
        if (Save_Load_Manager.Instance.gameData.lastDay == -9999)
        {
            // Birinci günü serbest bırak.
            dailyButtons[0].interactable = true;
        }
        // Oyunla aynı yıldalar
        else if (Game_Manager.Instance.Year == Save_Load_Manager.Instance.gameData.year)
        {
            // LastDay bugüne ait
            if (Game_Manager.Instance.DayOfYear == Save_Load_Manager.Instance.gameData.lastDay)
            {
                // Ödül alınmış
                bool finded = false;
                for (int e = 0; e < Save_Load_Manager.Instance.gameData.dailyReward.Count && !finded; e++)
                {
                    if (Save_Load_Manager.Instance.gameData.dailyReward[e].dailyTaked)
                    {
                        dailyButtons[e].transform.parent.Find("Image-Daily-Onay").gameObject.SetActive(true);
                    }
                    else
                    {
                        finded = true;
                    }
                }
            }
            // LastDay düne ait
            else if (Game_Manager.Instance.DayOfYear == Save_Load_Manager.Instance.gameData.lastDay + 1)
            {
                // Günü gelmiş ödül butonu aç
                bool finded = false;
                for (int e = 0; e < Save_Load_Manager.Instance.gameData.dailyReward.Count && !finded; e++)
                {
                    if (Save_Load_Manager.Instance.gameData.dailyReward[e].dailyTaked)
                    {
                        dailyButtons[e].transform.parent.Find("Image-Daily-Onay").gameObject.SetActive(true);
                    }
                    else
                    {
                        dailyButtons[e].interactable = true;
                        finded = true;
                    }
                }
                // Son 7 günün tamamı alınmış yeni haftaya geçmişiz
                if (!finded)
                {
                    // Haftayı yenile
                    ResetDailyReward();
                    // İlk gün butonu aç
                    dailyButtons[0].interactable = true;
                }
            }
            // LastDay dünden önceki günlerden birine ait
            else if(Game_Manager.Instance.DayOfYear > Save_Load_Manager.Instance.gameData.lastDay + 1)
            {
                int dailyOrder = -1;
                bool finded = false;
                for (int e = 0; e < Save_Load_Manager.Instance.gameData.dailyReward.Count && !finded; e++)
                {
                    if (Save_Load_Manager.Instance.gameData.dailyReward[e].dailyTaked)
                    {
                        dailyButtons[e].transform.parent.Find("Image-Daily-Onay").gameObject.SetActive(true);
                    }
                    else
                    {
                        Transform tr = dailyButtons[e].transform.parent.Find("Button-Daily-Missing");
                        tr.gameObject.SetActive(true);
                        tr.GetComponent<Button>().onClick.RemoveAllListeners();
                        tr.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            Reklam_Manager.Instance.RewardShowAd(() =>
                            {
                                dailyButtons[dailyOrder].transform.parent.Find("Button-Daily-Missing").gameObject.SetActive(false);
                                dailyButtons[dailyOrder].interactable = true;
                            });
                        });
                        dailyOrder = e;
                        finded = true;
                    }
                }
                // Son 7 günün tamamı alınmış yeni haftaya geçmişiz
                if (!finded)
                {
                    // Haftayı yenile
                    ResetDailyReward();
                    // İlk gün butonu aç
                    dailyButtons[0].interactable = true;
                }
            }
        }
        else // Oyunla farklı yıldalar
        {
            bool finded = false;
            for (int e = 0; e < Save_Load_Manager.Instance.gameData.dailyReward.Count && !finded; e++)
            {
                if (Save_Load_Manager.Instance.gameData.dailyReward[e].dailyTaked)
                {
                    dailyButtons[e].transform.parent.Find("Image-Daily-Onay").gameObject.SetActive(true);
                }
                else
                {
                    dailyButtons[e].interactable = true;
                    finded = true;
                }
            }
            // Son 7 günün tamamı alınmış yeni haftaya geçmişiz
            if (!finded)
            {
                // Haftayı yenile
                ResetDailyReward();
                // İlk gün butonu aç
                dailyButtons[0].interactable = true;
            }
            Save_Load_Manager.Instance.gameData.year = Game_Manager.Instance.Year;
        }
        Save_Load_Manager.Instance.SaveGame();
    }
    private void DailyReward(int dailyOrder)
    {
        Save_Load_Manager.Instance.gameData.dailyReward[dailyOrder].dailyTaked = true;
        dailyButtons[dailyOrder].interactable = false;
        dailyButtons[dailyOrder].transform.parent.Find("Image-Daily-Onay").gameObject.SetActive(true);
        Save_Load_Manager.Instance.gameData.year = Game_Manager.Instance.Year;
        Save_Load_Manager.Instance.gameData.lastDay = Game_Manager.Instance.DayOfYear;

        if (Save_Load_Manager.Instance.gameData.dailyReward[dailyOrder].dailyType == DailyType.Gold)
        {
            SetGoldSmooth(Save_Load_Manager.Instance.gameData.dailyReward[dailyOrder].dailyRewardAmount);
        }
        else if (Save_Load_Manager.Instance.gameData.dailyReward[dailyOrder].dailyType == DailyType.Exp)
        {
            Game_Manager.Instance.AddExpAmount(Save_Load_Manager.Instance.gameData.dailyReward[dailyOrder].dailyRewardAmount);
        }
        else if (Save_Load_Manager.Instance.gameData.dailyReward[dailyOrder].dailyType == DailyType.Malzeme)
        {
            Inventory_Manager.Instance.AddItem(new NeededItemHolder(all_Item_Holder.MalzemeList[Save_Load_Manager.Instance.gameData.dailyReward[dailyOrder].dailyRewardOrder], 1, InventoryType.Material));
        }
        else if (Save_Load_Manager.Instance.gameData.dailyReward[dailyOrder].dailyType == DailyType.Alet)
        {
            Inventory_Manager.Instance.AddItem(new NeededItemHolder(all_Item_Holder.ToolList[Save_Load_Manager.Instance.gameData.dailyReward[dailyOrder].dailyRewardOrder], 1, InventoryType.Alet));
        }
        else if (Save_Load_Manager.Instance.gameData.dailyReward[dailyOrder].dailyType == DailyType.Bomb)
        {
            Save_Load_Manager.Instance.gameData.allSpecialBomb[Save_Load_Manager.Instance.gameData.dailyReward[dailyOrder].dailyRewardOrder].bombAmount++;
            SetBomb(Save_Load_Manager.Instance.gameData.dailyReward[dailyOrder].dailyRewardOrder);
        }
        else if (Save_Load_Manager.Instance.gameData.dailyReward[dailyOrder].dailyType == DailyType.Bilet)
        {

        }
        Save_Load_Manager.Instance.SaveGame();
    }
    private void ResetDailyReward()
    {
        // 1 gün -> Altın -> 100 - 500
        int dailyAmount = Random.Range(100, 500);
        int dailyOrder = 0;
        Sprite dailyIcon = all_Item_Holder.GenelIcons[0];
        SetDailyReward(0, false, dailyAmount, dailyOrder, DailyType.Gold, dailyIcon);

        // 2 gün -> Exp -> 50 - 250
        dailyAmount = Random.Range(50, 250);
        dailyOrder = 1;
        dailyIcon = all_Item_Holder.GenelIcons[1];
        SetDailyReward(1, false, dailyAmount, dailyOrder, DailyType.Exp, dailyIcon);

        // 3 gün -> Malzeme -> 1 - 10 Malzeme
        dailyAmount = Random.Range(500, 1000);
        dailyOrder = Random.Range(0, all_Item_Holder.MalzemeList.Count);
        dailyIcon = all_Item_Holder.MalzemeList[dailyOrder].MyIcon;
        SetDailyReward(2, false, dailyAmount, dailyOrder, DailyType.Malzeme, dailyIcon);

        // 4 gün -> Altın -> 500 - 1000
        dailyAmount = Random.Range(500, 1000);
        dailyOrder = 0;
        dailyIcon = all_Item_Holder.GenelIcons[0];
        SetDailyReward(3, false, dailyAmount, dailyOrder, DailyType.Gold, dailyIcon);

        // 5 gün -> Exp -> 250 - 500
        dailyAmount = Random.Range(250, 500);
        dailyOrder = 1;
        dailyIcon = all_Item_Holder.GenelIcons[1];
        SetDailyReward(4, false, dailyAmount, dailyOrder, DailyType.Exp, dailyIcon);

        // 6 gün -> Malzeme -> 1 - 2 Alet
        dailyAmount = Random.Range(250, 500);
        dailyOrder = Random.Range(0, all_Item_Holder.MalzemeList.Count);
        dailyIcon = all_Item_Holder.MalzemeList[dailyOrder].MyIcon;
        SetDailyReward(5, false, dailyAmount, dailyOrder, DailyType.Malzeme, dailyIcon);

        // 7 gün -> Bomba
        dailyAmount = Random.Range(250, 500);
        dailyOrder = Random.Range(1, Enum.GetValues(typeof(BombType)).Length);
        dailyIcon = all_Item_Holder.BombList[dailyOrder].MyIcon;
        SetDailyReward(6, false, dailyAmount, dailyOrder, DailyType.Bomb, dailyIcon);

        Save_Load_Manager.Instance.SaveGame();
    }
    private void SetDailyReward(int buttonOrder, bool isTaked, int dailyAmount, int dailyOrder, DailyType dailyType, Sprite icon)
    {
        dailyButtons[buttonOrder].transform.parent.Find("Text-Daily").GetComponent<TextMeshProUGUI>().text = dailyAmount.ToString();
        dailyButtons[buttonOrder].transform.parent.Find("Image-Daily-Onay").gameObject.SetActive(false);
        dailyButtons[buttonOrder].transform.parent.Find("Button-Daily-Missing").gameObject.SetActive(false);
        Image dailyImage = dailyButtons[buttonOrder].transform.parent.Find("Image-Daily-Icon").GetComponent<Image>();
        dailyImage.sprite = icon;
        Save_Load_Manager.Instance.gameData.dailyReward[buttonOrder] = new DailyReward(dailyType, isTaked, dailyOrder, dailyAmount);
    }
    // Canvas -> Panel-Menu -> Button-Continue'a atandı
    public void ContinueGame()
    {
        OpenLevel();
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
        OpenLevel();
        Map_Construct_Manager.Instance.ConstructMap(Save_Load_Manager.Instance.LoadBoard(BoardSaveType.MyLevel, order));
    }
    private void OpenLevel()
    {
        Audio_Manager.Instance.PlayGameStart();
        Warning_Manager.Instance.ShowMessage("Please wait. Level loading...", 3);
        // Panel kapanacak
        panelMenu.SetActive(false);
        panelMyLevels.SetActive(false);
        Game_Manager.Instance.SetGameType(GameType.Game);
    }
    // Canvas -> Panel-Menu -> Button-Create-Map'a atandı
    public void OpenCreateMapPanel()
    {
        panelMenu.SetActive(false);
        panelCreator.SetActive(true);
        panelBoardSize.SetActive(true);

        // Değişkenleri fiazla
        correctDungeonSize = true;
        inputBoardSizeX.text = 11.ToString();
        inputBoardSizeY.text = 11.ToString();

        correctMagicStoneAmount = false;
        inputMagicStoneAmount.text = 0.ToString();

        correctBoxAmount = false;
        inputBoxAmount.text = 0.ToString();
        Map_Creater_Manager.Instance.ChoosedBoxList.Clear();

        correctWallAmount = true;
        inputWallAmount.text = 0.ToString();
        Map_Creater_Manager.Instance.ChoosedWallList.Clear();

        correctTrapAmount = true;
        inputTrapAmount.text = 0.ToString();
        Map_Creater_Manager.Instance.ChoosedTrapList.Clear();

        correctEnemyAmount = true;
        inputEnemyAmount.text = 0.ToString();
        Map_Creater_Manager.Instance.ChoosedEnemyList.Clear();

        correctBossEnemyAmount = true;
        inputBossEnemyAmount.text = 0.ToString();
        Map_Creater_Manager.Instance.ChoosedBossEnemyList.Clear();

        panelDungeonSetting.DOScaleX(0, 0.1f);
        panelCreateSetting.DOScaleY(0, 0.1f);
        buttonDungeonCreate.interactable = false;

        toggleDungeonSetting.isOn = false;
        toggleDungeonFree.isOn = true;

        // Gate fix
        Map_Creater_Manager.Instance.ReleaseGate();

        buttonCreaterWallList.Clear();
        buttonCreaterBoxList.Clear();
        buttonCreaterTrapList.Clear();
        buttonCreaterEnemyList.Clear();
        buttonCreaterBossEnemyList.Clear();
        CreateAllDungeonBoardCreatorChooser();
    }
    // Canvas -> Panel-Menu -> Button-Map'a atandı
    public void OpenMap()
    {
        OpenMask(0, () =>
        {
            panelMenu.SetActive(false);
            cameraMenu.SetActive(false);
            cameraMap.SetActive(true);
            panelLevelsMap.SetActive(true);
            Map_Camera_Manager.Instance.transform.position = new Vector3(-10, -45, -32);
            CloseMask(0.5f, null);
        });
    }
    public void OpenLevel(BoardSaveType boardSaveType, int levelOrder)
    {
        Audio_Manager.Instance.PlayGameStart();
        OpenMask(0, () =>
        {
            cameraMenu.SetActive(true);
            cameraMap.SetActive(false);
            panelLevelsMap.SetActive(false);
            // Ekran kapatıldı.
            Warning_Manager.Instance.ShowMessage("Please wait. Level loading...", 3);
            Game_Manager.Instance.SetGameType(GameType.Game);
            Camera_Manager.Instance.transform.position = Vector3.zero;
            CloseMask(0.5f, () =>
            {
                // Ekran açıldı.
                Map_Construct_Manager.Instance.ConstructMap(Save_Load_Manager.Instance.LoadBoard(boardSaveType, levelOrder));
            });
        });
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

    #region Level Start Help
    // Canvas -> Panel-Game -> Panel-Level-Help -> Button-Close'a atandı
    public void LevelStartHelp(bool isShow)
    {
        imageLevelHelpPlayerIcon.sprite = all_Item_Holder.PlayerSourceList[Save_Load_Manager.Instance.gameData.playerOrder].MyIcon;
        rectLevelHelp.DOAnchorPos(new Vector2(0, isShow ? 25 : -325), 0.5f).OnComplete(() =>
        {
            if (isShow)
            {
                Player_Base.Instance.ResetBase();

                buttonLevelHelpLife.interactable = true;
                buttonLevelHelpAmount.interactable = true;
                buttonLevelHelpPower.interactable = true;
                buttonDoubleReward.interactable = true;
                buttonOffer1.interactable = true;
                buttonOffer2.interactable = true;
                buttonOffer3.interactable = true;

            }
            else
            {
                Game_Manager.Instance.StartLevel();

                imageLevelHelpLife.color = Color.white;
                imageLevelHelpPower.color = Color.white;
                imageLevelHelpAmount.color = Color.white;
            }
        });
    }
    public void LevelStartHelpLife()
    {
        // Life ve speed artacak
        Reklam_Manager.Instance.RewardShowAd(() =>
        {
            player_Base.Increaselife();
            player_Base.IncreaseSpeed();
            buttonLevelHelpLife.interactable = false;
            imageLevelHelpLife.color = new Color(100.0f / 255, 100.0f / 255, 100.0f / 255, 1);
        });
    }
    public void LevelStartHelpAmount()
    {
        // Bomb Amount
        Reklam_Manager.Instance.RewardShowAd(() =>
        {
            player_Base.IncreaseBombAmount();
            buttonLevelHelpAmount.interactable = false;
            imageLevelHelpAmount.color = new Color(100.0f / 255, 100.0f / 255, 100.0f / 255, 1);
        });
    }
    public void LevelStartHelpPower()
    {
        // Power, Fire limit
        Reklam_Manager.Instance.RewardShowAd(() =>
        {
            player_Base.IncreaseBombFirePower();
            player_Base.IncreaseBombFireLimit();
            buttonLevelHelpPower.interactable = false;
            imageLevelHelpPower.color = new Color(100.0f / 255, 100.0f / 255, 100.0f / 255, 1);
        });
    }
    #endregion

    #region Player Choose
    public void SetPlayerInfo()
    {
        //bombClockActiviter.SetActive(all_Item_Holder.PlayerSourceList[Save_Load_Manager.Instance.gameData.playerOrder].MyBombType == BombType.Clock);
        player_Base.SetPlayerStat(joystickMove);
        isBuyed = Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerBuyed;
        buttonPlayerBuy.gameObject.SetActive(!isBuyed);
        buttonPlayerChoose.gameObject.SetActive(isBuyed);
        buttonPlayerChoose.interactable = playerOrder != Save_Load_Manager.Instance.gameData.playerOrder;
        all_Item_Holder.PlayerSourceList[playerOrder].SetSpriteOrder();
        imageBombFire.sprite = all_Item_Holder.PlayerSourceList[playerOrder].SetImageBombFireLimit(0);
        textShopPlayerName.text = all_Item_Holder.PlayerSourceList[playerOrder].MyName;
        textShopPlayerPrice.text = isBuyed ? "Buyed" : all_Item_Holder.PlayerSourceList[playerOrder].MyPrice.ToString();

        //textShopPlayerBombType.text = all_Item_Holder.PlayerSourceList[playerOrder].MyBombingType;
        textShopPlayerLevel.text = Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerLevel.ToString();
        textShopPlayerUpgrade.text = Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStatAmount.ToString();

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

        CheckUpgradeButton();
    }
    private void CheckUpgradeButton()
    {
        if (Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStatAmount > 0)
        {
            if (all_Item_Holder.LearnPlayerOrder(player_Base.Player_Source) == playerOrder && player_Base.HasCurse)
            {
                // Oynanan playerda curse var
                Warning_Manager.Instance.ShowMessage("You have some curse so you can not upgrade your stats.");
                CloseUpgradeButton();
            }
            else
            {
                buttonShopPlayerLife.gameObject.SetActive(all_Item_Holder.PlayerSourceList[playerOrder].CanUpgradeLife(Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myLife));
                buttonShopPlayerSpeed.gameObject.SetActive(all_Item_Holder.PlayerSourceList[playerOrder].CanUpgradeSpeed(Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.mySpeed));
                buttonShopPlayerBombAmount.gameObject.SetActive(all_Item_Holder.PlayerSourceList[playerOrder].CanUpgradeBombAmount(Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myBombAmount));
                buttonShopPlayerBombPower.gameObject.SetActive(all_Item_Holder.PlayerSourceList[playerOrder].CanUpgradeBombPower(Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myBombPower));
                buttonShopPlayerBombFireLimit.gameObject.SetActive(all_Item_Holder.PlayerSourceList[playerOrder].CanUpgradeBombFireLimit(Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myBombFireLimit));
                buttonShopPlayerBoxPassing.gameObject.SetActive(all_Item_Holder.PlayerSourceList[playerOrder].CanUpgradeBoxPassing(Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myBombBoxPassing));
                buttonShopPlayerBoxPushingTime.gameObject.SetActive(all_Item_Holder.PlayerSourceList[playerOrder].CanUpgradeBoxPushingTime(Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myBombPushingTime));
            }
        }
        else
        {
            CloseUpgradeButton();
        }
    }
    private void CloseUpgradeButton()
    {
        buttonShopPlayerLife.gameObject.SetActive(false);
        buttonShopPlayerSpeed.gameObject.SetActive(false);
        buttonShopPlayerBombAmount.gameObject.SetActive(false);
        buttonShopPlayerBombPower.gameObject.SetActive(false);
        buttonShopPlayerBombFireLimit.gameObject.SetActive(false);
        buttonShopPlayerBoxPassing.gameObject.SetActive(false);
        buttonShopPlayerBoxPushingTime.gameObject.SetActive(false);
    }
    // Canvas -> Panel-Menu -> Panel-Shop -> Panel-Shop-Player-Container -> Panel-Player-Stats -> Button-Player-Upgrade-Life'a atandı
    public void UpgradePlayerLife()
    {
        Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myLife++;
        if (Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStatAmount == 0)
        {
            CheckUpgradeButton();
        }
    }
    // Canvas -> Panel-Menu -> Panel-Shop -> Panel-Shop-Player-Container -> Panel-Player-Stats -> Button-Player-Upgrade-Speed'a atandı
    public void UpgradePlayerSpeed()
    {
        Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.mySpeed += 10;
        if (Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStatAmount == 0)
        {
            CheckUpgradeButton();
        }
    }
    // Canvas -> Panel-Menu -> Panel-Shop -> Panel-Shop-Player-Container -> Panel-Player-Stats -> Button-Player-Upgrade-BombAmount'a atandı
    public void UpgradePlayerBombAmount()
    {
        Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myBombAmount++;
        if (Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStatAmount == 0)
        {
            CheckUpgradeButton();
        }
    }
    // Canvas -> Panel-Menu -> Panel-Shop -> Panel-Shop-Player-Container -> Panel-Player-Stats -> Button-Player-Upgrade-BombPower'a atandı
    public void UpgradePlayerBombPower()
    {
        Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myBombPower++;
        if (Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStatAmount == 0)
        {
            CheckUpgradeButton();
        }
    }
    // Canvas -> Panel-Menu -> Panel-Shop -> Panel-Shop-Player-Container -> Panel-Player-Stats -> Button-Player-Upgrade-BombFireLimit'a atandı
    public void UpgradePlayerBombFireLimit()
    {
        Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myBombFireLimit++;
        if (Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStatAmount == 0)
        {
            CheckUpgradeButton();
        }
    }
    // Canvas -> Panel-Menu -> Panel-Shop -> Panel-Shop-Player-Container -> Panel-Player-Stats -> Button-Player-Upgrade-BoxPassing'a atandı
    public void UpgradePlayerBoxPassing()
    {
        Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myBombBoxPassing++;
        if (Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStatAmount == 0)
        {
            CheckUpgradeButton();
        }
    }
    // Canvas -> Panel-Menu -> Panel-Shop -> Panel-Shop-Player-Container -> Panel-Player-Stats -> Button-Player-Upgrade-BoxPushingTime'a atandı
    public void UpgradePlayerBoxPushingTime()
    {
        Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStat.myBombPushingTime -= 0.1f;
        if (Save_Load_Manager.Instance.gameData.allPlayers[playerOrder].playerStatAmount == 0)
        {
            CheckUpgradeButton();
        }
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
            Warning_Manager.Instance.NotHaveGold();
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
        SetMyLevelButtons();
        SetDungeonSetting();
        SetDaily();
        SetToolBuyButton();
        SetMaterialBuyButton();

        buttonOffer1.onClick.AddListener(EarnOffer1);
        buttonOffer2.onClick.AddListener(EarnOffer2);
        buttonOffer3.onClick.AddListener(EarnOffer3);
        buttonDoubleReward.onClick.AddListener(EarnDoubleReward);

        buttonLevelHelpLife.onClick.AddListener(LevelStartHelpLife);
        imageLevelHelpLife = buttonLevelHelpLife.transform.Find("Button-Upgrade").GetComponent<Image>();
        buttonLevelHelpPower.onClick.AddListener(LevelStartHelpPower);
        imageLevelHelpPower = buttonLevelHelpPower.transform.Find("Button-Upgrade").GetComponent<Image>();
        buttonLevelHelpAmount.onClick.AddListener(LevelStartHelpAmount);
        imageLevelHelpAmount = buttonLevelHelpAmount.transform.Find("Button-Upgrade").GetComponent<Image>();

        butonIncBombPower.onClick.AddListener(IncBombPower);
        butonIncBombPowerAds.onClick.AddListener(IncBombPowerAds);
        butonDecBombPower.onClick.AddListener(DecBombPower);

        butonIncBombLimit.onClick.AddListener(IncBombLimit);
        butonIncBombLimitAds.onClick.AddListener(IncBombLimitAds);
        butonDecBombLimit.onClick.AddListener(DecBombLimit);

        butonIncBombFireTime.onClick.AddListener(IncBombFireTime);
        butonIncBombFireTimeAds.onClick.AddListener(IncBombFireTimeAds);
        butonDecBombFireTime.onClick.AddListener(DecBombFireTime);
    }
    public void SetGold()
    {
        Audio_Manager.Instance.PlayGoldChance();
        textMenuGoldAmount.text = Save_Load_Manager.Instance.gameData.gold.ToString();
        textCreatorGoldAmount.text = Save_Load_Manager.Instance.gameData.gold.ToString();
        textCraftGoldAmount.text = Save_Load_Manager.Instance.gameData.gold.ToString();
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
            textCraftGoldAmount.text = (goldChangedStaertedAmount + (int)value).ToString();
        }, startValue: 0, endValue: goldChangedAmount, duration: 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            goldChangedAmount = 0;
            Save_Load_Manager.Instance.SaveGame();
        });
    }
    #endregion

    #region Bomb
    private void SetBombText()
    {
        // Setting BombAmount
        for (int e = allBombs.Count - 1; e >= 0; e--)
        {
            Destroy(allBombs[e].rectBombParent.gameObject);
            allBombs.RemoveAt(e);
        }
        Button butonSimple = rectBombButtonSimple.GetChild(0).GetComponent<Button>();
        butonSimple.onClick.RemoveAllListeners();
        for (int e = 0; e < Save_Load_Manager.Instance.gameData.allSpecialBomb.Count; e++)
        {
            int order = e;
            RectTransform rectBomb = Instantiate(rectBombButtonSimple, objBombButtonHolder);
            rectBomb.name = "Panel-Special-Bomb-" + Save_Load_Manager.Instance.gameData.allSpecialBomb[e].bombType;
            rectBomb.DORotate(Vector3.forward * 180, 0.5f);

            Image image = rectBomb.GetChild(0).GetComponent<Image>();
            image.sprite = all_Item_Holder.BombList[(int)Save_Load_Manager.Instance.gameData.allSpecialBomb[e].bombType].MyIcon;

            Button buton = rectBomb.GetChild(0).GetComponent<Button>();
            buton.name = "Button-Special-Bomb-" + Save_Load_Manager.Instance.gameData.allSpecialBomb[e].bombType;
            buton.onClick.AddListener(() =>
            {
                UseSpecialBomb(order);
            });

            TextMeshProUGUI textBombAmount = rectBomb.GetChild(1).GetComponent<TextMeshProUGUI>();
            textBombAmount.name = "Text-Special-Bomb-" + Save_Load_Manager.Instance.gameData.allSpecialBomb[e].bombType;
            textBombAmount.text = Save_Load_Manager.Instance.gameData.allSpecialBomb[e].bombAmount.ToString();

            TextMeshProUGUI textBombPower = rectBomb.GetChild(2).GetComponent<TextMeshProUGUI>();
            textBombPower.name = "Text-Special-Bomb-Power-" + Save_Load_Manager.Instance.gameData.allSpecialBomb[e].bombType;
            textBombPower.text = Save_Load_Manager.Instance.gameData.allSpecialBomb[e].bombPower.ToString();

            TextMeshProUGUI textBombLimit = rectBomb.GetChild(3).GetComponent<TextMeshProUGUI>();
            textBombLimit.name = "Text-Special-Bomb-Limit-" + Save_Load_Manager.Instance.gameData.allSpecialBomb[e].bombType;
            textBombLimit.text = Save_Load_Manager.Instance.gameData.allSpecialBomb[e].bombLimit.ToString();

            TextMeshProUGUI textBombFireTime = rectBomb.GetChild(4).GetComponent<TextMeshProUGUI>();
            textBombFireTime.name = "Text-Special-Bomb-Fire-Time-" + Save_Load_Manager.Instance.gameData.allSpecialBomb[e].bombType;
            textBombFireTime.text = Save_Load_Manager.Instance.gameData.allSpecialBomb[e].bombFireTime.ToString();

            allBombs.Add(new BombList(rectBomb, textBombAmount, textBombPower, textBombLimit, textBombFireTime));
        }
        choosedBomb = rectBombButtonSimple;
        butonSimple.onClick.AddListener(() =>
        {
            UseSimpleBomb();
        });
    }
    private void UseSimpleBomb()
    {
        if (Game_Manager.Instance.LevelStart)
        {
            player_Base.UseSimpleBomb();
        }
    }
    private void UseSpecialBomb(int order)
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (Game_Manager.Instance.GameType == GameType.MapCreate)
        {
            if (!Map_Holder.Instance.BoardGate.IsGameFinish)
            {
                return;
            }
        }
        player_Base.UseSpecialBomb(order);
    }
    // Canvas -> Panel-Game -> Button-Bomb-Clock-Activiter'a atandı
    public void UseBombClockActiviter()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        Player_Base.Instance.UseBombClockActiviter();
        if (Save_Load_Manager.Instance.gameData.allSpecialBomb[bombOrder].bombAmount == 0)
        {
            bombOrder = 0;
            changeTime = true;
            bombClockActiviter.SetActive(false);
            allBombs[bombOrder].rectBombParent.DORotate(Vector3.forward * 180, 0.5f).OnComplete(() =>
            {
                choosedBomb = rectBombButtonSimple;
                choosedBomb.DORotate(Vector3.zero, 0.5f).OnComplete(() =>
                {
                    changeTime = false;
                });
            });
        }
    }
    // Canvas -> Panel-Game -> Button-Bomb-Change'a atandı.
    public void ChangeBomb()
    {
        if (changeTime)
        {
            return;
        }
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        changeTime = true;
        bombClockActiviter.SetActive(false);
        choosedBomb.DORotate(Vector3.forward * 180, 0.5f).OnComplete(() =>
        {
            bool findedBomb = false;
            for (int e = bombOrder; e < Save_Load_Manager.Instance.gameData.allSpecialBomb.Count && !findedBomb; e++)
            {
                if (Save_Load_Manager.Instance.gameData.allSpecialBomb[e].bombAmount > 0)
                {
                    bombOrder = e + 1;
                    findedBomb = true;
                    choosedBomb = allBombs[e].rectBombParent;
                    choosedBomb.DORotate(Vector3.zero, 0.5f).OnComplete(() =>
                    {
                        changeTime = false;
                    });
                }
                else if (Save_Load_Manager.Instance.gameData.allSpecialBomb[e].bombType == BombType.Clock && player_Base.ClocksAmount > 0)
                {
                    bombOrder = e + 1;
                    findedBomb = true;
                    choosedBomb = allBombs[e].rectBombParent;
                    choosedBomb.DORotate(Vector3.zero, 0.5f).OnComplete(() =>
                    {
                        changeTime = false;
                        bombClockActiviter.SetActive(true);
                    });
                }
            }
            if (!findedBomb)
            {
                bombOrder = 0;
                findedBomb = true;
                choosedBomb = rectBombButtonSimple;
                choosedBomb.DORotate(Vector3.zero, 0.5f).OnComplete(() => changeTime = false);
            }
        });
    }
    public void AddBombPanel(BombType bombType, int bombPower, int bombLimit, float bombFireTime)
    {
        int order = Save_Load_Manager.Instance.gameData.allSpecialBomb.Count;
        Save_Load_Manager.Instance.gameData.allSpecialBomb.Add(new BombAmount(bombType, 1, bombPower, bombLimit, bombFireTime));
        RectTransform rectBomb = Instantiate(rectBombButtonSimple, objBombButtonHolder);
        rectBomb.name = "Panel-Special-Bomb-" + bombType;
        rectBomb.DORotate(Vector3.forward * 180, 0.5f);

        Image image = rectBomb.GetChild(0).GetComponent<Image>();
        image.sprite = all_Item_Holder.BombList[(int)bombType].MyIcon;

        Button buton = rectBomb.GetChild(0).GetComponent<Button>();
        buton.name = "Button-Special-Bomb-" + bombType;
        buton.onClick.AddListener(() =>
        {
            UseSpecialBomb(order);
        });

        TextMeshProUGUI textBombAmount = rectBomb.GetChild(1).GetComponent<TextMeshProUGUI>();
        textBombAmount.name = "Text-Special-Bomb-" + bombType;
        textBombAmount.text = 1.ToString();

        TextMeshProUGUI textBombPower = rectBomb.GetChild(1).GetComponent<TextMeshProUGUI>();
        textBombPower.name = "Text-Special-Bomb-Power-" + bombType;
        textBombPower.text = bombPower.ToString();

        TextMeshProUGUI textBombLimit = rectBomb.GetChild(1).GetComponent<TextMeshProUGUI>();
        textBombLimit.name = "Text-Special-Bomb-Limit-" + bombType;
        textBombLimit.text = bombLimit.ToString();

        TextMeshProUGUI textBombFireTime = rectBomb.GetChild(1).GetComponent<TextMeshProUGUI>();
        textBombFireTime.name = "Text-Special-Bomb-Fire-Time-" + bombType;
        textBombFireTime.text = bombFireTime.ToString();

        allBombs.Add(new BombList(rectBomb, textBombAmount, textBombPower, textBombLimit, textBombFireTime));
    }
    public void SetBomb(int order)
    {
        allBombs[order].textBombAmount.text = Save_Load_Manager.Instance.gameData.allSpecialBomb[order].bombAmount.ToString();
        if (Save_Load_Manager.Instance.gameData.allSpecialBomb[order].bombAmount == 0)
        {
            if (Save_Load_Manager.Instance.gameData.allSpecialBomb[order].bombType != BombType.Clock)
            {
                changeTime = true;
                choosedBomb.DORotate(Vector3.forward * 180, 0.5f).OnComplete(() =>
                {
                    choosedBomb = rectBombButtonSimple;
                    choosedBomb.DORotate(Vector3.zero, 0.5f).OnComplete(() =>
                    {
                        changeTime = false;
                        bombOrder = 0;
                    });
                });
            }
        }
    }
    #endregion

    #region Player Stats
    private void Instance_OnGameStart(object sender, EventArgs eventArgs)
    {
        SetPlayerSettingPanel(true);
        imagePlayerIcon.sprite = all_Item_Holder.PlayerSourceList[Save_Load_Manager.Instance.gameData.playerOrder].MyIcon;
        textPlayerName.text = Save_Load_Manager.Instance.gameData.accountName;
        SetPlayerLifeText();
        SetPlayerSpeedText();
        SetPlayerBombAmountText();
        SetPlayerPowerText();
        SetPlayerBombFireLimitText();
        SetPlayerBoxPassingText();
        SetPlayerBoxPushingTimeText();
        panelGame.SetActive(true);
        SetActiveMapProcessHolder(false);
        SetBombText();

        // Setting BombAmount
        for (int e = 1; e < allBombs.Count; e++)
        {
            SetBomb(e);
        }
        choosedBomb = rectBombButtonSimple;
        choosedBomb.DORotate(Vector3.zero, 0.5f);
    }
    public void SetLevel()
    {
        int exp = Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerExp;
        int expMax = Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerExpMax;
        int level = Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerLevel;

        textPlayerExp.text = "Exp : " + exp.ToString();
        float percent = 1.0f * exp / expMax;
        textPlayerLevel.text = "Level : " + level + " - %" + (percent * 100).ToString("N2");
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
    public void SetPlayerSpeedText()
    {
        textPlayerSpeed.text = (Player_Base.Instance.MySpeed * 0.01f).ToString();
        textPlayerSpeed.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerSpeed.DOAnchorPos(new Vector3(60, 0, 0), 0.5f);
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
    public void SetPlayerPowerText()
    {
        textPlayerPower.text = Player_Base.Instance.MyBombPower.ToString();
        textPlayerPower.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerPower.DOAnchorPos(new Vector3(60, 0, 0), 0.5f);
        });
    }
    public void SetPlayerBombFireLimitText()
    {
        textPlayerBombFireLimit.text = Player_Base.Instance.MyBombFireLimit.ToString();
        textPlayerBombFireLimit.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerBombFireLimit.DOAnchorPos(new Vector3(60, 0, 0), 0.5f);
        });
    }
    public void SetPlayerBoxPassingText()
    {
        textPlayerBoxPassing.text = Player_Base.Instance.MyBombBoxPassing.ToString();
        textPlayerBoxPassing.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerBoxPassing.DOAnchorPos(new Vector3(60, 0, 0), 0.5f);
        });
    }
    public void SetPlayerBoxPushingTimeText()
    {
        textPlayerBoxPushingTime.text = Player_Base.Instance.MyBombPushingTime.ToString();
        textPlayerBoxPushingTime.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
        {
            rectPlayerBoxPushingTime.DOAnchorPos(new Vector3(60, 0, 0), 0.5f);
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
            Warning_Manager.Instance.ShowMessage("Board Size Width must be integer number.");
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
            Warning_Manager.Instance.ShowMessage("Board Size Height must be integer number.");
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
            Warning_Manager.Instance.ShowMessage("The number of Wall must be integer number.");
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
                    Warning_Manager.Instance.ShowMessage("You must select at least 1 type of box.");
                    correctMagicStoneAmount = false;
                }
            }
        }
        else
        {
            Warning_Manager.Instance.ShowMessage("The number of Magic Stone must be integer number.");
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
                    Warning_Manager.Instance.ShowMessage("You must choose some type of Box.");
                    correctBoxAmount = false;
                }
            }
        }
        else
        {
            Warning_Manager.Instance.ShowMessage("The number of Boxes must be integer number.");
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
            Warning_Manager.Instance.ShowMessage("The number of Trap must be integer number.");
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
            Warning_Manager.Instance.ShowMessage("The number of Enemy must be integer number.");
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
            Warning_Manager.Instance.ShowMessage("The number of Boss Enemy must be integer number.");
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
            Warning_Manager.Instance.ShowMessage("The number of Magic Stone must be integer number.");
            correctMagicStoneAmount = false;
        }
        Map_Creater_Manager.Instance.SetMagicStoneBoard(magicStoneAmount);
        CheckDungeonSettingCorrect();
    }
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Panel-Dungeon-Setting -> Toggle-Free-Start-Random'a atandı
    public void DungeonCreateSetting(bool isOn)
    {
        panelCreateSetting.DOScaleY(isOn ? 1 : 0, 1.0f);
        CheckDungeonSettingCorrect();
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
        for (int e = wallChooseParent.childCount - 1; e >= 0; e--)
        {
            Destroy(wallChooseParent.GetChild(e).gameObject);
        }
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
        for (int e = boxChooseParent.childCount - 1; e >= 0; e--)
        {
            Destroy(boxChooseParent.GetChild(e).gameObject);
        }
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
        for (int e = trapChooseParent.childCount - 1; e >= 0; e--)
        {
            Destroy(trapChooseParent.GetChild(e).gameObject);
        }
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
        for (int e = enemyChooseParent.childCount - 1; e >= 0; e--)
        {
            Destroy(enemyChooseParent.GetChild(e).gameObject);
        }
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
        for (int e = bossEnemyChooseParent.childCount - 1; e >= 0; e--)
        {
            Destroy(bossEnemyChooseParent.GetChild(e).gameObject);
        }
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
    // Canvas -> Panel-Creator -> Panel-Placement -> Button-Placement-Type'a atandı
    public void SetCreatingObjectAmount()
    {
        isMultiple = !isMultiple;
        imageMultiplePlacement.gameObject.SetActive(isMultiple);
        inputMultiplePlacementAmount.gameObject.SetActive(isMultiple);
        objCreatingMultipleObject.SetActive(isMultiple);
        objEmptyArea.SetActive(isMultiple);
        textCreateObjectSetting.text = "";
        Map_Holder.Instance.SetMultiplePlacementObject(null);

        if (isMultiple)
        {
            textPlacementType.text = "Single Placement";
            textCreateObjectSetting.text = "Choose a Board Object.";
        }
        else
        {
            textPlacementType.text = "Multiple Random Placement";
            inputMultiplePlacementAmount.text = 0.ToString();
            textCreateObjectSetting.text = "";
            imageMultiplePlacement.sprite = emptySlotIcon;
            amountMultiple = 0;
        }
    }
    // Canvas -> Panel-Creator -> Panel-Placement -> InputField-Multiple-Placement-Amount'a atandı
    public void SetMultiplePlacementAmount(string amountText)
    {
        if (string.IsNullOrEmpty(amountText))
        {
            textCreateObjectSetting.text = "Amount Text can not be empty.";
        }
        if (int.TryParse(amountText, out int amount))
        {
            amountMultiple = amount;
            textCreateObjectSetting.text = "You will placement " + amount + " board object.";
        }
        else
        {
            textCreateObjectSetting.text = "Amount Text must be integer number.";
        }
    }
    // Canvas -> Panel-Creator -> Panel-Placement -> Button-Create-Placement'a atandı
    public void CreateMultiplePlacement()
    {
        Map_Holder.Instance.CreateMultiplePlacement();
    }
    public void SetMultiplePlacementObject(Item_Board item)
    {
        if (item.MyBoardType == BoardType.Gate)
        {
            Warning_Manager.Instance.ShowMessage("You cannot place Gate Object in more than one place.", 2);
            SetCreatingObjectAmount();
            textCreateObjectSetting.text = "";
            return;
        }
        imageMultiplePlacement.sprite = item.MyIcon;
        Map_Holder.Instance.SetMultiplePlacementObject(item);
        textCreateObjectSetting.text = "You place a " + item.MyBoardType + " type object.";
    }
    // Canvas -> Panel-Creator -> Panel-Map-Size -> Button-Create'a atandı.
    public void CreateMapBoard()
    {
        panelBoardSize.SetActive(false);
        objCheckingMapButton.SetActive(Save_Load_Manager.Instance.SaveType == BoardSaveType.MyLevel);
        buttonSaveMap.interactable = Save_Load_Manager.Instance.SaveType == BoardSaveType.GameLevel;
        Debug.Log("CreateMapBoard");
        Map_Creater_Manager.Instance.SetBoardSize(int.Parse(inputBoardSizeX.text), int.Parse(inputBoardSizeY.text));
    }
    public void MapProcess(float process)
    {
        imageProcess.fillAmount = process;
        textProcess.text = (process * 100).ToString("N2");
    }
    public void MapProcessBase(string processBase)
    {
        textProcessBase.text = processBase + " Process -> % ";
        imageProcess.fillAmount = 0;
        textProcess.text = 0.ToString("N2");
    }
    public void SetActiveMapProcessHolder(bool process)
    {
        panelProcessHolder.SetActive(process);
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
            CloseAllObjectTypePanels();
            panelMenu.SetActive(false);
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
    public void SetPanelCreatorButtonTypeList(bool isActive)
    {
        objCreatorButtonTypeList.SetActive(isActive);
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
        // Obje listesi
        objCreatorTypeList.SetActive(isActive);
    }
    // Canvas -> Panel-Creator -> Panel-Objects-Behaviour -> Panel-Simple -> Button-Move'a atandı
    public void MoveObject()
    {
        Map_Creater_Manager.Instance.MoveObject();
    }
    // Canvas -> Panel-Creator -> Panel-Objects-Behaviour -> Panel-Simple -> Button-Turn'a atandı
    public void TurnObject()
    {
        Map_Creater_Manager.Instance.TurnObject();
    }
    // Canvas -> Panel-Creator -> Panel-Objects-Behaviour -> Panel-Simple -> Button-Destroy'a atandı
    public void DestroyObject()
    {
        Map_Creater_Manager.Instance.DestroyObject();
    }
    public void CloseAllObjectTypePanels()
    {
        objPlacement.SetActive(false);
        allPanelObjectTypes.ForEach(obj => obj.SetActive(false));
    }
    // Canvas -> Panel-Creator -> Panel-Creator-Type içindeki butonlara atandı
    public void CloseAllObjectTypePanels(GameObject obj)
    {
        objPlacement.SetActive(true);
        allPanelObjectTypes.ForEach(obj => obj.SetActive(false));
        obj.SetActive(true);
    }
    #endregion

    #region Creator Object Setting
    // Canvas -> Panel-Creator -> Panel-Objects-Behaviour -> Button-Close'a atandı
    public void CloseBaseSetting()
    {
        SetCreatorButtons(true);
        panelSettingBase.SetActive(false);
        Map_Creater_Manager.Instance.FixOrder();
        if (Map_Creater_Manager.Instance.TrapTrigger != null)
        {
            Map_Creater_Manager.Instance.TrapTrigger.transform.localScale = Vector3.one * 1;
            Map_Creater_Manager.Instance.ChooseTrigger(null);
        }
        if (Map_Creater_Manager.Instance.TrapBase != null)
        {
            Map_Creater_Manager.Instance.TrapBase.transform.localScale = Vector3.one * 1;
            Map_Creater_Manager.Instance.ChooseTrap(null);
        }
    }
    /// <summary>
    /// Objelerin ayar panelini açmaya yarar
    /// </summary>
    /// <param name="canMove">Hareket ettirme butonunu açar kapatır. Slottan konulduysa false olabilir.</param>
    public void OpenBaseSetting(bool canMove)
    {
        SetCreatorButtons(false);
        panelSettingBase.SetActive(true);
        objButtonMove.SetActive(canMove);
    }
    public void CloseSettingPanels()
    {
        toggleSettingTrigger.gameObject.SetActive(false);
        toggleSettingAlwaysActive.gameObject.SetActive(false);
        toggleSettingTimer.gameObject.SetActive(false);
        toggleStartingWithActivated.gameObject.SetActive(false);
        panelSettingForObjectTimer1.gameObject.SetActive(false);
        panelSettingForObjectTimer2.gameObject.SetActive(false);
        panelSettingForObjectEffect.gameObject.SetActive(false);
        textSettingInfo.text = "Click for immobilize.";
    }
    /// <summary>
    /// Trigger sürekli mi yoksa 1 kere mi aktif edecek
    /// </summary>
    /// <param name="canMove">True is 1 time work. False is always aktif-deaktive</param>
    public void OpenTriggerSetting()
    {
        toggleSettingTrigger.gameObject.SetActive(true);
        toggleSettingTrigger.onValueChanged.RemoveAllListeners();
        toggleSettingTrigger.onValueChanged.AddListener((bool isOn) =>
        {
            SetTrigger(isOn);
        });
    }
    /// <summary>
    /// Objenin triggere ihtiyacı varsa çağrılır.
    /// </summary>
    /// <param name="isNeedTrigger">Trigger seçme butonu açık veya kapalı.</param>
    public void OpenTriggerForObject(bool isNeedTrigger)
    {
        toggleSettingTrigger.gameObject.SetActive(isNeedTrigger);
        toggleSettingTrigger.onValueChanged.RemoveAllListeners();
        toggleSettingTrigger.isOn = true;
        toggleSettingTrigger.onValueChanged.AddListener((bool isOn) =>
        {
            SetForTrigger(isOn);
        });
    }
    /// <summary>
    /// Objenin sürekli çalışmasını sağlayacak toggle ı açar veya kapatır
    /// </summary>
    /// <param name="isActive">Toggle açık veya kapalı.</param>
    public void OpenPanelSettingTimerActive(bool isActive)
    {
        toggleSettingAlwaysActive.gameObject.SetActive(isActive);
        toggleSettingAlwaysActive.onValueChanged.RemoveAllListeners();
        toggleSettingAlwaysActive.onValueChanged.AddListener((bool isOn) =>
        {
            SetForTimer2(isOn);
            SetForAlwaysActivited(isOn);
        });
    }
    /// <summary>
    /// Obje zamanlı çalıştırıp kapatacak inputları açıp kapatır.
    /// </summary>
    /// <param name="isTimer2">2. Zaman inputu açık veya kapalı</param>
    public void OpenPanelSettingTimer(bool isTimer2)
    {
        toggleStartingWithActivated.gameObject.SetActive(false);
        panelSettingForObjectTimer1.SetActive(false);
        panelSettingForObjectTimer2.SetActive(false);
        panelSettingForObjectEffect.gameObject.SetActive(false);
        toggleSettingTimer.onValueChanged.RemoveAllListeners();
        if (isTimer2)
        {
            toggleSettingTimer.onValueChanged.AddListener((bool isOn) =>
            {
                //textSettingInfo.text = "Set object's time for waiting and working.";
                SetForTimer1(isOn);
                SetForTimer2(isOn);
            });
        }
        else
        {
            toggleSettingTimer.onValueChanged.AddListener((bool isOn) =>
            {
                //textSettingInfo.text = "Set object's time for waiting.";
                SetForTimer1(isOn);
            });
        }
    }
    // Trigger seçilince working seçme toggle ına verilecek
    public void SetTrigger(bool isOn)
    {
        textSettingInfo.text = "Choose Trigger working times. Choosing is 1 time, not choosing is always work.";
        Map_Creater_Manager.Instance.TrapTrigger.SetWorkTime(isOn);
    }
    // Obje seçilince trigger seçme toggle ına verilecek
    public void SetForTrigger(bool isOn)
    {
        panelTriggerList.SetActive(isOn);
        if (Map_Creater_Manager.Instance.TrapBase.TryGetComponent(out IHasTrigger hasTrigger))
        {
            hasTrigger.SetHasTrigger(isOn);
        }
        if (isOn)
        {
            for (int e = panelTriggerListParent.childCount - 1; e >= 0; e--)
            {
                Destroy(panelTriggerListParent.GetChild(e).gameObject);
            }
            for (int e = 0; e < Map_Holder.Instance.TrapObjects.Count; e++)
            {
                if (Map_Holder.Instance.TrapObjects[e].TryGetComponent(out Trap_Trigger trigger))
                {
                    Slot_Trigger trigSlot = Instantiate(slotTrigger, panelTriggerListParent);
                    trigSlot.SlotFull(trigger);
                }
            }

            textSettingInfo.text = "Choose trigger for object. You can see trigger list.";
            toggleSettingAlwaysActive.gameObject.SetActive(false);
            toggleSettingAlwaysActive.SetIsOnWithoutNotify(false);
            panelSettingForObjectEffect.gameObject.SetActive(false);

            toggleSettingTimer.gameObject.SetActive(false);
            toggleSettingTimer.SetIsOnWithoutNotify(false);

            toggleStartingWithActivated.gameObject.SetActive(false);
            panelSettingForObjectTimer1.SetActive(false);
            panelSettingForObjectTimer2.SetActive(false);
        }
        else
        {
            textSettingInfo.text = "Choose a behaviour for object.";
            toggleSettingAlwaysActive.gameObject.SetActive(true);
            toggleSettingAlwaysActive.SetIsOnWithoutNotify(false);

            toggleSettingTimer.gameObject.SetActive(true);
            toggleSettingTimer.SetIsOnWithoutNotify(false);
        }
    }
    // Obje trigger ile aktif olduğunda, aktif olarak mı başlayacak yoksa pasif olarak mı ?
    // Canvas -> Panel-Creator -> Panel-Objects-Behaviour -> Toggle-StartActivatedWithTrigger'a atandı
    public void SetForTriggerActivated(bool isOn)
    {
        if (Map_Creater_Manager.Instance.TrapBase.TryGetComponent(out ISetActivited startActivate))
        {
            startActivate.SetActivited(isOn);
        }
    }
    // Obje sçeilince sürekli aktiflik toggle ına verilecek
    public void SetForAlwaysActivited(bool isOn)
    {
        if (Map_Creater_Manager.Instance.TrapBase.TryGetComponent(out IAlwaysActivite alwaysActivite))
        {
            alwaysActivite.SetAlwaysActivite(isOn);
        }
        panelSettingForObjectEffect.gameObject.SetActive(isOn);
        if (isOn)
        {
            textSettingInfo.text = "Your object will always activated.";
            toggleSettingTrigger.gameObject.SetActive(false);
            toggleSettingTrigger.SetIsOnWithoutNotify(false);
            panelTriggerList.SetActive(false);

            toggleSettingTimer.gameObject.SetActive(false);
            toggleSettingTimer.SetIsOnWithoutNotify(false);

            toggleStartingWithActivated.gameObject.SetActive(false);
            panelSettingForObjectTimer1.SetActive(false);
            panelSettingForObjectTimer2.SetActive(false);
        }
        else
        {
            textSettingInfo.text = "Choose a behaviour for object.";
            toggleSettingTrigger.gameObject.SetActive(true);
            toggleSettingTimer.gameObject.SetActive(true);
        }
    }
    // Obje seçilince objenin türüne göre zaman seçme tagglına atanacak
    public void SetForTimer1(bool isOn)
    {
        panelSettingForObjectTimer1.SetActive(isOn);
        if (isOn)
        {
            textSettingInfo.text = "Set some time for your behaviour.";
            toggleSettingTrigger.gameObject.SetActive(false);
            toggleSettingTrigger.SetIsOnWithoutNotify(false);

            toggleSettingAlwaysActive.gameObject.SetActive(false);
            toggleSettingAlwaysActive.SetIsOnWithoutNotify(false);
            panelSettingForObjectEffect.gameObject.SetActive(false);
        }
        else
        {
            textSettingInfo.text = "Choose a behaviour for object.";
            toggleSettingTrigger.gameObject.SetActive(true);
            toggleSettingAlwaysActive.gameObject.SetActive(true);
        }
    }
    // Obje seçilince objenin türüne göre zaman seçme tagglına atanacak
    public void SetForTimer2(bool isOn)
    {
        panelSettingForObjectTimer2.SetActive(isOn);
        toggleStartingWithActivated.gameObject.SetActive(isOn);
    }
    // Obje seçilince obje aktif olarak mı başlasın
    // Canvas -> Panel-Creator -> Panel-Objects-Behaviour -> Toggle-StartActivated'a atandı
    public void StartingWithActivated(bool isOn)
    {
        if (Map_Creater_Manager.Instance.TrapBase.TryGetComponent(out ISetActivited setActivited))
        {
            setActivited.SetActivited(isOn);
            textSettingInfo.text = "Starting with " + (isOn ? "activated." : "deActivated.");
        }
    }
    // Canvas -> Panel-Creator -> Panel-Objects-Behaviour -> Panel-Time-For-Effect -> InputField--For-Effect'a atandı
    public void SetTimerEffect(string time)
    {
        if (string.IsNullOrEmpty(time))
        {
            textSettingInfo.text = "Effect time can not be empty.";
        }
        if (float.TryParse(time, out float effectTime))
        {
            if (Map_Creater_Manager.Instance.TrapBase.TryGetComponent(out ISetEffectTime setEffectTime))
            {
                setEffectTime.SetEffectTime(effectTime);
                textSettingInfo.text = "Effect time set.";
            }
        }
        else
        {
            textSettingInfo.text = "Effect time must be decimal number or integer number.";
        }
    }
    // Canvas -> Panel-Creator -> Panel-Objects-Behaviour -> Panel-Time-For-Waiting -> InputField--For-Waiting'a atandı
    public void SetTimerWaiting(string time)
    {
        if (string.IsNullOrEmpty(time))
        {
            textSettingInfo.text = "Waiting time can not be empty.";
        }
        if (float.TryParse(time, out float waitTime))
        {
            if (Map_Creater_Manager.Instance.TrapBase.TryGetComponent(out ISetWaitingTime setWaitingTime))
            {
                setWaitingTime.SetWaitingTime(waitTime);
                textSettingInfo.text = "Waiting time set.";
            }
        }
        else
        {
            textSettingInfo.text = "Waiting time must be decimal number or integer number.";
        }
    }
    // Canvas -> Panel-Creator -> Panel-Objects-Behaviour -> Panel-Time-For-Working -> InputField--For-Working'a atandı
    public void SetTimerWorking(string time)
    {
        if (string.IsNullOrEmpty(time))
        {
            textSettingInfo.text = "Working time can not be empty.";
        }
        if (float.TryParse(time, out float workTime))
        {
            if (Map_Creater_Manager.Instance.TrapBase.TryGetComponent(out ISetWorkingTime setWorkingTime))
            {
                setWorkingTime.SetWorkingTime(workTime);
                textSettingInfo.text = "Working time set.";
            }
        }
        else
        {
            textSettingInfo.text = "Working time must be decimal number or integer number.";
        }
    }
    #endregion

    #region Creator Slot
    private void SetGateCreatorSlot()
    {
        for (int e = 0; e < all_Item_Holder.GateList.Count; e++)
        {
            Slot_Creator slot = Instantiate(slotCreator, gateParent);
            slot.SetSlot(all_Item_Holder.GateList[e]);
            slot.gameObject.SetActive(true);
        }
    }
    private void SetWallCreatorSlot()
    {
        for (int e = 0; e < all_Item_Holder.WallList.Count; e++)
        {
            Slot_Creator slot = Instantiate(slotCreator, wallParent);
            slot.SetSlot(all_Item_Holder.WallList[e]);
            slot.gameObject.SetActive(true);
        }
    }
    private void SetBoxCreatorSlot()
    {
        for (int e = 0; e < all_Item_Holder.BoxList.Count; e++)
        {
            Slot_Creator slot = Instantiate(slotCreator, boxParent);
            slot.SetSlot(all_Item_Holder.BoxList[e]);
            slot.gameObject.SetActive(true);
        }
    }
    private void SetTrapCreatorSlot()
    {
        for (int e = 0; e < all_Item_Holder.TrapList.Count; e++)
        {
            Slot_Creator slot = Instantiate(slotCreator, trapParent);
            slot.SetSlot(all_Item_Holder.TrapList[e]);
            slot.gameObject.SetActive(true);
        }
    }
    private void SetEnemyCreatorSlot()
    {
        for (int e = 0; e < all_Item_Holder.EnemyList.Count; e++)
        {
            Slot_Creator slot = Instantiate(slotCreator, enemyParent);
            slot.SetSlot(all_Item_Holder.EnemyList[e]);
            slot.gameObject.SetActive(true);
        }
    }
    private void SetBossEnemyCreatorSlot()
    {
        for (int e = 0; e < all_Item_Holder.BossEnemyList.Count; e++)
        {
            Slot_Creator slot = Instantiate(slotCreator, bossEnemyParent);
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

        // Delete 0 Amount Special Bomb
        for (int i = Save_Load_Manager.Instance.gameData.allSpecialBomb.Count - 1; i >= 0; i--)
        {
            if (Save_Load_Manager.Instance.gameData.allSpecialBomb[i].bombAmount <= 0)
            {
                Save_Load_Manager.Instance.gameData.allSpecialBomb.RemoveAt(i);
            }
        }
    }
    // Canvas -> Panel-Game-Finish -> Button-Menu'a atandı
    public void GoMenu()
    {
        OpenMask(0.05f, () =>
        {
            player_Base.transform.GetChild(0).eulerAngles = Vector3.zero;
            SendMapBoardToPool();
            panelMenu.SetActive(true);
            panelGame.SetActive(false);
            CloseMask(0.25f, null);
        });
    }
    // Canvas -> Panel-Game-Finish -> Button-Next'a atandı
    public void NextLevel()
    {
        Reload();
    }
    // Canvas -> Panel-Game-Finish -> Button-Reload'a atandı
    public void Reload()
    {
        OpenMask(0.05f, () =>
        {
            SendMapBoardToPool();
            CloseMask(0.25f, () =>
            {
                Map_Construct_Manager.Instance.ConstructMap(Save_Load_Manager.Instance.LoadBoard(BoardSaveType.GameLevel, Save_Load_Manager.Instance.gameData.lastLevel));
            });
        });
    }
    private void SendMapBoardToPool()
    {
        panelGame.SetActive(false);
        player_Base.ResetBase();
        panelGameFinish.SetActive(false);
        Map_Holder.Instance.CloseBoardGround();
        Map_Holder.Instance.SendToPoolAllObjects();
        Map_Holder.Instance.SendToPoolOutSideWallObjects();
    }
    // Canvas -> Panel-Game-Finish -> Panel-Container -> Panel-Reward -> Button-Double'a atandı
    public void EarnDoubleReward()
    {
        // Power, Fire limit
        Reklam_Manager.Instance.RewardShowAd(() =>
        {
            Game_Manager.Instance.GiveToPlayerExp(Game_Manager.Instance.EarnExp);
            SetGoldSmooth(Game_Manager.Instance.EarnGold);
            buttonDoubleReward.interactable = false;
        });
    }
    public void EarnOffer1()
    {
        // Power, Fire limit
        Reklam_Manager.Instance.RewardShowAd(() =>
        {
            Game_Manager.Instance.GiveToPlayerExp(500);
            buttonOffer1.interactable = false;
        });
    }
    public void EarnOffer2()
    {
        // Power, Fire limit
        Reklam_Manager.Instance.RewardShowAd(() =>
        {
            SetGoldSmooth(100);
            buttonOffer2.interactable = false;
        });
    }
    public void EarnOffer3()
    {
        buttonOffer3.interactable = false;
        Warning_Manager.Instance.ShowMessage("Magic system will come next upgrade.");
        //// Power, Fire limit
        //Reklam_Manager.Instance.RewardShowAd(() =>
        //{
        //    Debug.LogWarning("Offer 3 verild.");
        //    // 1 Magic verilecek ilerde
        //    buttonOffer3.interactable = false;
        //});
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
                // Open Mask
                DOTween.To(value => { sceneMaskedImage.localScale = Vector3.one * value; }, startValue: 0, endValue: 2, duration: 1.5f)
                .SetEase(Ease.Linear).OnComplete(() =>
                {
                    action?.Invoke();
                });
            });
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    #endregion

    #region Craft
    public void SetRecipeList(Item_Bomb bombItem)
    {
        myRecipeItem = null;
        buttonCraftIcon.sprite = craftSlotIcon;
        for (int e = 0; e < myMaterialList.Count; e++)
        {
            myMaterialList[e].SlotRelease();
        }
        for (int e = 0; e < myRecipeList.Count; e++)
        {
            myRecipeList[e].SlotRelease();
        }
        for (int e = 0; e < bombItem.MyRecipeList.Count; e++)
        {
            myRecipeList[e].SetRecipeSlot(bombItem.MyRecipeList[e]);
        }
        objBombStatPanel.SetActive(false);
    }
    public void SetMaterialList(Item_Recipe recipeItem)
    {
        myRecipeItem = recipeItem;
        buttonCraftIcon.sprite = myRecipeItem.MyIcon;
        for (int e = 0; e < myMaterialList.Count; e++)
        {
            myMaterialList[e].SlotRelease();
        }
        for (int e = 0; e < recipeItem.MyNeededItemList.Count; e++)
        {
            myMaterialList[e].SetMaterialSlot(recipeItem.MyNeededItemList[e]);
        }
        if (myRecipeItem is Item_Bomb)
        {
            objBombStatPanel.SetActive(true);

            bombPower = 1;
            bombLimit = 1;
            bombFireTime = 0.5f;

            Item_Bomb itemBomb = myRecipeItem as Item_Bomb;
            imageBombPowerIcon.sprite = itemBomb.MyItemPower.MyIcon;
            imageBombLimitIcon.sprite = itemBomb.MyItemLimit.MyIcon;
            imageBombFireTimeIcon.sprite = itemBomb.MyItemFireTime.MyIcon;

            textBombPowerAmount.text = "0";
            textBombLimitAmount.text = "0";
            textBombFireTimeAmount.text = "0";

            bool hasPowerStone = Inventory_Manager.Instance.HasItem(itemBomb.MyItemPower, bombPower) == 0;

            butonIncBombPower.gameObject.SetActive(hasPowerStone);
            butonIncBombPowerAds.gameObject.SetActive(!hasPowerStone);

            bool hasLimitStone = Inventory_Manager.Instance.HasItem(itemBomb.MyItemLimit, bombLimit) == 0;
            butonIncBombLimit.gameObject.SetActive(hasLimitStone);
            butonIncBombLimitAds.gameObject.SetActive(!hasLimitStone);

            bool hasFireTimeStone = Inventory_Manager.Instance.HasItem(itemBomb.MyItemFireTime, 1) == 0;
            butonIncBombFireTime.gameObject.SetActive(hasFireTimeStone);
            butonIncBombFireTimeAds.gameObject.SetActive(!hasFireTimeStone);
        }
        else
        {
            objBombStatPanel.SetActive(false);
        }
    }
    private void SetBombPower(int amount)
    {
        bombPower += amount;
        int removedAmount = bombPower - 1;
        textBombPowerAmount.text = removedAmount.ToString();
        Warning_Manager.Instance.ShowMessage((myRecipeItem as Item_Bomb).MyBombType + " bomb power is " + bombPower + " unit.");

        bool hasPowerStone = Inventory_Manager.Instance.HasItem((myRecipeItem as Item_Bomb).MyItemPower, bombPower - 1) == 0;
        butonIncBombPower.gameObject.SetActive(hasPowerStone);
        butonIncBombPowerAds.gameObject.SetActive(!hasPowerStone);
    }
    public void IncBombPower()
    {
        if (bombPower >= 3)
        {
            Warning_Manager.Instance.ShowMessage("The power cannot be increased any further.");
            return;
        }
        SetBombPower(1);
    }
    public void IncBombPowerAds()
    {
        if (bombPower >= 3)
        {
            Warning_Manager.Instance.ShowMessage("The power cannot be increased any further.");
            return;
        }
        Reklam_Manager.Instance.RewardShowAd(() =>
        {
            SetBombPower(1);
        });
    }
    public void DecBombPower()
    {
        if (bombPower <= 1)
        {
            Warning_Manager.Instance.ShowMessage("The power cannot be decreased any further.");
            return;
        }
        SetBombPower(-1);
    }
    private void SetBombLimit(int amount)
    {
        bombLimit += amount;
        int removedAmount = bombLimit - 1;
        textBombLimitAmount.text = removedAmount.ToString();
        Warning_Manager.Instance.ShowMessage((myRecipeItem as Item_Bomb).MyBombType + " bomb limit is " + bombLimit + " unit.");

        bool hasLimitStone = Inventory_Manager.Instance.HasItem((myRecipeItem as Item_Bomb).MyItemLimit, bombLimit) == 0;
        butonIncBombLimit.gameObject.SetActive(hasLimitStone);
        butonIncBombLimitAds.gameObject.SetActive(!hasLimitStone);
    }
    public void IncBombLimit()
    {
        if (bombLimit >= 3)
        {
            Warning_Manager.Instance.ShowMessage("The Fire Limit cannot be increased any further.");
            return;
        }
        SetBombLimit(1);
    }
    public void IncBombLimitAds()
    {
        if (bombLimit >= 3)
        {
            Warning_Manager.Instance.ShowMessage("The Fire Limit cannot be increased any further.");
            return;
        }
        Reklam_Manager.Instance.RewardShowAd(() =>
        {
            SetBombLimit(1);
        });
    }
    public void DecBombLimit()
    {
        if (bombLimit <= 1)
        {
            Warning_Manager.Instance.ShowMessage("The Fire Limit cannot be decreased any further.");
            return;
        }
        SetBombLimit(-1);
    }
    private void SetBombFireTime(float amount)
    {
        bombFireTime += amount;
        int removedAmount = Mathf.RoundToInt(bombFireTime * 2) - 1;
        textBombFireTimeAmount.text = removedAmount.ToString();
        Warning_Manager.Instance.ShowMessage((myRecipeItem as Item_Bomb).MyBombType + " bomb fire time is " + bombFireTime + " second.");

        bool hasFireTimeStone = Inventory_Manager.Instance.HasItem((myRecipeItem as Item_Bomb).MyItemFireTime, 1) == 0;

        butonIncBombFireTime.gameObject.SetActive(hasFireTimeStone);
        butonIncBombFireTimeAds.gameObject.SetActive(!hasFireTimeStone);
    }
    public void IncBombFireTime()
    {
        if (bombFireTime >= 3.0f)
        {
            Warning_Manager.Instance.ShowMessage("The Fire Time cannot be increased any further.");
            return;
        }
        SetBombFireTime(0.5f);
    }
    public void IncBombFireTimeAds()
    {
        if (bombFireTime >= 3.0f)
        {
            Warning_Manager.Instance.ShowMessage("The Fire Time cannot be increased any further.");
            return;
        }
        Reklam_Manager.Instance.RewardShowAd(() =>
        {
            SetBombFireTime(0.5f);
        });
    }
    public void DecBombFireTime()
    {
        if (bombFireTime <= 0.5f)
        {
            Warning_Manager.Instance.ShowMessage("The Fire Time cannot be decreased any further.");
            return;
        }
        SetBombFireTime(-0.5f);
    }
    // Canvas -> Panel-Craft-System -> Panel-Craft -> Button-Craft'a atandı
    public void CraftItem()
    {
        if (Save_Load_Manager.Instance.gameData.gold < myRecipeItem.MyRecipePrice)
        {
            Warning_Manager.Instance.NotHaveGold();
            return;
        }
        bool canCraft = true;
        for (int e = 0; e < myMaterialList.Count && canCraft; e++)
        {
            if (myMaterialList[e].IsSlotEmpty())
            {
                continue;
            }
            canCraft = myMaterialList[e].HasSlotNeededAmount();
        }
        if (canCraft)
        {
            for (int h = 0; h < myRecipeItem.MyNeededItemList.Count; h++)
            {
                Inventory_Manager.Instance.RemoveItem(myRecipeItem.MyNeededItemList[h]);
            }
            if (myRecipeItem is Item_Bomb)
            {
                Item_Bomb itemBomb = myRecipeItem as Item_Bomb;
                int removedAmount = bombPower - 1;
                for (int e = 0; e < removedAmount; e++)
                {
                    Inventory_Manager.Instance.RemoveItem(new NeededItemHolder(itemBomb.MyItemPower, 1, InventoryType.Material));
                }
                removedAmount = bombLimit - 1;
                for (int e = 0; e < removedAmount; e++)
                {
                    Inventory_Manager.Instance.RemoveItem(new NeededItemHolder(itemBomb.MyItemLimit, 1, InventoryType.Material));
                }
                removedAmount = Mathf.RoundToInt(bombFireTime * 2) - 1;
                for (int e = 0; e < removedAmount; e++)
                {
                    Inventory_Manager.Instance.RemoveItem(new NeededItemHolder(itemBomb.MyItemFireTime, 1, InventoryType.Material));
                }
            }
            SetGoldSmooth(-myRecipeItem.MyRecipePrice);
            myRecipeItem.AddMyItemToInventory(bombPower, bombLimit, bombFireTime);
            SetMaterialList(myRecipeItem);
        }
        else
        {
            Warning_Manager.Instance.ShowMessage("Your Recipe need some items.", 2);
        }
    }
    // Canvas -> Panel-Menu -> Panel-Buttons -> Button-Craft'a atandı
    public void ClearCraftSlots()
    {
        for (int e = 0; e < myRecipeList.Count; e++)
        {
            myRecipeList[e].SlotRelease();
        }
        for (int e = 0; e < myMaterialList.Count; e++)
        {
            myMaterialList[e].SlotRelease();
        }
        buttonCraftIcon.sprite = craftSlotIcon;
        objBombStatPanel.SetActive(false);
    }
    #endregion

    #region Shop
    public void SetShopPanel(GameObject panel)
    {
        for (int e = 0; e < shopPanelList.Count; e++)
        {
            shopPanelList[e].SetActive(false);
        }
        panel.SetActive(true);
    }
    // Canvas -> Panel-Menu -> Panel-Shop -> Panel-Shop-Inventory-Slot -> Panel-Button-Slot-X -> Button-Buy-Slot'lara atandı
    public void BuyInventorySlot(int slotAmount)
    {
        if (slotAmount == 1) // 100 Gold
        {
            if (Save_Load_Manager.Instance.gameData.gold < 100)
            {
                Warning_Manager.Instance.NotHaveGold();
                return;
            }
            SetGoldSmooth(-100);
        }
        else if (slotAmount == 2) // 200 Gold
        {
            if (Save_Load_Manager.Instance.gameData.gold < 200)
            {
                Warning_Manager.Instance.NotHaveGold();
                return;
            }
            SetGoldSmooth(-200);
        }
        else if (slotAmount == 3) // 300 Gold
        {
            if (Save_Load_Manager.Instance.gameData.gold < 300)
            {
                Warning_Manager.Instance.NotHaveGold();
                return;
            }
            SetGoldSmooth(-300);
        }
        else if (slotAmount == 5) // 475 Gold
        {
            if (Save_Load_Manager.Instance.gameData.gold < 475)
            {
                Warning_Manager.Instance.NotHaveGold();
                return;
            }
            SetGoldSmooth(-475);
        }
        else if (slotAmount == 15) // 1425 Gold
        {
            if (Save_Load_Manager.Instance.gameData.gold < 1425)
            {
                Warning_Manager.Instance.NotHaveGold();
                return;
            }
            SetGoldSmooth(-1425);
        }
        else if (slotAmount == 25) // 2375 Gold
        {
            if (Save_Load_Manager.Instance.gameData.gold < 2375)
            {
                Warning_Manager.Instance.NotHaveGold();
                return;
            }
            SetGoldSmooth(-2375);
        }
        else if (slotAmount == 50) // 4500 Gold
        {
            if (Save_Load_Manager.Instance.gameData.gold < 4500)
            {
                Warning_Manager.Instance.NotHaveGold();
                return;
            }
            SetGoldSmooth(-4500);
        }
        else if (slotAmount == 75) // 6750 Gold
        {
            if (Save_Load_Manager.Instance.gameData.gold < 6750)
            {
                Warning_Manager.Instance.NotHaveGold();
                return;
            }
            SetGoldSmooth(-6750);
        }
        else if (slotAmount == 100) // 9000 Gold
        {
            if (Save_Load_Manager.Instance.gameData.gold < 9000)
            {
                Warning_Manager.Instance.NotHaveGold();
                return;
            }
            SetGoldSmooth(-9000);
        }
        Inventory_Manager.Instance.AddInventorySlot(slotAmount);
        Save_Load_Manager.Instance.SaveGame();
    }
    // Canvas -> Panel-Menu -> Panel-Shop -> Panel-Shop-Inventory-Slot -> Panel-Button-Slot-1 -> Button-Buy-Advertisement'lere atandı
    public void BuyInventorySlotWithAds()
    {
        Reklam_Manager.Instance.RewardShowAd(() =>
        {
            Inventory_Manager.Instance.AddInventorySlot(1);
            Save_Load_Manager.Instance.SaveGame();
        });
    }
    // Canvas -> Panel-Menu -> Panel-Shop -> Panel-Shop-Gold-Container -> Panel-Shop-Gold-Parent -> Panel-Button-Gold-100 -> Button-Buy-Advertisement'lere atandı
    public void BuyGoldWithAds()
    {
        Reklam_Manager.Instance.RewardShowAd(() =>
        {
            SetGoldSmooth(100);
        });
    }
    private void SetToolBuyButton()
    {
        for (int e = 0; e < all_Item_Holder.ToolList.Count; e++)
        {
            Transform obj = Instantiate(objTool, objToolParent);
            Image image = obj.GetChild(0).GetComponent<Image>();
            image.sprite = all_Item_Holder.ToolList[e].MyIcon;

            TextMeshProUGUI text = obj.GetChild(1).GetComponent<TextMeshProUGUI>();
            text.text = "Gives 1 " + all_Item_Holder.ToolList[e].MyName + " tool.";

            Button buton = obj.GetChild(2).GetComponent<Button>();
            int order = e;
            buton.onClick.AddListener(() => BuyTool(order));

            TextMeshProUGUI butonText = buton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            butonText.text = (all_Item_Holder.ToolList[e].MyRecipeItem.MyRecipePrice * 3) + " Gold";
        }
    }
    private void BuyTool(int order)
    {
        int price = all_Item_Holder.ToolList[order].MyRecipeItem.MyRecipePrice * 3;
        if (Save_Load_Manager.Instance.gameData.gold < price)
        {
            Warning_Manager.Instance.NotHaveGold();
            return;
        }
        Inventory_Manager.Instance.AddItem(new NeededItemHolder(all_Item_Holder.ToolList[order].MyRecipeItem.MyRecipeItem, 1, InventoryType.Alet));
        SetGoldSmooth(-price);
    }
    private void SetMaterialBuyButton()
    {
        for (int e = 0; e < all_Item_Holder.MalzemeList.Count; e++)
        {
            Transform obj = Instantiate(objMaterial, objMaterialParent);
            Image image = obj.GetChild(0).GetComponent<Image>();
            image.sprite = all_Item_Holder.MalzemeList[e].MyIcon;

            TextMeshProUGUI text = obj.GetChild(1).GetComponent<TextMeshProUGUI>();
            text.text = "Gives 1 " + all_Item_Holder.MalzemeList[e].MyName + " material.";

            Button buton = obj.GetChild(2).GetComponent<Button>();
            int order = e;
            buton.onClick.AddListener(() => BuyMaterialWithGold(order));

            TextMeshProUGUI butonText = buton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            butonText.text = 10 + " Gold";


            Button butonAds = obj.GetChild(3).GetComponent<Button>();
            Reklam_Manager.Instance.AddReklamButton(butonAds);
            int orderAds = e;
            butonAds.onClick.AddListener(() => BuyMaterialWithAds(order));
        }
    }
    private void BuyMaterialWithGold(int order)
    {
        if (Save_Load_Manager.Instance.gameData.gold < 10)
        {
            Warning_Manager.Instance.NotHaveGold();
            return;
        }
        Inventory_Manager.Instance.AddItem(new NeededItemHolder(all_Item_Holder.MalzemeList[order], 1, InventoryType.Material));
        SetGoldSmooth(-10);

        Save_Load_Manager.Instance.SaveGame();
    }
    private void BuyMaterialWithAds(int order)
    {
        Reklam_Manager.Instance.RewardShowAd(() =>
        {
            Inventory_Manager.Instance.AddItem(new NeededItemHolder(all_Item_Holder.MalzemeList[order], 1, InventoryType.Material));
            Save_Load_Manager.Instance.SaveGame();
        });
    }
    #endregion
}