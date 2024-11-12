using DG.Tweening;
using UnityEngine;

public class Direction_Player_End_Way : Moving_Base
{
    // Playerı görürse playera kadar gider, player yoksa yolun sonuna kadar gider. Bulamazsa rastgele dolaşır.
    private int boardIndex;
    private bool findedPlayer;
    private Vector3 playerDirection;

    public override void OnStart()
    {
        boardIndex = 1 << LayerMask.NameToLayer("Board") | 1 << LayerMask.NameToLayer("Player");
    }
    public override void OnSet()
    {
        FindPlayer();
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
        if (!findedPlayer)
        {
            RaycastHit raycast;
            Ray rayRight = new(transform.position + Vector3.up * 0.5f, MyBase.Direction);
            // Sağa bak
            if (Physics.Raycast(rayRight, out raycast, 1, boardIndex))
            {
                if (raycast.transform.CompareTag("Player"))
                {
                    if (CheckDistance(raycast.transform.position, transform.position, 1))
                    {
                        raycast.transform.GetComponent<Player_Base>().TakeDamage(MyBase.MyItem.MyDamage);
                        StopAttackToPlayer();
                    }
                }
                else if (raycast.transform.CompareTag("Board"))
                {
                    CheckDistance(raycast.transform.position, transform.position + MyBase.Direction, 0.05f);
                }
            }
            MyBase.TurnPlayerView(MyBase.Direction);
            transform.Translate(MyBase.Direction * Time.deltaTime * MyBase.MySpeed);
        }
    }
    private bool CheckDistance(Vector3 pointStart, Vector3 pointEnd, float distance)
    {
        if (Vector3.Distance(pointStart, pointEnd) < distance)
        {
            StopEnemy();
            return true;
        }
        return false;
    }
    private void StopEnemy()
    {
        MyBase.SetMySpeed(0);
        DOVirtual.DelayedCall(1.0f, () =>
        {
            FindPlayer();
            MyBase.ResetSpeed();
        });
    }
    private void StopAttackToPlayer()
    {
        findedPlayer = true;
        DOVirtual.DelayedCall(5.0f, () =>
        {
            FindPlayer();
            findedPlayer = false;
        });
    }
    // Playerı görmeye çalışıyoruz
    private void FindPlayer()
    {
        transform.position = MyBase.SetPos();
        playerDirection = FindPlayerDirection();
        if (playerDirection != Vector3.zero)
        {
            MyBase.DebuffMySpeed(5);
            MyBase.SetDirection(playerDirection);
        }
        else
        {
            // bir yön seç
            Vector3 direc = FindRandomDirection();
            if (direc == Vector3Int.zero)
            {
                return;
            }
            MyBase.SetDirection(direc);
            UnityEditor.EditorApplication.isPaused = true;
        }
    }
    private Vector3 FindPlayerDirection()
    {
        RaycastHit raycast;
        Ray rayBack = new (transform.position + Vector3.up * 0.5f, Vector3.back);
        Ray rayLeft = new (transform.position + Vector3.up * 0.5f, Vector3.left);
        Ray rayRight = new (transform.position + Vector3.up * 0.5f, Vector3.right);
        Ray rayForward = new (transform.position + Vector3.up * 0.5f, Vector3.forward);

        // Sağa bak
        if (Physics.Raycast(rayRight, out raycast, BoardWeight, boardIndex))
        {
            if (raycast.transform.CompareTag("Player"))
            {
                return Vector3.right;
            }
        }
        // Sol bak
        if (Physics.Raycast(rayLeft, out raycast, BoardWeight, boardIndex))
        {
            if (raycast.transform.CompareTag("Player"))
            {
                return Vector3.left;
            }
        }
        // İleri bak
        if (Physics.Raycast(rayForward, out raycast, BoardHeight, boardIndex))
        {
            if (raycast.transform.CompareTag("Player"))
            {
                return Vector3.forward;
            }
        }
        // Geri bak
        if (Physics.Raycast(rayBack, out raycast, BoardHeight, boardIndex))
        {
            if (raycast.transform.CompareTag("Player"))
            {
                return Vector3.back;
            }
        }
        return Vector3.zero;
    }
}