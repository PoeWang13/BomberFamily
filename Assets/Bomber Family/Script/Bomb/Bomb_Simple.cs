using UnityEngine;

public class Bomb_Simple : Bomb_Base
{
    private int movingStep = 0;
    private bool canGoRight = true;
    private bool canGoLeft = true;
    private bool canGoForward = true;
    private bool canGoBack = true;

    public override void Bombed()
    {
        if (IsExploded)
        {
            return;
        }
        SetExploded();
        MyOwner.IncreaseBombAmount(BombType.Simple);
        canGoRight = canGoLeft = canGoForward = canGoBack = true;
        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
        while (movingStep < MyOwner.CharacterStat.myBombFireLimit)
        {
            movingStep++;
            // Sağa gidebilir mi
            if (canGoRight)
            {
                // Sınırlar içinde mi
                if (MyCoor.x + 1 * movingStep < Map_Holder.Instance.GameBoard.GetLength(0))
                {
                    // Bir obje var mı
                    if (Map_Holder.Instance.GameBoard[MyCoor.x + 1 * movingStep, MyCoor.y].board_Object is null)
                    {
                        // Bomb Fire bırak
                        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + 1 * movingStep, 0, MyCoor.y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                    }
                    else if (!Map_Holder.Instance.GameBoard[MyCoor.x + 1 * movingStep, MyCoor.y].board_Object.activeSelf)
                    {
                        // Bomb Fire bırak
                        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + 1 * movingStep, 0, MyCoor.y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                    }
                    else if (Map_Holder.Instance.GameBoard[MyCoor.x + 1 * movingStep, MyCoor.y].board_Game.boardType == BoardType.Box)
                    {
                        canGoRight = false;
                        // Bomb Fire bırak
                        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + 1 * movingStep, 0, MyCoor.y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                    }
                    else if (Map_Holder.Instance.GameBoard[MyCoor.x + 1 * movingStep, MyCoor.y].board_Object.TryGetComponent(out Bomb_Base bomb_Base))
                    {
                        canGoRight = false;
                        // Bomb patlat
                        bomb_Base.Bombed();
                    }
                }
                else
                {
                    canGoRight = false;
                }
            }
            // Sola gidebilir mi
            if (canGoLeft)
            {
                // Sınırlar içinde mi
                if (MyCoor.x - 1 * movingStep >= 0)
                {
                    // Bir obje var mı
                    if (Map_Holder.Instance.GameBoard[MyCoor.x - 1 * movingStep, MyCoor.y].board_Object is null)
                    {
                        // Bomb Fire bırak
                        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x - 1 * movingStep, 0, MyCoor.y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                    }
                    else if (!Map_Holder.Instance.GameBoard[MyCoor.x - 1 * movingStep, MyCoor.y].board_Object.activeSelf)
                    {
                        // Bomb Fire bırak
                        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x - 1 * movingStep, 0, MyCoor.y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                    }
                    else if (Map_Holder.Instance.GameBoard[MyCoor.x - 1 * movingStep, MyCoor.y].board_Game.boardType == BoardType.Box)
                    {
                        canGoLeft = false;
                        // Bomb Fire bırak
                        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x - 1 * movingStep, 0, MyCoor.y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                    }
                    else if (Map_Holder.Instance.GameBoard[MyCoor.x - 1 * movingStep, MyCoor.y].board_Object.TryGetComponent(out Bomb_Base bomb_Base))
                    {
                        canGoLeft = false;
                        // Bomb patlat
                        bomb_Base.Bombed();
                    }
                }
                else
                {
                    canGoLeft = false;
                }
            }
            // İleri gidebilir mi
            if (canGoForward)
            {
                // Sınırlar içinde mi
                if (MyCoor.y + 1 * movingStep < Map_Holder.Instance.GameBoard.GetLength(1))
                {
                    // Bir obje var mı
                    if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y + 1 * movingStep].board_Object is null)
                    {
                        // Bomb Fire bırak
                        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y + 1 * movingStep)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                    }
                    else if (!Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y + 1 * movingStep].board_Object.activeSelf)
                    {
                        // Bomb Fire bırak
                        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y + 1 * movingStep)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                    }
                    else if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y + 1 * movingStep].board_Game.boardType == BoardType.Box)
                    {
                        canGoForward = false;
                        // Bomb Fire bırak
                        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y + 1 * movingStep)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                    }
                    else if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y + 1 * movingStep].board_Object.TryGetComponent(out Bomb_Base bomb_Base))
                    {
                        canGoForward = false;
                        // Bomb patlat
                        bomb_Base.Bombed();
                    }
                }
                else
                {
                    canGoForward = false;
                }
            }
            // Geri gidebilir mi
            if (canGoBack)
            {
                // Sınırlar içinde mi
                if (MyCoor.y - 1 * movingStep >= 0)
                {
                    // Bir obje var mı
                    if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y - 1 * movingStep].board_Object is null)
                    {
                        // Bomb Fire bırak
                        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y - 1 * movingStep)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                    }
                    else if (!Map_Holder.Instance.GameBoard[MyCoor.x,MyCoor.y - 1 * movingStep].board_Object.activeSelf)
                    {
                        // Bomb Fire bırak
                        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y - 1 * movingStep)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                    }
                    else if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y - 1 * movingStep].board_Game.boardType == BoardType.Box)
                    {
                        canGoBack = false;
                        // Bomb Fire bırak
                        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y - 1 * movingStep)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                    }
                    else if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y - 1 * movingStep].board_Object.TryGetComponent(out Bomb_Base bomb_Base))
                    {
                        canGoBack = false;
                        // Bomb patlat
                        bomb_Base.Bombed();
                    }
                }
                else
                {
                    canGoBack = false;
                }
            }
            if (!canGoRight && !canGoLeft && !canGoForward && !canGoBack)
            {
                movingStep = MyOwner.CharacterStat.myBombFireLimit;
            }
        }
        movingStep = 0;
        Game_Manager.Instance.AddBombAmount();
        EnterHavuz();
    }
}