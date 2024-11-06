using UnityEngine;

public class Bomb_Searcher : Bomb_Base
{
    public override void Bombed()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (IsExploded)
        {
            return;
        }
        SetExploded();
        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.BombFirePower);
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                // X sınırlar içinde mi
                if (MyCoor.x + x < 0 || MyCoor.x + x >= Map_Holder.Instance.GameBoard.GetLength(0))
                {
                    continue;
                }
                // Y sınırlar içinde mi
                if (MyCoor.y + y < 0 || MyCoor.y + y >= Map_Holder.Instance.GameBoard.GetLength(1))
                {
                    continue;
                }
                // Bir obje var mı
                if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Object is null)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.BombFirePower);
                }
                // Bulunan obje deactif mi
                else if (!Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Object.activeSelf)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.BombFirePower);
                }
                // Bulunan obje kutu mu
                else if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Game.boardType == BoardType.Box)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.BombFirePower);
                }
                else if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Object.TryGetComponent(out Bomb_Base bomb_Base))
                {
                    // Bomb patlat
                    bomb_Base.Bombed();
                }
            }
        }
        EnterHavuz();
    }
    private void Update()
    {
    }
}