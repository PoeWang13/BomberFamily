using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

[System.Serializable]
public class BoardDikenData
{
    public bool isActivited;
    public Vector2Int myTriggedObjectCoor;
}
[System.Serializable]
public class BoardDiken
{
    public BoardDikenData dikenBoardObject;
}
[System.Serializable]
public class BoardTriggerData
{
    public List<Vector2Int> myAllCoor = new List<Vector2Int>();
    public void AddCoor(Vector2Int newCoor)
    {
        myAllCoor.Add(newCoor);
    }
}
[System.Serializable]
public class BoardTrigger
{
    public BoardTriggerData triggerBoardObject;
}
[System.Serializable]
public class BoardData
{
    public TrapType trapType;
    public string boardString;
}
[System.Serializable]
public class BoardContainer
{
    public BoardData boardData;
}
[System.Serializable]
public class BoardCoor
{
    public int x;
    public int y;
    public BoardCoor(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public bool SameCoor(int x, int y)
    {
        return this.x == x && this.y == y;
    }
}
public class Map_Creater_Manager : Singletion<Map_Creater_Manager>
{
    public event System.EventHandler OnTriggerTime;

    [SerializeField] private All_Item_Holder all_Item_Holder;

    // Board Setting
    private int boardWallAmount;
    private int boardBoxAmount;
    private int boardTrapAmount;
    private int boardEnemyAmount;
    private int boardBossEnemyAmount;
    private int boardMagicStoneAmount;

    private int boxOrder;
    private int wallOrder;
    private int enemyOrder;
    private int bossEnemyOrder;
    private int createdOrder;
    private int layerMaskIndex;
    private int emptyBoardAmount;

    private bool hasGate;
    private bool movingObj;
    private bool addMagicStone;
    private bool randomBoardChecking;

    private float randomBoardCheckTime;

    private Camera myCamera;
    private Item createdItem;
    private Item createdNullItem;
    private Color createdObjectColor;
    private GameObject createdObject;
    private Board_Trigger board_Trigger;
    private Material createdObjectMaterial;
    private List<BoardCoor> myBoardList = new List<BoardCoor>();
    private List<BoardCoor> myBoardListBackup = new List<BoardCoor>();

    public bool HasGate { get { return hasGate; } }
    public int WallOrder { get { return wallOrder; } }
    public int BoxOrder { get { return boxOrder; } }
    public int CreatedOrder { get { return createdOrder; } }

    private void Start()
    {
        myCamera = Camera.main;
        layerMaskIndex = 1 << LayerMask.NameToLayer("Ground");
        createdNullItem  = ScriptableObject.CreateInstance(typeof(Item)) as Item;
        createdItem = createdNullItem;
    }

    #region Set Board Setting
    public void SetWallBoard(int amount)
    {
        boardWallAmount = amount;
    }
    public void SetBoxBoard(int amount)
    {
        boardBoxAmount = amount;
    }
    public void SetTrapBoard(int amount)
    {
        boardTrapAmount = amount;
    }
    public void SetEnemyBoard(int amount)
    {
        boardEnemyAmount = amount;
    }
    public void SetBossEnemyBoard(int amount)
    {
        boardBossEnemyAmount = amount;
    }
    public void SetMagicStoneBoard(int amount)
    {
        boardMagicStoneAmount = amount;
    }
    public void SetBoardSize(int sizeX, int sizeY)
    {
        myBoardList.Clear();
        addMagicStone = false;
        sizeX = sizeX % 2 == 0 ? sizeX + 1 : sizeX;
        sizeY = sizeY % 2 == 0 ? sizeY + 1 : sizeY;
        Map_Holder.Instance.SetBoardSize(new Vector2Int(sizeX, sizeY));
        Game_Manager.Instance.SetGameType(GameType.MapCreate);
        Map_Holder.Instance.SendToPoolAllObjects();
        Map_Holder.Instance.SetBoardGround();
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                myBoardList.Add(new BoardCoor(x, y));
            }
        }
        CreateLimitWall();
    }
    private void CreateLimitWall()
    {
        boxOrder = Random.Range(0, all_Item_Holder.BoxList.Count);
        wallOrder = Random.Range(0, all_Item_Holder.WallList.Count);
        // Dış çerçeveye duvarlar eklenecek
        Map_Holder.Instance.CreateOutsideWall();
        Map_Holder.Instance.SetBoardForNonUseable();
        if (Canvas_Manager.Instance.ToggleDungeonSetting)
        {
            // Ortaya duvarlar konacak
            StartCoroutine(MiddleWall());
        }
    }
    #endregion

    #region Create My Game Board
    public void SetCreatedObject(Item item)
    {
        Canvas_Manager.Instance.SetPanelObjectBehaviourPanel(true, false, true);
        Canvas_Manager.Instance.SetCreatorButtons(false);
        Board_Object board_Object = item.MyObject.HavuzdanObjeIste(myCamera.ScreenToWorldPoint(Input.mousePosition)).GetComponent<Board_Object>();
        board_Object.SetBoardOrder(all_Item_Holder.LearnOrder(item));
        createdObject = board_Object.gameObject;
        Canvas_Manager.Instance.SetSaveButtonForMyLevel();
        Utils.SetParent(createdObject, "Board_" + board_Object.LearnBoardType());
        createdItem = item;
        createdOrder = all_Item_Holder.LearnOrder(item);
        createdObjectMaterial = createdObject.GetComponentInChildren<Renderer>().material;
        createdObjectColor = createdObjectMaterial.color;
    }
    public void SetTriggerObject(Board_Trigger trigger)
    {
        board_Trigger = trigger;
        OnTriggerTime?.Invoke(this, System.EventArgs.Empty);
    }
    public void SetObjectForTrigger(Board_Diken diken)
    {
        // Boardda bu koordinatları kullanarak special stringe dikenin koordinatlarını ver.
        BoardContainer boardContainer = new BoardContainer();
        // Boardda bu koordinatları kullanarak special stringe dikenin koordinatlarını ver.
        BoardTrigger triggerData = new BoardTrigger();
        if (!string.IsNullOrEmpty(Map_Holder.Instance.GameBoard[board_Trigger.MyCoor.x, board_Trigger.MyCoor.y].board_Game.boardSpecial))
        {
            boardContainer = JsonUtility.FromJson<BoardContainer>(Map_Holder.Instance.GameBoard[board_Trigger.MyCoor.x, board_Trigger.MyCoor.y].board_Game.boardSpecial);
            triggerData = JsonUtility.FromJson<BoardTrigger>(boardContainer.boardData.boardString);
        }
        else
        {
            boardContainer.boardData = new BoardData();
            boardContainer.boardData.trapType = TrapType.Trigged;
        }
        triggerData.triggerBoardObject = new BoardTriggerData();
        triggerData.triggerBoardObject.AddCoor(diken.MyCoor);
        string jsonTriggerData = JsonUtility.ToJson(triggerData, true);
        boardContainer.boardData.boardString = jsonTriggerData;
        string jsonBoardTriggerData = JsonUtility.ToJson(boardContainer, true);
        Map_Holder.Instance.GameBoard[board_Trigger.MyCoor.x, board_Trigger.MyCoor.y].board_Game.boardSpecial = jsonBoardTriggerData;

        // Dikeni setle
        boardContainer = new BoardContainer();
        boardContainer.boardData = new BoardData();
        boardContainer.boardData.trapType = TrapType.Diken;
        BoardDiken dikenData = new BoardDiken();
        dikenData.dikenBoardObject = new BoardDikenData();
        dikenData.dikenBoardObject.myTriggedObjectCoor = board_Trigger.MyCoor;
        string jsonDikenData = JsonUtility.ToJson(dikenData, true);
        boardContainer.boardData.boardString = jsonDikenData;
        string jsonBoardDikenData = JsonUtility.ToJson(boardContainer, true);
        Map_Holder.Instance.GameBoard[diken.MyCoor.x, diken.MyCoor.y].board_Game.boardSpecial = jsonBoardDikenData;

        // Creator paneli geri aç
        Canvas_Manager.Instance.SetCreatorButtons(true);
    }
    private void Update()
    {
        if (movingObj)
        {
            //RayCaster(true);
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 500, layerMaskIndex))
            {
                int x = Mathf.RoundToInt(hit.point.x);
                int z = Mathf.RoundToInt(hit.point.z);
                if (x >= Map_Holder.Instance.BoardSize.x)
                {
                    return;
                }
                if (z >= Map_Holder.Instance.BoardSize.y)
                {
                    return;
                }
                if (Map_Holder.Instance.GameBoard[x, z].board_Game.boardType == BoardType.Empty)
                {
                    createdObject.transform.position = new Vector3(x, 0, z);
                    createdObjectMaterial.color = createdObjectColor;
                    if (Input.GetMouseButtonUp(0))
                    {
                        // Seçili parçayı ayarla ve bırak
                        Board_Object board_Object = createdObject.GetComponent<Board_Object>();
                        BoardType boardType = board_Object.LearnBoardType();
                        board_Object.SetBoardCoor(new Vector2Int(x, z));
                        Map_Holder.Instance.GameBoard[x, z] = new GameBoard(new Board(boardType, board_Object.BoardOrder), board_Object.gameObject);
                        //Map_Holder.Instance.MyBoard[x, z] = new Board(boardType, board_Object.BoardOrder);
                        Canvas_Manager.Instance.SetCreatorButtons(true);
                        Canvas_Manager.Instance.SetPanelObjectBehaviourPanel(false);
                        // Objeyi bırak
                        movingObj = false;
                    }
                }
                else
                {
                    createdObjectMaterial.color = Color.red;
                }
            }
        }
        else
        {
            if (createdItem != createdNullItem)
            {
                //RayCaster(false);
                Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 500, layerMaskIndex))
                {
                    int x = Mathf.RoundToInt(hit.point.x);
                    int z = Mathf.RoundToInt(hit.point.z);
                    if (x < 0 || x >= Map_Holder.Instance.BoardSize.x)
                    {
                        return;
                    }
                    if (z < 0 || z >= Map_Holder.Instance.BoardSize.y)
                    {
                        return;
                    }
                    if (Map_Holder.Instance.GameBoard[x, z].board_Game.boardType == BoardType.Empty)
                    {
                        createdObject.transform.position = new Vector3(x, 0, z);
                        createdObjectMaterial.color = createdObjectColor;
                        if (Input.GetMouseButtonUp(0))
                        {
                            // Seçili parçayı ayarla ve bırak
                            Board_Object board_Object = createdObject.GetComponent<Board_Object>();
                            BoardType boardType = board_Object.LearnBoardType();
                            board_Object.SetBoardCoor(new Vector2Int(x, z));
                            Map_Holder.Instance.GameBoard[x, z] = new GameBoard(boardType, createdOrder, board_Object.gameObject);
                            //Map_Holder.Instance.MyBoard[x, z] = new Board(boardType, createdOrder);
                            if (boardType == BoardType.Wall)
                            {
                                Map_Holder.Instance.WallObjects.Add(board_Object);
                            }
                            else if (boardType == BoardType.Box)
                            {
                                Map_Holder.Instance.BoxObjects.Add(board_Object);
                            }
                            else if (boardType == BoardType.Trap)
                            {
                                Map_Holder.Instance.TrapObjects.Add(board_Object);
                            }
                            else if (boardType == BoardType.Enemy)
                            {
                                Map_Holder.Instance.EnemyObjects.Add(board_Object);
                            }
                            else if (boardType == BoardType.BossEnemy)
                            {
                                Map_Holder.Instance.BossEnemyObjects.Add(board_Object);
                            }
                            else if (boardType == BoardType.Gate)
                            {
                                hasGate = true;
                                Map_Holder.Instance.GateObjects.Add(board_Object);
                            }
                            RemoveBoardFromList(x, z);
                            // Seçili parçayı null objeye eşitle
                            createdItem = createdNullItem;
                            createdOrder = -1;
                            Canvas_Manager.Instance.SetCreatorButtons(true);
                            Canvas_Manager.Instance.SetPanelObjectBehaviourPanel(false);
                        }
                    }
                    else
                    {
                        createdObjectMaterial.color = Color.red;
                    }
                }
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    createdItem.MyObject.ObjeyiHavuzaYerlestir(createdObject.GetComponent<PoolObje>());
                    createdItem = createdNullItem;
                    Canvas_Manager.Instance.SetCreatorButtons(true);
                    Canvas_Manager.Instance.SetPanelObjectBehaviourPanel(false);
                }
            }
        }
    }
    private void RayCaster(bool isMover)
    {
        Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 500, layerMaskIndex))
        {
            int x = Mathf.RoundToInt(hit.point.x);
            int z = Mathf.RoundToInt(hit.point.z);
            if (x >= Map_Holder.Instance.BoardSize.x)
            {
                return;
            }
            if (z >= Map_Holder.Instance.BoardSize.y)
            {
                return;
            }
            if (Map_Holder.Instance.GameBoard[x, z].board_Game.boardType == BoardType.Empty)
            {
                createdObject.transform.position = new Vector3(x, 0, z);
                createdObjectMaterial.color = createdObjectColor;
                if (Input.GetMouseButtonUp(0))
                {
                    // Seçili parçayı ayarla ve bırak
                    Board_Object board_Object = createdObject.GetComponent<Board_Object>();
                    BoardType boardType = board_Object.LearnBoardType();
                    board_Object.SetBoardCoor(new Vector2Int(x, z));
                    Map_Holder.Instance.GameBoard[x, z] = new GameBoard(boardType, createdOrder, board_Object.gameObject);
                    //Map_Holder.Instance.MyBoard[x, z] = new Board(boardType, createdOrder);
                    if (isMover)
                    {
                        MoverActive(boardType);
                    }
                    else
                    {
                        CreatorActive(boardType, board_Object);
                    }
                    RemoveBoardFromList(x, z);
                    Canvas_Manager.Instance.SetCreatorButtons(true);
                    Canvas_Manager.Instance.SetPanelObjectBehaviourPanel(false);
                }
            }
            else
            {
                createdObjectMaterial.color = Color.red;
            }
        }
    }
    private void MoverActive(BoardType boardType)
    {
        if (boardType == BoardType.Gate)
        {
            hasGate = true;
        }
    }
    private void CreatorActive(BoardType boardType, Board_Object board_Object)
    {
        if (boardType == BoardType.Wall)
        {
            Map_Holder.Instance.WallObjects.Add(board_Object);
        }
        else if (boardType == BoardType.Box)
        {
            Map_Holder.Instance.BoxObjects.Add(board_Object);
        }
        else if (boardType == BoardType.Trap)
        {
            Map_Holder.Instance.TrapObjects.Add(board_Object);
        }
        else if (boardType == BoardType.Enemy)
        {
            Map_Holder.Instance.EnemyObjects.Add(board_Object);
        }
        else if (boardType == BoardType.BossEnemy)
        {
            Map_Holder.Instance.BossEnemyObjects.Add(board_Object);
        }
        else if (boardType == BoardType.Gate)
        {
            hasGate = true;
            Map_Holder.Instance.GateObjects.Add(board_Object);
        }
        // Seçili parçayı null objeye eşitle
        createdItem = createdNullItem;
        createdOrder = -1;
    }
    public void ChooseStuckObject(GameObject obj)
    {
        createdObject = obj;
        Canvas_Manager.Instance.SetCreatorButtons(false);
        Canvas_Manager.Instance.SetPanelObjectBehaviourPanel(true, true, true);
        Board_Object board_Object = createdObject.GetComponent<Board_Object>();
        myBoardList.Add(new BoardCoor(board_Object.MyCoor.x, board_Object.MyCoor.y));
    }
    public void MoveObject()
    {
        movingObj = true;
        Canvas_Manager.Instance.SetPanelObjectBehaviourPanel(false);
        Board_Object board_Object = createdObject.GetComponent<Board_Object>();
        //Map_Holder.Instance.MyBoard[board_Object.MyCoor.x, board_Object.MyCoor.y] = new Board();
        Map_Holder.Instance.GameBoard[board_Object.MyCoor.x, board_Object.MyCoor.y] = new GameBoard();
    }
    public void TurnObject()
    {
        createdObject.transform.Rotate(Vector3.up * 90);
    }
    public void DestroyObject()
    {
        Canvas_Manager.Instance.SetCreatorButtons(true);
        Canvas_Manager.Instance.SetPanelObjectBehaviourPanel(false);
        Board_Object board_Object = createdObject.GetComponent<Board_Object>();
        myBoardList.Add(new BoardCoor(board_Object.MyCoor.x, board_Object.MyCoor.y));
        if (Map_Holder.Instance.GameBoard[board_Object.MyCoor.x, board_Object.MyCoor.y].board_Game.boardType == BoardType.Wall)
        {
            Map_Holder.Instance.WallObjects.Remove(board_Object);
        }
        else if (Map_Holder.Instance.GameBoard[board_Object.MyCoor.x, board_Object.MyCoor.y].board_Game.boardType == BoardType.Box)
        {
            Map_Holder.Instance.BoxObjects.Remove(board_Object);
        }
        else if (Map_Holder.Instance.GameBoard[board_Object.MyCoor.x, board_Object.MyCoor.y].board_Game.boardType == BoardType.Trap)
        {
            Map_Holder.Instance.TrapObjects.Remove(board_Object);
        }
        else if (Map_Holder.Instance.GameBoard[board_Object.MyCoor.x, board_Object.MyCoor.y].board_Game.boardType == BoardType.Enemy)
        {
            Map_Holder.Instance.EnemyObjects.Remove(board_Object);
        }
        else if (Map_Holder.Instance.GameBoard[board_Object.MyCoor.x, board_Object.MyCoor.y].board_Game.boardType == BoardType.BossEnemy)
        {
            Map_Holder.Instance.BossEnemyObjects.Remove(board_Object);
        }
        else if (Map_Holder.Instance.GameBoard[board_Object.MyCoor.x, board_Object.MyCoor.y].board_Game.boardType == BoardType.Gate)
        {
            hasGate = false;
            Map_Holder.Instance.GateObjects.Remove(board_Object);
        }
        //Map_Holder.Instance.MyBoard[board_Object.MyCoor.x, board_Object.MyCoor.y] = new Board();
        Map_Holder.Instance.GameBoard[board_Object.MyCoor.x, board_Object.MyCoor.y] = new GameBoard();
        board_Object.EnterHavuz();
    }
    public bool CheckLevelMap()
    {
        // Magic stone - Box karşılaştırması yapılacak
        if (boardMagicStoneAmount < 1)
        {
            // Bütün boxlar kırılmamış, devam ettir.
            Warning_Manager.Instance.ShowMessage("You should have more than 0 Magic Stone.");
            return false;
        }
        else if (Map_Holder.Instance.BoxObjects.Count < boardMagicStoneAmount)
        {
            // Bütün boxlar kırılmamış, devam ettir.
            Warning_Manager.Instance.ShowMessage("The number of Boxes must be equal or more to the number of Magic Stone. (" + boardMagicStoneAmount + ")");
            return false;
        }
        else if (!hasGate)
        {
            Warning_Manager.Instance.ShowMessage("You should have a GATE.");
            return false;
        }
        if (!addMagicStone)
        {
            Board_Gate.Instance.SetNeededMagicStone(boardMagicStoneAmount);
            Map_Holder.Instance.SetMagicStone(boardMagicStoneAmount);
            addMagicStone = true;
        }
        return true;
    }
    public void CheckingLevelMap()
    {
        Canvas_Manager.Instance.SetButtonChangeMap(false);
        Debug.Log("Hold on. We checking your map.");
        Warning_Manager.Instance.ShowMessage("Hold on. We checking your map.", 3);
        Player_Base.Instance.SetEffectivePlayer(false);
        StartCoroutine(CheckingForLevelMap());
    }
    IEnumerator CheckingForLevelMap()
    {
        bool mapReady = true;
        // Bütün boxlar kırılmış ve kapı açılmışsa haritayı kaydet.
        for (int x = 0; x < Map_Holder.Instance.BoardSize.x && mapReady; x++)
        {
            for (int y = 0; y < Map_Holder.Instance.BoardSize.y && mapReady; y++)
            {
                yield return new WaitForSeconds(0.05f);
                if (Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.Box)
                {
                    if (Map_Holder.Instance.GameBoard[x, y].board_Object.activeSelf)
                    {
                        mapReady = false;
                    }
                }
            }
        }
        if (!mapReady)
        {
            Player_Base.Instance.SetEffectivePlayer(true);
            Player_Base.Instance.SetPosition(Vector3.zero);
            Debug.Log("Destroy all BOX and try to save again.");
            Warning_Manager.Instance.ShowMessage("Destroy all BOX and try to save again.", 3);
        }
        else
        {
            Debug.LogWarning("Map kontrol ediliyor. Lütfen bekleyin.");
            Warning_Manager.Instance.ShowMessage("Map kontrol ediliyor. Lütfen bekleyin.", 3);
            // Duvarların ortaasında boşluk olup olmadığı kontrol edilecek
            randomBoardCheckTime = 5;
            randomBoardChecking = true;
            StartCoroutine(CheckBoardForMyLevel(0, 0));
            while (randomBoardChecking)
            {
                randomBoardCheckTime -= 0.05f;
                yield return new WaitForSeconds(0.05f);
                if (randomBoardCheckTime <= 0)
                {
                    randomBoardChecking = false;
                }
            }
            if (myBoardList.Count == 0)
            {
                Debug.Log("Boardda geçilemeyen boşluk yok.");
                Canvas_Manager.Instance.SetSaveButton();

                for (int x = 0; x < Map_Holder.Instance.BoardSize.x; x++)
                {
                    for (int y = 0; y < Map_Holder.Instance.BoardSize.y; y++)
                    {
                        if (Map_Holder.Instance.GameBoard[x, y].board_Object is null)
                        {
                            Map_Holder.Instance.GameBoard[x, y].board_Game.boardType = BoardType.Empty;
                            //Map_Holder.Instance.MyBoard[x, y].boardType = BoardType.Empty;
                        }
                        else
                        {
                            if (Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.Bomb)
                            {
                                Map_Holder.Instance.GameBoard[x, y] = new GameBoard();
                            }
                            else
                            {
                                Board_Object board_Object = Map_Holder.Instance.GameBoard[x, y].board_Object.GetComponent<Board_Object>();
                                board_Object.SetBoardCoor(new Vector2Int(x, y));
                                Map_Holder.Instance.GameBoard[x, y].board_Game.boardType = board_Object.LearnBoardType();
                                //Map_Holder.Instance.MyBoard[x, y].boardType = board_Object.LearnBoardType();
                                board_Object.havuzum.HavuzdanObjeIste(new Vector3(x, 0, y));
                            }
                        }
                    }
                }
                Map_Holder.Instance.SetBoardForNonUseable();
                Canvas_Manager.Instance.SetButtonChangeMap(true);
                Board_Gate.Instance.SetNeededMagicStone(boardMagicStoneAmount);
                addMagicStone = false;
                FixBoardCoorList();
                Debug.Log("Map is good so You can save it.");
                Warning_Manager.Instance.ShowMessage("Map is good so You can save it or change it", 3);
            }
            else
            {
                // Geçilemeyen alanlar var onu iptal ettir.
                for (int x = 0; x < Map_Holder.Instance.BoardSize.x; x++)
                {
                    for (int y = 0; y < Map_Holder.Instance.BoardSize.y; y++)
                    {
                        if (Map_Holder.Instance.GameBoard[x, y].board_Object is null)
                        {
                            Map_Holder.Instance.GameBoard[x, y].board_Game.boardType = BoardType.Empty;
                            //Map_Holder.Instance.MyBoard[x, y].boardType = BoardType.Empty;
                        }
                        else
                        {
                            if (Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.Bomb)
                            {
                                Map_Holder.Instance.GameBoard[x, y] = new GameBoard();
                            }
                            else
                            {
                                Board_Object board_Object = Map_Holder.Instance.GameBoard[x, y].board_Object.GetComponent<Board_Object>();
                                board_Object.SetBoardCoor(new Vector2Int(x, y));
                                Map_Holder.Instance.GameBoard[x, y].board_Game.boardType = board_Object.LearnBoardType();
                                //Map_Holder.Instance.MyBoard[x, y].boardType = board_Object.LearnBoardType();
                                board_Object.havuzum.HavuzdanObjeIste(new Vector3(x, 0, y));
                            }
                        }
                    }
                }
                Canvas_Manager.Instance.CanNotSaveMap();
                Map_Holder.Instance.SetBoardForNonUseable();
                Canvas_Manager.Instance.SetButtonChangeMap(true);
                Board_Gate.Instance.SetNeededMagicStone(boardMagicStoneAmount);
                addMagicStone = false;
                FixBoardCoorList();
                Debug.Log("Player can not move some area. Fixed pls.");
                Warning_Manager.Instance.ShowMessage("Player can not move some area. Fixed pls.", 3);
            }
        }
    }
    public void SaveMyLevel()
    {
        LevelBoard levelBoard = new LevelBoard();
        levelBoard.magicStone = boardMagicStoneAmount;
        levelBoard.levelSize = Map_Holder.Instance.BoardSize;
        for (int x = 0; x < Map_Holder.Instance.BoardSize.x; x++)
        {
            for (int y = 0; y < Map_Holder.Instance.BoardSize.y; y++)
            {
                levelBoard.levelBoard.Add(Map_Holder.Instance.GameBoard[x, y].board_Game);
            }
        }
        Save_Load_Manager.Instance.SaveBoard(BoardSaveType.MyLevelBoard, levelBoard);
    }
    IEnumerator CheckBoardForMyLevel(int rndX, int rndY)
    {
        yield return new WaitForSeconds(0.05f);
        if (Map_Holder.Instance.GameBoard[rndX, rndY].board_Game.boardType == BoardType.Empty || Map_Holder.Instance.GameBoard[rndX, rndY].board_Game.boardType == BoardType.NonUseable)
        {
            myBoardListBackup.Add(new BoardCoor(rndX, rndY));
            RemoveBoardFromList(rndX, rndY);
            Map_Holder.Instance.GameBoard[rndX, rndY].board_Game.boardType = BoardType.Checked;
            randomBoardCheckTime = 5;
            CheckBoardLimitForMyLevel(rndX, rndY);
        }
    }
    private void CheckBoardLimitForMyLevel(int rndX, int rndY)
    {
        if (rndX + 1 != Map_Holder.Instance.BoardSize.x)
        {
            StartCoroutine(CheckBoardForMyLevel(rndX + 1, rndY));
        }
        if (rndY - 1 != -1)
        {
            StartCoroutine(CheckBoardForMyLevel(rndX, rndY - 1));
        }
        if (rndX - 1 != -1)
        {
            StartCoroutine(CheckBoardForMyLevel(rndX - 1, rndY));
        }
        if (rndY + 1 != Map_Holder.Instance.BoardSize.y)
        {
            StartCoroutine(CheckBoardForMyLevel(rndX, rndY + 1));
        }
    }
    private void RemoveBoardFromList(int x, int z)
    {
        bool finded = false;
        for (int e = 0; e < myBoardList.Count && !finded; e++)
        {
            if (myBoardList[e].SameCoor(x, z))
            {
                finded = true;
                myBoardList.RemoveAt(e);
            }
        }
    }
    private void FixBoardCoorList()
    {
        for (int e = 0; e < myBoardListBackup.Count; e++)
        {
            myBoardList.Add(myBoardListBackup[e]);
        }
        myBoardListBackup.Clear();
    }
    #endregion

    #region Create Random Game Board
    IEnumerator MiddleWall()
    {
        Debug.Log("Toplam Alan : " + (Map_Holder.Instance.BoardSize.x * Map_Holder.Instance.BoardSize.y) + " -> Duvar Alan : " + boardWallAmount + " -> Empty Alan : " + emptyBoardAmount);
        if (boardWallAmount > 0)
        {
            while (boardWallAmount > 0)
            {
                yield return new WaitForSeconds(0.25f);
                int rndX = Random.Range(1, Map_Holder.Instance.BoardSize.x - 1);
                int rndY = Random.Range(1, Map_Holder.Instance.BoardSize.y - 1);
                if (Map_Holder.Instance.GameBoard[rndX, rndY].board_Game.boardType != BoardType.Wall)
                {
                    boardWallAmount--;
                    RemoveBoardFromList(rndX, rndY);
                    PoolObje poolObje = all_Item_Holder.WallList[wallOrder].MyObject.HavuzdanObjeIste(new Vector3(rndX, 0, rndY));
                    GameObject gameObje = poolObje.gameObject;
                    gameObje.transform.SetParent(Map_Holder.Instance.BoardWallParent);
                    gameObje.name = "WallInSide -> X: " + rndX + ", Y: " + rndY;
                    Map_Holder.Instance.WallObjects.Add(poolObje);
                    //Map_Holder.Instance.MyBoard[rndX, rndY] = new Board(BoardType.Wall, wallOrder);
                    Map_Holder.Instance.GameBoard[rndX, rndY] = new GameBoard(BoardType.Wall, wallOrder, gameObje);
                }
            }
            Debug.LogWarning("Tüm duvarlar yerleştirildi. Map kontrol ediliyor.");
            // Duvarların ortaasında boşluk olup olmadığı kontrol edilecek
            randomBoardCheckTime = 3;
            randomBoardChecking = true;
            StartCoroutine(CheckBoardForGame(0, 0));
            while (randomBoardChecking)
            {
                randomBoardCheckTime -= 0.05f;
                yield return new WaitForSeconds(0.05f);
                if (randomBoardCheckTime <= 0)
                {
                    randomBoardChecking = false;
                }
            }
            if (myBoardList.Count == 0)
            {
                Debug.LogWarning("Boardda geçilemeyen boşluk yok. Kutuları koymaya başlıyoruz.");
                SetBoardForUsing();
                StartCoroutine(CreateBox());
            }
            else
            {
                // Geçilemeyen alanlar var onu iptal ettir.
                Warning_Manager.Instance.ShowMessage("Player can not move some area. Fixed pls.");
            }
        }
        else
        {
            Debug.LogWarning("Levelde duvar istenmemiş. Kutu aşamasına geçiliyor.");
            SetBoardForUsing();
            StartCoroutine(CreateBox());
        }
    }
    IEnumerator CheckBoardForGame(int rndX, int rndY)
    {
        yield return new WaitForSeconds(0.25f);
        if (Map_Holder.Instance.GameBoard[rndX, rndY].board_Game.boardType == BoardType.Empty || Map_Holder.Instance.GameBoard[rndX, rndY].board_Game.boardType == BoardType.NonUseable)
        {
            RemoveBoardFromList(rndX, rndY);
            Map_Holder.Instance.GameBoard[rndX, rndY].board_Game.boardType = BoardType.Checked;
            if (myBoardList.Count > 0)
            {
                randomBoardCheckTime = 3;
                CheckBoardLimitForMyLevel(rndX, rndY);
            }
        }
        else
        {
            CheckBoardLimitForGame(rndX, rndY);
        }
    }
    private void CheckBoardLimitForGame(int rndX, int rndY)
    {
        if (rndX + 1 != Map_Holder.Instance.BoardSize.x)
        {
            StartCoroutine(CheckBoardForGame(rndX + 1, rndY));
        }
        if (rndY - 1 != -1)
        {
            StartCoroutine(CheckBoardForGame(rndX, rndY - 1));
        }
        if (rndX - 1 != -1)
        {
            StartCoroutine(CheckBoardForGame(rndX - 1, rndY));
        }
        if (rndY + 1 != Map_Holder.Instance.BoardSize.y)
        {
            StartCoroutine(CheckBoardForGame(rndX, rndY + 1));
        }
    }
    private void SetBoardForUsing()
    {
        Map_Holder.Instance.SetBoardForNonUseable();

        for (int x = 0; x < Map_Holder.Instance.BoardSize.x; x++)
        {
            for (int y = 0; y < Map_Holder.Instance.BoardSize.y; y++)
            {
                if (Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.Checked)
                {
                    Map_Holder.Instance.GameBoard[x, y].board_Game.boardType = BoardType.Empty;
                }
            }
        }
    }
    IEnumerator CreateBox()
    {
        yield return new WaitForSeconds(0.05f);
        if (boardBoxAmount > 0)
        {
            while (boardBoxAmount > 0)
            {
                yield return new WaitForSeconds(0.25f);
                int rndX = Random.Range(1, Map_Holder.Instance.BoardSize.x - 1);
                int rndY = Random.Range(1, Map_Holder.Instance.BoardSize.y - 1);
                if (Map_Holder.Instance.GameBoard[rndX, rndY].board_Game.boardType == BoardType.Empty)
                {
                    boardBoxAmount--;
                    RemoveBoardFromList(rndX, rndY);
                    PoolObje poolObje = all_Item_Holder.BoxList[boxOrder].MyObject.HavuzdanObjeIste(new Vector3(rndX, 0, rndY));
                    GameObject gameObje = poolObje.gameObject;
                    gameObje.transform.SetParent(Map_Holder.Instance.BoardBoxParent);
                    gameObje.name = "Box - " + boardBoxAmount + " -> X: " + rndX + ", Y: " + rndY;
                    gameObje.GetComponent<Board_Object>().SetBoardCoor(new Vector2Int(rndX, rndY));
                    Map_Holder.Instance.BoxObjects.Add(poolObje);
                    //Map_Holder.Instance.MyBoard[rndX, rndY] = new Board(BoardType.Box, boxOrder);
                    Map_Holder.Instance.GameBoard[rndX, rndY] = new GameBoard(BoardType.Box, boxOrder, gameObje);
                }
            }
            Debug.LogWarning("Tüm kutular yerleştirildi.");
        }
        else
        {
            Debug.LogWarning("Levelde kutu istenmemiş. Enemy aşamasına geçiliyor.");
        }
        StartCoroutine(CreateEnemy());
    }
    IEnumerator CreateEnemy()
    {
        yield return new WaitForSeconds(0.05f);
        if (boardEnemyAmount > 0)
        {
            while (boardEnemyAmount > 0)
            {
                yield return new WaitForSeconds(0.25f);
                int rndX = Random.Range(1, Map_Holder.Instance.BoardSize.x - 1);
                int rndY = Random.Range(1, Map_Holder.Instance.BoardSize.y - 1);
                if (Map_Holder.Instance.GameBoard[rndX, rndY].board_Game.boardType == BoardType.Empty)
                {
                    boardEnemyAmount--;
                    RemoveBoardFromList(rndX, rndY);
                    PoolObje poolObje = all_Item_Holder.EnemyList[enemyOrder].MyObject.HavuzdanObjeIste(new Vector3(rndX, 0, rndY));
                    GameObject gameObje = poolObje.gameObject;
                    gameObje.transform.SetParent(Map_Holder.Instance.BoardEnemyParent);
                    gameObje.name = "Enemy - " + boardEnemyAmount + " -> X: " + rndX + ", Y: " + rndY;
                    gameObje.GetComponent<Board_Object>().SetBoardCoor(new Vector2Int(rndX, rndY));
                    Map_Holder.Instance.EnemyObjects.Add(poolObje);
                    //Map_Holder.Instance.MyBoard[rndX, rndY] = new Board(BoardType.Enemy, enemyOrder);
                    Map_Holder.Instance.GameBoard[rndX, rndY] = new GameBoard(BoardType.Enemy, enemyOrder, gameObje);
                }
            }
            Debug.LogWarning("Tüm düşmanlar yerleştirildi.");
        }
        else
        {
            Debug.LogWarning("Levelde düşman istenmemiş. Boss Enemy aşamasına geçiliyor.");
        }
        StartCoroutine(CreateBossEnemy());
    }
    IEnumerator CreateBossEnemy()
    {
        yield return new WaitForSeconds(0.05f);
        if (boardBossEnemyAmount > 0)
        {
            while (boardBossEnemyAmount > 0)
            {
                yield return new WaitForSeconds(0.25f);
                int rndX = Random.Range(1, Map_Holder.Instance.BoardSize.x - 1);
                int rndY = Random.Range(1, Map_Holder.Instance.BoardSize.y - 1);
                if (Map_Holder.Instance.GameBoard[rndX, rndY].board_Game.boardType == BoardType.Empty)
                {
                    boardBossEnemyAmount--;
                    RemoveBoardFromList(rndX, rndY);
                    PoolObje poolObje = all_Item_Holder.BossEnemyList[bossEnemyOrder].MyObject.HavuzdanObjeIste(new Vector3(rndX, 0, rndY));
                    GameObject gameObje = poolObje.gameObject;
                    gameObje.name = "Boss Enemy - " + boardBossEnemyAmount + " -> X: " + rndX + ", Y: " + rndY;
                    gameObje.transform.SetParent(Map_Holder.Instance.BoardBossEnemyParent);
                    gameObje.GetComponent<Board_Object>().SetBoardCoor(new Vector2Int(rndX, rndY));
                    Map_Holder.Instance.BossEnemyObjects.Add(poolObje);
                    //Map_Holder.Instance.MyBoard[rndX, rndY] = new Board(BoardType.BossEnemy, bossEnemyOrder);
                    Map_Holder.Instance.GameBoard[rndX, rndY] = new GameBoard(BoardType.BossEnemy, bossEnemyOrder, gameObje);
                }
            }
            Debug.LogWarning("Tüm patron düşmanlar yerleştirildi.");
        }
        else
        {
            Debug.LogWarning("Levelde patron düşman istenmemiş. Tuzak aşamasına geçiliyor.");
        }
        StartCoroutine(CreateTrap());
    }
    IEnumerator CreateTrap()
    {
        yield return new WaitForSeconds(0.05f);
        if (boardTrapAmount > 0)
        {
            while (boardTrapAmount > 0)
            {
                int trapOrder = Random.Range(0, all_Item_Holder.TrapList.Count);
                yield return new WaitForSeconds(0.25f);
                int rndX = Random.Range(1, Map_Holder.Instance.BoardSize.x - 1);
                int rndY = Random.Range(1, Map_Holder.Instance.BoardSize.y - 1);
                if (Map_Holder.Instance.GameBoard[rndX, rndY].board_Game.boardType == BoardType.Empty)
                {
                    boardTrapAmount--;
                    RemoveBoardFromList(rndX, rndY);
                    PoolObje poolObje = all_Item_Holder.TrapList[trapOrder].MyObject.HavuzdanObjeIste(new Vector3(rndX, 0, rndY));
                    GameObject gameObje = poolObje.gameObject;
                    gameObje.transform.SetParent(Map_Holder.Instance.BoardTrapParent);
                    gameObje.name = "Trap - " + boardTrapAmount + " -> X: " + rndX + ", Y: " + rndY;
                    gameObje.GetComponent<Board_Object>().SetBoardCoor(new Vector2Int(rndX, rndY));
                    Map_Holder.Instance.TrapObjects.Add(poolObje);
                    //Map_Holder.Instance.MyBoard[rndX, rndY] = new Board(BoardType.Trap, trapOrder);
                    Map_Holder.Instance.GameBoard[rndX, rndY] = new GameBoard(BoardType.Trap, trapOrder, gameObje);
                }
            }
            Debug.LogWarning("Tüm tuzaklar yerleştirildi. Tuzakların ayarlarını yapabilir ve sonra leveli kaydedebilirsiniz.");
        }
        else
        {
            Debug.LogWarning("Levelde tuzak istenmemiş. Leveli kaydedebilirsiniz.");
        }
    }
    // Yaptığımız level mapini game için kaydediyoruz.
    public void SaveGameMap()
    {
        // Magic stone - Box karşılaştırması yapılacak
        if (boardMagicStoneAmount < 1)
        {
            // Bütün boxlar kırılmamış, devam ettir.
            Warning_Manager.Instance.ShowMessage("You should have more than 0 Magic Stone.");
            return;
        }
        else if (Map_Holder.Instance.BoxObjects.Count < boardMagicStoneAmount)
        {
            // Bütün boxlar kırılmamış, devam ettir.
            Warning_Manager.Instance.ShowMessage("The number of Boxes must be equal or more to the number of Magic Stone. (" + boardMagicStoneAmount + ")");
            return;
        }
        else if (!hasGate)
        {
            Warning_Manager.Instance.ShowMessage("You should have a GATE.");
            return;
        }

        // Bütün boxlar kırılmış ve kapı açılmış, haritayı save et.
        LevelBoard levelBoard = new LevelBoard();
        levelBoard.magicStone = boardMagicStoneAmount;
        levelBoard.levelSize = Map_Holder.Instance.BoardSize;
        for (int x = 0; x < Map_Holder.Instance.BoardSize.x; x++)
        {
            for (int y = 0; y < Map_Holder.Instance.BoardSize.y; y++)
            {
                levelBoard.levelBoard.Add(Map_Holder.Instance.GameBoard[x, y].board_Game);
            }
        }
        Save_Load_Manager.Instance.SaveBoard(BoardSaveType.GameLevelBoard, levelBoard);
    }
    #endregion
}