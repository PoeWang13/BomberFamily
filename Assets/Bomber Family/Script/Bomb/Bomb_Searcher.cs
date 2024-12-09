using UnityEngine;

public class Bomb_Searcher : Bomb_Base
{
    private int speed;
    private bool hasPlayer;
    private float searchingTime = 1;
    private float searchingTimeNext;

    private Animator myAnimator;
    private Vector3 direction;
    private Transform myEnemy;
    private (Transform, Vector3Int) path = new(null, Vector3Int.zero);

    private void Awake()
    {
        myAnimator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        hasPlayer = MyOwner is Player_Base;
        myAnimator = GetComponentInChildren<Animator>();
    }
    public override void Bombed()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (IsExploded)
        {
            return;
        }
        SetExploded();
        for (int x = -BombLimit; x < BombLimit + 1; x++)
        {
            for (int y = -BombLimit; y < BombLimit + 1; y++)
            {
                // X sınırlar içinde mi
                if (MyCoor.x + x < 0 || MyCoor.x + x >= Map_Holder.Instance.GameBoard.GetLength(0))
                {
                    continue;
                }
                // Y sınırlar içinde mi
                if (MyCoor.y + y < 0 || MyCoor.y + y >= Map_Holder.Instance.GameBoard.GetLength(1))
                {
                    continue;
                }
                // Bir obje var mı
                if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Object is null)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(BombPower, BombFireTime);
                }
                // Bulunan obje deactif mi
                else if (!Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Object.activeSelf)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(BombPower, BombFireTime);
                }
                // Bulunan obje kutu mu
                else if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Game.boardType == BoardType.NonUseable)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(BombPower, BombFireTime);
                }
                // Bulunan obje kutu mu
                else if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Game.boardType == BoardType.Empty)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(BombPower, BombFireTime);
                }
                // Bulunan obje kutu mu
                else if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Game.boardType == BoardType.Box)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(BombPower, BombFireTime);
                }
                else if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Object.CompareTag("Bomb"))
                {
                    // Bomb patlat
                    Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Object.GetComponent<Bomb_Base>().Bombed();
                }
            }
        }
        EnterHavuz();
    }
    private void Update()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        searchingTimeNext += Time.deltaTime;
        if (searchingTimeNext > searchingTime)
        {
            searchingTimeNext = 0;
            // En yakın düşmanı bulunca ona doğru gideceğiz ve patlayacağız
            SetDirectionToClosestPos(hasPlayer);
        }
        if (path.Item1 is not null)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            if (path.Item1 == myEnemy)
            {
                if (Vector3.SqrMagnitude(transform.position - myEnemy.position) < 0.05f)
                {
                    SetBoardCoor(new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)));
                    Bombed();
                }
            }
            else
            {
                if (Vector3.SqrMagnitude(transform.position - path.Item1.position) < 0.05f)
                {
                    SetSpeed(0);
                }
            }
        }
    }
    private void SetDirectionToClosestPos(bool isPlayer)
    {
        path = Map_Holder.Instance.LearnClosestEnemy(transform, isPlayer);

        if (path.Item1 is null)
        {
            SetSpeed(0);
            return;
        }
        else
        {
            SetSpeed(1);
            // Direction belirle
            myEnemy = path.Item1;
            Vector3Int enemyPos = path.Item2;
            direction = SetDirection(new Vector3Int(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z)), enemyPos);
        }
    }
    private Vector3Int SetDirection(Vector3Int myPos, Vector3Int enemyPos)
    {
        if (myPos.x > enemyPos.x) // Bomba sağda yani sola gitmeliyiz 
        {
            return Vector3Int.left;
        }
        if (myPos.x < enemyPos.x) // Bomba solda yani sağa gitmeliyiz 
        {
            return Vector3Int.right;
        }
        if (myPos.z > enemyPos.z) // Bomba ileride yani geri gitmeliyiz 
        {
            return Vector3Int.back;
        }
        if (myPos.z < enemyPos.z) // Bomba geride yani ileri gitmeliyiz 
        {
            return Vector3Int.forward;
        }
        return Vector3Int.zero;
    }
    private void SetSpeed(int sp)
    {
        speed = sp;
        myAnimator.SetFloat("Speed", speed);
    }
}