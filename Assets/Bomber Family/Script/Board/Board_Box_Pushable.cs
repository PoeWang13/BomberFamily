using DG.Tweening;
using UnityEngine;

public class Board_Box_Pushable : Board_Object
{
    private bool isMoving;
    private bool isPlaying;
    private float pushTime;
    private float pushTimeNext;
    private Vector2Int pushDirection;
    private bool isPlayerOutSide = true;
    private Transform myView;

    public override void OnStart()
    {
        myView = transform.Find("BoardBoxSimpleView");
    }
    public override void SetMouseButton()
    {
        Canvas_Manager.Instance.OpenBaseSetting(true);
        Canvas_Manager.Instance.CloseSettingPanels();
        Map_Creater_Manager.Instance.ChooseStuckObject(this);
    }
    private void Update()
    {
        if (isMoving)
        {
            return;
        }
        if (isPlayerOutSide)
        {
            return;
        }
        if (IsPushing())
        {
            Vector2Int pushingPoint = MyCoor + pushDirection;
            pushTimeNext += Time.deltaTime;
            if (pushTimeNext > pushTime)
            {
                pushTimeNext = 0;
                isMoving = true;
                Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y] = new GameBoard();
                SetBoardCoor(pushingPoint);
                Map_Holder.Instance.GameBoard[pushingPoint.x, pushingPoint.y] = new GameBoard(MyBoardType, MyBoardOrder, gameObject);
                transform.DOMove(new Vector3Int(pushingPoint.x, 0, pushingPoint.y), 0.5f).OnComplete(() =>
                {
                    isMoving = false;
                    isPlaying = false;
                });
            }
            else if (pushTimeNext > pushTime * 0.5f)
            {
                // Yer kontrolü
                if (pushingPoint.x < 0)
                {
                    pushTimeNext = 0;
                    return;
                }
                else if (pushingPoint.y < 0)
                {
                    pushTimeNext = 0;
                    return;
                }
                else if (pushingPoint.x >= Map_Holder.Instance.BoardSize.x)
                {
                    pushTimeNext = 0;
                    return;
                }
                else if (pushingPoint.y >= Map_Holder.Instance.BoardSize.y)
                {
                    pushTimeNext = 0;
                    return;
                }
                if (Map_Holder.Instance.GameBoard[pushingPoint.x, pushingPoint.y].board_Object is not null)
                {
                    if (Map_Holder.Instance.GameBoard[pushingPoint.x, pushingPoint.y].board_Object.activeSelf)
                    {
                        pushTimeNext = 0;
                        return;
                    }
                }
                if (!isPlaying)
                {
                    isPlaying = true;
                    if (pushDirection.x > 0 || pushDirection.x < 0)
                    {
                        myView.DOPunchRotation(Vector3.right * 20, 2, 2, 0.5f);
                    }
                    else if (pushDirection.y > 0 || pushDirection.y < 0)
                    {
                        myView.DOPunchRotation(Vector3.forward * 20, 2, 2, 0.5f);
                    }
                }
            }
        }
        else
        {
            pushTimeNext = 0;
            isPlaying = false;
        }
    }
    private bool IsPushing()
    {
        if (transform.position.x > Player_Base.Instance.transform.position.x)
        {
            if (Player_Base.Instance.Direction.x == 1)
            {
                pushDirection = Vector2Int.right;
                return true;
            }
        }
        if (transform.position.x < Player_Base.Instance.transform.position.x)
        {
            if (Player_Base.Instance.Direction.x == -1)
            {
                pushDirection = Vector2Int.left;
                return true;
            }
        }
        if (transform.position.z > Player_Base.Instance.transform.position.z)
        {
            if (Player_Base.Instance.Direction.z == 1)
            {
                pushDirection = Vector2Int.up;
                return true;
            }
        }
        if (transform.position.z < Player_Base.Instance.transform.position.z)
        {
            if (Player_Base.Instance.Direction.z == -1)
            {
                pushDirection = Vector2Int.down;
                return true;
            }
        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOutSide = false;
            pushTime = other.GetComponent<Player_Base>().CharacterStat.myBombPushingTime;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOutSide = true;
        }
    }
}