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

    public void SetInstance()
    {
        instance = this;
    }
    public override void OnStart()
    {
        // Görünmez ve etkisiz yap
        //SetEffectivePlayer(false);
        boardBombParent = Utils.MakeChieldForGameElement("Board_Bomb");
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
        if (Input.GetKey(KeyCode.Space))
        {
            // Bomb bırak
            Bomb_Base bomb = player_Source.MyBombPooler.HavuzdanObjeIste(transform.position).GetComponent<Bomb_Base>();
            bomb.SetBomb(this);
        }
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
    public override void ResetBase()
    {

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
    public void UseBomb()
    {
        if (MyBombAmount > 0)
        {
            if (!CanCreateBomb())
            {
                return;
            }
            UseSimpleBomb();
            // Bomb bırak
            Bomb_Base bomb = player_Source.MyBombPooler.HavuzdanObjeIste(transform.position).GetComponent<Bomb_Base>();
            SetBomb(bomb);
            Canvas_Manager.Instance.SetPlayerBombAmountText();
        }
    }
    private void SetBomb(Bomb_Base bomb, bool isSearcher = false)
    {
        bomb.SetBomb(this, isSearcher);
        bomb.SetBoardCoor(MyCoor);
        bomb.transform.SetParent(boardBombParent);
        Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y] = new GameBoard(BoardType.Bomb, bomb.BoardOrder, bomb.gameObject);
    }
    public void UseBombClockActiviter()
    {
        for (int e = 0; e < clocks.Count; e++)
        {
            clocks[e].Bombed();
        }
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
    public override void IncreaseBombAmount(BombType bombType)
    {
        if (player_Source.MyBombType == bombType)
        {
            IncreaseBomb();
        }
        Canvas_Manager.Instance.SetPlayerBombAmountText();
    }
    #endregion
}