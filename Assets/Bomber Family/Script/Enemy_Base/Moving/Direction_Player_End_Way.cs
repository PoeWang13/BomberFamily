using UnityEngine;

public class Direction_Player_End_Way : Moving_Base
{
    // Playerı görürse playera kadar gider, player yoksa yolun sonuna kadar gider. Bulamazsa rastgele dolaşır.
    private bool seePlayer;
    private int playerMaskIndex;
    private Vector3Int playerDirection;
    private float randomDirectionTimeNext;

    public override void OnStart()
    {
        playerMaskIndex = 1 << LayerMask.NameToLayer("Player");
        MyBase.SetDirection(MyBase.LearnIntDirection(FindRandomDirection() - MyBase.LearnIntDirection(transform.position)));
    }
    private void Update()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (seePlayer)
        {
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, playerDirection, 1111111, playerMaskIndex))
            {
                // Playerı görüyoruz. Mesafe kontrolü yap
                if (Vector3.Distance(transform.position, Player.position) < 0.05f)
                {
                    MyBase.SetSpeed(0);
                }
            }
            else if (Physics.Raycast(new Ray(transform.position + Vector3.up * 0.5f, playerDirection), out RaycastHit hitInfo, 1111111, BoardMaskIndex))
            {
                // Playerı göremiyoruz. Son noktaya mesafe kontrolü yap
                if (playerDirection.x == 1)
                {
                    if (Vector3.Distance(transform.position, hitInfo.transform.position + Vector3.left) < 0.05f)
                    {
                        FindDirection2();
                    }
                }
                else if (playerDirection.x == -1)
                {
                    if (Vector3.Distance(transform.position, hitInfo.transform.position + Vector3.right) < 0.05f)
                    {
                        FindDirection2();
                    }
                }
                else if (playerDirection.z == 1)
                {
                    if (Vector3.Distance(transform.position, hitInfo.transform.position + Vector3.back) < 0.05f)
                    {
                        FindDirection2();
                    }
                }
                else if (playerDirection.z == -1)
                {
                    if (Vector3.Distance(transform.position, hitInfo.transform.position + Vector3.forward) < 0.05f)
                    {
                        FindDirection2();
                    }
                }
            }
        }
        else
        {
            randomDirectionTimeNext += Time.deltaTime;
            if (randomDirectionTimeNext > ChangeDirectionTime)
            {
                randomDirectionTimeNext = 0;
                FindDirection1();
            }
            if (Vector3.Distance(transform.position, MyDirections[RndDirec]) < 0.05f)
            {
                MyBase.SetDirection(FindRandomDirection() - MyBase.LearnIntDirection(transform.position));
            }
        }
        transform.Translate(MyBase.Direction * Time.deltaTime * MyBase.MySpeed);
    }
    private void FindDirection1()
    {
        playerDirection = FindPlayerDirection();
        if (playerDirection != Vector3Int.zero)
        {
            seePlayer = true;
            MyBase.SetDirection(playerDirection);
        }
        else
        {
            MyBase.SetDirection(FindRandomDirection() - MyBase.LearnIntDirection(transform.position));
        }
    }
    private void FindDirection2()
    {
        playerDirection = FindPlayerDirection();
        if (playerDirection != Vector3Int.zero)
        {
            MyBase.SetDirection(playerDirection);
        }
        else
        {
            seePlayer = false;
            MyBase.SetDirection(FindRandomDirection() - MyBase.LearnIntDirection(transform.position));
        }
    }
    private Vector3Int FindPlayerDirection()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.right, BoardWeight, playerMaskIndex))
        {
            if (transform.position.x < BoardWeight - 1)
            {
                return Vector3Int.right;
            }
        }
        // Sol bak
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.left, BoardWeight, playerMaskIndex))
        {
            if (transform.position.x > 0)
            {
                return Vector3Int.left;
            }
        }
        // İleri bak
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.forward, BoardHeight, playerMaskIndex))
        {
            if (transform.position.z < BoardHeight - 1)
            {
                return Vector3Int.forward;
            }
        }
        // Geri bak
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.back, BoardHeight, playerMaskIndex))
        {
            if (transform.position.z > 0)
            {
                return Vector3Int.back;
            }
        }
        return Vector3Int.zero;
    }
}