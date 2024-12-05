using UnityEngine;

public class Bomb_Simple : Bomb_Base
{
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
        canGoRight = canGoLeft = canGoForward = canGoBack = true;
        // Merkez
        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
        // Sağ
        for (int e = 1; e < MyOwner.CharacterStat.myBombFireLimit + 1 && canGoRight; e++)
        {
            if (MyCoor.x + e >= Map_Holder.Instance.GameBoard.GetLength(0))
            {
                canGoRight = true;
                continue;
            }
            // Bomb Fire bırak
            BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + e, 0, MyCoor.y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
            // Bir obje var mı
            if (Map_Holder.Instance.GameBoard[MyCoor.x + e, MyCoor.y].board_Game.boardType == BoardType.Box)
            {
                canGoRight = false;
            }
            else if(Map_Holder.Instance.GameBoard[MyCoor.x + e, MyCoor.y].board_Game.boardType == BoardType.Wall)
            {
                canGoRight = false;
            }
            else if(Map_Holder.Instance.GameBoard[MyCoor.x + e, MyCoor.y].board_Game.boardType == BoardType.Trap)
            {
                canGoRight = false;
            }
            else if(Map_Holder.Instance.GameBoard[MyCoor.x + e, MyCoor.y].board_Game.boardType == BoardType.Gate)
            {
                canGoRight = false;
            }
            else if (Map_Holder.Instance.GameBoard[MyCoor.x + e, MyCoor.y].board_Object != null)
            {
                if (Map_Holder.Instance.GameBoard[MyCoor.x + e, MyCoor.y].board_Object.CompareTag("Bomb"))
                {
                    if (Map_Holder.Instance.GameBoard[MyCoor.x + e, MyCoor.y].board_Object.activeSelf)
                    {
                        canGoRight = false;
                        // Bomb patlat
                        Map_Holder.Instance.GameBoard[MyCoor.x + e, MyCoor.y].board_Object.GetComponent<Bomb_Base>().Bombed();
                    }
                }
            }
        }
        // Sol
        for (int e = 1; e < MyOwner.CharacterStat.myBombFireLimit + 1 && canGoLeft; e++)
        {
            if (MyCoor.x - e < 0)
            {
                canGoLeft = true;
                continue;
            }
            // Bomb Fire bırak
            BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x - e, 0, MyCoor.y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
            // Bir obje var mı
            if (Map_Holder.Instance.GameBoard[MyCoor.x - e, MyCoor.y].board_Game.boardType == BoardType.Box)
            {
                canGoLeft = false;
            }
            else if(Map_Holder.Instance.GameBoard[MyCoor.x - e, MyCoor.y].board_Game.boardType == BoardType.Wall)
            {
                canGoLeft = false;
            }
            else if(Map_Holder.Instance.GameBoard[MyCoor.x - e, MyCoor.y].board_Game.boardType == BoardType.Trap)
            {
                canGoLeft = false;
            }
            else if(Map_Holder.Instance.GameBoard[MyCoor.x - e, MyCoor.y].board_Game.boardType == BoardType.Gate)
            {
                canGoLeft = false;
            }
            else if (Map_Holder.Instance.GameBoard[MyCoor.x - e, MyCoor.y].board_Object != null)
            {
                if (Map_Holder.Instance.GameBoard[MyCoor.x - e, MyCoor.y].board_Object.CompareTag("Bomb"))
                {
                    if (Map_Holder.Instance.GameBoard[MyCoor.x - e, MyCoor.y].board_Object.activeSelf)
                    {
                        canGoLeft = false;
                        // Bomb patlat
                        Map_Holder.Instance.GameBoard[MyCoor.x - e, MyCoor.y].board_Object.GetComponent<Bomb_Base>().Bombed();
                    }
                }
            }
        }
        // İleri
        for (int e = 1; e < MyOwner.CharacterStat.myBombFireLimit + 1 && canGoForward; e++)
        {
            if (MyCoor.y + e >= Map_Holder.Instance.GameBoard.GetLength(0))
            {
                canGoForward = true;
                continue;
            }
            // Bomb Fire bırak
            BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y + e)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
            // Bir obje var mı
            if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y + e].board_Game.boardType == BoardType.Box)
            {
                canGoForward = false;
            }
            else if(Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y + e].board_Game.boardType == BoardType.Wall)
            {
                canGoForward = false;
            }
            else if(Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y + e].board_Game.boardType == BoardType.Trap)
            {
                canGoForward = false;
            }
            else if(Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y + e].board_Game.boardType == BoardType.Gate)
            {
                canGoForward = false;
            }
            else if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y + e].board_Object != null)
            {
                if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y + e].board_Object.CompareTag("Bomb"))
                {
                    if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y + e].board_Object.activeSelf)
                    {
                        canGoForward = false;
                        // Bomb patlat
                        Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y + e].board_Object.GetComponent<Bomb_Base>().Bombed();
                    }
                }
            }
        }
        // Geri
        for (int e = 1; e < MyOwner.CharacterStat.myBombFireLimit + 1 && canGoBack; e++)
        {
            if (MyCoor.y - e < 0)
            {
                canGoBack = true;
                continue;
            }
            // Bomb Fire bırak
            BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y - e)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
            // Bir obje var mı
            if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y - e].board_Game.boardType == BoardType.Box)
            {
                canGoBack = false;
            }
            else if(Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y - e].board_Game.boardType == BoardType.Wall)
            {
                canGoBack = false;
            }
            else if(Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y - e].board_Game.boardType == BoardType.Trap)
            {
                canGoBack = false;
            }
            else if(Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y - e].board_Game.boardType == BoardType.Gate)
            {
                canGoBack = false;
            }
            else if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y - e].board_Object != null)
            {
                if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y - e].board_Object.CompareTag("Bomb"))
                {
                    if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y - e].board_Object.activeSelf)
                    {
                        canGoBack = false;
                        // Bomb patlat
                        Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y - e].board_Object.GetComponent<Bomb_Base>().Bombed();
                    }
                }
            }
        }
        Game_Manager.Instance.AddBombAmount();
        MyOwner.IncreaseBombAmount();
        EnterHavuz();
        Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y] = new GameBoard();
    }
}