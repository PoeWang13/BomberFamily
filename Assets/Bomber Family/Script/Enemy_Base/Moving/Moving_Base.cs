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
    private List<Vector3Int> myDirections = new List<Vector3Int>();

    public float ChangeDirectionTime { get { return changeDirectionTime; } }
    public Transform Player { get { return player_Base; } }
    public int BoardWeight { get { return boardWeight; } }
    public int BoardHeight { get { return boardHeight; } }
    public int RndDirec { get { return rndDirec; } }
    public int BoardMaskIndex { get { return boardMaskIndex; } }
    public Enemy_Base MyBase { get { return myBase; } }
    public List<Vector3Int> MyDirections { get { return myDirections; } }

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
        changeDirectionTime = 1 / myBase.MySpeed;
        OnStart();
    }
    public virtual void OnStart()
    {
    }
    private void TurnPlayerView(Vector3 direction)
    {
        Quaternion direc = Quaternion.LookRotation(direction);
        Vector3 rot = direc.eulerAngles;
        myBase.CharacterView.rotation = Quaternion.Euler(0, rot.y, 0);
    }
    public Vector3Int FindRandomDirection()
    {
        myDirections.Clear();
        // Sağ bak
        RaycastHit hitInfo;
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, Vector3.right);
        if (Physics.Raycast(ray, out hitInfo, boardHeight, boardMaskIndex))
        {
            if (transform.position.x < boardWeight - 1)
            {
                if (Vector3.Distance(hitInfo.transform.position, transform.position) > 1.5f)
                {
                    myDirections.Add(myBase.LearnIntDirection(hitInfo.transform.position + Vector3.left));
                }
            }
        }
        // Sol bak
        ray = new Ray(transform.position + Vector3.up * 0.5f, Vector3.left);
        if (Physics.Raycast(ray, out hitInfo, boardHeight, boardMaskIndex))
        {
            if (transform.position.x > 0)
            {
                if (Vector3.Distance(hitInfo.transform.position, transform.position) > 1.5f)
                {
                    myDirections.Add(myBase.LearnIntDirection(hitInfo.transform.position + Vector3.right));
                }
            }
        }
        // İleri bak
        ray = new Ray(transform.position + Vector3.up * 0.5f, Vector3.forward);
        if (Physics.Raycast(ray, out hitInfo, boardHeight, boardMaskIndex))
        {
            if (transform.position.z < boardWeight - 1)
            {
                if (Vector3.Distance(hitInfo.transform.position, transform.position) > 1.5f)
                {
                    myDirections.Add(myBase.LearnIntDirection(hitInfo.transform.position + Vector3.back));
                }
            }
        }
        // Geri bak
        ray = new Ray(transform.position + Vector3.up * 0.5f, Vector3.back);
        if (Physics.Raycast(ray, out hitInfo, boardHeight, boardMaskIndex))
        {
            if (transform.position.z > 0)
            {
                if (Vector3.Distance(hitInfo.transform.position, transform.position) > 1.5f)
                {
                    myDirections.Add(myBase.LearnIntDirection(hitInfo.transform.position + Vector3.forward));
                }
            }
        }
        if (myDirections.Count > 0)
        {
            rndDirec = Random.Range(0, myDirections.Count);
            Vector3Int vector = myBase.LearnIntDirection(myDirections[rndDirec]);
            return vector;
        }
        return Vector3Int.zero;
    }
}