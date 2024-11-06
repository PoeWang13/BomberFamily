using UnityEngine;

public class Bomb_Base : Board_Object
{
    [SerializeField] private Pooler bombFirePool;
    [SerializeField] private float bombFireTime = 1;

    private bool useTimeForExplode;
    private bool isExploded;
    private float bombFireTimeNext;
    private Player_Base myOwner;

    public bool IsExploded { get { return isExploded; } }
    public Player_Base MyOwner { get { return myOwner; } }
    public Pooler BombFirePool { get { return bombFirePool; } }

    public void SetBomb(Player_Base player_Base, bool isUseTime = false)
    {
        isExploded = false;
        bombFireTimeNext = 0;
        myOwner = player_Base;
        useTimeForExplode = isUseTime;
        Physics.IgnoreCollision(GetComponent<Collider>(), player_Base.MyCollider);
    }
    public void SetExploded()
    {
        isExploded = true;
    }
    private void Update()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (!isExploded)
        {
            if (!useTimeForExplode)
            {
                bombFireTimeNext += Time.deltaTime;
                if (bombFireTimeNext > bombFireTime)
                {
                    bombFireTimeNext = 0;
                    Bombed();
                }
            }
        }
    }
    public virtual void Bombed()
    {
    }
}