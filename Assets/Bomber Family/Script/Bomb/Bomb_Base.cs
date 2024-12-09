using UnityEngine;

public enum BombType
{
    Area = 0,
    Anti = 1,
    Clock = 2,
    Nuclear = 3,
    Searcher = 4,
    Elektro = 5,
    Lav = 6,
    Buz = 7,
    Sis = 8,
    Zehir = 9,
}
public class Bomb_Base : Board_Object
{
    [SerializeField] private Pooler bombFirePool;
    [SerializeField] private float bombExplodedTime = 1;

    private bool useTimeForExplode;
    private bool isExploded;
    private float bombExplodedTimeNext;
    private int bombPower;
    private int bombLimit;
    private float bombFireTime;
    private Character_Base myOwner;

    public int BombPower { get { return bombPower; } }
    public int BombLimit { get { return bombLimit; } }
    public float BombFireTime { get { return bombFireTime; } }
    public bool IsExploded { get { return isExploded; } }
    public Character_Base MyOwner { get { return myOwner; } }
    public Pooler BombFirePool { get { return bombFirePool; } }

    private void Awake()
    {
        Game_Manager.Instance.OnGameFinish += Instance_OnGameFinish;
    }
    private void Instance_OnGameFinish(object sender, System.EventArgs e)
    {
        EnterHavuz();
    }
    public void SetBomb(Character_Base character_Base, int power, int limit, float fireTime, bool isUseTime = false)
    {
        isExploded = false;
        bombExplodedTimeNext = 0;
        myOwner = character_Base;
        bombPower = power;
        bombLimit = limit;
        bombFireTime = fireTime;
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
                bombExplodedTimeNext += Time.deltaTime;
                if (bombExplodedTimeNext > bombExplodedTime)
                {
                    bombExplodedTimeNext = 0;
                    Bombed();
                }
            }
        }
    }
    public virtual void Bombed()
    {
    }
}