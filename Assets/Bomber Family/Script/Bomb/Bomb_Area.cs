using UnityEngine;

public class Bomb_Area : Bomb_Base
{
    private int bombAreaLimit = 3;
    private int bombAmount = 0;
    private int bombDirection = -1;
    public override void Bombed()
    {
        if (IsExploded)
        {
            return;
        }
        SetExploded();
        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.BombFirePower);
        for (int x = -bombAreaLimit; x < bombAreaLimit + 1; x++) // -3 , -2, -1, 0, 1, 2, 3
        {
            int pozitifAmount = Mathf.Abs(bombAmount) + 1;
            for (int y = bombAmount; y < pozitifAmount; y++)// -3, 0 + -2,1 + 
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
            if (x == 0)
            {
                bombDirection = 1;
            }
            bombAmount += bombDirection;
        }
        EnterHavuz();
    }
}