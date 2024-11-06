using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public enum CurseType
{
    DecLife,
    DecPower,
    DecSimple,
    IncSpeed,
    DecSpeed,
    IncLimit,
    DecLimit,
}
[System.Serializable]
public class CurseClass
{
    public int orjValue;
    public float curseTime;
    public CurseType curseType;

    public CurseClass(int orjValue, float curseTime, int curseType)
    {
        this.orjValue = orjValue;
        this.curseTime = curseTime;
        this.curseType = (CurseType)curseType;
    }
}
public class Character_Base : PoolObje, IDamegable
{
    [Header("Character Base")]
    [SerializeField] private GameObject objFreeze;

    private int orjLife;
    private int orjSpeed;
    private int myLife;
    private int mySpeed;
    private float shieldTime;
    private bool canMove;
    private bool canHurt;
    private bool canCurse;
    private bool isDead;
    private bool isFreeze;
    private int bombFirePower = 1;
    private int bombFireLimit = 1;
    private int simpleBombAmount = 1;
    private Vector2Int myCoor;
    private Vector3 direction;
    private Collider myCollider;
    private Animator myAnimator;
    private Rigidbody myRigidbody;
    private Transform characterView;
    private List<CurseClass> allCurses = new List<CurseClass>();

    public void SetDirection(Vector3Int direc)
    {
        if (direc.x > 0)
        {
            direc.x = 1;
        }
        else if (direc.x < 0)
        {
            direc.x = -1;
        }
        else if (direc.z > 0)
        {
            direc.z = 1;
        }
        else if (direc.z < 0)
        {
            direc.z = -1;
        }
        TurnPlayerView(direc);
        direction = direc;
    }
    public Vector3Int LearnIntDirection(Vector3 vector)
    {
        return new Vector3Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
    }
    private void TurnPlayerView(Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            return;
        }
        Quaternion direc = Quaternion.LookRotation(direction);
        Vector3 rot = direc.eulerAngles;
        CharacterView.rotation = Quaternion.Euler(0, rot.y, 0);
    }
    public int MyLife { get { return myLife; } }
    public int MySpeed { get { return mySpeed; } }
    public bool CanMove { get { return canMove; } }
    public bool IsDead { get { return isDead; } }
    public bool IsFreeze { get { return isFreeze; } }
    public Vector3 Direction { get { return direction; } }
    public int BombFirePower { get { return bombFirePower; } }
    public int BombFireLimit { get { return bombFireLimit; } }
    public int SimpleBombAmount { get { return simpleBombAmount; } }
    public Rigidbody MyRigidbody { get { return myRigidbody; } }
    public Vector2Int MyCoor { get { return myCoor; } }
    public Animator MyAnimator { get { return myAnimator; } }
    public Collider MyCollider { get { return myCollider; } }
    public Transform CharacterView { get { return characterView; } }

    private void Awake()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponentInChildren<Animator>();
        characterView = transform.Find("CharacterView");
        OnAwake();
    }
    public virtual void OnAwake()
    {

    }
    private void Start()
    {
        Canvas_Manager.Instance.OnGameWin += Instance_OnGameWin;
        Canvas_Manager.Instance.OnGameLost += Instance_OnGameLost;
        OnStart();
    }
    public virtual void OnStart()
    {

    }
    public void SetCharacterView(bool isActive)
    {
        characterView.gameObject.SetActive(isActive);
    }
    private void Instance_OnGameLost(object sender, System.EventArgs e)
    {
        canMove = false;
    }
    private void Instance_OnGameWin(object sender, System.EventArgs e)
    {
        canMove = false;
    }
    public override void ObjeHavuzEnter()
    {
        ResetBase();
        base.ObjeHavuzEnter();
    }
    private void Update()
    {
        if (isDead)
        {
            return;
        }
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (canMove)
        {
            Move();
        }
        UseShield();
        UseCurse();
        SetMyCoor(new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)));
        MyAnimator.SetFloat("Speed", direction.sqrMagnitude * MySpeed);
    }
    public virtual void Move()
    {

    }

    #region Set Variable
    public void SetLife(int newLife)
    {
        orjLife = newLife;
        ResetLife();
    }
    public void SetSpeed(int newSpeed)
    {
        orjSpeed = newSpeed;
        ResetSpeed();
    }
    public void SetLife(float newLife)
    {
        myLife = (int)(orjLife * newLife);
    }
    public void SetSpeed(float newSpeed)
    {
        mySpeed = (int)(orjSpeed * newSpeed);
    }
    public void ResetLife()
    {
        myLife = orjLife;
    }
    public void ResetSpeed()
    {
        mySpeed = orjSpeed;
    }
    public void SetScale(bool isBig)
    {
        transform.DOScale(isBig ? Vector3.one : Vector3.zero, 0.5f);
    }
    public void SetPosition(Vector3 newPos)
    {
        transform.position = newPos;
    }
    public void SetMove(bool move)
    {
        canMove = move;
    }
    public void SetMyCoor(Vector2Int coor)
    {
        myCoor = coor;
    }
    public virtual void ResetBase()
    {
        shieldTime = 0;
        canCurse = false;
        canMove = true;
        isDead = false;
        myLife = orjLife;
        mySpeed = orjSpeed;
        allCurses.Clear();
    }
    #endregion

    #region TakeDamage
    public void TakeDamage(int damage)
    {
        if (canHurt)
        {
            return;
        }
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        myLife -= damage;
        TakedDamage(damage);
        if (myLife <= 0)
        {
            isDead = true;
            Dead();
        }
    }
    public virtual void TakedDamage(int damage)
    {
    }
    public virtual void Dead()
    {
    }
    #endregion

    #region Freeze
    public void Freeze(bool freeze)
    {
        isFreeze = freeze;
        if (isFreeze)
        {
            // Hız düşür
            mySpeed = 0;
            myRigidbody.velocity = Vector3.zero;
            // 3 saniye sonra freeze bozulsun.
            DOTween.To(value => { }, startValue: 0, endValue: 1, duration: 3.0f).SetEase(Ease.Linear).
                    OnComplete(() =>
                    {
                        Freeze(false);
                    });
        }
        else
        {
            mySpeed = orjSpeed;
        }
        objFreeze.SetActive(isFreeze);
    }
    #endregion

    #region Shield
    public void UseShield()
    {
        if (canHurt)
        {
            shieldTime -= Time.deltaTime;
            if (shieldTime < 0)
            {
                canHurt = false;
            }
        }
    }
    #endregion

    #region Increase Stats
    public void IncreaseShieldTime()
    {
        shieldTime += 3;
        canHurt = true;
    }
    public void UseSimpleBomb()
    {
        simpleBombAmount--;
    }
    public virtual void Increaselife()
    {
        myLife++;
        bool finded = false;
        for (int e = 0; e < allCurses.Count && !finded; e++)
        {
            if (allCurses[e].curseType == CurseType.DecLife)
            {
                allCurses[e].orjValue++;
                finded = true;
            }
        }
    }
    public virtual void IncreaseSpeed()
    {
        mySpeed += 10;
        bool finded = false;
        for (int e = 0; e < allCurses.Count && !finded; e++)
        {
            if (allCurses[e].curseType == CurseType.IncSpeed || allCurses[e].curseType == CurseType.DecSpeed)
            {
                allCurses[e].orjValue++;
                finded = true;
            }
        }
    }
    public virtual void IncreaseBombFirePower()
    {
        bombFirePower++;
        bool finded = false;
        for (int e = 0; e < allCurses.Count && !finded; e++)
        {
            if (allCurses[e].curseType == CurseType.DecPower)
            {
                allCurses[e].orjValue++;
                finded = true;
            }
        }
    }
    public virtual void IncreaseBombFireLimit()
    {
        bombFireLimit++;
        bool finded = false;
        for (int e = 0; e < allCurses.Count && !finded; e++)
        {
            if (allCurses[e].curseType == CurseType.IncLimit || allCurses[e].curseType == CurseType.DecLimit)
            {
                allCurses[e].orjValue++;
                finded = true;
            }
        }
    }
    public virtual void IncreaseSimpleBombAmount()
    {
        simpleBombAmount++;
        bool finded = false;
        for (int e = 0; e < allCurses.Count && !finded; e++)
        {
            if (allCurses[e].curseType == CurseType.DecSimple)
            {
                allCurses[e].orjValue++;
                finded = true;
            }
        }
    }
    public virtual void IncreaseClockBombAmount()
    {
    }
    public virtual void IncreaseNukleerBombAmount()
    {
    }
    public virtual void IncreaseAreaBombAmount()
    {
    }
    public virtual void IncreaseAntiWallBombAmount()
    {
    }
    public virtual void IncreaseSearcherBombAmount()
    {
    }
    #endregion

    public bool CanCreateBomb()
    {
        // Bir obje var mı veya active değil mi
        if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Object is null || !Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Object.activeSelf)
        {
            return true;
        }
        return false;
    }

    #region Curse
    public void UseCurse()
    {
        if (canCurse)
        {
            for (int e = allCurses.Count - 1; e >= 0; e--)
            {
                allCurses[e].curseTime -= Time.deltaTime;
                if (allCurses[e].curseTime <= 0)
                {
                    if (allCurses[e].curseType == CurseType.DecLife)
                    {
                        myLife = allCurses[e].orjValue;
                        Canvas_Manager.Instance.SetPlayerLifeText();
                    }
                    else if (allCurses[e].curseType == CurseType.DecPower)
                    {
                        bombFirePower = allCurses[e].orjValue;
                        Canvas_Manager.Instance.SetPlayerPowerText();
                    }
                    else if (allCurses[e].curseType == CurseType.DecSimple)
                    {
                        simpleBombAmount = allCurses[e].orjValue;
                        Canvas_Manager.Instance.SetPlayerSimpleBombAmountText();
                    }
                    else if (allCurses[e].curseType == CurseType.IncSpeed || allCurses[e].curseType == CurseType.DecSpeed)
                    {
                        mySpeed = allCurses[e].orjValue;
                        Canvas_Manager.Instance.SetPlayerSpeedText();
                    }
                    else if (allCurses[e].curseType == CurseType.IncLimit || allCurses[e].curseType == CurseType.DecLimit)
                    {
                        bombFireLimit = allCurses[e].orjValue;
                        Canvas_Manager.Instance.SetPlayerBombFireLimitText();
                    }
                    allCurses.RemoveAt(e);
                }
            }
            if (allCurses.Count == 0)
            {
                canCurse = false;
            }
        }
    }
    public void GiveCurse()
    {
        canCurse = true;
        bool finded = false;
        int rndCurse = Random.Range(0, System.Enum.GetValues(typeof(CurseType)).Length);
        for (int e = 0; e < allCurses.Count && !finded; e++)
        {
            if (allCurses[e].curseType == (CurseType)rndCurse)
            {
                allCurses[e].curseTime += 5;
                finded = true;
            }
        }
        if (!finded)
        {
            if ((CurseType)rndCurse == CurseType.DecLife)
            {
                allCurses.Add(new CurseClass(myLife, 5, rndCurse));
                myLife = 1;
                Canvas_Manager.Instance.SetPlayerLifeText();
            }
            else if ((CurseType)rndCurse == CurseType.DecPower)
            {
                allCurses.Add(new CurseClass(bombFirePower, 5, rndCurse));
                bombFirePower = 1;
                Canvas_Manager.Instance.SetPlayerPowerText();
            }
            else if ((CurseType)rndCurse == CurseType.DecSimple)
            {
                allCurses.Add(new CurseClass(simpleBombAmount, 5, rndCurse));
                simpleBombAmount = 1;
                Canvas_Manager.Instance.SetPlayerSimpleBombAmountText();
            }
            else if ((CurseType)rndCurse == CurseType.IncSpeed)
            {
                allCurses.Add(new CurseClass(mySpeed, 5, rndCurse));
                mySpeed = 1500;
                Canvas_Manager.Instance.SetPlayerSpeedText();
            }
            else if ((CurseType)rndCurse == CurseType.DecSpeed)
            {
                allCurses.Add(new CurseClass(mySpeed, 5, rndCurse));
                mySpeed = 50;
                Canvas_Manager.Instance.SetPlayerSpeedText();
            }
            else if ((CurseType)rndCurse == CurseType.IncLimit)
            {
                allCurses.Add(new CurseClass(bombFireLimit, 5, rndCurse));
                bombFireLimit = 1000;
                Canvas_Manager.Instance.SetPlayerBombFireLimitText();
            }
            else if ((CurseType)rndCurse == CurseType.DecLimit)
            {
                allCurses.Add(new CurseClass(bombFireLimit, 5, rndCurse));
                bombFireLimit = 1;
                Canvas_Manager.Instance.SetPlayerBombFireLimitText();
            }
        }
    }
    #endregion
}