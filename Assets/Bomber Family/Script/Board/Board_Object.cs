using System;
using UnityEngine;

public class Board_Object : PoolObje
{
    [SerializeField] private BoardType boardType;

    private int boardOrder;
    private Vector2Int myCoor;
    private bool isStuck;
    private Collider myCollider;

    public Vector2Int MyCoor { get { return myCoor; } }
    public bool IsStuck { get { return isStuck; } }
    public int BoardOrder { get { return boardOrder; } }
    public Collider MyCollider { get { return myCollider; } }

    private void Start()
    {
        myCollider = GetComponent<Collider>();
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
            }
        }
    }
    public virtual void SetMouseButton()
    {
        Map_Creater_Manager.Instance.ChooseStuckObject(gameObject);
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
        boardOrder = order;
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
    public BoardType LearnBoardType()
    {
        return boardType;
    }
    public override void ObjeHavuzEnter()
    {
        base.ObjeHavuzEnter();
        myCollider.enabled = true;
    }

    internal object DOMoveY(int v1, float v2)
    {
        throw new NotImplementedException();
    }
}