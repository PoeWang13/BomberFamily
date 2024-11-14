using UnityEngine;
using System.Collections.Generic;

public class Moving_Base : MonoBehaviour
{

    private int rndDirec;
    private int boardWeight;
    private int boardHeight;
    private int boardMaskIndex;
    private float changeDirectionTime;

    private Enemy_Base myBase;
    private Transform player_Base;
    private List<Vector3> myDirections = new List<Vector3>();

    public float ChangeDirectionTime { get { return changeDirectionTime; } }
    public Transform Player { get { return player_Base; } }
    public int BoardWeight { get { return boardWeight; } }
    public int BoardHeight { get { return boardHeight; } }
    public int RndDirec { get { return rndDirec; } }
    public int BoardMaskIndex { get { return boardMaskIndex; } }
    public Enemy_Base MyBase { get { return myBase; } }
    public List<Vector3> MyDirections { get { return myDirections; } }

    private void Awake()
    {
        myBase = GetComponent<Enemy_Base>();
        boardMaskIndex = 1 << LayerMask.NameToLayer("Board");
    }
    private void Start()
    {
        player_Base = Player_Base.Instance.transform;
        boardWeight = Map_Holder.Instance.GameBoard.GetLength(0);
        boardHeight = Map_Holder.Instance.GameBoard.GetLength(1);
        Invoke("SetDirectionTime", 0.1f);
        OnStart();
    }
    public virtual void OnStart()
    {
    }
    private void SetDirectionTime()
    {
        changeDirectionTime = 1 / myBase.MySpeed;
    }
    public virtual void OnSet()
    {
    }
    public Vector3 FindRandomDirection()
    {
        myDirections.Clear();
        // Sağ bak
        RaycastHit hitInfo;
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, Vector3.right);
        if (Physics.Raycast(ray, out hitInfo, boardWeight, boardMaskIndex))
        {
            if (Vector3.Distance(hitInfo.transform.position, transform.position) > 1.25f)
            {
                myDirections.Add(Vector3.right);
            }
        }
        // Sol bak
        ray = new Ray(transform.position + Vector3.up * 0.5f, Vector3.left);
        if (Physics.Raycast(ray, out hitInfo, boardWeight, boardMaskIndex))
        {
            if (Vector3.Distance(hitInfo.transform.position, transform.position) > 1.25f)
            {
                myDirections.Add(Vector3.left);
            }
        }
        // İleri bak
        ray = new Ray(transform.position + Vector3.up * 0.5f, Vector3.forward);
        if (Physics.Raycast(ray, out hitInfo, boardHeight, boardMaskIndex))
        {
            if (Vector3.Distance(hitInfo.transform.position, transform.position) > 1.25f)
            {
                myDirections.Add(Vector3.forward);
            }
        }
        // Geri bak
        ray = new Ray(transform.position + Vector3.up * 0.5f, Vector3.back);
        if (Physics.Raycast(ray, out hitInfo, boardHeight, boardMaskIndex))
        {
            if (Vector3.Distance(hitInfo.transform.position, transform.position) > 1.25f)
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