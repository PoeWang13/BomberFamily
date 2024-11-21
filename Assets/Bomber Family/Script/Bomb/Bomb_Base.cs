using UnityEngine;

public enum BombType
{
    Simple = 0,
    Area = 1,
    Anti = 2,
    Clock = 3,
    Nucleer = 4,
    Searcher = 5,
    Elektro = 6,
    Lav = 7,
    Buz = 8,
    Sis = 9,
    Zehir = 10,
}
public class Bomb_Base : Board_Object
{
    [SerializeField] private Pooler bombFirePool;
    [SerializeField] private float bombFireTime = 1;

    private bool useTimeForExplode;
    private bool isExploded;
    private float bombFireTimeNext;
    private Character_Base myOwner;

    public bool IsExploded { get { return isExploded; } }
    public Character_Base MyOwner { get { return myOwner; } }
    public Pooler BombFirePool { get { return bombFirePool; } }

    public void SetBomb(Character_Base character_Base, bool isUseTime = false)
    {
        isExploded = false;
        bombFireTimeNext = 0;
        myOwner = character_Base;
        useTimeForExplode = isUseTime;
    }
    public void SetExploded()
    {
        Audio_Manager.Instance.PlayGamePatlama();
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