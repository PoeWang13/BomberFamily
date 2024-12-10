using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BoardContainer
{
    public BoardData boardData = new BoardData();
}
[System.Serializable]
public class BoardData
{
    public TrapType trapType;
    public string boardString;
}
public interface ISetWorkTime
{
    public void SetWorkTime(bool isOneTime);
}
public interface ISetActivatedObject
{
    public void SetActivatedObject(Vector2Int objCoor);
}
public interface ISetDeActivatedObject
{
    public void SetDeActivatedObject(Vector2Int objCoor);
}
[System.Serializable]
public class BoardTrigger
{
    public BoardTriggerData triggerBoardObject = new BoardTriggerData();
}
[System.Serializable]
public class BoardTriggerData
{
    public bool isOneTime;
    public List<Vector2Int> myAllCoor = new List<Vector2Int>();
    public bool AddCoor(Vector2Int newCoor)
    {
        if (!myAllCoor.Contains(newCoor))
        {
            myAllCoor.Add(newCoor);
            return true;
        }
        return false;
    }
    public bool RemoveCoor(Vector2Int newCoor)
    {
        if (myAllCoor.Contains(newCoor))
        {
            myAllCoor.Remove(newCoor);
            return true;
        }
        return false;
    }
    public bool HasCoor(Vector2Int newCoor)
    {
        for (int e = 0; e < myAllCoor.Count; e++)
        {
            if (myAllCoor[e].x == newCoor.x && myAllCoor[e].y == newCoor.y)
            {
                return true;
            }
        }
        return false;
    }
}
public interface IHasTrigger
{
    public void SetHasTrigger(bool isHasTrigger);
}
public interface IAlwaysActivite
{
    public void SetAlwaysActivite(bool isAlwaysActivite);
}
public interface ISetEffectTime
{
    public void SetEffectTime(float effectTime);
}
public interface ISetActivited
{
    public void SetActivited(bool isActivited);
}
public interface ISetWaitingTime
{
    public void SetWaitingTime(float waitingTime);
}
public interface ISetWorkingTime
{
    public void SetWorkingTime(float workingTime);
}
[System.Serializable]
public class BoardTrapTimer1
{
    public BoardTrapTimer1Data BoardObject = new BoardTrapTimer1Data();
}
[System.Serializable]
public class BoardTrapTimer1Data
{
    public float myWaitingTime;
}
[System.Serializable]
public class BoardTrapTimer2
{
    public BoardTrapTimer2Data BoardObject = new BoardTrapTimer2Data();
}
[System.Serializable]
public class BoardTrapTimer2Data
{
    public bool isTrigger;
    public bool isAlwaysActivited;
    public float myEffectTime;
    public bool isActivited;
    public Vector2 myStopWorkTime;
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

    [SerializeField] private Camera myCamera;
    [SerializeField] private All_Item_Holder all_Item_Holder;

    // Board Setting
    private int boardWallAmount;
    private int boardBoxAmount;
    private int boardTrapAmount;
    private int boardEnemyAmount;
    private int boardBossEnemyAmount;
    private int boardMagicStoneAmount;
    private int boardOrjWallAmount;
    private int boardOrjBoxAmount;
    private int boardOrjTrapAmount;
    private int boardOrjEnemyAmount;
    private int boardOrjBossEnemyAmount;

    private int layerMaskIndex;
    private int emptyBoardAmount;
    private int createdOrder = -1;

    private bool hasGate;
    private bool movingObj;
    private bool addMagicStone;
    private bool randomBoardChecking;

    private float randomBoardCheckTime;

    private Item_Board createdItem;
    private Item_Board createdNullItem;
    private GameObject createdObject;
    private Trap_Trigger trapTrigger;
    private Trap_Base trapBase;
    private List<Color> createdObjectColors = new List<Color>();
    private List<Material> createdObjectMaterials = new List<Material>();
    private List<int> choosedWallList = new List<int>();
    private List<int> choosedBoxList = new List<int>();
    private List<int> choosedTrapList = new List<int>();
    private List<int> choosedEnemyList = new List<int>();
    private List<int> choosedBossEnemyList = new List<int>();

    public bool HasGate { get { return hasGate; } }
    public int CreatedOrder { get { return createdOrder; } }
    public Trap_Trigger TrapTrigger { get { return trapTrigger; } }
    public Trap_Base TrapBase { get { return trapBase; } }
    public List<int> ChoosedWallList { get { return choosedWallList; } }
    public List<int> ChoosedBoxList { get { return choosedBoxList; } }
    public List<int> ChoosedTrapList { get { return choosedTrapList; } }
    public List<int> ChoosedEnemyList { get { return choosedEnemyList; } }
    public List<int> ChoosedBossEnemyList { get { return choosedBossEnemyList; } }

    private void Start()
    {
        layerMaskIndex = 1 << LayerMask.NameToLayer("Ground");
        createdNullItem  = ScriptableObject.CreateInstance(typeof(Item_Board)) as Item_Board;
        createdItem = createdNullItem;
    }

    #region Set Board Setting
    public void SetWallBoard(int amount)
    {
        boardOrjWallAmount = amount;
    }
    public void SetBoxBoard(int amount)
    {
        boardOrjBoxAmount = amount;
    }
    public void SetTrapBoard(int amount)
    {
        boardOrjTrapAmount = amount;
    }
    public void SetEnemyBoard(int amount)
    {
        boardOrjEnemyAmount = amount;
    }
    public void SetBossEnemyBoard(int amount)
    {
        boardOrjBossEnemyAmount = amount;
    }
    public void SetMagicStoneBoard(int amount)
    {
        boardMagicStoneAmount = amount;
    }
    public void ReleaseGate()
    {
        hasGate = false;
    }
    public void SetBoardSize(int sizeX, int sizeY)
    {
        Map_Holder.Instance.MyBoardList.Clear();
        Map_Holder.Instance.MyBoardListBackup.Clear();
        addMagicStone = false;
        sizeX = sizeX % 2 == 0 ? sizeX + 1 : sizeX;
        sizeY = sizeY % 2 == 0 ? sizeY + 1 : sizeY;
        Map_Holder.Instance.SetBoardSize(new Vector2Int(sizeX, sizeY));
        Game_Manager.Instance.SetGameType(GameType.MapCreate);
        Map_Holder.Instance.SendToPoolAllObjects();
        Map_Holder.Instance.SendToPoolOutSideWallObjects();
        Map_Holder.Instance.SetBoardGround();
        CreateLimitWall();
    }
    private void CreateLimitWall()
    {
        // Dış çerçeveye duvarlar eklenecek
        Map_Holder.Instance.CreateOutsideWall();
        Canvas_Manager.Instance.SetPanelCreatorButtonTypeList(!Canvas_Manager.Instance.ToggleDungeonSetting);
        Map_Creator_Camera_Manager.Instance.SetCameraLimit(Map_Holder.Instance.BoardSize);
        if (Canvas_Manager.Instance.ToggleDungeonSetting)
        {
            // Ortaya duvarlar konacak
            Canvas_Manager.Instance.SetActiveMapProcessHolder(true);
            StartCoroutine(MiddleWall());
        }
    }
    #endregion

    #region Create My Game Board
    public void SetCreatedObject(Item_Board item)
    {
        Canvas_Manager.Instance.SetCreatorButtons(false);
        Board_Object board_Object = item.MyPool.HavuzdanObjeIste(myCamera.ScreenToWorldPoint(Input.mousePosition)).GetComponent<Board_Object>();
        int order = all_Item_Holder.LearnOrder(item);
        board_Object.SetBoardOrder(order);
        createdObject = board_Object.gameObject;
        Utils.SetParent(createdObject, "Board_" + board_Object.MyBoardType);
        createdItem = item;
        createdOrder = order;

        createdObjectMaterials.Clear();
        createdObjectColors.Clear();
        Renderer[] render = createdObject.GetComponentsInChildren<Renderer>();
        for (int e = 0; e < render.Length; e++)
        {
            createdObjectMaterials.Add(render[e].material);
        }
        for (int e = 0; e < createdObjectMaterials.Count; e++)
        {
            createdObjectColors.Add(createdObjectMaterials[e].color);
        }
    }
    public void ChooseTrigger(Trap_Trigger trigger)
    {
        if (trapBase != null)
        {
            trapBase.transform.localScale = Vector3.one;
        }
        if (trapTrigger != null)
        {
            trapTrigger.transform.localScale = Vector3.one;
        }
        if (createdObject != null)
        {
            createdObject.transform.localScale = Vector3.one;
        }
        trapTrigger = trigger;
    }
    public void ChooseTrap(Trap_Base trap)
    {
        if (trapBase != null)
        {
            trapBase.transform.localScale = Vector3.one;
        }
        if (trapTrigger != null)
        {
            trapTrigger.transform.localScale = Vector3.one;
        }
        if (createdObject != null)
        {
            createdObject.transform.localScale = Vector3.one;
        }
        trapBase = trap;
    }
    public void FixOrder()
    {
        createdOrder = -1;
        if (createdObject != null)
        {
            if (createdObject.transform.localScale.x == 1)
            {
                createdObject.GetComponent<PoolObje>().EnterHavuz();
            }
            createdObject.transform.localScale = Vector3.one;
        }
        createdObjectMaterials.Clear();
        createdObjectColors.Clear();
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
                if (x < 0 || x >= Map_Holder.Instance.BoardSize.x)
                {
                    return;
                }
                if (z < 0 || z >= Map_Holder.Instance.BoardSize.y)
                {
                    return;
                }
                createdObject.transform.position = new Vector3(x, 0, z);
                if (Map_Holder.Instance.GameBoard[x, z].board_Game.boardType == BoardType.Empty)
                {
                    for (int e = 0; e < createdObjectMaterials.Count; e++)
                    {
                        createdObjectMaterials[e].color = createdObjectColors[e];
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        // Seçili parçayı ayarla ve bırak
                        Board_Object board_Object = createdObject.GetComponent<Board_Object>();
                        BoardType boardType = board_Object.MyBoardType;
                        board_Object.SetBoardCoor(new Vector2Int(x, z));
                        Map_Holder.Instance.GameBoard[x, z] = new GameBoard(boardType, board_Object.MyBoardOrder, board_Object.gameObject);
                        Canvas_Manager.Instance.SetCreatorButtons(true);
                        Canvas_Manager.Instance.CloseBaseSetting();
                        // Objeyi bırak
                        movingObj = false;
                        createdObject.transform.localScale = Vector3.one;
                        createdItem = createdNullItem;
                        createdOrder = -1;
                        createdObject = null;
                    }
                }
                else
                {
                    for (int e = 0; e < createdObjectMaterials.Count; e++)
                    {
                        createdObjectMaterials[e].color = Color.red;
                    }
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
                        for (int e = 0; e < createdObjectMaterials.Count; e++)
                        {
                            createdObjectMaterials[e].color = createdObjectColors[e];
                        }
                        if (Input.GetMouseButtonUp(0))
                        {
                            // Seçili parçayı ayarla ve bırak
                            Board_Object board_Object = createdObject.GetComponent<Board_Object>();
                            BoardType boardType = board_Object.MyBoardType;
                            board_Object.SetBoardCoor(new Vector2Int(x, z));
                            Map_Holder.Instance.GameBoard[x, z] = new GameBoard(boardType, createdOrder, board_Object.gameObject);
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
                                board_Object.GetComponent<Rigidbody>().isKinematic = true;
                                Map_Holder.Instance.EnemyObjects.Add(board_Object);
                            }
                            else if (boardType == BoardType.BossEnemy)
                            {
                                board_Object.GetComponent<Rigidbody>().isKinematic = true;
                                Map_Holder.Instance.BossEnemyObjects.Add(board_Object);
                            }
                            else if (boardType == BoardType.Gate)
                            {
                                hasGate = true;
                                Map_Holder.Instance.SetBoardGate(board_Object);
                            }
                            Map_Holder.Instance.RemoveBoardFromList(x, z);
                            // Seçili parçayı null objeye eşitle
                            createdObject.transform.localScale = Vector3.one;
                            createdItem = createdNullItem;
                            createdObject = null;
                            createdOrder = -1;
                            Canvas_Manager.Instance.SetCreatorButtons(true);
                            Canvas_Manager.Instance.CloseBaseSetting();
                        }
                    }
                    else
                    {
                        for (int e = 0; e < createdObjectMaterials.Count; e++)
                        {
                            createdObjectMaterials[e].color = Color.red;
                        }
                    }
                }
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    createdItem.MyPool.ObjeyiHavuzaYerlestir(createdObject.GetComponent<PoolObje>());
                    createdItem = createdNullItem;
                    Canvas_Manager.Instance.SetCreatorButtons(true);
                    Canvas_Manager.Instance.CloseBaseSetting();
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
                for (int e = 0; e < createdObjectMaterials.Count; e++)
                {
                    createdObjectMaterials[e].color = createdObjectColors[e];
                }
                if (Input.GetMouseButtonUp(0))
                {
                    // Seçili parçayı ayarla ve bırak
                    Board_Object board_Object = createdObject.GetComponent<Board_Object>();
                    BoardType boardType = board_Object.MyBoardType;
                    board_Object.SetBoardCoor(new Vector2Int(x, z));
                    Map_Holder.Instance.GameBoard[x, z] = new GameBoard(boardType, createdOrder, board_Object.gameObject);
                    if (isMover)
                    {
                        MoverActive(boardType);
                    }
                    else
                    {
                        CreatorActive(boardType, board_Object);
                    }
                    Map_Holder.Instance.RemoveBoardFromList(x, z);
                    Canvas_Manager.Instance.SetCreatorButtons(true);
                    Canvas_Manager.Instance.CloseBaseSetting();
                }
            }
            else
            {
                for (int e = 0; e < createdObjectMaterials.Count; e++)
                {
                    createdObjectMaterials[e].color = Color.red;
                }
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
    public void ChooseStuckObject(Board_Object board_Object)
    {
        if (createdObject != null)
        {
            createdObject.transform.localScale = Vector3.one;
        }
        createdObject = board_Object.gameObject;
        Canvas_Manager.Instance.SetCreatorButtons(false);
        Map_Holder.Instance.MyBoardList.Add(new BoardCoor(board_Object.MyCoor.x, board_Object.MyCoor.y));
    }
    public void MoveObject()
    {
        movingObj = true;
        Canvas_Manager.Instance.OpenBaseSetting(false);
        Board_Object board_Object = createdObject.GetComponent<Board_Object>();
        Map_Holder.Instance.GameBoard[board_Object.MyCoor.x, board_Object.MyCoor.y] = new GameBoard();
        createdOrder = board_Object.MyBoardOrder;

        createdObjectMaterials.Clear();
        createdObjectColors.Clear();
        Renderer[] render = createdObject.GetComponentsInChildren<Renderer>();
        for (int e = 0; e < render.Length; e++)
        {
            createdObjectMaterials.Add(render[e].material);
        }
        for (int e = 0; e < render.Length; e++)
        {
            createdObjectColors.Add(render[e].material.color);
        }
    }
    public void TurnObject()
    {
        createdObject.transform.Rotate(Vector3.up * 90);
    }
    public void DestroyObject()
    {
        Canvas_Manager.Instance.SetGoldSmooth(Mathf.FloorToInt(createdItem.MyPrice * 0.5f));
        Canvas_Manager.Instance.CloseBaseSetting();
        if (createdObject != null)
        {
            Board_Object board_Object = createdObject.GetComponent<Board_Object>();
            Map_Holder.Instance.MyBoardList.Add(new BoardCoor(board_Object.MyCoor.x, board_Object.MyCoor.y));
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
            Map_Holder.Instance.GameBoard[board_Object.MyCoor.x, board_Object.MyCoor.y] = new GameBoard();
            board_Object.EnterHavuz();
        }
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
            Map_Holder.Instance.BoardGate.SetNeededMagicStone(boardMagicStoneAmount);
            Map_Holder.Instance.SetMagicStone(boardMagicStoneAmount);
            addMagicStone = true;
        }
        for (int e = 0; e < Map_Holder.Instance.AllEnemyObjects.Count; e++)
        {
            Map_Holder.Instance.AllEnemyObjects[e].GetComponent<Rigidbody>().isKinematic = false;
        }
        return true;
    }
    public void CheckingLevelMap()
    {
        Canvas_Manager.Instance.SetButtonChangeMap(false);
        Warning_Manager.Instance.ShowMessage("Hold on. We checking your map. Please wait...", 3);
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
            Map_Holder.Instance.BoardGate.ResetFinishing();
            Warning_Manager.Instance.ShowMessage("Destroy all BOX and try to save again.", 3);
        }
        else
        {
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
            if (Map_Holder.Instance.MyBoardList.Count == 0)
            {
                Canvas_Manager.Instance.SetSaveButton();

                for (int x = 0; x < Map_Holder.Instance.BoardSize.x; x++)
                {
                    for (int y = 0; y < Map_Holder.Instance.BoardSize.y; y++)
                    {
                        if (Map_Holder.Instance.GameBoard[x, y].board_Object is null)
                        {
                            Map_Holder.Instance.GameBoard[x, y].board_Game.boardType = BoardType.Empty;
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
                                Map_Holder.Instance.GameBoard[x, y].board_Game = new Board(board_Object.MyBoardType, board_Object.MyBoardOrder, 
                                    Map_Holder.Instance.GameBoard[x, y].board_Game.boardSpecial);
                                board_Object.havuzum.HavuzdanObjeIste(new Vector3(x, 0, y));
                            }
                        }
                    }
                }
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
                                Map_Holder.Instance.GameBoard[x, y].board_Game = new Board(board_Object.MyBoardType, board_Object.MyBoardOrder,
                                    Map_Holder.Instance.GameBoard[x, y].board_Game.boardSpecial);
                                board_Object.havuzum.HavuzdanObjeIste(new Vector3(x, 0, y));
                            }
                        }
                    }
                }
                Canvas_Manager.Instance.CanNotSaveMap();
                Warning_Manager.Instance.ShowMessage("Player can not move some area. Fixed pls.", 3);
            }
            addMagicStone = false;
            Map_Holder.Instance.SetBoardForUsing();
            Canvas_Manager.Instance.SetButtonChangeMap(true);
            Map_Holder.Instance.BoardGate.SetNeededMagicStone(boardMagicStoneAmount);
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
        Save_Load_Manager.Instance.SaveBoard(levelBoard);
    }
    IEnumerator CheckBoardForMyLevel(int rndX, int rndY)
    {
        yield return new WaitForSeconds(0.05f);
        if (Map_Holder.Instance.GameBoard[rndX, rndY].board_Game.boardType == BoardType.Empty || Map_Holder.Instance.GameBoard[rndX, rndY].board_Game.boardType == BoardType.NonUseable)
        {
            Map_Holder.Instance.MyBoardListBackup.Add(new BoardCoor(rndX, rndY));
            Map_Holder.Instance.RemoveBoardFromList(rndX, rndY);
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
    #endregion

    #region Create Random Game Board

    public Vector2Int Coor;
    [ContextMenu("Board Kontrol")]
    private void BoardKontrol()
    {
        Debug.Log(Map_Holder.Instance.GameBoard[Coor.x, Coor.y].board_Object);
        Debug.Log(Map_Holder.Instance.GameBoard[Coor.x, Coor.y].board_Game.boardOrder);
        Debug.Log(Map_Holder.Instance.GameBoard[Coor.x, Coor.y].board_Game.boardType);
        Debug.Log(Map_Holder.Instance.GameBoard[Coor.x, Coor.y].board_Game.boardSpecial);
    }
    IEnumerator MiddleWall()
    {
        boardTrapAmount = 0;
        boardBossEnemyAmount = 0;
        boardEnemyAmount = 0;
        boardBoxAmount = 0;
        boardWallAmount = 0;
        Debug.Log("Toplam Alan : " + (Map_Holder.Instance.BoardSize.x * Map_Holder.Instance.BoardSize.y) + " -> Duvar Alan : " + boardWallAmount + " -> Empty Alan : " + emptyBoardAmount);
        if (boardOrjWallAmount > 0)
        {
            Canvas_Manager.Instance.MapProcessBase("Wall");
            while (boardWallAmount != boardOrjWallAmount)
            {
                int rndOrder = Random.Range(0, Map_Holder.Instance.MyBoardList.Count);
                Vector3Int newPlace = new Vector3Int(Map_Holder.Instance.MyBoardList[rndOrder].x, 0, Map_Holder.Instance.MyBoardList[rndOrder].y);
                yield return new WaitForSeconds(0.05f);
                if (Map_Holder.Instance.GameBoard[newPlace.x, newPlace.z].board_Game.boardType == BoardType.Empty)
                {
                    int rndWall = Random.Range(0, choosedWallList.Count);
                    bool hasGold = false;
                    while (choosedWallList.Count > 0 && !hasGold)
                    {
                        rndWall = Random.Range(0, choosedWallList.Count);
                        if (Save_Load_Manager.Instance.gameData.gold >= all_Item_Holder.WallList[choosedWallList[rndWall]].MyPrice)
                        {
                            Canvas_Manager.Instance.SetGoldSmooth(-all_Item_Holder.WallList[choosedWallList[rndWall]].MyPrice);
                            boardWallAmount++;
                            Canvas_Manager.Instance.MapProcess(1.0f * boardWallAmount / boardOrjWallAmount);
                            Map_Holder.Instance.RemoveBoardFromList(newPlace.x, newPlace.z);
                            hasGold = true;
                        }
                        else
                        {
                            choosedWallList.RemoveAt(rndWall);
                            Warning_Manager.Instance.ShowMessage("You don't have enough gold to build the wall. Wall order is " + rndWall + ".");
                            if (choosedWallList.Count == 0)
                            {
                                boardWallAmount = boardOrjWallAmount;
                            }
                        }
                    }
                    if (hasGold)
                    {
                        PoolObje poolObje = all_Item_Holder.WallList[choosedWallList[rndWall]].MyPool.HavuzdanObjeIste(new Vector3(newPlace.x, 0, newPlace.z));
                        GameObject gameObje = poolObje.gameObject;
                        gameObje.transform.SetParent(Map_Holder.Instance.BoardWallParent);
                        gameObje.name = "WallInSide -> X: " + newPlace.x + ", Y: " + newPlace.z;
                        Board_Object board = gameObje.GetComponent<Board_Object>();
                        board.SetBoardCoor(new Vector2Int(newPlace.x, newPlace.z));
                        board.SetBoardOrder(choosedWallList[rndWall]);
                        Map_Holder.Instance.WallObjects.Add(poolObje);
                        Map_Holder.Instance.GameBoard[newPlace.x, newPlace.z] = new GameBoard(BoardType.Wall, choosedWallList[rndWall], gameObje);
                    }
                }
            }
            Warning_Manager.Instance.ShowMessage("All walls are crafted. Checking the map for walking.");
            // Duvarların ortaasında boşluk olup olmadığı kontrol edilecek
            randomBoardCheckTime = 3;
            randomBoardChecking = true;
            Canvas_Manager.Instance.SetActiveMapProcess(false);
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
            Canvas_Manager.Instance.SetActiveMapProcess(true);
            if (Map_Holder.Instance.MyBoardList.Count == 0)
            {
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
            Warning_Manager.Instance.ShowMessage("No wall is wanted in the level. Moving on to the box stage.");
            StartCoroutine(CreateBox());
        }
    }
    IEnumerator CheckBoardForGame(int rndX, int rndY)
    {
        yield return new WaitForSeconds(0.25f);
        if (Map_Holder.Instance.GameBoard[rndX, rndY].board_Game.boardType == BoardType.Empty || Map_Holder.Instance.GameBoard[rndX, rndY].board_Game.boardType == BoardType.NonUseable)
        {
            Map_Holder.Instance.HasNotBackup(rndX, rndY);
            Map_Holder.Instance.RemoveBoardFromList(rndX, rndY);
            Map_Holder.Instance.GameBoard[rndX, rndY].board_Game.boardType = BoardType.Checked;
            if (Map_Holder.Instance.MyBoardList.Count > 0)
            {
                randomBoardCheckTime = 3;
                CheckBoardLimitForGame(rndX, rndY);
            }
        }
    }
    private void CheckBoardLimitForGame(int rndX, int rndY)
    {
        if (rndX - 1 != -1)
        {
            StartCoroutine(CheckBoardForGame(rndX - 1, rndY));
        }
        if (rndX + 1 != Map_Holder.Instance.BoardSize.x)
        {
            StartCoroutine(CheckBoardForGame(rndX + 1, rndY));
        }
        if (rndY - 1 != -1)
        {
            StartCoroutine(CheckBoardForGame(rndX, rndY - 1));
        }
        if (rndY + 1 != Map_Holder.Instance.BoardSize.y)
        {
            StartCoroutine(CheckBoardForGame(rndX, rndY + 1));
        }
    }
    IEnumerator CreateBox()
    {
        Map_Holder.Instance.SetBoardForUsing();
        if (boardOrjBoxAmount > 0)
        {
            Canvas_Manager.Instance.SetActiveMapProcessHolder(true);
            Canvas_Manager.Instance.MapProcessBase("Box");
            while (boardBoxAmount != boardOrjBoxAmount)
            {
                int rndOrder = Random.Range(0, Map_Holder.Instance.MyBoardList.Count);
                Vector3Int newPlace = new Vector3Int(Map_Holder.Instance.MyBoardList[rndOrder].x, 0, Map_Holder.Instance.MyBoardList[rndOrder].y);
                yield return new WaitForSeconds(0.05f);
                if (Map_Holder.Instance.GameBoard[newPlace.x, newPlace.z].board_Game.boardType == BoardType.Empty)
                {
                    int rndBox = Random.Range(0, choosedBoxList.Count);
                    bool hasGold = false;
                    while (choosedBoxList.Count > 0 && !hasGold)
                    {
                        rndBox = Random.Range(0, choosedBoxList.Count);
                        if (Save_Load_Manager.Instance.gameData.gold >= all_Item_Holder.BoxList[choosedBoxList[rndBox]].MyPrice)
                        {
                            Canvas_Manager.Instance.SetGoldSmooth(-all_Item_Holder.BoxList[choosedBoxList[rndBox]].MyPrice);
                            hasGold = true;
                            boardBoxAmount++;
                            Canvas_Manager.Instance.MapProcess(1.0f * boardBoxAmount / boardOrjBoxAmount);
                            Map_Holder.Instance.RemoveBoardFromList(newPlace.x, newPlace.z);
                        }
                        else
                        {
                            choosedBoxList.RemoveAt(rndBox);
                            Warning_Manager.Instance.ShowMessage("You don't have enough gold to build the box. Box order is " + rndBox + ".");
                            if (choosedBoxList.Count == 0)
                            {
                                boardBoxAmount = boardOrjBoxAmount;
                            }
                        }
                    }
                    if (hasGold)
                    {
                        PoolObje poolObje = all_Item_Holder.BoxList[choosedBoxList[rndBox]].MyPool.HavuzdanObjeIste(new Vector3(newPlace.x, 0, newPlace.z));
                        GameObject gameObje = poolObje.gameObject;
                        gameObje.transform.SetParent(Map_Holder.Instance.BoardBoxParent);
                        gameObje.name = "Box - " + boardBoxAmount + " -> X: " + newPlace.x + ", Y: " + newPlace.z;
                        Board_Object board = gameObje.GetComponent<Board_Object>();
                        board.SetBoardCoor(new Vector2Int(newPlace.x, newPlace.z));
                        board.SetBoardOrder(choosedBoxList[rndBox]);
                        Map_Holder.Instance.BoxObjects.Add(poolObje);
                        Map_Holder.Instance.GameBoard[newPlace.x, newPlace.z] = new GameBoard(BoardType.Box, choosedBoxList[rndBox], gameObje);
                    }
                }
            }
            Warning_Manager.Instance.ShowMessage("All boxes have been placed.");
        }
        else
        {
            Warning_Manager.Instance.ShowMessage("No Box is wanted in the level. Moving on to Enemy stage.");
        }
        StartCoroutine(CreateEnemy());
    }
    IEnumerator CreateEnemy()
    {
        if (boardOrjEnemyAmount > 0)
        {
            Canvas_Manager.Instance.SetActiveMapProcessHolder(true);
            Canvas_Manager.Instance.MapProcessBase("Enemy");
            while (boardEnemyAmount != boardOrjEnemyAmount)
            {
                int rndOrder = Random.Range(0, Map_Holder.Instance.MyBoardList.Count);
                Vector3Int newPlace = new Vector3Int(Map_Holder.Instance.MyBoardList[rndOrder].x, 0, Map_Holder.Instance.MyBoardList[rndOrder].y);
                yield return new WaitForSeconds(0.05f);
                if (Map_Holder.Instance.GameBoard[newPlace.x, newPlace.z].board_Game.boardType == BoardType.Empty)
                {
                    boardEnemyAmount++;
                    Canvas_Manager.Instance.MapProcess(1.0f * boardEnemyAmount / boardOrjEnemyAmount);
                    Map_Holder.Instance.RemoveBoardFromList(newPlace.x, newPlace.z);
                    int rndEnemy = Random.Range(0, choosedEnemyList.Count);
                    bool hasGold = false;
                    while (choosedEnemyList.Count > 0 && !hasGold)
                    {
                        rndEnemy = Random.Range(0, choosedEnemyList.Count);
                        if (Save_Load_Manager.Instance.gameData.gold >= all_Item_Holder.EnemyList[choosedEnemyList[rndEnemy]].MyPrice)
                        {
                            Canvas_Manager.Instance.SetGoldSmooth(-all_Item_Holder.EnemyList[choosedEnemyList[rndEnemy]].MyPrice);
                            hasGold = true;
                        }
                        else
                        {
                            choosedEnemyList.RemoveAt(rndEnemy);
                            Warning_Manager.Instance.ShowMessage("You don't have enough gold to build enemy. Enemy order is " + rndEnemy + ".");
                            if (choosedEnemyList.Count == 0)
                            {
                                boardEnemyAmount = boardOrjEnemyAmount;
                            }
                        }
                    }
                    if (hasGold)
                    {
                        PoolObje poolObje = all_Item_Holder.EnemyList[choosedEnemyList[rndEnemy]].MyPool.HavuzdanObjeIste(new Vector3(newPlace.x, 0, newPlace.z));
                        GameObject gameObje = poolObje.gameObject;
                        gameObje.transform.SetParent(Map_Holder.Instance.BoardEnemyParent);
                        gameObje.name = "Enemy - " + boardEnemyAmount + " -> X: " + newPlace.x + ", Y: " + newPlace.z;
                        Board_Object board = gameObje.GetComponent<Board_Object>();
                        board.SetBoardCoor(new Vector2Int(newPlace.x, newPlace.z));
                        board.SetBoardOrder(choosedEnemyList[rndEnemy]);
                        Map_Holder.Instance.EnemyObjects.Add(poolObje);
                        Map_Holder.Instance.GameBoard[newPlace.x, newPlace.z] = new GameBoard(BoardType.Enemy, choosedEnemyList[rndEnemy], gameObje);
                    }
                }
            }
            Warning_Manager.Instance.ShowMessage("All Enemyies have been placed.");
        }
        else
        {
            Warning_Manager.Instance.ShowMessage("No Enemy is wanted in the level. Moving on to Boss Enemy stage.");
        }
        StartCoroutine(CreateBossEnemy());
    }
    IEnumerator CreateBossEnemy()
    {
        yield return new WaitForSeconds(0.05f);
        if (boardOrjBossEnemyAmount > 0)
        {
            Canvas_Manager.Instance.SetActiveMapProcessHolder(true);
            Canvas_Manager.Instance.MapProcessBase("Boss Enemy");
            while (boardBossEnemyAmount != boardOrjBossEnemyAmount)
            {
                int rndOrder = Random.Range(0, Map_Holder.Instance.MyBoardList.Count);
                Vector3Int newPlace = new Vector3Int(Map_Holder.Instance.MyBoardList[rndOrder].x, 0, Map_Holder.Instance.MyBoardList[rndOrder].y);
                yield return new WaitForSeconds(0.05f);
                if (Map_Holder.Instance.GameBoard[newPlace.x, newPlace.z].board_Game.boardType == BoardType.Empty)
                {
                    boardBossEnemyAmount++;
                    Canvas_Manager.Instance.MapProcess(1.0f * boardBossEnemyAmount / boardOrjBossEnemyAmount);
                    Map_Holder.Instance.RemoveBoardFromList(newPlace.x, newPlace.z);
                    int rndBossEnemy = Random.Range(0, choosedBossEnemyList.Count);
                    bool hasGold = false;
                    while (choosedBossEnemyList.Count > 0 && !hasGold)
                    {
                        rndBossEnemy = Random.Range(0, choosedBossEnemyList.Count);
                        if (Save_Load_Manager.Instance.gameData.gold >= all_Item_Holder.BossEnemyList[choosedBossEnemyList[rndBossEnemy]].MyPrice)
                        {
                            Canvas_Manager.Instance.SetGoldSmooth(-all_Item_Holder.BossEnemyList[choosedBossEnemyList[rndBossEnemy]].MyPrice);
                            hasGold = true;
                        }
                        else
                        {
                            choosedBossEnemyList.RemoveAt(rndBossEnemy);
                            Warning_Manager.Instance.ShowMessage("You don't have enough gold to build boss enemy. Boss Enemy order is " + rndBossEnemy + ".");
                            if (choosedBossEnemyList.Count == 0)
                            {
                                boardBossEnemyAmount = boardOrjBossEnemyAmount;
                            }
                        }
                    }
                    if (hasGold)
                    {
                        PoolObje poolObje = all_Item_Holder.BossEnemyList[choosedBossEnemyList[rndBossEnemy]].MyPool.HavuzdanObjeIste(new Vector3(newPlace.x, 0, newPlace.z));
                        GameObject gameObje = poolObje.gameObject;
                        gameObje.name = "Boss Enemy - " + boardBossEnemyAmount + " -> X: " + newPlace.x + ", Y: " + newPlace.z;
                        gameObje.transform.SetParent(Map_Holder.Instance.BoardBossEnemyParent);
                        Board_Object board = gameObje.GetComponent<Board_Object>();
                        board.SetBoardCoor(new Vector2Int(newPlace.x, newPlace.z));
                        board.SetBoardOrder(choosedBossEnemyList[rndBossEnemy]);
                        Map_Holder.Instance.BossEnemyObjects.Add(poolObje);
                        Map_Holder.Instance.GameBoard[newPlace.x, newPlace.z] = new GameBoard(BoardType.BossEnemy, choosedBossEnemyList[rndBossEnemy], gameObje);
                    }
                }
            }
            Warning_Manager.Instance.ShowMessage("All Boss Enemyies have been placed.");
        }
        else
        {
            Warning_Manager.Instance.ShowMessage("No Boss Enemy is wanted in the level. Moving on to Trap stage.");
        }
        StartCoroutine(CreateTrap());
    }
    IEnumerator CreateTrap()
    {
        yield return new WaitForSeconds(0.05f);
        if (boardOrjTrapAmount > 0)
        {
            Canvas_Manager.Instance.SetActiveMapProcessHolder(true);
            Canvas_Manager.Instance.MapProcessBase("Trap");
            while (boardTrapAmount != boardOrjTrapAmount)
            {
                int rndOrder = Random.Range(0, Map_Holder.Instance.MyBoardList.Count);
                Vector3Int newPlace = new Vector3Int(Map_Holder.Instance.MyBoardList[rndOrder].x, 0, Map_Holder.Instance.MyBoardList[rndOrder].y);
                yield return new WaitForSeconds(0.05f);
                if (Map_Holder.Instance.GameBoard[newPlace.x, newPlace.z].board_Game.boardType == BoardType.Empty)
                {
                    int rndTrap = Random.Range(0, choosedTrapList.Count);
                    bool hasGold = false;
                    while (choosedTrapList.Count > 0 && !hasGold)
                    {
                        rndTrap = Random.Range(0, choosedTrapList.Count);
                        if (Save_Load_Manager.Instance.gameData.gold >= all_Item_Holder.TrapList[choosedTrapList[rndTrap]].MyPrice)
                        {
                            boardTrapAmount++;
                            Canvas_Manager.Instance.MapProcess(1.0f * boardTrapAmount / boardOrjTrapAmount);
                            Map_Holder.Instance.RemoveBoardFromList(newPlace.x, newPlace.z);
                            Canvas_Manager.Instance.SetGoldSmooth(-all_Item_Holder.TrapList[choosedTrapList[rndTrap]].MyPrice);
                            hasGold = true;
                        }
                        else
                        {
                            choosedTrapList.RemoveAt(rndTrap);
                            Warning_Manager.Instance.ShowMessage("You don't have enough gold to build the trap. Trap order is " + rndTrap + ".");
                            if (choosedTrapList.Count == 0)
                            {
                                boardTrapAmount = boardOrjTrapAmount;
                            }
                        }
                    }
                    if (hasGold)
                    {
                        PoolObje poolObje = all_Item_Holder.TrapList[choosedTrapList[rndTrap]].MyPool.HavuzdanObjeIste(new Vector3(newPlace.x, 0, newPlace.z));
                        GameObject gameObje = poolObje.gameObject;
                        gameObje.transform.SetParent(Map_Holder.Instance.BoardTrapParent);
                        gameObje.name = "Trap - " + boardTrapAmount + " -> X: " + newPlace.x + ", Y: " + newPlace.z;
                        Board_Object board = gameObje.GetComponent<Board_Object>();
                        board.SetBoardCoor(new Vector2Int(newPlace.x, newPlace.z));
                        board.SetBoardOrder(choosedTrapList[rndTrap]);
                        Map_Holder.Instance.TrapObjects.Add(poolObje);
                        Map_Holder.Instance.GameBoard[newPlace.x, newPlace.z] = new GameBoard(BoardType.Trap, choosedTrapList[rndTrap], gameObje);
                    }
                }
            }
            Warning_Manager.Instance.ShowMessage("All Trap have been placed. You can setting all traps and record level.");
        }
        else
        {
            Warning_Manager.Instance.ShowMessage("No Trap is wanted in the level. You can record level.");
        }
        Canvas_Manager.Instance.SetActiveMapProcessHolder(false);
        Canvas_Manager.Instance.SetPanelCreatorButtonTypeList(true);
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
        Save_Load_Manager.Instance.SaveBoard(levelBoard);
    }
    #endregion
}