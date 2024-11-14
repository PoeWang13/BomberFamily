using UnityEngine;
using System.Collections.Generic;

public class Moving_Base : MonoBehaviour
{
    [SerializeField] private float changeDirectionTime = 5;

    private int rndDirec;
    private int boardWeight;
    private int boardHeight;
    private int boardMaskIndex;
    private int playerMaskIndex;

    private Enemy_Base myBase;
    private Transform player_Base;
    private List<Vector3> myDirections = new List<Vector3>();

    public float ChangeDirectionTime { get { return changeDirectionTime; } }
    public Transform Player { get { return player_Base; } }
    public int BoardWeight { get { return boardWeight; } }
    public int BoardHeight { get { return boardHeight; } }
    public int RndDirec { get { return rndDirec; } }
    public int BoardMaskIndex { get { return boardMaskIndex; } }
    public int PlayerMaskIndex { get { return playerMaskIndex; } }
    public Enemy_Base MyBase { get { return myBase; } }
    public List<Vector3> MyDirections { get { return myDirections; } }

    private void Awake()
    {
        myBase = GetComponent<Enemy_Base>();
        boardMaskIndex = 1 << LayerMask.NameToLayer("Board");
        playerMaskIndex = 1 << LayerMask.NameToLayer("Player");
    }
    private void Start()
    {
        player_Base = Player_Base.Instance.transform;
        boardWeight = Map_Holder.Instance.GameBoard.GetLength(0);
        boardHeight = Map_Holder.Instance.GameBoard.GetLength(1);
        OnStart();
    }
    public virtual void OnStart()
    {
    }
    public void OnSet()
    {
        myBase.SetMove(true);
    }
    private void Update()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (!MyBase.CanMove)
        {
            return;
        }
        Move();
        MyBase.TurnPlayerView(MyBase.Direction);
        transform.Translate(MyBase.Direction * Time.deltaTime * MyBase.MySpeed);
    }
    public virtual void Move()
    {

    }
    public Vector3 FindRandomDirection(bool isCoward = false)
    {
        myDirections.Clear();
        // Sağ bak
        if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.right, 1, boardMaskIndex))
        {
            if (isCoward)
            {
                if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.right, BoardWeight, playerMaskIndex))
                {
                    myDirections.Add(Vector3.right);
                }
            }
            else
            {
                myDirections.Add(Vector3.right);
            }
        }
        // Sol bak
        if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.left, 1, boardMaskIndex))
        {
            if (isCoward)
            {
                if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.left, BoardWeight, playerMaskIndex))
                {
                    myDirections.Add(Vector3.left);
                }
            }
            else
            {
                myDirections.Add(Vector3.left);
            }
        }
        // İleri bak
        if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.forward, 1, boardMaskIndex))
        {
            if (isCoward)
            {
                if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.forward, boardHeight, playerMaskIndex))
                {
                    myDirections.Add(Vector3.forward);
                }
            }
            else
            {
                myDirections.Add(Vector3.forward);
            }
        }
        // Geri bak
        if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.back, 1, boardMaskIndex))
        {
            if (isCoward)
            {
                if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.back, boardHeight, playerMaskIndex))
                {
                    myDirections.Add(Vector3.back);
                }
            }
            else
            {
                myDirections.Add(Vector3.back);
            }
        }
        if (myDirections.Count > 0)
        {
            rndDirec = Random.Range(0, myDirections.Count);
            return myDirections[rndDirec];
        }
        return Vector3Int.zero;
    }
}