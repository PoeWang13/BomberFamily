using UnityEngine;
using System.Collections.Generic;

public class Player_Base : Character_Base
{
    [ContextMenu("Center")]
    private void Center()
    {
        transform.position = new Vector3(5, 0, 5);
        gameObject.SetActive(false);
    }
    private static Player_Base instance;
    public static Player_Base Instance { get { return instance; } }

    [Header("Player Base")]
    [SerializeField] private Player_Source player_Source;

    private Joystick joystickMove;
    private Transform boardBombParent;
    private List<Bomb_Base> clocks = new List<Bomb_Base>();

    public Player_Source Player_Source { get { return player_Source; } }
    public int ClocksAmount { get { return clocks.Count; } }

    public void SetInstance()
    {
        instance = this;
    }
    public override void OnStart()
    {
        // Görünmez ve etkisiz yap
        boardBombParent = Utils.MakeChieldForGameElement("Board_Bomb");
        Game_Manager.Instance.OnGameStart += Instance_OnGameStart;
    }
    private void Instance_OnGameStart(object sender, System.EventArgs e)
    {
        SetMove(true);
        SetEffectivePlayer(true);
        SetPosition(Vector3.zero);

    }
    public void SetPlayerStat(Joystick joystick)
    {
        joystickMove = joystick;
        SetCharacterStat(Save_Load_Manager.Instance.gameData.allPlayers[Save_Load_Manager.Instance.gameData.playerOrder].playerStat);
        SetMyLife(CharacterStat.myLife);
        SetMySpeed(CharacterStat.mySpeed);
    }
    public override void Move()
    {
        SetDirectionPlayer(joystickMove.Direction());
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    // Bomb bırak
        //    Bomb_Base bomb = player_Source.MyBombPooler.HavuzdanObjeIste(transform.position).GetComponent<Bomb_Base>();
        //    bomb.SetBomb(this);
        //}
    }
    private void FixedUpdate()
    {
        if (IsDead)
        {
            return;
        }
        if (IsFreeze)
        {
            return;
        }
        if (!CanMove)
        {
            return;
        }
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (MyRigidbody.isKinematic)
        {
            return;
        }
        MyRigidbody.velocity = Direction * MySpeed * Time.deltaTime;
    }

    #region Genel
    public void SetEffectivePlayer(bool isEffective)
    {
        SetCharacterView(isEffective);
        MyRigidbody.isKinematic = !isEffective;
    }
    #endregion

    #region Taked Damage
    public override void TakedDamage(int damage)
    {
        Game_Manager.Instance.AddLoseLifeAmount(damage);
        // karakter titreyecek ve can düşme animasyonu yapacak
        Canvas_Manager.Instance.SetPlayerLifeText();
    }

    public override void Dead()
    {
        // karakter ölüm animi yapacak
        MyRigidbody.velocity = Vector3.zero;
        Canvas_Manager.Instance.GameLost();
    }
    #endregion

    #region Use Bomb
    public void UseSimpleBomb()
    {
        if (MyBombAmount > 0)
        {
            if (!CanCreateBomb())
            {
                return;
            }
            UseBomb();
            // Bomb bırak
            SetBomb();
            Canvas_Manager.Instance.SetPlayerBombAmountText();
        }
    }
    private void SetBomb()
    {
        Bomb_Base bomb = player_Source.MyBombPooler.HavuzdanObjeIste(LearnIntDirection(transform.position)).GetComponent<Bomb_Base>();
        bomb.SetBomb(this, CharacterStat.myBombPower, CharacterStat.myBombFireLimit, CharacterStat.myBombFireTime, false);
        bomb.SetBoardCoor(MyCoor);
        bomb.transform.SetParent(boardBombParent);
        Map_Holder.Instance.AllBombObjects.Add(bomb);
        Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y] = new GameBoard(BoardType.Bomb, bomb.MyBoardOrder, bomb.gameObject);
    }
    public void UseBombClockActiviter()
    {
        for (int e = 0; e < clocks.Count; e++)
        {
            clocks[e].Bombed();
        }
        clocks.Clear();
    }
    public void AddBombClock(Bomb_Base bomb)
    {
        clocks.Add(bomb);
    }
    #endregion

    #region Increase Stats
    public override void Increaselife()
    {
        base.Increaselife();
        Canvas_Manager.Instance.SetPlayerLifeText();
    }
    public override void IncreaseSpeed()
    {
        base.IncreaseSpeed();
        Canvas_Manager.Instance.SetPlayerSpeedText();
    }
    public override void IncreaseBombAmount()
    {
        IncreaseBomb();
        Canvas_Manager.Instance.SetPlayerBombAmountText();
    }
    public override void IncreaseBombFirePower()
    {
        base.IncreaseBombFirePower();
        Canvas_Manager.Instance.SetPlayerPowerText();
    }
    public override void IncreaseBombFireLimit()
    {
        base.IncreaseBombFireLimit();
        Canvas_Manager.Instance.SetPlayerBombFireLimitText();
    }
    public override void IncreaseBoxPassing()
    {
        base.IncreaseBoxPassing();
        Canvas_Manager.Instance.SetPlayerBoxPassingText();
    }
    public override void IncreasePushingTime()
    {
        base.IncreasePushingTime();
        Canvas_Manager.Instance.SetPlayerBoxPushingTimeText();
    }
    #endregion

    #region Use Special Bomb
    public override void UseSpecialBomb(int order)
    {
        if (Save_Load_Manager.Instance.gameData.allSpecialBomb[order].bombAmount > 0)
        {
            Save_Load_Manager.Instance.gameData.allSpecialBomb[order].bombAmount--;
            Canvas_Manager.Instance.SetBomb(order);
            base.UseSpecialBomb(order);
            Save_Load_Manager.Instance.SaveGame();
        }
    }
    #endregion
}