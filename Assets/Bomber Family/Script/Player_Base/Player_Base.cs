using UnityEngine;
using System.Collections.Generic;

public class Player_Base : Character_Base
{
    private static Player_Base instance;
    public static Player_Base Instance { get { return instance; } }

    [Header("Player Base")]
    [SerializeField] private Joystick joystickMove;
    [SerializeField] private Item_Character myItem;

    private Transform boardBombParent;
    private List<Bomb_Base> clocks = new List<Bomb_Base>();

    public override void OnAwake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public override void OnStart()
    {
        SetLife(Save_Load_Manager.Instance.gameData.life);
        SetSpeed(Save_Load_Manager.Instance.gameData.speed);
        // Görünmez ve etkisiz yap
        SetEffectivePlayer(false);
        boardBombParent = Utils.MakeChieldForGameElement("Board_Bomb");
    }
    public override void Move()
    {
        SetDirection(LearnIntDirection(joystickMove.Direction()));
        if (Input.GetKey(KeyCode.Space))
        {
            // Bomb bırak
            Bomb_Base bomb = myItem.MyBombSimple.HavuzdanObjeIste(transform.position).GetComponent<Bomb_Base>();
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
    public void UseBombSimple()
    {
        if (SimpleBombAmount > 0)
        {
            if (!CanCreateBomb())
            {
                return;
            }
            Debug.Log("Use Bomb Simple");
            UseSimpleBomb();
            // Bomb bırak
            Bomb_Base bomb = myItem.MyBombSimple.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y)).GetComponent<Bomb_Simple>();
            SetBomb(bomb);
            Canvas_Manager.Instance.SetPlayerSimpleBombAmountText();
        }
    }
    public void UseBombClock()
    {
        if (Save_Load_Manager.Instance.gameData.clockBombAmount > 0)
        {
            if (!CanCreateBomb())
            {
                return;
            }
            // Bomb bırak
            Bomb_Base bomb = myItem.MyBombClock.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y)).GetComponent<Bomb_Simple>();
            SetBomb(bomb, true);
            clocks.Add(bomb);
            Save_Load_Manager.Instance.gameData.clockBombAmount--;
            Canvas_Manager.Instance.SetPlayerClockBombAmountText();
        }
    }
    public void UseBombNucleer()
    {
        if (Save_Load_Manager.Instance.gameData.nukleerBombAmount > 0)
        {
            if (!CanCreateBomb())
            {
                return;
            }
            // Bomb bırak
            Bomb_Base bomb = myItem.MyBombNucleer.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y)).GetComponent<Bomb_Nukleer>();
            SetBomb(bomb);
            Save_Load_Manager.Instance.gameData.nukleerBombAmount--;
            Canvas_Manager.Instance.SetPlayerNucleerBombAmountText();
        }
    }
    public void UseBombArea()
    {
        if (Save_Load_Manager.Instance.gameData.areaBombAmount > 0)
        {
            if (!CanCreateBomb())
            {
                return;
            }
            // Bomb bırak
            Bomb_Base bomb = myItem.MyBombArea.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y)).GetComponent<Bomb_Area>();
            SetBomb(bomb);
            Save_Load_Manager.Instance.gameData.areaBombAmount--;
            Canvas_Manager.Instance.SetPlayerAreaBombAmountText();
        }
    }
    public void UseBombAntiWall()
    {
        if (Save_Load_Manager.Instance.gameData.antiWallBombAmount > 0)
        {
            if (!CanCreateBomb())
            {
                return;
            }
            // Bomb bırak
            Bomb_Base bomb = myItem.MyBombAnti_Wall.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y)).GetComponent<Bomb_Anti_Wall>();
            SetBomb(bomb);
            Save_Load_Manager.Instance.gameData.antiWallBombAmount--;
            Canvas_Manager.Instance.SetPlayerAntiWallBombAmountText();
        }
    }
    public void UseBombSearcher()
    {
        if (Save_Load_Manager.Instance.gameData.searcherBombAmount > 0)
        {
            if (!CanCreateBomb())
            {
                return;
            }
            // Bomb bırak
            Bomb_Base bomb = myItem.MyBombSearcher.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y)).GetComponent<Bomb_Searcher>();
            SetBomb(bomb, true);
            Save_Load_Manager.Instance.gameData.searcherBombAmount--;
            Canvas_Manager.Instance.SetPlayerSearcherBombAmountText();
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
    public override void IncreaseSimpleBombAmount()
    {
        base.IncreaseSimpleBombAmount();
        Canvas_Manager.Instance.SetPlayerSimpleBombAmountText();
    }
    public override void IncreaseClockBombAmount()
    {
        if (Save_Load_Manager.Instance.gameData.clockBombAmount == 0)
        {
            Canvas_Manager.Instance.AddBombRect(BombType.Clock);
        }
        Save_Load_Manager.Instance.gameData.clockBombAmount++;
        Canvas_Manager.Instance.SetPlayerClockBombAmountText();
    }
    public override void IncreaseNukleerBombAmount()
    {
        if (Save_Load_Manager.Instance.gameData.nukleerBombAmount == 0)
        {
            Canvas_Manager.Instance.AddBombRect(BombType.Nucleer);
        }
        Save_Load_Manager.Instance.gameData.nukleerBombAmount++;
        Canvas_Manager.Instance.SetPlayerNucleerBombAmountText();
    }
    public override void IncreaseAreaBombAmount()
    {
        if (Save_Load_Manager.Instance.gameData.areaBombAmount == 0)
        {
            Canvas_Manager.Instance.AddBombRect(BombType.Area);
        }
        Save_Load_Manager.Instance.gameData.areaBombAmount++;
        Canvas_Manager.Instance.SetPlayerAreaBombAmountText();
    }
    public override void IncreaseAntiWallBombAmount()
    {
        if (Save_Load_Manager.Instance.gameData.antiWallBombAmount == 0)
        {
            Canvas_Manager.Instance.AddBombRect(BombType.Anti);
        }
        Save_Load_Manager.Instance.gameData.antiWallBombAmount++;
        Canvas_Manager.Instance.SetPlayerAntiWallBombAmountText();
    }
    public override void IncreaseSearcherBombAmount()
    {
        if (Save_Load_Manager.Instance.gameData.searcherBombAmount == 0)
        {
            Canvas_Manager.Instance.AddBombRect(BombType.Searcher);
        }
        Save_Load_Manager.Instance.gameData.searcherBombAmount++;
        Canvas_Manager.Instance.SetPlayerSearcherBombAmountText();
    }
    #endregion
}