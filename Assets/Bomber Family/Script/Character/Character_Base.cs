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
public class Character_Base : Board_Object, IDamegable
{
    [SerializeField] private CharacterStat characterStat;

    private int myLife;
    private int mySpeed;
    private int myBombAmount = 1;
    private int myBombPower = 1;
    private int myBombFireLimit = 1;
    private int myBombBoxPassing ;
    private float myBombPushingTime;
    private float shieldTime;
    private float shieldAngle;
    private bool canMove;
    private bool canProtect;
    private bool canCurse;
    private bool isDead;
    private bool isFreeze;
    private Vector3 direction;
    private GameObject objFreeze;
    private Animator myAnimator;
    private Rigidbody myRigidbody;
    private Transform objShield;
    private Transform characterView;
    private List<CurseClass> allCurses = new List<CurseClass>();

    public int MyLife { get { return myLife; } }
    public int MySpeed { get { return mySpeed; } }
    public int MyBombAmount { get { return myBombAmount; } }
    public int MyBombPower { get { return myBombPower; } }
    public int MyBombFireLimit { get { return myBombFireLimit; } }
    public int MyBombBoxPassing { get { return myBombBoxPassing; } }
    public float MyBombPushingTime { get { return myBombPushingTime; } }
    public bool CanMove { get { return canMove; } }
    public bool IsDead { get { return isDead; } }
    public bool IsFreeze { get { return isFreeze; } }
    public bool HasCurse { get { return allCurses.Count > 0; } }
    public Vector3 Direction { get { return direction; } }
    public CharacterStat CharacterStat { get { return characterStat; } }
    public Rigidbody MyRigidbody { get { return myRigidbody; } }
    public Animator MyAnimator { get { return myAnimator; } }
    public Transform CharacterView { get { return characterView; } }

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponentInChildren<Animator>();
        characterView = transform.Find("CharacterView");
        objFreeze = characterView.Find("Freeze").gameObject;
        objShield = characterView.Find("Shield");
        Game_Manager.Instance.OnGameStart += Instance_OnGameStart;
        OnAwake();
    }
    public virtual void OnAwake()
    {

    }
    public override void OnStart()
    {
    }
    private void Instance_OnGameStart(object sender, System.EventArgs e)
    {
        canMove = true;
        SetScale(true);
    }
    public void SetCharacterStat(CharacterStat stat)
    {
        characterStat = stat;
    }
    public void SetCharacterView(bool isActive)
    {
        characterView.gameObject.SetActive(isActive);
    }
    public override void OnGameLost()
    {
        canMove = false;
    }
    public override void OnGameWin()
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
        if (canProtect)
        {
            shieldAngle += Time.deltaTime;
            objShield.eulerAngles = new Vector3(0, shieldAngle, 0);
        }
        UseShield();
        UseCurseTime();
        SetBoardCoor(new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)));
        myAnimator.SetFloat("Speed", direction.sqrMagnitude * MySpeed);
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
    public void SetIntPos()
    {
        transform.position = new Vector3Int(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z));
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
        myBombPower = characterStat.myBombPower;
    }
    public void ResetLimit()
    {
        myBombFireLimit = characterStat.myBombFireLimit;
    }
    public void ResetAmount()
    {
        myBombAmount = characterStat.myBombAmount;
    }
    public void ResetBoxPassing()
    {
        myBombBoxPassing = characterStat.myBombBoxPassing;
    }
    public void ResetPushingTime()
    {
        myBombPushingTime = characterStat.myBombPushingTime;
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
    public void StopMovingForXTime(float stoppingTime = 1)
    {
        canMove = false;
        Vector3 endPos = LearnIntDirection(transform.position);
        transform.DOMove(endPos, stoppingTime * 0.5f);
        DOVirtual.DelayedCall(stoppingTime, () =>
        {
            canMove = true;
        });
    }
    public virtual void ResetBase()
    {
        shieldTime = 0;
        canCurse = false;
        canMove = true;
        isDead = false;
        isFreeze = false;
        Freeze(false);
        ResetLife();
        ResetSpeed();
        ResetAmount();
        ResetPower();
        ResetLimit();
        ResetBoxPassing();
        ResetPushingTime();
        allCurses.Clear();
        myAnimator.SetBool("Dead", false);
    }
    public bool CanCreateBomb()
    {
        // Bir obje var mı veya active değil mi
        if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardType == BoardType.NonUseable)
        {
            return true;
        }
        if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardType == BoardType.Checked)
        {
            return true;
        }
        if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardType == BoardType.Empty)
        {
            return true;
        }
        if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardType == BoardType.Enemy || Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardType == BoardType.BossEnemy)
        {
            return true;
        }
        if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardType == BoardType.Player || Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardType == BoardType.Npc)
        {
            return true;
        }
        if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardType == BoardType.Box && !Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Object.activeSelf)
        {
            return true;
        }
        if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Game.boardType == BoardType.Bomb && !Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y].board_Object.activeSelf)
        {
            return true;
        }
        return false;
    }

    #endregion

    #region TakeDamage
    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }
        if (canProtect)
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
            MyAnimator.SetBool("Dead", true);
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
    public void IgnoreCollider(Collider collider)
    {
        Physics.IgnoreCollision(MyNotTriggeredCollider, collider);
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
            // 3 saniye sonra freeze bozulsun
            DOVirtual.DelayedCall(2.5f, () =>
            {
                objFreeze.transform.DOScale(Vector3.zero, 0.5f).
                    OnComplete(() =>
                    {
                        Freeze(false);
                        objFreeze.transform.localScale = Vector3.one * 1.5f;
                    });
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
        if (canProtect)
        {
            shieldTime -= Time.deltaTime;
            if (shieldTime < 0)
            {
                shieldAngle = 0;
                canProtect = false;
            }
        }
    }
    #endregion

    #region Use Special Bomb
    public virtual void UseSpecialBomb(int order)
    {
        Game_Manager.Instance.UseSpecialBomb(this, order);
    }
    #endregion

    #region Increase Stats
    public void IncreaseShieldTime()
    {
        shieldTime += 3;
        canProtect = true;
    }
    public void UseBomb()
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
        myBombPower++;
    }
    public virtual void IncreaseBombFireLimit()
    {
        myBombFireLimit++;
    }
    public virtual void IncreaseBoxPassing()
    {
        myBombBoxPassing++;
    }
    public virtual void IncreasePushingTime()
    {
        myBombPushingTime -= 0.1f;
    }
    public virtual void IncreaseBombAmount()
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
                myBombPower = 1;
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