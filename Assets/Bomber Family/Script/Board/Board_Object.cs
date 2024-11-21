using UnityEngine;

public class Board_Object : PoolObje
{
    [SerializeField] private BoardType myBoardType;
    [SerializeField] private Collider myCollider;

    private bool isStuck;
    private bool isEnter;
    private int myBoardOrder;
    private Vector2Int myCoor;

    public Vector2Int MyCoor { get { return myCoor; } }
    public bool IsStuck { get { return isStuck; } }
    public int MyBoardOrder { get { return myBoardOrder; } }
    public BoardType MyBoardType { get { return myBoardType; } }
    public Collider MyCollider { get { return myCollider; } }

    private void Start()
    {
        Canvas_Manager.Instance.OnGameWin += Instance_OnGameWin;
        Canvas_Manager.Instance.OnGameLost += Instance_OnGameLost;
        OnStart();
    }
    public virtual void OnStart()
    {
    }
    private void OnMouseUpAsButton()
    {
        if (Game_Manager.Instance.GameType == GameType.MapCreate)
        {
            if (isStuck && Map_Creater_Manager.Instance.CreatedOrder < 0)
            {
                SetMouseButton();
                if (isEnter)
                {
                    transform.localScale = Vector3.one * 2;
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
        if (myCollider != null)
        {
            myCollider.enabled = true;
        }
        base.ObjeHavuzEnter();
    }
}