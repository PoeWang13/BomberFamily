using UnityEngine;
using UnityEngine.EventSystems;

public class Board_Object : PoolObje
{
    [SerializeField] private BoardType myBoardType;
    
    private Collider myTriggeredCollider;
    private Collider myNotTriggeredCollider;
    private bool isStuck;
    private bool isEnter;
    private int myBoardOrder;
    private Vector2Int myCoor;

    public Vector2Int MyCoor { get { return myCoor; } }
    public bool IsStuck { get { return isStuck; } }
    public int MyBoardOrder { get { return myBoardOrder; } }
    public BoardType MyBoardType { get { return myBoardType; } }
    public Collider MyNotTriggeredCollider { get { return myNotTriggeredCollider; } }
    public Collider MyTriggeredCollider { get { return myTriggeredCollider; } }

    private void Start()
    {
        Canvas_Manager.Instance.OnGameWin += Instance_OnGameWin;
        Canvas_Manager.Instance.OnGameLost += Instance_OnGameLost;
        Collider[] colliders = GetComponentsInChildren<Collider>();

        for (int e = 0; e < colliders.Length; e++)
        {
            if (colliders[e].isTrigger)
            {
                myTriggeredCollider = colliders[e];
            }
            if (!colliders[e].isTrigger)
            {
                myNotTriggeredCollider = colliders[e];
            }
        }
        OnStart();
    }
    public virtual void OnStart()
    {
    }
    private void OnMouseUpAsButton()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Game_Manager.Instance.GameType == GameType.MapCreate)
        {
            if (isStuck && Map_Creater_Manager.Instance.CreatedOrder < 0)
            {
                SetMouseButton();
                if (isEnter)
                {
                    transform.localScale = Vector3.one * 1.5f;
                }
            }
        }
    }
    private void OnMouseEnter()
    {
        isEnter = true;
    }
    private void OnMouseExit()
    {
        isEnter = false;
    }
    public virtual void SetMouseButton()
    {
        Map_Creater_Manager.Instance.ChooseStuckObject(this);
        Canvas_Manager.Instance.OpenBaseSetting(true);
        Canvas_Manager.Instance.CloseSettingPanels();
    }
    private void Instance_OnGameLost(object sender, System.EventArgs e)
    {
        OnGameLost();
    }
    public virtual void OnGameLost()
    {
    }
    private void Instance_OnGameWin(object sender, System.EventArgs e)
    {
        OnGameWin();
    }
    public virtual void OnGameWin()
    {

    }
    public void SetBoardOrder(int order)
    {
        myBoardOrder = order;
    }
    public void SetBoardCoor(Vector2Int coor)
    {
        myCoor = coor;
        isStuck = true;
        SetObject();
    }
    public virtual void SetObject()
    {
    }
    public override void ObjeHavuzEnter()
    {
        if (myNotTriggeredCollider != null)
        {
            myNotTriggeredCollider.enabled = true;
        }
        base.ObjeHavuzEnter();
    }
}