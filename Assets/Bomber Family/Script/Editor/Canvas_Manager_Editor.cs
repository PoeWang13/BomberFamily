using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Canvas_Manager))]
public class Canvas_Manager_Editor : Editor
{
    #region Property
    #region Genel
    private bool showGenel;
    private SerializedProperty emptySlotIcon;
    private SerializedProperty sceneMaskedImage;
    private SerializedProperty all_Item_Holder;
    private SerializedProperty textMenuGoldAmount;
    private SerializedProperty textCreatorGoldAmount;
    #endregion

    #region Name
    private bool showName;
    private SerializedProperty panelName;
    private SerializedProperty inputName;
    #endregion

    #region Game Finish
    private bool showGameFinish;
    private SerializedProperty panelGameFinish;
    private SerializedProperty objNextLevelButton;
    private SerializedProperty objReloadButton;
    private SerializedProperty textLevelResult;
    private SerializedProperty textLevelTime;
    private SerializedProperty textKillingEnemyAmontK;
    private SerializedProperty textBrokeBoxAmont;
    private SerializedProperty textUseBombAmont;
    private SerializedProperty textLoseLifeAmont;
    private SerializedProperty textActiveTrapAmont;
    private SerializedProperty textCaughtTrapAmont;
    private SerializedProperty textEarnGold;
    private SerializedProperty textEarnExp;
    private SerializedProperty buttonDoubleReward;
    private SerializedProperty buttonOffer1;
    private SerializedProperty buttonOffer2;
    private SerializedProperty buttonOffer3;
    #endregion

    #region Menu
    private bool showMenu;
    private SerializedProperty panelMenu;
    private SerializedProperty panelShop;
    private SerializedProperty buttonMyLevel;
    private SerializedProperty panelLevelsMap;
    private SerializedProperty myLevelButtonParent;
    private SerializedProperty dailyButtons;
    #endregion

    #region Creator
    private bool showCreator;
    private SerializedProperty panelCreator;
    private SerializedProperty panelBoardSize;
    private SerializedProperty panelProcessHolder;
    private SerializedProperty panelProcess;
    private SerializedProperty inputBoardSizeX;
    private SerializedProperty inputBoardSizeY;
    private SerializedProperty slotCreator;
    private SerializedProperty gateParent;
    private SerializedProperty wallParent;
    private SerializedProperty boxParent;
    private SerializedProperty trapParent;
    private SerializedProperty enemyParent;
    private SerializedProperty bossEnemyParent;
    private SerializedProperty buttonSaveMap;
    private SerializedProperty imageProcess;
    private SerializedProperty textProcess;
    private SerializedProperty textProcessBase;
    private SerializedProperty objCheckingMapButton;
    private SerializedProperty objSaveMapButtonsParent;
    private SerializedProperty objCreatorButtonTypeList;
    private SerializedProperty objCreatorTypeList;
    private SerializedProperty objChangeMapButton;
    private SerializedProperty objEmptyArea;
    private SerializedProperty imageMultiplePlacement;
    private SerializedProperty objCreatingMultipleObject;
    private SerializedProperty textPlacementType;
    private SerializedProperty textCreateObjectSetting;
    private SerializedProperty inputMultiplePlacementAmount;
    #endregion

    #region Creator Object Setting
    private bool showCreatorObjectSetting;
    private SerializedProperty panelSettingBase;
    private SerializedProperty objButtonMove;
    private SerializedProperty slotTrigger;
    private SerializedProperty toggleSettingTrigger;
    private SerializedProperty toggleSettingAlwaysActive;
    private SerializedProperty toggleSettingTimer;
    private SerializedProperty toggleStartingWithActivated;
    private SerializedProperty panelTriggerList;
    private SerializedProperty panelTriggerListParent;
    private SerializedProperty panelSettingForObjectTimer1;
    private SerializedProperty panelSettingForObjectTimer2;
    private SerializedProperty panelSettingForObjectEffect;
    private SerializedProperty textSettingInfo;
    #endregion

    #region Dungeon Create Setting
    private bool showDungeonCreateSetting;
    private SerializedProperty toggleDungeonFree;
    private SerializedProperty toggleDungeonSetting;
    private SerializedProperty buttonDungeonCreate;
    private SerializedProperty buttonChooser;
    private SerializedProperty wallChooseParent;
    private SerializedProperty boxChooseParent;
    private SerializedProperty trapChooseParent;
    private SerializedProperty enemyChooseParent;
    private SerializedProperty bossEnemyChooseParent;
    private SerializedProperty panelCreateSetting;
    private SerializedProperty panelDungeonSetting;
    private SerializedProperty inputDungeonSizeX;
    private SerializedProperty inputDungeonSizeY;
    private SerializedProperty inputWallAmount;
    private SerializedProperty inputBoxAmount;
    private SerializedProperty inputTrapAmount;
    private SerializedProperty inputEnemyAmount;
    private SerializedProperty inputBossEnemyAmount;
    private SerializedProperty inputMagicStoneAmount;
    #endregion

    #region Player
    private bool showPlayer;
    private SerializedProperty panelGame;
    private SerializedProperty panelHelp;
    private SerializedProperty buttonHelpCloser;
    private SerializedProperty panelPlayerSetting;
    private SerializedProperty imagePlayerIcon;
    private SerializedProperty imagePlayerLevelPercent;
    private SerializedProperty textPlayerExp;
    private SerializedProperty textPlayerLevel;
    private SerializedProperty textPlayerName;
    private SerializedProperty rectPlayerLife;
    private SerializedProperty textPlayerLife;
    private SerializedProperty rectPlayerSpeed;
    private SerializedProperty textPlayerSpeed;
    private SerializedProperty rectPlayerBombAmount;
    private SerializedProperty textPlayerBombAmount;
    private SerializedProperty rectPlayerPower;
    private SerializedProperty textPlayerPower;
    private SerializedProperty rectPlayerBombFireLimit;
    private SerializedProperty textPlayerBombFireLimit;
    private SerializedProperty rectPlayerBoxPassing;
    private SerializedProperty textPlayerBoxPassing;
    private SerializedProperty rectPlayerBoxPushingTime;
    private SerializedProperty textPlayerBoxPushingTime;
    #endregion

    #region Bomb Button
    private bool showBombButton;
    private SerializedProperty bombClockActiviter;
    private SerializedProperty allBombs;
    #endregion

    #region Player Choose
    private bool showPlayerChoose;
    private SerializedProperty joystickMove;
    private SerializedProperty buttonPlayerBuy;
    private SerializedProperty buttonPlayerChoose;
    private SerializedProperty imageBombFire;
    private SerializedProperty textShopPlayerName;
    private SerializedProperty textShopPlayerPrice;
    private SerializedProperty textShopPlayerLevel;
    private SerializedProperty textShopPlayerUpgrade;
    private SerializedProperty textShopPlayerBombType;
    private SerializedProperty textShopPlayerLife;
    private SerializedProperty textShopPlayerSpeed;
    private SerializedProperty textShopPlayerBombAmount;
    private SerializedProperty textShopPlayerBombPower;
    private SerializedProperty textShopPlayerBombFireLimit;
    private SerializedProperty textShopPlayerBoxPassing;
    private SerializedProperty textShopPlayerBoxPushingTime;
    private SerializedProperty textShopPlayerUpgradeLife;
    private SerializedProperty textShopPlayerUpgradeSpeed;
    private SerializedProperty textShopPlayerUpgradeBombAmount;
    private SerializedProperty textShopPlayerUpgradeBombPower;
    private SerializedProperty textShopPlayerUpgradeBombFireLimit;
    private SerializedProperty textShopPlayerUpgradeBoxPassing;
    private SerializedProperty textShopPlayerUpgradeBoxPushingTime;
    private SerializedProperty buttonShopPlayerLife;
    private SerializedProperty buttonShopPlayerSpeed;
    private SerializedProperty buttonShopPlayerBombAmount;
    private SerializedProperty buttonShopPlayerBombPower;
    private SerializedProperty buttonShopPlayerBombFireLimit;
    private SerializedProperty buttonShopPlayerBoxPassing;
    private SerializedProperty buttonShopPlayerBoxPushingTime;
    #endregion

    #region Level Start Help
    private bool showLevelStartHelp;
    private SerializedProperty rectLevelHelp;
    private SerializedProperty imageLevelHelpPlayerIcon;
    private SerializedProperty buttonLevelHelpLife;
    private SerializedProperty buttonLevelHelpAmount;
    private SerializedProperty buttonLevelHelpPower;
    private SerializedProperty cameraMenu;
    private SerializedProperty cameraMap;
    #endregion

    #region Craft
    private bool showCraft;
    private SerializedProperty buttonCraft;
    private SerializedProperty buttonCraftIcon;
    private SerializedProperty myMaterialList;
    private SerializedProperty myRecipeList;
    #endregion
    #endregion

    private void OnEnable()
    {
        Serialized();
    }
    public virtual void Serialized()
    {
        #region Genel
        emptySlotIcon = serializedObject.FindProperty("emptySlotIcon");
        sceneMaskedImage = serializedObject.FindProperty("sceneMaskedImage");
        all_Item_Holder = serializedObject.FindProperty("all_Item_Holder");
        textMenuGoldAmount = serializedObject.FindProperty("textMenuGoldAmount");
        textCreatorGoldAmount = serializedObject.FindProperty("textCreatorGoldAmount");
        #endregion

        #region Name
        panelName = serializedObject.FindProperty("panelName");
        inputName = serializedObject.FindProperty("inputName");
        #endregion

        #region Game Finish
        panelGameFinish = serializedObject.FindProperty("panelGameFinish");
        objNextLevelButton = serializedObject.FindProperty("objNextLevelButton");
        objReloadButton = serializedObject.FindProperty("objReloadButton");
        textLevelResult = serializedObject.FindProperty("textLevelResult");
        textLevelTime = serializedObject.FindProperty("textLevelTime");
        textKillingEnemyAmontK = serializedObject.FindProperty("textKillingEnemyAmontK");
        textBrokeBoxAmont = serializedObject.FindProperty("textBrokeBoxAmont");
        textUseBombAmont = serializedObject.FindProperty("textUseBombAmont");
        textLoseLifeAmont = serializedObject.FindProperty("textLoseLifeAmont");
        textActiveTrapAmont = serializedObject.FindProperty("textActiveTrapAmont");
        textCaughtTrapAmont = serializedObject.FindProperty("textCaughtTrapAmont");
        textEarnGold = serializedObject.FindProperty("textEarnGold");
        textEarnExp = serializedObject.FindProperty("textEarnExp");
        buttonDoubleReward = serializedObject.FindProperty("buttonDoubleReward");
        buttonOffer1 = serializedObject.FindProperty("buttonOffer1");
        buttonOffer2 = serializedObject.FindProperty("buttonOffer2");
        buttonOffer3 = serializedObject.FindProperty("buttonOffer3");
        #endregion

        #region Menu
        panelMenu = serializedObject.FindProperty("panelMenu");
        panelShop = serializedObject.FindProperty("panelShop");
        buttonMyLevel = serializedObject.FindProperty("buttonMyLevel");
        panelLevelsMap = serializedObject.FindProperty("panelLevelsMap");
        myLevelButtonParent = serializedObject.FindProperty("myLevelButtonParent");
        dailyButtons = serializedObject.FindProperty("dailyButtons");
        #endregion

        #region Creator
        panelCreator = serializedObject.FindProperty("panelCreator");
        panelBoardSize = serializedObject.FindProperty("panelBoardSize");
        panelProcessHolder = serializedObject.FindProperty("panelProcessHolder");
        panelProcess = serializedObject.FindProperty("panelProcess");
        inputBoardSizeX = serializedObject.FindProperty("inputBoardSizeX");
        inputBoardSizeY = serializedObject.FindProperty("inputBoardSizeY");
        slotCreator = serializedObject.FindProperty("slotCreator");
        gateParent = serializedObject.FindProperty("gateParent");
        wallParent = serializedObject.FindProperty("wallParent");
        boxParent = serializedObject.FindProperty("boxParent");
        trapParent = serializedObject.FindProperty("trapParent");
        enemyParent = serializedObject.FindProperty("enemyParent");
        bossEnemyParent = serializedObject.FindProperty("bossEnemyParent");
        buttonSaveMap = serializedObject.FindProperty("buttonSaveMap");
        imageProcess = serializedObject.FindProperty("imageProcess");
        textProcess = serializedObject.FindProperty("textProcess");
        textProcessBase = serializedObject.FindProperty("textProcessBase");
        objCheckingMapButton = serializedObject.FindProperty("objCheckingMapButton");
        objSaveMapButtonsParent = serializedObject.FindProperty("objSaveMapButtonsParent");
        objCreatorButtonTypeList = serializedObject.FindProperty("objCreatorButtonTypeList");
        objCreatorTypeList = serializedObject.FindProperty("objCreatorTypeList");
        objChangeMapButton = serializedObject.FindProperty("objChangeMapButton");
        objEmptyArea = serializedObject.FindProperty("objEmptyArea");
        imageMultiplePlacement = serializedObject.FindProperty("imageMultiplePlacement");
        objCreatingMultipleObject = serializedObject.FindProperty("objCreatingMultipleObject");
        textPlacementType = serializedObject.FindProperty("textPlacementType");
        textCreateObjectSetting = serializedObject.FindProperty("textCreateObjectSetting");
        inputMultiplePlacementAmount = serializedObject.FindProperty("inputMultiplePlacementAmount");
        #endregion

        #region Creator Object Setting
        panelSettingBase = serializedObject.FindProperty("panelSettingBase");
        objButtonMove = serializedObject.FindProperty("objButtonMove");
        slotTrigger = serializedObject.FindProperty("slotTrigger");
        toggleSettingTrigger = serializedObject.FindProperty("toggleSettingTrigger");
        toggleSettingAlwaysActive = serializedObject.FindProperty("toggleSettingAlwaysActive");
        toggleSettingTimer = serializedObject.FindProperty("toggleSettingTimer");
        toggleStartingWithActivated = serializedObject.FindProperty("toggleStartingWithActivated");
        panelTriggerList = serializedObject.FindProperty("panelTriggerList");
        panelTriggerListParent = serializedObject.FindProperty("panelTriggerListParent");
        panelSettingForObjectTimer1 = serializedObject.FindProperty("panelSettingForObjectTimer1");
        panelSettingForObjectTimer2 = serializedObject.FindProperty("panelSettingForObjectTimer2");
        panelSettingForObjectEffect = serializedObject.FindProperty("panelSettingForObjectEffect");
        textSettingInfo = serializedObject.FindProperty("textSettingInfo");
        #endregion

        #region Dungeon Create Setting
        toggleDungeonFree = serializedObject.FindProperty("toggleDungeonFree");
        toggleDungeonSetting = serializedObject.FindProperty("toggleDungeonSetting");
        buttonDungeonCreate = serializedObject.FindProperty("buttonDungeonCreate");
        buttonChooser = serializedObject.FindProperty("buttonChooser");
        wallChooseParent = serializedObject.FindProperty("wallChooseParent");
        boxChooseParent = serializedObject.FindProperty("boxChooseParent");
        trapChooseParent = serializedObject.FindProperty("trapChooseParent");
        enemyChooseParent = serializedObject.FindProperty("enemyChooseParent");
        bossEnemyChooseParent = serializedObject.FindProperty("bossEnemyChooseParent");
        panelCreateSetting = serializedObject.FindProperty("panelCreateSetting");
        panelDungeonSetting = serializedObject.FindProperty("panelDungeonSetting");
        inputDungeonSizeX = serializedObject.FindProperty("inputDungeonSizeX");
        inputDungeonSizeY = serializedObject.FindProperty("inputDungeonSizeY");
        inputWallAmount = serializedObject.FindProperty("inputWallAmount");
        inputBoxAmount = serializedObject.FindProperty("inputBoxAmount");
        inputTrapAmount = serializedObject.FindProperty("inputTrapAmount");
        inputEnemyAmount = serializedObject.FindProperty("inputEnemyAmount");
        inputBossEnemyAmount = serializedObject.FindProperty("inputBossEnemyAmount");
        inputMagicStoneAmount = serializedObject.FindProperty("inputMagicStoneAmount");
        #endregion

        #region Player
        panelGame = serializedObject.FindProperty("panelGame");
        panelHelp = serializedObject.FindProperty("panelHelp");
        buttonHelpCloser = serializedObject.FindProperty("buttonHelpCloser");
        panelPlayerSetting = serializedObject.FindProperty("panelPlayerSetting");
        imagePlayerIcon = serializedObject.FindProperty("imagePlayerIcon");
        imagePlayerLevelPercent = serializedObject.FindProperty("imagePlayerLevelPercent");
        textPlayerExp = serializedObject.FindProperty("textPlayerExp");
        textPlayerLevel = serializedObject.FindProperty("textPlayerLevel");
        textPlayerName = serializedObject.FindProperty("textPlayerName");
        rectPlayerLife = serializedObject.FindProperty("rectPlayerLife");
        textPlayerLife = serializedObject.FindProperty("textPlayerLife");
        rectPlayerSpeed = serializedObject.FindProperty("rectPlayerSpeed");
        textPlayerSpeed = serializedObject.FindProperty("textPlayerSpeed");
        rectPlayerBombAmount = serializedObject.FindProperty("rectPlayerBombAmount");
        textPlayerBombAmount = serializedObject.FindProperty("textPlayerBombAmount");
        rectPlayerPower = serializedObject.FindProperty("rectPlayerPower");
        textPlayerPower = serializedObject.FindProperty("textPlayerPower");
        rectPlayerBombFireLimit = serializedObject.FindProperty("rectPlayerBombFireLimit");
        textPlayerBombFireLimit = serializedObject.FindProperty("textPlayerBombFireLimit");
        rectPlayerBoxPassing = serializedObject.FindProperty("rectPlayerBoxPassing");
        textPlayerBoxPassing = serializedObject.FindProperty("textPlayerBoxPassing");
        rectPlayerBoxPushingTime = serializedObject.FindProperty("rectPlayerBoxPushingTime");
        textPlayerBoxPushingTime = serializedObject.FindProperty("textPlayerBoxPushingTime");
        #endregion

        #region Bomb Button
        bombClockActiviter = serializedObject.FindProperty("bombClockActiviter");
        allBombs = serializedObject.FindProperty("allBombs");
        #endregion

        #region Choose
        joystickMove = serializedObject.FindProperty("joystickMove");
        buttonPlayerBuy = serializedObject.FindProperty("buttonPlayerBuy");
        buttonPlayerChoose = serializedObject.FindProperty("buttonPlayerChoose");
        imageBombFire = serializedObject.FindProperty("imageBombFire");
        textShopPlayerName = serializedObject.FindProperty("textShopPlayerName");
        textShopPlayerPrice = serializedObject.FindProperty("textShopPlayerPrice");
        textShopPlayerLevel = serializedObject.FindProperty("textShopPlayerLevel");
        textShopPlayerUpgrade = serializedObject.FindProperty("textShopPlayerUpgrade");
        textShopPlayerBombType = serializedObject.FindProperty("textShopPlayerBombType");
        textShopPlayerLife = serializedObject.FindProperty("textShopPlayerLife");
        textShopPlayerSpeed = serializedObject.FindProperty("textShopPlayerSpeed");
        textShopPlayerBombAmount = serializedObject.FindProperty("textShopPlayerBombAmount");
        textShopPlayerBombPower = serializedObject.FindProperty("textShopPlayerBombPower");
        textShopPlayerBombFireLimit = serializedObject.FindProperty("textShopPlayerBombFireLimit");
        textShopPlayerBoxPassing = serializedObject.FindProperty("textShopPlayerBoxPassing");
        textShopPlayerBoxPushingTime = serializedObject.FindProperty("textShopPlayerBoxPushingTime");
        textShopPlayerUpgradeLife = serializedObject.FindProperty("textShopPlayerUpgradeLife");
        textShopPlayerUpgradeSpeed = serializedObject.FindProperty("textShopPlayerUpgradeSpeed");
        textShopPlayerUpgradeBombAmount = serializedObject.FindProperty("textShopPlayerUpgradeBombAmount");
        textShopPlayerUpgradeBombPower = serializedObject.FindProperty("textShopPlayerUpgradeBombPower");
        textShopPlayerUpgradeBombFireLimit = serializedObject.FindProperty("textShopPlayerUpgradeBombFireLimit");
        textShopPlayerUpgradeBoxPassing = serializedObject.FindProperty("textShopPlayerUpgradeBoxPassing");
        textShopPlayerUpgradeBoxPushingTime = serializedObject.FindProperty("textShopPlayerUpgradeBoxPushingTime");
        buttonShopPlayerLife = serializedObject.FindProperty("buttonShopPlayerLife");
        buttonShopPlayerSpeed = serializedObject.FindProperty("buttonShopPlayerSpeed");
        buttonShopPlayerBombAmount = serializedObject.FindProperty("buttonShopPlayerBombAmount");
        buttonShopPlayerBombPower = serializedObject.FindProperty("buttonShopPlayerBombPower");
        buttonShopPlayerBombFireLimit = serializedObject.FindProperty("buttonShopPlayerBombFireLimit");
        buttonShopPlayerBoxPassing = serializedObject.FindProperty("buttonShopPlayerBoxPassing");
        buttonShopPlayerBoxPushingTime = serializedObject.FindProperty("buttonShopPlayerBoxPushingTime");
        #endregion

        #region Level Start Help
        rectLevelHelp = serializedObject.FindProperty("rectLevelHelp");
        imageLevelHelpPlayerIcon = serializedObject.FindProperty("imageLevelHelpPlayerIcon");
        buttonLevelHelpLife = serializedObject.FindProperty("buttonLevelHelpLife");
        buttonLevelHelpAmount = serializedObject.FindProperty("buttonLevelHelpAmount");
        buttonLevelHelpPower = serializedObject.FindProperty("buttonLevelHelpPower");
        cameraMenu = serializedObject.FindProperty("cameraMenu");
        cameraMap = serializedObject.FindProperty("cameraMap");
        #endregion

        #region Craft
        buttonCraft = serializedObject.FindProperty("buttonCraft");
        buttonCraftIcon = serializedObject.FindProperty("buttonCraftIcon");
        myMaterialList = serializedObject.FindProperty("myMaterialList");
        myRecipeList = serializedObject.FindProperty("myRecipeList");
        #endregion
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        // Genel
        showGenel = EditorGUILayout.Toggle("Genel", showGenel);
        if (showGenel)
        {
            EditorGUILayout.PropertyField(emptySlotIcon);
            EditorGUILayout.PropertyField(sceneMaskedImage);
            EditorGUILayout.PropertyField(all_Item_Holder);
            EditorGUILayout.PropertyField(textMenuGoldAmount);
            EditorGUILayout.PropertyField(textCreatorGoldAmount);
            EditorGUILayout.Space(5);
        }

        // Name
        showName = EditorGUILayout.Toggle("Name", showName);
        if (showName)
        {
            EditorGUILayout.PropertyField(panelName);
            EditorGUILayout.PropertyField(inputName);
            EditorGUILayout.Space(5);
        }

        // Game Finish
        showGameFinish = EditorGUILayout.Toggle("Game Finish", showGameFinish);
        if (showGameFinish)
        {
            EditorGUILayout.PropertyField(panelGameFinish);
            EditorGUILayout.PropertyField(objNextLevelButton);
            EditorGUILayout.PropertyField(objReloadButton);
            EditorGUILayout.PropertyField(textLevelResult);
            EditorGUILayout.PropertyField(textLevelTime);
            EditorGUILayout.PropertyField(textKillingEnemyAmontK);
            EditorGUILayout.PropertyField(textBrokeBoxAmont);
            EditorGUILayout.PropertyField(textUseBombAmont);
            EditorGUILayout.PropertyField(textLoseLifeAmont);
            EditorGUILayout.PropertyField(textActiveTrapAmont);
            EditorGUILayout.PropertyField(textCaughtTrapAmont);
            EditorGUILayout.PropertyField(textEarnGold);
            EditorGUILayout.PropertyField(textEarnExp);
            EditorGUILayout.PropertyField(buttonDoubleReward);
            EditorGUILayout.PropertyField(buttonOffer1);
            EditorGUILayout.PropertyField(buttonOffer2);
            EditorGUILayout.PropertyField(buttonOffer3);
            EditorGUILayout.Space(5);
        }

        // Menu
        showMenu = EditorGUILayout.Toggle("Menu", showMenu);
        if (showMenu)
        {
            EditorGUILayout.PropertyField(panelMenu);
            EditorGUILayout.PropertyField(panelShop);
            EditorGUILayout.PropertyField(buttonMyLevel);
            EditorGUILayout.PropertyField(panelLevelsMap);
            EditorGUILayout.PropertyField(myLevelButtonParent);
            EditorGUILayout.PropertyField(dailyButtons);
            EditorGUILayout.Space(5);
        }

        // Creator
        showCreator = EditorGUILayout.Toggle("Creator", showCreator);
        if (showCreator)
        {
            EditorGUILayout.PropertyField(panelCreator);
            EditorGUILayout.PropertyField(panelBoardSize);
            EditorGUILayout.PropertyField(panelProcessHolder);
            EditorGUILayout.PropertyField(panelProcess);
            EditorGUILayout.PropertyField(inputBoardSizeX);
            EditorGUILayout.PropertyField(inputBoardSizeY);
            EditorGUILayout.PropertyField(slotCreator);
            EditorGUILayout.PropertyField(gateParent);
            EditorGUILayout.PropertyField(wallParent);
            EditorGUILayout.PropertyField(boxParent);
            EditorGUILayout.PropertyField(trapParent);
            EditorGUILayout.PropertyField(enemyParent);
            EditorGUILayout.PropertyField(bossEnemyParent);
            EditorGUILayout.PropertyField(buttonSaveMap);
            EditorGUILayout.PropertyField(imageProcess);
            EditorGUILayout.PropertyField(textProcess);
            EditorGUILayout.PropertyField(textProcessBase);
            EditorGUILayout.PropertyField(objCheckingMapButton);
            EditorGUILayout.PropertyField(objSaveMapButtonsParent);
            EditorGUILayout.PropertyField(objCreatorButtonTypeList);
            EditorGUILayout.PropertyField(objCreatorTypeList);
            EditorGUILayout.PropertyField(objChangeMapButton);
            EditorGUILayout.PropertyField(objEmptyArea);
            EditorGUILayout.PropertyField(imageMultiplePlacement);
            EditorGUILayout.PropertyField(objCreatingMultipleObject);
            EditorGUILayout.PropertyField(textPlacementType);
            EditorGUILayout.PropertyField(textCreateObjectSetting);
            EditorGUILayout.PropertyField(inputMultiplePlacementAmount);
            EditorGUILayout.Space(5);
        }

        // Creator Object Setting
        showCreatorObjectSetting = EditorGUILayout.Toggle("Creator Object Setting", showCreatorObjectSetting);
        if (showCreatorObjectSetting)
        {
            EditorGUILayout.PropertyField(panelSettingBase);
            EditorGUILayout.PropertyField(objButtonMove);
            EditorGUILayout.PropertyField(slotTrigger);
            EditorGUILayout.PropertyField(toggleSettingTrigger);
            EditorGUILayout.PropertyField(toggleSettingAlwaysActive);
            EditorGUILayout.PropertyField(toggleSettingTimer);
            EditorGUILayout.PropertyField(toggleStartingWithActivated);
            EditorGUILayout.PropertyField(panelTriggerList);
            EditorGUILayout.PropertyField(panelTriggerListParent);
            EditorGUILayout.PropertyField(panelSettingForObjectTimer1);
            EditorGUILayout.PropertyField(panelSettingForObjectTimer2);
            EditorGUILayout.PropertyField(panelSettingForObjectEffect);
            EditorGUILayout.PropertyField(textSettingInfo);
            EditorGUILayout.Space(5);
        }

        // Dungeon Create Setting
        showDungeonCreateSetting = EditorGUILayout.Toggle("Dungeon Create Setting", showDungeonCreateSetting);
        if (showDungeonCreateSetting)
        {
            EditorGUILayout.PropertyField(toggleDungeonFree);
            EditorGUILayout.PropertyField(toggleDungeonSetting);
            EditorGUILayout.PropertyField(buttonDungeonCreate);
            EditorGUILayout.PropertyField(buttonChooser);
            EditorGUILayout.PropertyField(wallChooseParent);
            EditorGUILayout.PropertyField(boxChooseParent);
            EditorGUILayout.PropertyField(trapChooseParent);
            EditorGUILayout.PropertyField(enemyChooseParent);
            EditorGUILayout.PropertyField(bossEnemyChooseParent);
            EditorGUILayout.PropertyField(panelCreateSetting);
            EditorGUILayout.PropertyField(panelDungeonSetting);
            EditorGUILayout.PropertyField(inputDungeonSizeX);
            EditorGUILayout.PropertyField(inputDungeonSizeY);
            EditorGUILayout.PropertyField(inputWallAmount);
            EditorGUILayout.PropertyField(inputBoxAmount);
            EditorGUILayout.PropertyField(inputTrapAmount);
            EditorGUILayout.PropertyField(inputEnemyAmount);
            EditorGUILayout.PropertyField(inputBossEnemyAmount);
            EditorGUILayout.PropertyField(inputMagicStoneAmount);
            EditorGUILayout.Space(5);
        }

        // Player
        showPlayer = EditorGUILayout.Toggle("Player", showPlayer);
        if (showPlayer)
        {
            EditorGUILayout.PropertyField(panelGame);
            EditorGUILayout.PropertyField(panelHelp);
            EditorGUILayout.PropertyField(buttonHelpCloser);
            EditorGUILayout.PropertyField(panelPlayerSetting);
            EditorGUILayout.PropertyField(imagePlayerIcon);
            EditorGUILayout.PropertyField(imagePlayerLevelPercent);
            EditorGUILayout.PropertyField(textPlayerExp);
            EditorGUILayout.PropertyField(textPlayerLevel);
            EditorGUILayout.PropertyField(textPlayerName);
            EditorGUILayout.PropertyField(rectPlayerLife);
            EditorGUILayout.PropertyField(textPlayerLife);
            EditorGUILayout.PropertyField(rectPlayerSpeed);
            EditorGUILayout.PropertyField(textPlayerSpeed);
            EditorGUILayout.PropertyField(rectPlayerBombAmount);
            EditorGUILayout.PropertyField(textPlayerBombAmount);
            EditorGUILayout.PropertyField(rectPlayerPower);
            EditorGUILayout.PropertyField(textPlayerPower);
            EditorGUILayout.PropertyField(rectPlayerBombFireLimit);
            EditorGUILayout.PropertyField(textPlayerBombFireLimit);
            EditorGUILayout.PropertyField(rectPlayerBoxPassing);
            EditorGUILayout.PropertyField(textPlayerBoxPassing);
            EditorGUILayout.PropertyField(rectPlayerBoxPushingTime);
            EditorGUILayout.PropertyField(textPlayerBoxPushingTime);
            EditorGUILayout.Space(5);
        }

        // Bomb Button
        showBombButton = EditorGUILayout.Toggle("Bomb Button", showBombButton);
        if (showBombButton)
        {
            EditorGUILayout.PropertyField(bombClockActiviter);
            EditorGUILayout.PropertyField(allBombs);
            EditorGUILayout.Space(5);
        }

        // Player Choose
        showPlayerChoose = EditorGUILayout.Toggle("Player Choose", showPlayerChoose);
        if (showPlayerChoose)
        {
            EditorGUILayout.PropertyField(joystickMove);
            EditorGUILayout.PropertyField(buttonPlayerBuy);
            EditorGUILayout.PropertyField(buttonPlayerChoose);
            EditorGUILayout.PropertyField(imageBombFire);
            EditorGUILayout.PropertyField(textShopPlayerName);
            EditorGUILayout.PropertyField(textShopPlayerPrice);
            EditorGUILayout.PropertyField(textShopPlayerLevel);
            EditorGUILayout.PropertyField(textShopPlayerUpgrade);
            EditorGUILayout.PropertyField(textShopPlayerBombType);
            EditorGUILayout.PropertyField(textShopPlayerLife);
            EditorGUILayout.PropertyField(textShopPlayerSpeed);
            EditorGUILayout.PropertyField(textShopPlayerBombAmount);
            EditorGUILayout.PropertyField(textShopPlayerBombPower);
            EditorGUILayout.PropertyField(textShopPlayerBombFireLimit);
            EditorGUILayout.PropertyField(textShopPlayerBoxPassing);
            EditorGUILayout.PropertyField(textShopPlayerBoxPushingTime);
            EditorGUILayout.PropertyField(textShopPlayerUpgradeLife);
            EditorGUILayout.PropertyField(textShopPlayerUpgradeSpeed);
            EditorGUILayout.PropertyField(textShopPlayerUpgradeBombAmount);
            EditorGUILayout.PropertyField(textShopPlayerUpgradeBombPower);
            EditorGUILayout.PropertyField(textShopPlayerUpgradeBombFireLimit);
            EditorGUILayout.PropertyField(textShopPlayerUpgradeBoxPassing);
            EditorGUILayout.PropertyField(textShopPlayerUpgradeBoxPushingTime);
            EditorGUILayout.PropertyField(buttonShopPlayerLife);
            EditorGUILayout.PropertyField(buttonShopPlayerSpeed);
            EditorGUILayout.PropertyField(buttonShopPlayerBombAmount);
            EditorGUILayout.PropertyField(buttonShopPlayerBombPower);
            EditorGUILayout.PropertyField(buttonShopPlayerBombFireLimit);
            EditorGUILayout.PropertyField(buttonShopPlayerBoxPassing);
            EditorGUILayout.PropertyField(buttonShopPlayerBoxPushingTime);
        }

        // Level Start Help
        showLevelStartHelp = EditorGUILayout.Toggle("Level Start Help", showLevelStartHelp);
        if (showLevelStartHelp)
        {
            EditorGUILayout.PropertyField(rectLevelHelp);
            EditorGUILayout.PropertyField(imageLevelHelpPlayerIcon);
            EditorGUILayout.PropertyField(buttonLevelHelpLife);
            EditorGUILayout.PropertyField(buttonLevelHelpAmount);
            EditorGUILayout.PropertyField(buttonLevelHelpPower);
            EditorGUILayout.PropertyField(cameraMenu);
            EditorGUILayout.PropertyField(cameraMap);
        }

        // Craft
        showCraft = EditorGUILayout.Toggle("Craft", showCraft);
        if (showCraft)
        {
            EditorGUILayout.PropertyField(buttonCraft);
            EditorGUILayout.PropertyField(buttonCraftIcon);
            EditorGUILayout.PropertyField(myMaterialList);
            EditorGUILayout.PropertyField(myRecipeList);
        }

        serializedObject.ApplyModifiedProperties();
    }
}