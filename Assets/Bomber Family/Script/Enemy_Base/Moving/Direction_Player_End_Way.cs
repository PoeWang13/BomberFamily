using UnityEngine;

public class Direction_Player_End_Way : Moving_Base
{
    // Playerı görürse playera kadar gider, player yoksa yolun sonuna kadar gider. Bulamazsa rastgele dolaşır.
    private int playerIndex;
    private bool playerSawed;
    private float randomDirectionTimeNext;
    private Vector3 playerDirection;

    public override void OnStart()
    {
        playerIndex = 1 << LayerMask.NameToLayer("Player");
        FindPlayer();
    }
    public override void Move()
    {
        if (Vector3.SqrMagnitude(transform.position + MyBase.Direction - Player.position) < 0.01f)
        {
            MyBase.StopMovingForXTime();
            return;
        }
        RaycastHit raycast;
        Ray ray = new(transform.position + Vector3.up * 0.5f, MyBase.Direction);
        if (Physics.Raycast(ray, out raycast, 1, BoardMaskIndex))
        {
            if (Vector3.SqrMagnitude(transform.position + MyBase.Direction - raycast.transform.position) < 0.01f)
            {
                MyBase.ResetSpeed();
                ChangeDirection();
            }
        }
        else
        {
            if (!playerSawed)
            {
                randomDirectionTimeNext += Time.deltaTime;
                if (randomDirectionTimeNext > ChangeDirectionTime)
                {
                    if (Vector3.SqrMagnitude(transform.position - MyBase.LearnIntDirection(transform.position)) < 0.01f)
                    {
                        randomDirectionTimeNext = 0;
                        ChangeDirection();
                    }
                }
            }
        }
    }
    private void ChangeDirection()
    {
        MyBase.SetIntPos();
        FindPlayer();
    }
    // Playerı görmeye çalışıyoruz
    private void FindPlayer()
    {
        playerSawed = false;
        playerDirection = FindPlayerDirection();
        if (playerDirection != Vector3.zero)
        {
            // Playerı gördük
            playerSawed = true;
            MyBase.DebuffMySpeed(5);
            MyBase.StopMovingForXTime();
            MyBase.SetDirection(playerDirection);
        }
        else
        {
            // Rastgele bir yön seç
            Vector3 direc = FindRandomDirection();
            if (direc == Vector3Int.zero)
            {
                return;
            }
            MyBase.SetDirection(direc);
        }
    }
    private Vector3 FindPlayerDirection()
    {
        // Sağa bak
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.right, BoardWeight, playerIndex))
        {
            return Vector3.right;
        }
        // Sol bak
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.left, BoardWeight, playerIndex))
        {
            return Vector3.left;
        }
        // İleri bak
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.forward, BoardHeight, playerIndex))
        {
            return Vector3.forward;
        }
        // Geri bak
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.back, BoardHeight, playerIndex))
        {
            return Vector3.back;
        }
        return Vector3.zero;
    }
}