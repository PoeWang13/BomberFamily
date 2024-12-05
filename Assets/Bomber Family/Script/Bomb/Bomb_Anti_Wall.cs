using DG.Tweening;
using UnityEngine;

public class Bomb_Anti_Wall : Bomb_Base
{
    private Transform view;
    private int bombAreaLimit = 1;
    private int bombAmount = 0;
    private ParticleSystem bombTrail;

    private void Awake()
    {
        view = transform.Find("BombView");
        bombTrail = view.Find("Bomb_Anti_Trail").GetComponent<ParticleSystem>();
    }
    public override void Bombed()
    {
        if (IsExploded)
        {
            return;
        }
        bombTrail.Play();
        SetExploded();
        view.DOLocalMoveY(10, 1).OnComplete(() =>
        {
            view.eulerAngles = Vector3.right * 180;
            view.DOLocalMoveY(0, 1).OnComplete(() =>
            {
                CreateFire();
            });
        });
    }
    private void CreateFire()
    {
        bombTrail.Stop();
        for (int x = -bombAreaLimit; x < bombAreaLimit + 1; x++) // -3, -2, -1, 0, 1, 2, 3
        {
            int pozitifAmount = bombAmount + 1;
            for (int y = -bombAmount; y < pozitifAmount; y++)     //  0,  1,  2, 3, 2, 1, 0
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
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                }
                // Bulunan obje deactif mi
                else if (!Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Object.activeSelf)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                }
                // Bir obje var mı
                else if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Game.boardType == BoardType.NonUseable)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                }
                // Bir obje var mı
                else if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Game.boardType == BoardType.Empty)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                }
                // Bulunan obje kutu mu
                else if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Game.boardType == BoardType.Box)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                }
                else if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Object.CompareTag("Bomb"))
                {
                    // Bomb patlat
                    Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Object.GetComponent<Bomb_Base>().Bombed();
                }
            }
            if (x < 0)
            {
                bombAmount++;
            }
            else
            {
                bombAmount--;
            }
        }
        bombAmount = 0;
        view.eulerAngles = Vector3.zero;
        transform.localPosition = Vector3.zero;
        EnterHavuz();
    }
}