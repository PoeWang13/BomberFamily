using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public enum CurseType
{
    DecLife,
    DecPower,
    DecBomb,
    IncSpeed,
    DecSpeed,
    IncLimit,
    DecLimit,
}
[System.Serializable]
public class CurseClass
{
    public float curseTime;
    public CurseType curseType;

    public CurseClass(float curseTime, int curseType)
    {
        this.curseTime = curseTime;
        this.curseType = (CurseType)curseType;
    }
}
public class Character_Base : PoolObje, IDamegable
{
    [SerializeField] private CharacterStat characterStat;

    private int myLife;
    private int mySpeed;
    private int myBombAmount = 1;
    private int myBombFirePower = 1;
    private int myBombFireLimit = 1;
    private float shieldTime;
    private bool canMove;
    private bool canHurt;
    private bool canCurse;
    private bool isDead;
    private bool isFreeze;
    private Vector2Int myCoor;
    private Vector3 direction;
    private GameObject objFreeze;
    private Collider myCollider;
    private Animator myAnimator;
    private Rigidbody myRigidbody;
    private Transform characterView;
    private List<CurseClass> allCurses = new List<CurseClass>();

    public int MyLife { get { return myLife; } }
    public int MySpeed { get { return mySpeed; } }
    public int MyBombAmount { get { return myBombAmount; } }
    public int MyBombFirePower { get { return myBombFirePower; } }
    public int MyBombFireLimit { get { return myBombFireLimit; } }
    public bool CanMove { get { return canMove; } }
    public bool IsDead { get { return isDead; } }
    public bool IsFreeze { get { return isFreeze; } }
    public Vector3 Direction { get { return direction; } }
    public CharacterStat CharacterStat { get { return characterStat; } }
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
        objFreeze = characterView.Find("Freeze").gameObject;
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
    public void SetCharacterStat(CharacterStat stat)
    {
        characterStat = stat;
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
        UseCurseTime();
        SetMyCoor(new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)));
        MyAnimator.SetFloat("Speed", direction.sqrMagnitude * MySpeed);
    }
    public virtual void Move()
    {

    }

    public void SetDirection(Vector3 direc)
    {
        direction = direc;
    }
    public void SetDirectionPlayer(Vector3 direc)
    {
        Vector3 dir = Vector3.zero;
        if (direc.x > 0)
        {
            if (direc.z > 0)
            {
                if (direc.x > direc.z)
                {
                    dir.x = 1;
                }
                else
                {
                    dir.z = 1;
                }
            }
            else if (direc.z < 0)
            {
                if (direc.x > Mathf.Abs(direc.z))
                {
                    dir.x = 1;
                }
                else
                {
                    dir.z = -1;
                }
            }
            else
            {
                dir.x = 1;
            }
        }
        else if (direc.x < 0)
        {
            if (direc.z > 0)
            {
                if (Mathf.Abs(direc.x) > direc.z)
                {
                    dir.x = -1;
                }
                else
                {
                    dir.z = 1;
                }
            }
            else if (direc.z < 0)
            {
                if (Mathf.Abs(direc.x) > Mathf.Abs(direc.z))
                {
                    dir.x = -1;
                }
                else
                {
                    dir.z = -1;
                }
            }
            else
            {
                dir.x = -1;
            }
        }
        else
        {
            if (direc.z > 0)
            {
                dir.z = 1;
            }
            else if (direc.z < 0)
            {
                dir.z = -1;
            }
        }
        TurnPlayerView(dir);
        direction = dir;
    }
    public Vector3 LearnDirection(Vector3 vector)
    {
        return new Vector3Int(Mathf.RoundToInt(vector.x), 0, Mathf.RoundToInt(vector.z));
    }
    public Vector3Int LearnIntDirection(Vector3 vector)
    {
        return new Vector3Int(Mathf.RoundToInt(vector.x), 0, Mathf.RoundToInt(vector.z));
    }
    public Vector3Int SetPos()
    {
        return new Vector3Int(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z));
    }
    public void TurnPlayerView(Vector3 dir)
    {
        if (dir == Vector3.zero)
        {
            return;
        }
        Quaternion direc = Quaternion.LookRotation(dir);
        Vector3 rot = direc.eulerAngles;
        CharacterView.rotation = Quaternion.Euler(0, rot.y, 0);
    }

    #region Set Variable
    public void SetMyLife(int newLife)
    {
        myLife = newLife;
    }
    public void SetMySpeed(int newSpeed)
    {
        mySpeed = newSpeed;
    }
    public void DebuffMyLife(float newLife)
    {
        myLife = (int)(characterStat.myLife * newLife);
    }
    public void DebuffMySpeed(float newSpeed)
    {
        mySpeed = (int)(characterStat.mySpeed * newSpeed);
    }
    public void ResetLife()
    {
        myLife = characterStat.myLife;
    }
    public void ResetSpeed()
    {
        mySpeed = characterStat.mySpeed;
    }
    public void ResetPower()
    {
        myBombFirePower = characterStat.myBombPower;
    }
    public void ResetLimit()
    {
        myBombFireLimit = characterStat.myBombFireLimit;
    }
    public void ResetAmount()
    {
        myBombAmount = characterStat.myBombAmount;
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
        ResetLife();
        ResetSpeed();
        allCurses.Clear();
    }
    public bool CanCreateBomb()
    {
        // Bir obje var mı veya active değil mi
        if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Object is null || !Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Object.activeSelf)
        {
            return true;
        }
        return false;
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
            mySpeed = characterStat.mySpeed;
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
        myBombAmount--;
    }
    public virtual void Increaselife()
    {
        myLife++;
    }
    public virtual void IncreaseSpeed()
    {
        mySpeed += 10;
    }
    public virtual void IncreaseBombFirePower()
    {
        myBombFirePower++;
    }
    public virtual void IncreaseBombFireLimit()
    {
        myBombFireLimit++;
    }
    public virtual void IncreaseBombAmount(BombType bombType)
    {
    }
    public void IncreaseBomb()
    {
        myBombAmount++;
    }
    #endregion

    #region Curse
    private void UseCurseTime()
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
                        ResetLife();
                        Canvas_Manager.Instance.SetPlayerLifeText();
                    }
                    else if (allCurses[e].curseType == CurseType.DecPower)
                    {
                        ResetPower();
                        Canvas_Manager.Instance.SetPlayerPowerText();
                    }
                    else if (allCurses[e].curseType == CurseType.DecBomb)
                    {
                        int amount = 1 - myBombAmount;
                        ResetAmount();
                        myBombAmount -= amount;
                        Canvas_Manager.Instance.SetPlayerBombAmountText();
                    }
                    else if (allCurses[e].curseType == CurseType.IncSpeed || allCurses[e].curseType == CurseType.DecSpeed)
                    {
                        ResetSpeed();
                        Canvas_Manager.Instance.SetPlayerSpeedText();
                    }
                    else if (allCurses[e].curseType == CurseType.IncLimit || allCurses[e].curseType == CurseType.DecLimit)
                    {
                        ResetLimit();
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
                allCurses.Add(new CurseClass(5, rndCurse));
                myLife = 1;
                Canvas_Manager.Instance.SetPlayerLifeText();
            }
            else if ((CurseType)rndCurse == CurseType.DecPower)
            {
                allCurses.Add(new CurseClass(5, rndCurse));
                myBombFirePower = 1;
                Canvas_Manager.Instance.SetPlayerPowerText();
            }
            else if ((CurseType)rndCurse == CurseType.DecBomb)
            {
                allCurses.Add(new CurseClass(5, rndCurse));
                myBombAmount = 1;
                Canvas_Manager.Instance.SetPlayerBombAmountText();
            }
            else if ((CurseType)rndCurse == CurseType.IncSpeed)
            {
                allCurses.Add(new CurseClass(5, rndCurse));
                mySpeed = 1500;
                Canvas_Manager.Instance.SetPlayerSpeedText();
            }
            else if ((CurseType)rndCurse == CurseType.DecSpeed)
            {
                allCurses.Add(new CurseClass(5, rndCurse));
                mySpeed = 50;
                Canvas_Manager.Instance.SetPlayerSpeedText();
            }
            else if ((CurseType)rndCurse == CurseType.IncLimit)
            {
                allCurses.Add(new CurseClass(5, rndCurse));
                myBombFireLimit = 1000;
                Canvas_Manager.Instance.SetPlayerBombFireLimitText();
            }
            else if ((CurseType)rndCurse == CurseType.DecLimit)
            {
                allCurses.Add(new CurseClass(5, rndCurse));
                myBombFireLimit = 1;
                Canvas_Manager.Instance.SetPlayerBombFireLimitText();
            }
        }
    }
    #endregion
}